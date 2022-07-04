//
//  
//
using System;
using System.Windows.Forms;
using System.Reflection;

namespace PowerControlTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = Assembly.GetEntryAssembly().GetName().Name + " v" + Assembly.GetEntryAssembly().GetName().Version;
            textBoxInput.Text = "Input...";
            toolStripStatusLabel1.Text = "Ready.";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void WriteLine(string text)
        {
            textBoxOutput.AppendText(text + Environment.NewLine);
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxOutput.Clear();
            textBoxInput.Clear();
            toolStripStatusLabel1.Text = "Ready.";
            //toolStripStatusLabel1.Text = String.Empty;

        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            WriteLine("OK clicked...");
            toolStripStatusLabel1.Text = "OK!";
        }

    }
}
