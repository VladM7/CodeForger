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
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool valid()
        {
            if(textBoxEmail.Text.IndexOf('@')==-1)
            {
                Label error=new Label();
                error.Text = "Invalid Email!";
                error.Location = new Point(10, 280);
                error.Size = new Size(350, 40);
                this.Controls.Add(error);
                return false;
            }
            if(textBoxPassword.Text.Length<3)
            {
                Label error = new Label();
                error.Text = "Password is too short! Minimum length is 3 characters.";
                error.Location = new Point(10, 280);
                error.Size = new Size(350, 40);
                this.Controls.Add(error);
                return false;
            }
            /*if(textBoxPassword!=textBoxConfirmPassword)
            {
                Label error = new Label();
                error.Text = "Passwords don't match!";
                error.Location = new Point(10, 280);
                error.Size = new Size(350, 40);
                this.Controls.Add(error);
                return false;
            }*/
            if(textBoxUsername.Text=="")
            {
                Label error = new Label();
                error.Text = "Username field is required!";
                error.Location = new Point(10, 280);
                error.Size = new Size(350, 40);
                this.Controls.Add(error);
                return false;
            }
            if (textBoxEmail.Text == "")
            {
                Label error = new Label();
                error.Text = "Email field is required!";
                error.Location = new Point(10, 280);
                error.Size = new Size(350, 40);
                this.Controls.Add(error);
                return false;
            }
            if (textBoxPassword.Text == "")
            {
                Label error = new Label();
                error.Text = "Password field is required!";
                error.Location = new Point(10, 280);
                error.Size = new Size(350, 40);
                this.Controls.Add(error);
                return false;
            }
            return true;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (valid() == true)
            {
                //Adaugare in DB
                UsersTableTableAdapter adapter = new UsersTableTableAdapter();
                adapter.Insert(textBoxUsername.Text, textBoxEmail.Text, textBoxPassword.Text);
                MessageBox.Show("Account created successfully!");
                this.Close();
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
