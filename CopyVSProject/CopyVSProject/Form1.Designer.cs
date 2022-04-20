namespace CopyVSProject
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSelectSource = new System.Windows.Forms.Button();
            this.txtBoxSource = new System.Windows.Forms.TextBox();
            this.btnCopyAndReplace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtRichTextBox = new System.Windows.Forms.RichTextBox();
            this.txtBoxExclude = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnZip = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnExplore = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectSource
            // 
            this.btnSelectSource.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectSource.Location = new System.Drawing.Point(667, 9);
            this.btnSelectSource.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSelectSource.Name = "btnSelectSource";
            this.btnSelectSource.Size = new System.Drawing.Size(113, 22);
            this.btnSelectSource.TabIndex = 0;
            this.btnSelectSource.Text = "Select";
            this.btnSelectSource.UseVisualStyleBackColor = true;
            this.btnSelectSource.Click += new System.EventHandler(this.btnSelectSource_Click);
            // 
            // txtBoxSource
            // 
            this.txtBoxSource.Location = new System.Drawing.Point(139, 9);
            this.txtBoxSource.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtBoxSource.Name = "txtBoxSource";
            this.txtBoxSource.Size = new System.Drawing.Size(518, 22);
            this.txtBoxSource.TabIndex = 1;
            this.txtBoxSource.Text = "D:\\Software\\DEV\\Work\\csharp\\Learning\\Examples";
            // 
            // btnCopyAndReplace
            // 
            this.btnCopyAndReplace.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyAndReplace.Location = new System.Drawing.Point(570, 536);
            this.btnCopyAndReplace.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnCopyAndReplace.Name = "btnCopyAndReplace";
            this.btnCopyAndReplace.Size = new System.Drawing.Size(113, 39);
            this.btnCopyAndReplace.TabIndex = 4;
            this.btnCopyAndReplace.Text = "Copy Project";
            this.btnCopyAndReplace.UseVisualStyleBackColor = true;
            this.btnCopyAndReplace.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Project Dir";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 579);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(803, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // txtRichTextBox
            // 
            this.txtRichTextBox.Location = new System.Drawing.Point(14, 71);
            this.txtRichTextBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtRichTextBox.Name = "txtRichTextBox";
            this.txtRichTextBox.Size = new System.Drawing.Size(766, 457);
            this.txtRichTextBox.TabIndex = 7;
            this.txtRichTextBox.Text = "";
            this.txtRichTextBox.WordWrap = false;
            // 
            // txtBoxExclude
            // 
            this.txtBoxExclude.Location = new System.Drawing.Point(139, 41);
            this.txtBoxExclude.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtBoxExclude.Name = "txtBoxExclude";
            this.txtBoxExclude.Size = new System.Drawing.Size(518, 22);
            this.txtBoxExclude.TabIndex = 8;
            this.txtBoxExclude.Text = ".vs; bin; obj; x86; x64; Debug; Release;";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 41);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Exclude Dirs";
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(17, 536);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(113, 38);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnZip
            // 
            this.btnZip.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZip.Location = new System.Drawing.Point(447, 536);
            this.btnZip.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnZip.Name = "btnZip";
            this.btnZip.Size = new System.Drawing.Size(113, 39);
            this.btnZip.TabIndex = 18;
            this.btnZip.Text = "Zip Project";
            this.btnZip.UseVisualStyleBackColor = true;
            this.btnZip.Click += new System.EventHandler(this.btnZip_Click);
            // 
            // btnClean
            // 
            this.btnClean.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClean.Location = new System.Drawing.Point(324, 535);
            this.btnClean.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(113, 39);
            this.btnClean.TabIndex = 19;
            this.btnClean.Text = "Clean Project";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(691, 547);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(89, 19);
            this.checkBox1.TabIndex = 20;
            this.checkBox1.Text = "Verbose";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnExplore
            // 
            this.btnExplore.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExplore.Location = new System.Drawing.Point(667, 41);
            this.btnExplore.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnExplore.Name = "btnExplore";
            this.btnExplore.Size = new System.Drawing.Size(113, 22);
            this.btnExplore.TabIndex = 21;
            this.btnExplore.Text = "Explore";
            this.btnExplore.UseVisualStyleBackColor = true;
            this.btnExplore.Click += new System.EventHandler(this.btnExplore_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnCopyAndReplace;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 601);
            this.Controls.Add(this.btnExplore);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnZip);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtBoxExclude);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRichTextBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCopyAndReplace);
            this.Controls.Add(this.txtBoxSource);
            this.Controls.Add(this.btnSelectSource);
            this.Font = new System.Drawing.Font("Lucida Console", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copy Visual Studio Project";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnSelectSource;
        private System.Windows.Forms.TextBox txtBoxSource;
        private System.Windows.Forms.Button btnCopyAndReplace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.RichTextBox txtRichTextBox;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.TextBox txtBoxExclude;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnZip;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnExplore;
    }
}

