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
        }

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            UsersTableTableAdapter adapter = new UsersTableTableAdapter();
            CodeForgerDBDataSet.UsersTableDataTable data = adapter.GetData();
            int accID = Properties.Settings.Default.AccountLogin;
            if (string.Equals(data[accID][3].ToString(), textBoxOldPassword.Text))
            {
                data[accID][3] = textBoxNewPassword.Text;
                adapter.Update(data);
                MessageBox.Show("Password updated successfully!");
                textBoxOldPassword.Text = "";
                textBoxNewPassword.Text = "";
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDeleteAccount_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete your account?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                UsersTableTableAdapter adapter = new UsersTableTableAdapter();
                CodeForgerDBDataSet.UsersTableDataTable data = adapter.GetData();
                int accID = Properties.Settings.Default.AccountLogin;
                data[accID].Delete();
                adapter.Update(data);

                Properties.Settings.Default.AccountLogin = -1;
                Properties.Settings.Default.RememberAccount = false;
                Properties.Settings.Default.Save();

                this.Close();
            }
        }
    }
}
