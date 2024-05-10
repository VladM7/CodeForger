using CodeForger.CodeForgerDBDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeForger
{
    public partial class Form1 : Form
    {
        public int ok = 0;
        public Form1()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.Dark_mode ;
            pictureBoxLogo.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
            ModeButton.BackColor = Color.Transparent;
            ModeButton.FlatStyle = FlatStyle.Flat;
            ModeButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            ModeButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            ModeButton.FlatAppearance.CheckedBackColor = Color.Transparent;
        }

        private void updateLoginButtonState()
        {
            if (Properties.Settings.Default.AccountLogin == -1)
            {
                buttonLog.Text = "Sign in";
                labelEmail.Text = "";
                buttonAccountSettings.Visible = false;
            }
            else
            {
                buttonLog.Text = "Sign out";
                UsersTableTableAdapter adapter = new UsersTableTableAdapter();
                CodeForgerDBDataSet.UsersTableDataTable data = adapter.GetData();
                labelEmail.Text = data[Properties.Settings.Default.AccountLogin][2].ToString();
                buttonAccountSettings.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RememberAccount == true)
            {
                updateLoginButtonState();
                buttonAccountSettings.Visible = true;
            }
            else
                buttonAccountSettings.Visible = false;
        }

        private void buttonNewFile_Click(object sender, EventArgs e)
        {
            var form = new FormMain();
            form.Show();
        }

        private void inchidere()
        {
            this.Show();
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            if (buttonLog.Text == "Sign in")
            {
                var form = new FormAccount();
                form.Show();
                this.Hide();
                form.FormClosed += (s, args) =>
                {
                    updateLoginButtonState();
                    this.Show();
                };
            }
            else
            {
                Properties.Settings.Default.AccountLogin = -1;
                Properties.Settings.Default.RememberAccount = false;
                Properties.Settings.Default.Save();
                buttonLog.Text = "Sign in";
                labelEmail.Text = "";
                buttonAccountSettings.Visible = false;
            }
        }

        private void buttonAccountSettings_Click(object sender, EventArgs e)
        {
            var form = new FormAccountSettings();
            form.Show();
            this.Hide();
            form.FormClosed += (s, args) =>
            {
                this.Show();
                updateLoginButtonState();
            };
        }
        private void ModeButton_Click(object sender, EventArgs e)
        {
            if (ok==0)
            {
                ModeButton.Image = Properties.Resources.soare;
                this.BackgroundImage = Properties.Resources.fundal;
                this.label1.ForeColor = Color.Black;
                this.buttonNewFile.ForeColor = Color.Black;
                this.buttonOpenFile.ForeColor = Color.Black;
                this.buttonAccountSettings.ForeColor = Color.Black;
                this.buttonLog.ForeColor = Color.Black;
                this.buttonNewFile.BackColor = Color.White;
                this.buttonOpenFile.BackColor = Color.White;
                this.buttonAccountSettings.BackColor = Color.White;
                this.buttonLog.BackColor = Color.White;
                ok = 1;
            }
            else
            {
                ModeButton.Image = Properties.Resources.luna;
                this.BackgroundImage = Properties.Resources.Dark_mode;
                this.label1.ForeColor = Color.White;
                this.buttonNewFile.ForeColor = Color.White;
                this.buttonOpenFile.ForeColor = Color.White;
                this.buttonAccountSettings.ForeColor = Color.White;
                this.buttonLog.ForeColor = Color.White;
                this.buttonNewFile.BackColor = Color.Gray;
                this.buttonOpenFile.BackColor = Color.Gray;
                this.buttonAccountSettings.BackColor = Color.Gray;
                this.buttonLog.BackColor = Color.Gray;
                ok = 0;
            }
        }
    }
}
