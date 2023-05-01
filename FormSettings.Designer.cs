namespace CodeForger
{
    partial class FormSettings
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.groupBoxEditor = new System.Windows.Forms.GroupBox();
            this.checkBoxErrorSquiggles = new System.Windows.Forms.CheckBox();
            this.checkBoxWordWrap = new System.Windows.Forms.CheckBox();
            this.groupBoxAppearance = new System.Windows.Forms.GroupBox();
            this.radioButtonLight = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButtonDark = new System.Windows.Forms.RadioButton();
            this.tabPageDatabase = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridViewDB = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageTabsWindows = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxShowOutput = new System.Windows.Forms.CheckBox();
            this.checkBoxShowErrorList = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxShowNavbar = new System.Windows.Forms.CheckBox();
            this.tabControlSettings.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.groupBoxEditor.SuspendLayout();
            this.groupBoxAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPageDatabase.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDB)).BeginInit();
            this.tabPageTabsWindows.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlSettings.Controls.Add(this.tabPageGeneral);
            this.tabControlSettings.Controls.Add(this.tabPageDatabase);
            this.tabControlSettings.Controls.Add(this.tabPageTabsWindows);
            this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSettings.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlSettings.ItemSize = new System.Drawing.Size(25, 100);
            this.tabControlSettings.Location = new System.Drawing.Point(0, 0);
            this.tabControlSettings.Multiline = true;
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(1996, 1065);
            this.tabControlSettings.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlSettings.TabIndex = 0;
            this.tabControlSettings.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlSettings_DrawItem);
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.groupBoxEditor);
            this.tabPageGeneral.Controls.Add(this.groupBoxAppearance);
            this.tabPageGeneral.Location = new System.Drawing.Point(104, 4);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(1888, 1057);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBoxEditor
            // 
            this.groupBoxEditor.Controls.Add(this.checkBoxErrorSquiggles);
            this.groupBoxEditor.Controls.Add(this.checkBoxWordWrap);
            this.groupBoxEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxEditor.Location = new System.Drawing.Point(3, 407);
            this.groupBoxEditor.Name = "groupBoxEditor";
            this.groupBoxEditor.Size = new System.Drawing.Size(1882, 261);
            this.groupBoxEditor.TabIndex = 1;
            this.groupBoxEditor.TabStop = false;
            this.groupBoxEditor.Text = "Editor";
            // 
            // checkBoxErrorSquiggles
            // 
            this.checkBoxErrorSquiggles.AutoSize = true;
            this.checkBoxErrorSquiggles.Location = new System.Drawing.Point(44, 171);
            this.checkBoxErrorSquiggles.Name = "checkBoxErrorSquiggles";
            this.checkBoxErrorSquiggles.Size = new System.Drawing.Size(364, 41);
            this.checkBoxErrorSquiggles.TabIndex = 1;
            this.checkBoxErrorSquiggles.Text = "Show error squiggles";
            this.checkBoxErrorSquiggles.UseVisualStyleBackColor = true;
            this.checkBoxErrorSquiggles.CheckedChanged += new System.EventHandler(this.checkBoxErrorSquiggles_CheckedChanged);
            // 
            // checkBoxWordWrap
            // 
            this.checkBoxWordWrap.AutoSize = true;
            this.checkBoxWordWrap.Location = new System.Drawing.Point(44, 93);
            this.checkBoxWordWrap.Name = "checkBoxWordWrap";
            this.checkBoxWordWrap.Size = new System.Drawing.Size(220, 41);
            this.checkBoxWordWrap.TabIndex = 0;
            this.checkBoxWordWrap.Text = "Word wrap";
            this.checkBoxWordWrap.UseVisualStyleBackColor = true;
            this.checkBoxWordWrap.CheckedChanged += new System.EventHandler(this.checkBoxWordWrap_CheckedChanged);
            // 
            // groupBoxAppearance
            // 
            this.groupBoxAppearance.Controls.Add(this.radioButtonLight);
            this.groupBoxAppearance.Controls.Add(this.pictureBox2);
            this.groupBoxAppearance.Controls.Add(this.pictureBox1);
            this.groupBoxAppearance.Controls.Add(this.radioButtonDark);
            this.groupBoxAppearance.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxAppearance.Location = new System.Drawing.Point(3, 3);
            this.groupBoxAppearance.Name = "groupBoxAppearance";
            this.groupBoxAppearance.Size = new System.Drawing.Size(1882, 404);
            this.groupBoxAppearance.TabIndex = 0;
            this.groupBoxAppearance.TabStop = false;
            this.groupBoxAppearance.Text = "Appearance";
            // 
            // radioButtonLight
            // 
            this.radioButtonLight.AutoSize = true;
            this.radioButtonLight.Location = new System.Drawing.Point(503, 302);
            this.radioButtonLight.Name = "radioButtonLight";
            this.radioButtonLight.Size = new System.Drawing.Size(219, 41);
            this.radioButtonLight.TabIndex = 3;
            this.radioButtonLight.TabStop = true;
            this.radioButtonLight.Text = "Light Mode";
            this.radioButtonLight.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CodeForger.Properties.Resources.light_setting_icon;
            this.pictureBox2.Location = new System.Drawing.Point(503, 66);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(180, 196);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CodeForger.Properties.Resources.dark_setting_icon;
            this.pictureBox1.Location = new System.Drawing.Point(44, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 196);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // radioButtonDark
            // 
            this.radioButtonDark.AutoSize = true;
            this.radioButtonDark.Location = new System.Drawing.Point(44, 302);
            this.radioButtonDark.Name = "radioButtonDark";
            this.radioButtonDark.Size = new System.Drawing.Size(217, 41);
            this.radioButtonDark.TabIndex = 0;
            this.radioButtonDark.TabStop = true;
            this.radioButtonDark.Text = "Dark Mode";
            this.radioButtonDark.UseVisualStyleBackColor = true;
            this.radioButtonDark.CheckedChanged += new System.EventHandler(this.radioButtonDark_CheckedChanged);
            // 
            // tabPageDatabase
            // 
            this.tabPageDatabase.Controls.Add(this.groupBox3);
            this.tabPageDatabase.Location = new System.Drawing.Point(104, 4);
            this.tabPageDatabase.Name = "tabPageDatabase";
            this.tabPageDatabase.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDatabase.Size = new System.Drawing.Size(1888, 1057);
            this.tabPageDatabase.TabIndex = 1;
            this.tabPageDatabase.Text = "Database";
            this.tabPageDatabase.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridViewDB);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1882, 1051);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Database Settings";
            // 
            // dataGridViewDB
            // 
            this.dataGridViewDB.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewDB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewDB.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewDB.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDB.DefaultCellStyle = dataGridViewCellStyle27;
            this.dataGridViewDB.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewDB.GridColor = System.Drawing.Color.White;
            this.dataGridViewDB.Location = new System.Drawing.Point(3, 77);
            this.dataGridViewDB.Name = "dataGridViewDB";
            this.dataGridViewDB.ReadOnly = true;
            this.dataGridViewDB.RowHeadersVisible = false;
            this.dataGridViewDB.RowHeadersWidth = 123;
            this.dataGridViewDB.RowTemplate.Height = 46;
            this.dataGridViewDB.Size = new System.Drawing.Size(1876, 968);
            this.dataGridViewDB.TabIndex = 1;
            this.dataGridViewDB.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDB_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "Name";
            this.Column1.MinimumWidth = 15;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 166;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "Date modified";
            this.Column2.MinimumWidth = 15;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 277;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Type";
            this.Column3.MinimumWidth = 15;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.Color.Red;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle25;
            this.Column4.HeaderText = "Delete";
            this.Column4.MinimumWidth = 15;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle26;
            this.Column5.HeaderText = "Rename";
            this.Column5.MinimumWidth = 15;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1876, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "From here you can manage your files that are saved into the database, as well as " +
    "change related settings such as encryption.";
            // 
            // tabPageTabsWindows
            // 
            this.tabPageTabsWindows.Controls.Add(this.groupBox2);
            this.tabPageTabsWindows.Controls.Add(this.groupBox1);
            this.tabPageTabsWindows.Location = new System.Drawing.Point(104, 4);
            this.tabPageTabsWindows.Name = "tabPageTabsWindows";
            this.tabPageTabsWindows.Size = new System.Drawing.Size(1888, 1057);
            this.tabPageTabsWindows.TabIndex = 2;
            this.tabPageTabsWindows.Text = "Tabs and windows";
            this.tabPageTabsWindows.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxShowOutput);
            this.groupBox2.Controls.Add(this.checkBoxShowErrorList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 156);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1888, 261);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Error list and Output tabs";
            // 
            // checkBoxShowOutput
            // 
            this.checkBoxShowOutput.AutoSize = true;
            this.checkBoxShowOutput.Location = new System.Drawing.Point(20, 163);
            this.checkBoxShowOutput.Name = "checkBoxShowOutput";
            this.checkBoxShowOutput.Size = new System.Drawing.Size(242, 41);
            this.checkBoxShowOutput.TabIndex = 2;
            this.checkBoxShowOutput.Text = "Show output";
            this.checkBoxShowOutput.UseVisualStyleBackColor = true;
            this.checkBoxShowOutput.CheckedChanged += new System.EventHandler(this.checkBoxShowOutput_CheckedChanged);
            // 
            // checkBoxShowErrorList
            // 
            this.checkBoxShowErrorList.AutoSize = true;
            this.checkBoxShowErrorList.Location = new System.Drawing.Point(20, 83);
            this.checkBoxShowErrorList.Name = "checkBoxShowErrorList";
            this.checkBoxShowErrorList.Size = new System.Drawing.Size(268, 41);
            this.checkBoxShowErrorList.TabIndex = 1;
            this.checkBoxShowErrorList.Text = "Show error list";
            this.checkBoxShowErrorList.UseVisualStyleBackColor = true;
            this.checkBoxShowErrorList.CheckedChanged += new System.EventHandler(this.checkBoxShowErrorList_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxShowNavbar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1888, 156);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Navigation bar";
            // 
            // checkBoxShowNavbar
            // 
            this.checkBoxShowNavbar.AutoSize = true;
            this.checkBoxShowNavbar.Location = new System.Drawing.Point(20, 75);
            this.checkBoxShowNavbar.Name = "checkBoxShowNavbar";
            this.checkBoxShowNavbar.Size = new System.Drawing.Size(354, 41);
            this.checkBoxShowNavbar.TabIndex = 0;
            this.checkBoxShowNavbar.Text = "Show navigation bar";
            this.checkBoxShowNavbar.UseVisualStyleBackColor = true;
            this.checkBoxShowNavbar.CheckedChanged += new System.EventHandler(this.checkBoxShowNavbar_CheckedChanged);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1996, 1065);
            this.Controls.Add(this.tabControlSettings);
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.groupBoxEditor.ResumeLayout(false);
            this.groupBoxEditor.PerformLayout();
            this.groupBoxAppearance.ResumeLayout(false);
            this.groupBoxAppearance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPageDatabase.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDB)).EndInit();
            this.tabPageTabsWindows.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageDatabase;
        private System.Windows.Forms.GroupBox groupBoxEditor;
        private System.Windows.Forms.GroupBox groupBoxAppearance;
        private System.Windows.Forms.TabPage tabPageTabsWindows;
        private System.Windows.Forms.RadioButton radioButtonLight;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioButtonDark;
        private System.Windows.Forms.CheckBox checkBoxErrorSquiggles;
        private System.Windows.Forms.CheckBox checkBoxWordWrap;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxShowOutput;
        private System.Windows.Forms.CheckBox checkBoxShowErrorList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxShowNavbar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridViewDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}