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

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            UsersTableTableAdapter adapter = new UsersTableTableAdapter();
            CodeForgerDBDataSet.UsersTableDataTable data = adapter.GetData();
            //MessageBox.Show(data.Rows[0][1].ToString());
            for (int i = 0; i < data.Rows.Count; i++)
            {
                //MessageBox.Show(data.Rows[i][1].ToString());
                if (string.Equals(data.Rows[i][2].ToString().Trim(), textBoxEmail.Text.Trim()))
                {
                    //MessageBox.Show("Email corect!");
                    if (string.Equals(data.Rows[i][3].ToString().Trim(), textBoxPassword.Text.Trim()))
                    {
                        MessageBox.Show("Email and password are correct!");
                        Properties.Settings.Default.RememberAccount = checkBoxRemember.Checked;
                        Properties.Settings.Default.AccountLogin = i;
                        Properties.Settings.Default.Save();
                        //MessageBox.Show(i + "");
                        this.Close();
                    }
                }
            }
        }
    }
}
