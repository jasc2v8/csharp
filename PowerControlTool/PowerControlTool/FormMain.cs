using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Security.Principal;
using System.Drawing;

namespace PowerControlTool
{
    public partial class FormMain : Form
    {

        //CancelFlag would randomly throw an exception, therefore:
        //make CancelFlag thread safe since it's updated by FormCountDown
        //https://stackoverflow.com/questions/7161413/thread-safe-properties-in-c-sharp

        private static readonly ReaderWriterLockSlim CancelFlagLocker = new ReaderWriterLockSlim();

        private static bool cancelFlag = false;

        public static bool CancelFlag
        {
            get
            {
                CancelFlagLocker.EnterReadLock();
                try { return cancelFlag; }
                finally { CancelFlagLocker.ExitReadLock(); }
            }
            set
            {
                CancelFlagLocker.EnterWriteLock();
                try { cancelFlag = value; }
                finally { CancelFlagLocker.ExitWriteLock(); }
            }
        }

        //public properties
        public static bool CountDownRunning { get; set; } = false;
        public static bool DarkTheme { get; set; } = false;

        //private properties
        private bool AllowClose { get; set; } = false;
        private bool RestartingAsAdmin { get; set; } = false;

        Form formCountDown = null; // new FormCountDown();

        [DllImport("user32.dll", SetLastError = true)]
        static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        private const int EWX_LOGOFF = 0x00000000;
        private const int EWX_SHUTDOWN = 0x00000001;
        private const int EWX_REBOOT = 0x00000002;
        private const int EWX_FORCE = 0x00000004;
        private const int EWX_POWEROFF = 0x00000008;
        private const int EWX_FORCEIFHUNG = 0x00000010;
        private const int SHTDN_REASON_MAJOR_OTHER = 0x00000000;

        public FormMain()
        {
            InitializeComponent();

            if (Properties.Settings.Default.RestartUEFI)
                DoRestartUEFI();

            if (Properties.Settings.Default.DarkTheme)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;

                foreach (Control c in Controls)
                {
                    if (c.GetType() == typeof(Button))
                        c.BackColor = Color.Black;
                }

                DarkTheme = true;
            }
            else
            {
                DarkTheme = false;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //allow form to close
            if (RestartingAsAdmin || AllowClose)
                return;

            //hide instead of close
            e.Cancel = true;
            this.Hide();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.DarkTheme)
                darkThemeToolStripMenuItem.Checked = true;
            else
                darkThemeToolStripMenuItem.Checked = false;

            notifyIcon1.Visible = true;

            if (Properties.Settings.Default.ThemeChanged)
            {
                Properties.Settings.Default.ThemeChanged = false;
                Properties.Settings.Default.Save();
                ShowForm();
            }
            else
            {
                this.Opacity = 0;
                this.Hide();
            }

            Properties.Settings.Default.RestartUEFI = false;
            Properties.Settings.Default.Save();

            //user.config file is at C:\Users\USERNAME\AppData\Local\csharp
            //var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            //MessageBox.Show(config.FilePath);

        }
        private void ShowForm()
        {
            if (this.Opacity == 0) this.Opacity = 1;
            this.Show();
        }
        private void ContextMenuItemExit_Click(object sender, EventArgs e)
        {
            AllowClose = true;
            this.Close();
        }
        private void ContextMenuItemOpen_Click(object sender, EventArgs e)
        {
            ShowForm();
        }
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            //handle left click only. right click is handled by the context menu
            if (e.Button == MouseButtons.Left)
            {
                ShowForm();
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void buttonDisplayOff_Click(object sender, EventArgs e)
        {
            if (DoCountDown(buttonDisplayOff.Text))
                return;

            int WM_SYSCOMMAND = 0x0112;
            int SC_MONITORPOWER = 0xF170;
            int MonitorState = 2; //On = -1, Off = 2, StandBy = 1

            SendMessage(this.Handle.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, MonitorState);
        }
        private void buttonRestart_Click(object sender, EventArgs e)
        {
            if (DoCountDown(buttonRestart.Text))
                return;

            Process.Start("shutdown", "/r /t 0");
        }
        private void buttonRestartUEFI_Click(object sender, EventArgs e)
        {
            if (DoCountDown(buttonRestartUEFI.Text))
                return;

            Properties.Settings.Default.RestartUEFI = true;
            Properties.Settings.Default.Save();

            if (UserIsAdmin())
                DoRestartUEFI();
            else
                DoRestartWithElevatedPrivileges();
        }
        private void buttonRestartRE_Click(object sender, EventArgs e)
        {
            if (DoCountDown(buttonRestartRE.Text))
                return;

            Process.Start("shutdown", "/r /o /t 0");
        }
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            Process.Start("ms-settings:powersleep");
        }
        private void buttonShutdown_Click(object sender, EventArgs e)
        {
            if (DoCountDown(buttonShutdown.Text))
                return;

            Process.Start("shutdown", "/s /t 0");
        }
        private void buttonSignOut_Click(object sender, EventArgs e)
        {
            if (DoCountDown(buttonSignOut.Text))
                return;

            ExitWindowsEx(EWX_LOGOFF + EWX_FORCE, SHTDN_REASON_MAJOR_OTHER);
        }
        private void buttonSleep_Click(object sender, EventArgs e)
        {

            if (DoCountDown(buttonSleep.Text))
                return;

            Application.SetSuspendState(PowerState.Suspend, true, true);
        }
        private void DoRestartWithElevatedPrivileges()
        {
            string exePath = Application.ExecutablePath;

            ProcessStartInfo startinfo = new ProcessStartInfo
            {
                FileName = exePath,
                UseShellExecute = true,
                Verb = "runas",
                Arguments = " "
            };

            try
            {
                Process.Start(startinfo);
            }
            catch
            {
                //User answered NO to the UAC prompt so just restart without admin privs
                Application.Restart();
            }
            RestartingAsAdmin = true;
            Application.Exit();
        }
        public static bool UserIsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void DoRestartUEFI()
        {
            Process.Start("shutdown", "/r /t 0 /fw");
        }
        private bool DoCountDown(string Title)
        {
            this.Hide();

            CancelFlag = false;
            CountDownRunning = true;

            formCountDown = new FormCountDown();
            formCountDown.Text = Title;
            formCountDown.Show();
            formCountDown.FormClosed += FormCountDown_FormClosed;

            //wait for CountDown form to close
            Thread thread = new Thread(() =>
            {
                do
                {
                    Thread.Sleep(100);
                    Application.DoEvents();

                } while (CountDownRunning);

            });

            thread.Start();

            //wait for thread to finish
            while (thread.IsAlive)
            {
                Thread.Sleep(10);
                Application.DoEvents();
            }

            if (CancelFlag)
                this.Show();

            //true if user pressed the Cancel button, or false if user pressed OK or form timed out
            return CancelFlag;
        }

        private void FormCountDown_FormClosed(object sender, FormClosedEventArgs e)
        {
            CountDownRunning = false;
        }

        private void darkThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DarkTheme = !Properties.Settings.Default.DarkTheme;
            Properties.Settings.Default.ThemeChanged = true;
            Properties.Settings.Default.Save();

            if (Properties.Settings.Default.DarkTheme)
                darkThemeToolStripMenuItem.Checked = true;
            else
                darkThemeToolStripMenuItem.Checked = false;

            Application.Restart();

            AllowClose = true;
            this.Close();
        }
    }
}
