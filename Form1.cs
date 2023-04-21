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
        public Form1()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.fundal;
            pictureBoxLogo.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
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

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            var form = new FormOpenFileDialog();
            form.Show();
        }
    }
}
