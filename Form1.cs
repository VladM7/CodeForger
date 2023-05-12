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
            //this.BackgroundImage = Properties.Resources.fundal;
            pictureBoxLogo.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
        }

        private void updateLoginButtonState()
        {
            //MessageBox.Show(Properties.Settings.Default.AccountLogin.ToString());

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
                foreach (var row in data)
                {
                    if (int.Parse(row[0].ToString()) == Properties.Settings.Default.AccountLogin)
                    {
                        labelEmail.Text = row[1].ToString();
                        break;
                    }
                }
                buttonAccountSettings.Visible = true;
            }
        }

        public void toggleDarkMode()
        {
            if (Properties.Settings.Default.AccountDarkModeSetting == false || Properties.Settings.Default.AccountLogin == -1)
            {
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
                this.labelCopyright.ForeColor = Color.Black;
                foreach (Button btn in this.Controls.OfType<Button>())
                    btn.FlatStyle = FlatStyle.Standard;
            }
            else
            {
                this.BackgroundImage = Properties.Resources.darkModeForm1;
                this.label1.ForeColor = Color.White;
                this.buttonNewFile.ForeColor = Color.White;
                this.buttonOpenFile.ForeColor = Color.White;
                this.buttonAccountSettings.ForeColor = Color.White;
                this.buttonLog.ForeColor = Color.White;
                this.buttonNewFile.BackColor = Color.DimGray;
                this.buttonOpenFile.BackColor = Color.DimGray;
                this.buttonAccountSettings.BackColor = Color.DimGray;
                this.buttonLog.BackColor = Color.DimGray;
                this.labelCopyright.ForeColor = Color.White;
                foreach (Button btn in this.Controls.OfType<Button>())
                    btn.FlatStyle = FlatStyle.Flat;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(Properties.Settings.Default.RememberAccount.ToString());
            //MessageBox.Show(Properties.Settings.Default.AccountLogin.ToString());
            if (Properties.Settings.Default.RememberAccount == true && Properties.Settings.Default.AccountLogin != -1)
            {
                updateLoginButtonState();
                buttonAccountSettings.Visible = true;
            }
            else
                buttonAccountSettings.Visible = false;

            toggleDarkMode();

            this.Location = new Point(
    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }

        private void buttonNewFile_Click(object sender, EventArgs e)
        {
            var form = new FormMain("Untitled*", null, null, null, null);
            form.Show();
            this.Hide();
            form.FormClosed += (sdr, args) =>
            {
                Application.Exit();
            };
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
            form.Owner = this;
            form.ShowDialog();
        }
    }
}
