namespace PowerControlTool
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonSleep = new System.Windows.Forms.Button();
            this.buttonSignOut = new System.Windows.Forms.Button();
            this.buttonShutdown = new System.Windows.Forms.Button();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.buttonRestartUEFI = new System.Windows.Forms.Button();
            this.buttonRestartRE = new System.Windows.Forms.Button();
            this.buttonDisplayOff = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.darkThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSleep
            // 
            this.buttonSleep.Location = new System.Drawing.Point(17, 17);
            this.buttonSleep.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonSleep.Name = "buttonSleep";
            this.buttonSleep.Size = new System.Drawing.Size(150, 50);
            this.buttonSleep.TabIndex = 0;
            this.buttonSleep.Text = "Sleep";
            this.buttonSleep.UseVisualStyleBackColor = true;
            this.buttonSleep.Click += new System.EventHandler(this.buttonSleep_Click);
            // 
            // buttonSignOut
            // 
            this.buttonSignOut.Location = new System.Drawing.Point(179, 17);
            this.buttonSignOut.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonSignOut.Name = "buttonSignOut";
            this.buttonSignOut.Size = new System.Drawing.Size(150, 50);
            this.buttonSignOut.TabIndex = 1;
            this.buttonSignOut.Text = "Sign Out";
            this.buttonSignOut.UseVisualStyleBackColor = true;
            this.buttonSignOut.Click += new System.EventHandler(this.buttonSignOut_Click);
            // 
            // buttonShutdown
            // 
            this.buttonShutdown.Location = new System.Drawing.Point(341, 17);
            this.buttonShutdown.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonShutdown.Name = "buttonShutdown";
            this.buttonShutdown.Size = new System.Drawing.Size(150, 50);
            this.buttonShutdown.TabIndex = 2;
            this.buttonShutdown.Text = "Shutdown";
            this.buttonShutdown.UseVisualStyleBackColor = true;
            this.buttonShutdown.Click += new System.EventHandler(this.buttonShutdown_Click);
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(17, 80);
            this.buttonRestart.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(150, 50);
            this.buttonRestart.TabIndex = 3;
            this.buttonRestart.Text = "Restart";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // buttonRestartUEFI
            // 
            this.buttonRestartUEFI.Location = new System.Drawing.Point(179, 80);
            this.buttonRestartUEFI.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonRestartUEFI.Name = "buttonRestartUEFI";
            this.buttonRestartUEFI.Size = new System.Drawing.Size(150, 50);
            this.buttonRestartUEFI.TabIndex = 4;
            this.buttonRestartUEFI.Text = "Restart to UEFI";
            this.buttonRestartUEFI.UseVisualStyleBackColor = true;
            this.buttonRestartUEFI.Click += new System.EventHandler(this.buttonRestartUEFI_Click);
            // 
            // buttonRestartRE
            // 
            this.buttonRestartRE.Location = new System.Drawing.Point(341, 80);
            this.buttonRestartRE.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonRestartRE.Name = "buttonRestartRE";
            this.buttonRestartRE.Size = new System.Drawing.Size(150, 50);
            this.buttonRestartRE.TabIndex = 5;
            this.buttonRestartRE.Text = "Restart to RE";
            this.buttonRestartRE.UseVisualStyleBackColor = true;
            this.buttonRestartRE.Click += new System.EventHandler(this.buttonRestartRE_Click);
            // 
            // buttonDisplayOff
            // 
            this.buttonDisplayOff.Location = new System.Drawing.Point(16, 143);
            this.buttonDisplayOff.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonDisplayOff.Name = "buttonDisplayOff";
            this.buttonDisplayOff.Size = new System.Drawing.Size(150, 50);
            this.buttonDisplayOff.TabIndex = 6;
            this.buttonDisplayOff.Text = "Display Off";
            this.buttonDisplayOff.UseVisualStyleBackColor = true;
            this.buttonDisplayOff.Click += new System.EventHandler(this.buttonDisplayOff_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(179, 143);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(150, 50);
            this.buttonSettings.TabIndex = 7;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(341, 143);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(150, 50);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Power Control Tool";
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuItemOpen,
            this.darkThemeToolStripMenuItem,
            this.ContextMenuItemExit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            this.contextMenuStrip1.Text = "Context Menu";
            // 
            // ContextMenuItemOpen
            // 
            this.ContextMenuItemOpen.Name = "ContextMenuItemOpen";
            this.ContextMenuItemOpen.Size = new System.Drawing.Size(180, 22);
            this.ContextMenuItemOpen.Text = "Power Control Tool";
            this.ContextMenuItemOpen.Click += new System.EventHandler(this.ContextMenuItemOpen_Click);
            // 
            // ContextMenuItemExit
            // 
            this.ContextMenuItemExit.Name = "ContextMenuItemExit";
            this.ContextMenuItemExit.Size = new System.Drawing.Size(180, 22);
            this.ContextMenuItemExit.Text = "Exit";
            this.ContextMenuItemExit.Click += new System.EventHandler(this.ContextMenuItemExit_Click);
            // 
            // darkThemeToolStripMenuItem
            // 
            this.darkThemeToolStripMenuItem.Name = "darkThemeToolStripMenuItem";
            this.darkThemeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.darkThemeToolStripMenuItem.Text = "Dark Theme";
            this.darkThemeToolStripMenuItem.Click += new System.EventHandler(this.darkThemeToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 206);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonDisplayOff);
            this.Controls.Add(this.buttonRestartRE);
            this.Controls.Add(this.buttonRestartUEFI);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.buttonShutdown);
            this.Controls.Add(this.buttonSignOut);
            this.Controls.Add(this.buttonSleep);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Power Control Tool";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSleep;
        private System.Windows.Forms.Button buttonSignOut;
        private System.Windows.Forms.Button buttonShutdown;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.Button buttonRestartUEFI;
        private System.Windows.Forms.Button buttonRestartRE;
        private System.Windows.Forms.Button buttonDisplayOff;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem darkThemeToolStripMenuItem;
    }
}

