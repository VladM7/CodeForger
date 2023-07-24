namespace CodeForger
{
    partial class FormSaveFileDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSaveFileDialog));
            this.radioButtonSaveDBFile = new System.Windows.Forms.RadioButton();
            this.radioButtonSaveExtFile = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBoxFileType = new System.Windows.Forms.ComboBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButtonSaveDBFile
            // 
            this.radioButtonSaveDBFile.AutoSize = true;
            this.radioButtonSaveDBFile.Checked = true;
            this.radioButtonSaveDBFile.Location = new System.Drawing.Point(12, 35);
            this.radioButtonSaveDBFile.Name = "radioButtonSaveDBFile";
            this.radioButtonSaveDBFile.Size = new System.Drawing.Size(593, 41);
            this.radioButtonSaveDBFile.TabIndex = 2;
            this.radioButtonSaveDBFile.TabStop = true;
            this.radioButtonSaveDBFile.Text = "Save file to database (recommended)";
            this.radioButtonSaveDBFile.UseVisualStyleBackColor = true;
            // 
            // radioButtonSaveExtFile
            // 
            this.radioButtonSaveExtFile.AutoSize = true;
            this.radioButtonSaveExtFile.Location = new System.Drawing.Point(12, 111);
            this.radioButtonSaveExtFile.Name = "radioButtonSaveExtFile";
            this.radioButtonSaveExtFile.Size = new System.Drawing.Size(229, 41);
            this.radioButtonSaveExtFile.TabIndex = 3;
            this.radioButtonSaveExtFile.Text = "Save locally";
            this.radioButtonSaveExtFile.UseVisualStyleBackColor = true;
            this.radioButtonSaveExtFile.CheckedChanged += new System.EventHandler(this.radioButtonSaveExtFile_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.radioButtonSaveDBFile);
            this.panel1.Controls.Add(this.radioButtonSaveExtFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 211);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1124, 420);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(644, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(458, 355);
            this.label1.TabIndex = 5;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 237);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(286, 79);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.comboBoxFileType);
            this.panel2.Controls.Add(this.textBoxTitle);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1124, 200);
            this.panel2.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::CodeForger.Properties.Resources.save_file_image;
            this.pictureBox1.Location = new System.Drawing.Point(651, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(473, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // comboBoxFileType
            // 
            this.comboBoxFileType.FormattingEnabled = true;
            this.comboBoxFileType.Items.AddRange(new object[] {
            ".txt (Text)",
            ".psc (Pseudocode)",
            ".lsp (LISP)",
            ".bf (Brainfuck)",
            ".c (C)",
            ".cpp (C++)"});
            this.comboBoxFileType.Location = new System.Drawing.Point(420, 138);
            this.comboBoxFileType.Name = "comboBoxFileType";
            this.comboBoxFileType.Size = new System.Drawing.Size(185, 45);
            this.comboBoxFileType.TabIndex = 2;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(144, 138);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(257, 44);
            this.textBoxTitle.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 37);
            this.label2.TabIndex = 0;
            this.label2.Text = "Title:";
            // 
            // FormSaveFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 631);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSaveFileDialog";
            this.Text = "Save File";
            this.Load += new System.EventHandler(this.FormSaveFileDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonSaveDBFile;
        private System.Windows.Forms.RadioButton radioButtonSaveExtFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxFileType;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}