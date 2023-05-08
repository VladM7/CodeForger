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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.fundal;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void disposeErrorLabel()
        {
            Control[] ctrls = this.Controls.Find("errorLabel", true);
            Label errorLabel = null;
            if (ctrls.Length > 0)
                errorLabel = ctrls[0] as Label;
            if (errorLabel != null)
                errorLabel.Dispose();
        }

        private bool valid()
        {
            disposeErrorLabel();

            if (textBoxUsername.Text == "")
            {
                Label error = new Label();
                error.Text = "Username field is required!";
                error.Name = "errorLabel";
                error.Location = new Point(10, 280);
                error.Size = new Size(350, 40);
                this.Controls.Add(error);
                error.BackColor = Color.Transparent;
                error.ForeColor = Color.Red;
                return false;
            }
            if (textBoxEmail.Text == "")
            {
                Label error = new Label();
                error.Text = "Email field is required!";
                error.Name = "errorLabel";
                error.Location = new Point(10, 280);
                error.Size = new Size(350, 40);
                this.Controls.Add(error);
                error.BackColor = Color.Transparent;
                error.ForeColor = Color.Red;
                return false;
            }
            if (textBoxPassword.Text == "")
            {
                Label error = new Label();
                error.Text = "Password field is required!";
                error.Name = "errorLabel";
                error.Location = new Point(10, 280);
                error.Size = new Size(350, 40);
                this.Controls.Add(error);
                error.BackColor = Color.Transparent;
                error.ForeColor = Color.Red;
                return false;
            }
            if (textBoxEmail.Text.IndexOf('@') == -1 || textBoxEmail.Text.IndexOf('.') == -1)
            {
                Label error = new Label();
                error.Text = "Invalid Email!";
                error.Name = "errorLabel";
                error.Location = new Point(10, 280);
                error.Size = new Size(350, 40);
                error.BackColor = Color.Transparent;
                error.ForeColor = Color.Red;
                this.Controls.Add(error);
                return false;
            }
            if (textBoxPassword.Text.Length < 3)
            {
                Label error = new Label();
                error.Text = "Password is too short! Minimum length is 3 characters.";
                error.Name = "errorLabel";
                error.Location = new Point(10, 280);
                error.Size = new Size(350, 40);
                this.Controls.Add(error);
                error.BackColor = Color.Transparent;
                error.ForeColor = Color.Red;
                return false;
            }
            if (textBoxPassword != textBoxConfirmPassword)
            {
                Label error = new Label();
                error.Text = "Passwords don't match!";
                error.Name = "errorLabel";
                error.Location = new Point(10, 280);
                error.Size = new Size(350, 40);
                this.Controls.Add(error);
                error.BackColor = Color.Transparent;
                error.ForeColor = Color.Red;
                return false;
            }
            return true;
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

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (valid() == true)
            {
                //Adaugare in DB
                UsersTableTableAdapter adapter = new UsersTableTableAdapter();
                adapter.Insert(textBoxUsername.Text, textBoxEmail.Text, encrypt(textBoxPassword.Text));

                var data = adapter.GetData();

                PrefsTableTableAdapter prefsTableTA = new PrefsTableTableAdapter();
                prefsTableTA.Insert(1, 1, 1, 1, 1, 1, int.Parse(data[data.Rows.Count - 1][0].ToString()));

                MessageBox.Show("Account created successfully!");
                this.Close();
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.Location = new Point(
    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }
    }
}
