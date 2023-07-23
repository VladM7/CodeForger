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
            comboBoxFileType.SelectedIndex = 0;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (radioButtonSaveDBFile.Checked)
            {
                CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
                string filetype = "";
                switch (comboBoxFileType.SelectedIndex)
                {
                    case 0:
                        filetype = "Text";
                        break;
                    case 1:
                        filetype = "LISP";
                        break;
                    case 2:
                        filetype = "Brainfuck";
                        break;
                    case 3:
                        filetype = "C";
                        break;
                    case 4:
                        filetype = "C++";
                        break;
                }

                if (filetype == "")
                {
                    MessageBox.Show("Please select a file type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                codeTableTA.Insert(contentsGlobal, filetype, DateTime.Now, Properties.Settings.Default.AccountLogin, textBoxTitle.Text);
                Properties.Settings.Default.OpenFileTitle = textBoxTitle.Text;
                Properties.Settings.Default.OpenFilePath = null;
                Properties.Settings.Default.OpenFileIsExternal = "0";
                this.Close();
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save File";
                //if (comboBoxFileType.SelectedIndex == 0)
                sfd.Filter = "Text files (*.txt)|*.txt|LISP files (*.lsp)|*.lsp|Brainfuck files (*.bf)|*.bf|C files (*.c)|*.c|C++ files (*.cpp)|*.cpp|All files (*.*)|*.*";
                switch (sfd.FilterIndex)
                {
                    case 0:
                        sfd.FileName = "untitled.txt";
                        break;
                    case 1:
                        sfd.FileName = "untitled.lsp";
                        break;
                    case 2:
                        sfd.FileName = "untitled.bf";
                        break;
                    case 3:
                        sfd.FileName = "untitled.c";
                        break;
                    case 4:
                        sfd.FileName = "untitled.cpp";
                        break;
                }
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
                    switch (Path.GetExtension(sfd.FileName))
                    {
                        case ".txt":
                            Properties.Settings.Default.OpenFileType = "Text";
                            break;
                        case ".lsp":
                            Properties.Settings.Default.OpenFileType = "LISP";
                            break;
                        case ".bf":
                            Properties.Settings.Default.OpenFileType = "Brainfuck";
                            break;
                        case ".c":
                            Properties.Settings.Default.OpenFileType = "C";
                            break;
                        case ".cpp":
                            Properties.Settings.Default.OpenFileType = "C++";
                            break;
                    }
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

        private void FormSaveFileDialog_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.AccountLogin == -1)
            {
                radioButtonSaveDBFile.Enabled = false;
                radioButtonSaveDBFile.Checked = false;
                radioButtonSaveExtFile.Checked = true;
            }

            this.Location = new Point(
    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }
    }
}
