using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PowerControlTool
{
    public partial class FormCountDown : Form
    {
        public FormCountDown()
        {
            InitializeComponent();

            if (FormMain.DarkTheme)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;

                foreach (Control c in Controls)
                {
                    c.ForeColor = Color.White;
                    c.BackColor = Color.Black;
                }

                labelCountDown.ForeColor = Color.Red;
                //buttonCancel.BackColor = Color.Black;
            }
        }

        private void FormCountDown_Shown(object sender, EventArgs e)
        {
            textBoxTitle.Text = this.Text; //.PadLeft(40, ' ');

            //pre v4.0
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                for (int i = 0; i < 5; i++)
                {
                    int n = 5 - i;
                    labelCountDown.Text = n.ToString();
                    Thread.Sleep(1000);
                }

                this.Close();
            }));
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            FormMain.CancelFlag = true;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            FormMain.CancelFlag = false;
            this.Close();
        }
    }
}
