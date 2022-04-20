using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Security.Principal;
using System.Media;

namespace PowerControlTool
{
    public partial class Form1 : Form
    {

        //[DllImport("user32.dll", SetLastError = true)]
        [DllImport("user32.dll")]
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

        private bool RestartingAsAdmin = false;
        private bool CloseForm = false;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //allow form to close
            if ( (RestartingAsAdmin) || (CloseForm) )
            {
                return;
            }

            //minimize instead of close
            e.Cancel = true;
            Hide();
            this.WindowState = FormWindowState.Minimized;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //always visible
            notifyIcon1.Visible = true;
        }
        private void ContextMenuItemExit_Click(object sender, EventArgs e)
        {
            CloseForm = true;
            this.Close();
        }
        private void ContextMenuItemOpen_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            //handle left click only. right click is handled by the context menu
            if (e.Button == MouseButtons.Left)
            {
                Show();
                this.WindowState = FormWindowState.Normal;
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }
        private void buttonDisplayOff_Click(object sender, EventArgs e)
        {
            int WM_SYSCOMMAND = 0x0112;
            int SC_MONITORPOWER = 0xF170;
            int MonitorState = 2; //On = -1, Off = 2, StandBy = 1

            var choice = MessageBox.Show("Display Off - Are you sure?\n\n(3 second delay to settle mouse)",
                Form1.ActiveForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (choice == DialogResult.Yes)
            {
                Thread.Sleep(3000);
                SendMessage(this.Handle.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, MonitorState);
            }
        }
        private void buttonRestart_Click(object sender, EventArgs e)
        {
            var choice = MessageBox.Show("Restart - Are you sure?",
                Form1.ActiveForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (choice == DialogResult.Yes)
            {
                Process.Start("shutdown", "/r /t 0");
            }
        }
        private void buttonRestartUEFI_Click(object sender, EventArgs e)
        {
            if (UserIsAdmin())
            {
                var choice = MessageBox.Show("Restart to UEFI - Are you sure?",
                 Form1.ActiveForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (choice == DialogResult.Yes)
                {
                    Process.Start("shutdown", "/r /t 0 /fw");
                }
            }
            else
            {
                var choice = MessageBox.Show("Press OK to Restart as Admin\n\nThen Press the [Restart to UEFI] Button again.",
                    Form1.ActiveForm.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (choice == DialogResult.OK)
                {
                    RestartWithElevatedPrivileges();
                }
            }
        }
        private void buttonRestartRE_Click(object sender, EventArgs e)
        {
            var choice = MessageBox.Show("Restart to RE - Are you sure?",
                Form1.ActiveForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (choice == DialogResult.Yes)
            {
                Process.Start("shutdown", "/r /o /t 0");
            }
        }
        private void buttonSettings_Click(object sender, EventArgs e)
        {

            Process.Start("ms-settings:powersleep");

        }
        private void buttonShutdown_Click(object sender, EventArgs e)
        {
            var choice = MessageBox.Show("Shutdown - Are you sure?",
                Form1.ActiveForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (choice == DialogResult.Yes)
            {
                Process.Start("shutdown", "/s /t 0");
            }
        }
        private void buttonSignOut_Click(object sender, EventArgs e)
        {
            var choice = MessageBox.Show("Sign Out - Are you sure?",
                Form1.ActiveForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (choice == DialogResult.Yes)
            {
                ExitWindowsEx(EWX_LOGOFF + EWX_FORCE, SHTDN_REASON_MAJOR_OTHER);
            }
        }
        private void buttonSleep_Click(object sender, EventArgs e)
        {

            var choice = MessageBox.Show("Sleep - Are you sure?",
                Form1.ActiveForm.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (choice == DialogResult.Yes)
            {
                const bool force = true, disableWakeEvent = true;

                Application.SetSuspendState(PowerState.Suspend,
                    force, disableWakeEvent);
            }
        }
        private void RestartWithElevatedPrivileges()
        {
            //Console:
            //string programpath = new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            //Or for Windows Forms:
            string programpath = System.Windows.Forms.Application.ExecutablePath;

            System.Diagnostics.ProcessStartInfo startinfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = programpath,
                UseShellExecute = true,
                Verb = "runas",
                Arguments = " "
            };

            System.Diagnostics.Process.Start(startinfo);
            //Console:
            //System.Environment.Exit(0); // return code 0, change if required
            //Or for Windows Forms:
            RestartingAsAdmin = true;
            System.Windows.Forms.Application.Exit( );
        }
        public static bool UserIsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
