using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeForger
{
    public partial class FormOpenFileDialog : Form
    {
        public FormOpenFileDialog()
        {
            InitializeComponent();
        }

        private void FormOpenFileDialog_Load(object sender, EventArgs e)
        {

        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "LISP files (txt,lsp)|*.txt;*.lsp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var form = new FormMain();
                form.Show();
                this.Hide();
                form.FormClosed += (s, args) =>
                {
                    this.Close();
                    foreach (Form f in Application.OpenForms)
                        if (f.Name == "Form1")
                            f.Close();
                };
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name == "Form1")
                        f.Hide();
                }
            }
        }

        private void radioButtonLoadDBFile_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLoadDBFile.Checked == true)
            {

            }
            else
            {
                Button buttonOpenFile = new Button();
                buttonOpenFile.Text = "Choose file";
                buttonOpenFile.Click += new EventHandler(buttonOpenFile_Click);
                panelOptions.Controls.Add(buttonOpenFile);
            }
        }
    }
}
