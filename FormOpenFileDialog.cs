﻿using CodeForger.CodeForgerDBDataSetTableAdapters;
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
            showDBData();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "LISP files (txt,lsp)|*.txt;*.lsp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string title = Path.GetFileName(ofd.FileName);
                string path = ofd.FileName;

                StreamReader sr = new StreamReader(path);
                string content = sr.ReadToEnd();
                //MessageBox.Show(title);

                var form = new FormMain(title, path, content);
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

        private void linkLabelFiles_Click(object sender, EventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;

            CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
            var data = codeTableTA.GetData();

            string text=linkLabel.Text;
            var name = text.Split('|');
            string codeName = name[0].Trim();

            foreach (var row in data)
            {
                if (string.Equals(row[5].ToString(), codeName))
                {
                    var form = new FormMain(row[5].ToString(), null, row[1].ToString());
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
        }

        private void showDBData()
        {
            LinkLabel[] files = new LinkLabel[100];
            int counter = 0;

            CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
            var data = codeTableTA.GetData();

            foreach (var row in data)
            {
                //MessageBox.Show(Convert.ToDateTime(row[3]).Day.ToString());
                files[counter] = new LinkLabel();
                files[counter].Size = new Size(200, 25);
                files[counter].Font = new Font("Microsoft Sans Serif", 10);
                files[counter].Text = row[5].ToString() + "     |     " + Convert.ToDateTime(row[3]).Day.ToString() + "." + Convert.ToDateTime(row[3]).Month.ToString() + "." + Convert.ToDateTime(row[3]).Year.ToString();
                files[counter].Location = new Point(10, counter * 30);
                files[counter].Click += new EventHandler(linkLabelFiles_Click);
                panelOptions.Controls.Add(files[counter]);
                counter++;
            }
        }

        private void radioButtonLoadDBFile_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLoadDBFile.Checked == true)
            {
                panelOptions.Controls.Clear();
                //MessageBox.Show(nr.ToString());
                showDBData();
            }
            else
            {
                panelOptions.Controls.Clear();
                Button buttonOpenFile = new Button();
                buttonOpenFile.Text = "Choose file";
                buttonOpenFile.Click += new EventHandler(buttonOpenFile_Click);
                panelOptions.Controls.Add(buttonOpenFile);
            }
        }
    }
}