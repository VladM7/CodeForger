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
    public partial class FormAccountSettings : Form
    {
        public FormAccountSettings()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.fundal;
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

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            UsersTableTableAdapter adapter = new UsersTableTableAdapter();
            CodeForgerDBDataSet.UsersTableDataTable data = adapter.GetData();
            int accID = Properties.Settings.Default.AccountLogin;

            int counter = 0;
            foreach (var row in data)
            {
                if (int.Parse(row[0].ToString()) == accID)
                {
                    if (string.Equals(row[3].ToString(), encrypt(textBoxOldPassword.Text)))
                    {
                        if (textBoxNewPassword.Text == textBoxOldPassword.Text)
                        {
                            MessageBox.Show("New password cannot be the same as the old password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBoxNewPassword.Text = "";
                            return;
                        }
                        if (textBoxNewPassword.Text.Length < 3)
                        {
                            MessageBox.Show("Minimum password length is 3 characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBoxOldPassword.Text = "";
                            textBoxNewPassword.Text = "";
                            return;
                        }
                        data[counter][3] = encrypt(textBoxNewPassword.Text);
                        adapter.Update(data);
                        MessageBox.Show("Password updated successfully!");
                        textBoxOldPassword.Text = "";
                        textBoxNewPassword.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Invalid old password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxOldPassword.Text = "";
                        textBoxNewPassword.Text = "";
                    }
                }
                counter++;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDeleteAccount_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete your account? This will also delete the files you saved in the database.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                UsersTableTableAdapter adapter = new UsersTableTableAdapter();
                CodeForgerDBDataSet.UsersTableDataTable data = adapter.GetData();
                int accID = Properties.Settings.Default.AccountLogin;

                int counter = 0;
                foreach (var row in data)
                {
                    if (int.Parse(row[0].ToString()) == accID)
                    {
                        data[counter].Delete();
                        break;
                    }
                    counter++;
                }
                //data[accID].Delete();
                adapter.Update(data);


                CodeTableTableAdapter codeTA = new CodeTableTableAdapter();
                var data2 = codeTA.GetData();

                counter = 0;
                foreach (var row in data2)
                {
                    if (int.Parse(row[4].ToString()) == accID)
                    {
                        data2[counter].Delete();
                    }
                    counter++;
                }
                codeTA.Update(data2);

                Properties.Settings.Default.AccountLogin = -1;
                Properties.Settings.Default.RememberAccount = false;
                Properties.Settings.Default.Save();

                this.Close();
            }
        }

        private void FormAccountSettings_Load(object sender, EventArgs e)
        {
            this.Location = new Point(
    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }
    }
}
