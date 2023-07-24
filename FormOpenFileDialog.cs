using CodeForger.CodeForgerDBDataSetTableAdapters;
using extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeForger
{
    public partial class FormOpenFileDialog : Form
    {
        public static string titleGlobal, pathGlobal, contentsGlobal, isexternalGlobal;

        public FormOpenFileDialog()
        {
            InitializeComponent();
        }

        private void FormOpenFileDialog_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.AccountLogin != -1)
            {
                displayDBData();
            }
            else
            {
                radioButtonLoadDBFile.Enabled = false;
                radioButtonLoadDBFile.Checked = false;
                radioButtonOpenExtFile.Checked = true;
            }

            this.Location = new Point(
    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            //MessageBox.Show(this.Owner.Name);
        }

        string parseFileExtension(string extension)
        {
            switch (extension)
            {
                case ".txt":
                    return "Text";
                case ".c":
                    return "C";
                case ".bf":
                    return "Brainfuck";
                case ".cpp":
                    return "C++";
                case ".lsp":
                    return "LISP";
                case ".psc":
                    return "Pseudocode";
            }
            return null;
        }

        string parseFileTypeName(string fileTypeName)
        {
            switch (fileTypeName)
            {
                case "Text":
                    return ".txt";
                case "C":
                    return ".c";
                case "Brainfuck":
                    return ".bf";
                case "C++":
                    return ".cpp";
                case "LISP":
                    return ".lsp";
                case "Pseudocode":
                    return ".psc";
            }
            return null;
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (*.txt)|*.txt|Pseudocode files (*.psc)|*.psc|Brainfuck files (*.bf)|*.bf|LISP files (*.lsp)|*.lsp|C files (*.c)|*.c|C++ files (*.cpp)|*.cpp|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string title = Path.GetFileName(ofd.FileName);
                string path = ofd.FileName;

                StreamReader sr = new StreamReader(path);
                string content = sr.ReadToEnd();
                sr.Close();
                //MessageBox.Show(title);

                titleGlobal = title;
                pathGlobal = path;
                contentsGlobal = content;
                isexternalGlobal = "1";

                Properties.Settings.Default.OpenFileTitle = titleGlobal;
                Properties.Settings.Default.OpenFilePath = pathGlobal;
                Properties.Settings.Default.OpenFileContents = contentsGlobal;
                Properties.Settings.Default.OpenFileIsExternal = isexternalGlobal;
                Properties.Settings.Default.OpenFileType = parseFileExtension(Path.GetExtension(path));
                //MessageBox.Show(Path.GetExtension(path));

                if (string.Equals(this.Owner.Name, "Form1"))
                {
                    var form = new FormMain(title, path, content, "1", parseFileExtension(Path.GetExtension(path)));
                    form.Show();
                    this.Hide();
                    //this.Close();
                    //this.Dispose();
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
                else
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

        /*private void showDBData()
        {
            LinkLabel[] files = new LinkLabel[100];
            int counter = 0;

            CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
            var data = codeTableTA.GetData();

            foreach (var row in data)
            {
                //MessageBox.Show(Convert.ToDateTime(row[3]).Day.ToString());
                files[counter] = new LinkLabel();
                files[counter].Size = new Size(300, 25);
                files[counter].Font = new Font("Microsoft Sans Serif", 10);
                files[counter].Text = row[5].ToString() + "     |     " + Convert.ToDateTime(row[3]).Day.ToString() + "." + Convert.ToDateTime(row[3]).Month.ToString() + "." + Convert.ToDateTime(row[3]).Year.ToString() + " at " + Convert.ToDateTime(row[3]).Hour + ":" + Convert.ToDateTime(row[3]).Minute;
                files[counter].Location = new Point(10, counter * 30);
                files[counter].Click += new EventHandler(linkLabelFiles_Click);
                panelOptions.Controls.Add(files[counter]);
                counter++;
            }
        }*/

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.RowIndex.ToString());
            DataGridView dataGridView = (DataGridView)panelOptions.Controls[0];

            if (e.RowIndex == dataGridView.Rows.Count - 1 || e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow dataGridViewRow = dataGridView.Rows[e.RowIndex];
            DataGridViewCell dataGridViewCell = dataGridViewRow.Cells[0];

            CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
            var data = codeTableTA.GetData();

            string codeName = dataGridViewCell.Value.ToString();

            int counter = 0;
            foreach (var row in data)
            {
                if (string.Equals(row[5].ToString(), codeName))
                {
                    titleGlobal = row[5].ToString();
                    pathGlobal = null;
                    contentsGlobal = row[1].ToString();
                    isexternalGlobal = "0";

                    Properties.Settings.Default.OpenFileTitle = titleGlobal;
                    Properties.Settings.Default.OpenFilePath = pathGlobal;
                    Properties.Settings.Default.OpenFileContents = contentsGlobal;
                    Properties.Settings.Default.OpenFileIsExternal = isexternalGlobal;

                    string extension = parseFileTypeName(row[2].ToString());

                    //Create a temporary file to allow compiling
                    string newPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\tmp\" + titleGlobal + extension));
                    if (!File.Exists(newPath))
                    {
                        File.Create(newPath).Close();
                    }
                    StreamWriter sr = new StreamWriter(newPath);
                    sr.Write(row[1].ToString());
                    sr.Close();

                    if (this.Owner.Name == "Form1")
                    {

                        var form = new FormMain(row[5].ToString(), counter.ToString(), row[1].ToString(), "0", row[2].ToString());
                        form.Show();
                        this.Hide();
                        //this.Close();
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
                    else
                    {
                        //MessageBox.Show("lol");
                        this.Close();
                    }
                }
                counter++;
            }
        }

        private void displayDBData()
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.Size = new Size(this.Width - 20, panelOptions.Height - 5);
            dataGridView.Location = new Point(0, 0);

            dataGridView.BackgroundColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.GridColor = Color.White;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            //dataGridView.CurrentCell = null;
            //dataGridView.ClearSelection();
            dataGridView.ReadOnly = true;
            //dataGridView.SelectedRows.Clear();
            //dataGridView.Column

            dataGridView.CellDoubleClick += dataGridView_CellDoubleClick;

            dataGridView.ColumnCount = 3;
            dataGridView.Columns[0].Name = "Name";
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[1].Name = "Date Modified";
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[2].Name = "Type";
            dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            panelOptions.Controls.Add(dataGridView);

            CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
            var data = codeTableTA.GetData();

            foreach (var row in data)
            {
                if (int.Parse(row[4].ToString()) == Properties.Settings.Default.AccountLogin)
                    dataGridView.Rows.Add(row[5].ToString(), row[3].ToString(), row[2].ToString());
            }
        }

        private void radioButtonLoadDBFile_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLoadDBFile.Checked == true)
            {
                panelOptions.Controls.Clear();
                //MessageBox.Show(nr.ToString());
                displayDBData();
            }
            else
            {
                panelOptions.Controls.Clear();
                Button buttonOpenFile = new Button();
                buttonOpenFile.Text = "Choose file";
                buttonOpenFile.Location = new Point(5, 5);
                buttonOpenFile.Size = new Size(120, 30);
                buttonOpenFile.Click += new EventHandler(buttonOpenFile_Click);
                panelOptions.Controls.Add(buttonOpenFile);
            }
        }
    }
}
