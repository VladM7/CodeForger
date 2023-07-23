using CodeForger.CodeForgerDBDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeForger
{
    public partial class FormAccount : Form
    {
        public FormAccount()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.fundal;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            var form = new FormLogin();
            form.Show();
            this.Hide();
            form.FormClosed += (s, args) =>
            {
                this.Show();
            };
        }

        public string cesar(StringBuilder s)
        {
            int n = s.Length;
            for (int i = 0; i < n; i++)
            {
                if (s[i] != 32)
                {
                    if ((s[i] >= 88 && s[i] <= 91) || (s[i] >= 120 && s[i] <= 123))
                        s[i] = (char)(s[i] - 23);
                    else
                        s[i] = (char)(s[i] + 3);
                }
            }

            return transport(s);
        }

        public string transport(StringBuilder s)
        {
            int n = s.Length;
            if (n % 5 == 0)
            {
                for (int i = 0; i < n; i += 5)
                {
                    char aux1 = (char)s[i];
                    char aux3 = (char)s[i + 2];
                    char aux4 = (char)s[i + 3];
                    char aux5 = (char)s[i + 4];
                    s[i] = (char)aux3;
                    s[i + 2] = (char)aux5;
                    s[i + 3] = (char)aux1;
                    s[i + 4] = (char)aux4;
                }
                string str = s.ToString();
                return str;
            }
            else
                if (n % 5 == 1)
            {
                for (int i = 0; i < n - 1; i += 5)
                {
                    char aux1 = (char)s[i];
                    char aux3 = (char)s[i + 2];
                    char aux4 = (char)s[i + 3];
                    char aux5 = (char)s[i + 4];
                    s[i] = (char)aux3;
                    s[i + 2] = (char)aux5;
                    s[i + 3] = (char)aux1;
                    s[i + 4] = (char)aux4;
                }
                string str = s.ToString();
                return str;
            }
            else
                if (n % 5 == 2)
            {
                for (int i = 0; i < n - 2; i += 5)
                {
                    char aux1 = (char)s[i];
                    char aux3 = (char)s[i + 2];
                    char aux4 = (char)s[i + 3];
                    char aux5 = (char)s[i + 4];
                    s[i] = (char)aux3;
                    s[i + 2] = (char)aux5;
                    s[i + 3] = (char)aux1;
                    s[i + 4] = (char)aux4;
                }
                char aux6 = (char)s[n - 1];
                char aux2 = (char)s[n - 2];
                s[n - 2] = (char)aux6;
                s[n - 1] = (char)aux2;
                string str = s.ToString();
                return str;
            }
            else
                if (n % 5 == 3)
            {
                for (int i = 0; i < n - 3; i += 5)
                {
                    char aux1 = (char)s[i];
                    char aux3 = (char)s[i + 2];
                    char aux4 = (char)s[i + 3];
                    char aux5 = (char)s[i + 4];
                    s[i] = (char)aux3;
                    s[i + 2] = (char)aux5;
                    s[i + 3] = (char)aux1;
                    s[i + 4] = (char)aux4;
                }
                char aux6 = (char)s[n - 1];
                char aux2 = (char)s[n - 2];
                char aux7 = (char)s[n - 3];
                s[n - 2] = (char)aux7;
                s[n - 1] = (char)aux2;
                s[n - 3] = (char)aux6;
                string str = s.ToString();
                return str;
            }
            else
                if (n % 5 == 4)
            {
                for (int i = 0; i < n - 4; i += 5)
                {
                    char aux1 = (char)s[i];
                    char aux3 = (char)s[i + 2];
                    char aux4 = (char)s[i + 3];
                    char aux5 = (char)s[i + 4];
                    s[i] = (char)aux3;
                    s[i + 2] = (char)aux5;
                    s[i + 3] = (char)aux1;
                    s[i + 4] = (char)aux4;
                }
                char aux6 = (char)s[n - 1];
                char aux2 = (char)s[n - 2];
                char aux7 = (char)s[n - 3];
                char aux8 = (char)s[n - 4];
                s[n - 2] = (char)aux7;
                s[n - 1] = (char)aux2;
                s[n - 3] = (char)aux8;
                s[n - 4] = (char)aux6;
                string str = s.ToString();
                return str;
            }
            return null;
        }

        private string encrypt(string text)
        {
            StringBuilder sb = new StringBuilder();
            string textBoxValue = text;
            sb.Append(textBoxValue);
            return cesar(sb);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxEmail.Text.Length == 0 || textBoxPassword.Text.Length == 0)
            {
                MessageBox.Show("Please fill out the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UsersTableTableAdapter adapter = new UsersTableTableAdapter();
            CodeForgerDBDataSet.UsersTableDataTable data = adapter.GetData();
            //MessageBox.Show(data.Rows[0][1].ToString());
            for (int i = 0; i < data.Rows.Count; i++)
            {
                //MessageBox.Show(data.Rows[i][1].ToString());
                if (string.Equals(data.Rows[i][2].ToString().Trim(), textBoxEmail.Text.Trim()))
                {
                    //MessageBox.Show("Email corect!");
                    if (string.Equals(data.Rows[i][3].ToString().Trim(), encrypt(textBoxPassword.Text.Trim()).Trim()))
                    {
                        MessageBox.Show("Email and password are correct!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Properties.Settings.Default.RememberAccount = checkBoxRemember.Checked;
                        Properties.Settings.Default.AccountLogin = int.Parse(data.Rows[i][0].ToString());
                        Properties.Settings.Default.Save();
                        //MessageBox.Show(i + "");
                        this.Close();
                        return;
                    }
                }
            }
            MessageBox.Show("Invalid email/password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            textBoxEmail.Text = "";
            textBoxPassword.Text = "";
            Properties.Settings.Default.AccountLogin = -1;
            Properties.Settings.Default.Save();
        }

        public void toggleDarkMode()
        {
            if (Properties.Settings.Default.AccountDarkModeSetting == false || Properties.Settings.Default.AccountLogin == -1)
            {
                this.BackgroundImage = Properties.Resources.fundal;
                this.label1.ForeColor = Color.Black;
                this.label2.ForeColor = Color.Black;
                this.label3.ForeColor = Color.Black;
                this.label4.ForeColor = Color.Black;
                this.label1.BackColor = Color.Transparent;
                this.label2.BackColor = Color.Transparent;
                this.label3.BackColor = Color.Transparent;
                this.label4.BackColor = Color.Transparent;
                pictureBoxLogo.BackColor = Color.Transparent;
                this.buttonRegister.ForeColor = Color.Black;
                this.buttonCancel.ForeColor = Color.Black;
                this.buttonRegister.BackColor = Color.White;
                this.buttonCancel.BackColor = Color.White;
                this.buttonLogin.ForeColor = Color.Black;
                this.buttonLogin.BackColor = Color.White;
                this.checkBoxRemember.ForeColor = Color.Black;
                this.checkBoxRemember.BackColor = Color.Transparent;
            }
            else
            {
                this.BackgroundImage = Properties.Resources.darkModeForm1;
                this.label1.ForeColor = Color.White;
                this.label2.ForeColor = Color.White;
                this.label3.ForeColor = Color.White;
                this.label4.ForeColor = Color.White;
                this.label1.BackColor = Color.Transparent;
                this.label2.BackColor = Color.Transparent;
                this.label3.BackColor = Color.Transparent;
                this.label4.BackColor = Color.Transparent;
                pictureBoxLogo.BackColor = Color.Transparent;
                this.buttonRegister.ForeColor = Color.White;
                this.buttonCancel.ForeColor = Color.White;
                this.buttonRegister.BackColor = Color.Gray;
                this.buttonCancel.BackColor = Color.Gray;
                this.buttonLogin.ForeColor = Color.White;
                this.buttonLogin.BackColor = Color.Gray;
                this.checkBoxRemember.ForeColor = Color.White;
                this.checkBoxRemember.BackColor = Color.Transparent;
                foreach (Button btn in this.Controls.OfType<Button>())
                    btn.FlatStyle = FlatStyle.Flat;
            }
        }

        private void FormAccount_Load(object sender, EventArgs e)
        {
            toggleDarkMode();

            this.Location = new Point(
    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }
    }
}
