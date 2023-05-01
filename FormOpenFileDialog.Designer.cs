namespace CodeForger
{
    partial class FormOpenFileDialog
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
            this.radioButtonLoadDBFile = new System.Windows.Forms.RadioButton();
            this.radioButtonOpenExtFile = new System.Windows.Forms.RadioButton();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonLoadDBFile
            // 
            this.radioButtonLoadDBFile.AutoSize = true;
            this.radioButtonLoadDBFile.Checked = true;
            this.radioButtonLoadDBFile.Location = new System.Drawing.Point(12, 12);
            this.radioButtonLoadDBFile.Name = "radioButtonLoadDBFile";
            this.radioButtonLoadDBFile.Size = new System.Drawing.Size(426, 41);
            this.radioButtonLoadDBFile.TabIndex = 0;
            this.radioButtonLoadDBFile.TabStop = true;
            this.radioButtonLoadDBFile.Text = "Load previously saved file";
            this.radioButtonLoadDBFile.UseVisualStyleBackColor = true;
            this.radioButtonLoadDBFile.CheckedChanged += new System.EventHandler(this.radioButtonLoadDBFile_CheckedChanged);
            // 
            // radioButtonOpenExtFile
            // 
            this.radioButtonOpenExtFile.AutoSize = true;
            this.radioButtonOpenExtFile.Location = new System.Drawing.Point(12, 88);
            this.radioButtonOpenExtFile.Name = "radioButtonOpenExtFile";
            this.radioButtonOpenExtFile.Size = new System.Drawing.Size(309, 41);
            this.radioButtonOpenExtFile.TabIndex = 1;
            this.radioButtonOpenExtFile.Text = "Open external file";
            this.radioButtonOpenExtFile.UseVisualStyleBackColor = true;
            // 
            // panelOptions
            // 
            this.panelOptions.AutoScroll = true;
            this.panelOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelOptions.Location = new System.Drawing.Point(0, 309);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(1404, 519);
            this.panelOptions.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonLoadDBFile);
            this.panel1.Controls.Add(this.radioButtonOpenExtFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1404, 150);
            this.panel1.TabIndex = 3;
            // 
            // FormOpenFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1404, 828);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelOptions);
            this.Name = "FormOpenFileDialog";
            this.Text = "Open File";
            this.Load += new System.EventHandler(this.FormOpenFileDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonLoadDBFile;
        private System.Windows.Forms.RadioButton radioButtonOpenExtFile;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.Panel panel1;
    }
}