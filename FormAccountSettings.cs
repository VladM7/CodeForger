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
                    if (string.Equals(row[3].ToString(), textBoxOldPassword.Text))
                    {
                        if (textBoxNewPassword.Text.Length < 3)
                        {
                            MessageBox.Show("Minimum password length is 3 characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBoxOldPassword.Text = "";
                            textBoxNewPassword.Text = "";
                            return;
                        }
                        data[counter][3] = textBoxNewPassword.Text;
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
