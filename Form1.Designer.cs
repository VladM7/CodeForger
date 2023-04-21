namespace CodeForger
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
            this.buttonNewFile = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLog = new System.Windows.Forms.Button();
            this.labelEmail = new System.Windows.Forms.Label();
            this.buttonAccountSettings = new System.Windows.Forms.Button();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNewFile
            // 
            this.buttonNewFile.Location = new System.Drawing.Point(360, 392);
            this.buttonNewFile.Margin = new System.Windows.Forms.Padding(1);
            this.buttonNewFile.Name = "buttonNewFile";
            this.buttonNewFile.Size = new System.Drawing.Size(288, 111);
            this.buttonNewFile.TabIndex = 0;
            this.buttonNewFile.Text = "New File";
            this.buttonNewFile.UseVisualStyleBackColor = true;
            this.buttonNewFile.Click += new System.EventHandler(this.buttonNewFile_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(672, 392);
            this.buttonOpenFile.Margin = new System.Windows.Forms.Padding(1);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(288, 111);
            this.buttonOpenFile.TabIndex = 1;
            this.buttonOpenFile.Text = "Open File";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(399, 174);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(513, 101);
            this.label1.TabIndex = 2;
            this.label1.Text = "CodeForger";
            // 
            // buttonLog
            // 
            this.buttonLog.Location = new System.Drawing.Point(1251, 10);
            this.buttonLog.Margin = new System.Windows.Forms.Padding(1);
            this.buttonLog.Name = "buttonLog";
            this.buttonLog.Size = new System.Drawing.Size(225, 68);
            this.buttonLog.TabIndex = 4;
            this.buttonLog.Text = "Sign in";
            this.buttonLog.UseVisualStyleBackColor = true;
            this.buttonLog.Click += new System.EventHandler(this.buttonLog_Click);
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(1244, 108);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(0, 37);
            this.labelEmail.TabIndex = 5;
            // 
            // buttonAccountSettings
            // 
            this.buttonAccountSettings.Location = new System.Drawing.Point(843, 10);
            this.buttonAccountSettings.Margin = new System.Windows.Forms.Padding(1);
            this.buttonAccountSettings.Name = "buttonAccountSettings";
            this.buttonAccountSettings.Size = new System.Drawing.Size(370, 68);
            this.buttonAccountSettings.TabIndex = 6;
            this.buttonAccountSettings.Text = "Account Settings";
            this.buttonAccountSettings.UseVisualStyleBackColor = true;
            this.buttonAccountSettings.Click += new System.EventHandler(this.buttonAccountSettings_Click);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(951, 131);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(319, 247);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 3;
            this.pictureBoxLogo.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1491, 774);
            this.Controls.Add(this.buttonAccountSettings);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.buttonLog);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.buttonNewFile);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNewFile;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button buttonLog;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Button buttonAccountSettings;
    }
}

