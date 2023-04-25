using CodeForger.CodeForgerDBDataSetTableAdapters;
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
using static System.Net.Mime.MediaTypeNames;

namespace CodeForger
{
    public partial class FormSaveFileDialog : Form
    {
        private string contentsGlobal;

        public FormSaveFileDialog(string contents)
        {
            InitializeComponent();
            contentsGlobal = contents;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (radioButtonSaveDBFile.Checked)
            {
                CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
                codeTableTA.Insert(contentsGlobal, "LISP", DateTime.Now, Properties.Settings.Default.AccountLogin, textBoxTitle.Text);
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save File";
                //if (comboBoxFileType.SelectedIndex == 0)
                sfd.Filter = "LISP files (txt,lsp)|*.txt;*.lsp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fs = File.Create(sfd.FileName))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(contentsGlobal);
                        fs.Write(bytes, 0, bytes.Length);
                    }
                    Properties.Settings.Default.OpenFileTitle = Path.GetFileName(sfd.FileName);
                    Properties.Settings.Default.OpenFilePath = sfd.FileName;
                    Properties.Settings.Default.OpenFileIsExternal = "1";
                    this.Close();
                }
            }
        }

        private void radioButtonSaveExtFile_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSaveExtFile.Checked)
            {
                label2.Enabled = false;
                label2.Visible = false;
                textBoxTitle.Enabled = false;
                textBoxTitle.Visible = false;
                comboBoxFileType.Enabled = false;
                comboBoxFileType.Visible = false;
            }
            else
            {
                label2.Enabled = true;
                label2.Visible = true;
                textBoxTitle.Enabled = true;
                textBoxTitle.Visible = true;
                comboBoxFileType.Enabled = true;
                comboBoxFileType.Visible = true;
            }
        }
    }
}
