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

namespace CodeForger
{
    public partial class FormMain : Form
    {
        string titleGlobal, contentsGlobal, pathGlobal, isExternalGlobal;

        string[,] openTabs = new string[100, 100];
        int tabsCounter = 0;

        private void toolStripButtonOpenFile_Click(object sender, EventArgs e)
        {
            var form1 = new FormOpenFileDialog();
            form1.Owner = this;
            form1.FormClosed += (sdr, args) =>
            {
                //MessageBox.Show(Properties.Settings.Default.OpenFileTitle);
                addTab(Properties.Settings.Default.OpenFileTitle, Properties.Settings.Default.OpenFilePath, Properties.Settings.Default.OpenFileContents, Properties.Settings.Default.OpenFileIsExternal);
            };
            form1.ShowDialog();
        }

        private void toolStripButtonNewFile_Click(object sender, EventArgs e)
        {
            addTab("Untitled*", null, null, null);
        }

        public FormMain(string title, string path, string contents, string isExternal)
        {
            InitializeComponent();
            titleGlobal = title;
            contentsGlobal = contents;
            pathGlobal = path;
            isExternalGlobal = isExternal;
        }

        private void richTextBoxCode_TextChanged(object sender, EventArgs e)
        {
            RichTextBox richTextBox = sender as RichTextBox;
            object tabPageAux = richTextBox.Parent;
            TabPage tabPage = tabPageAux as TabPage;
            char x = richTextBox.Name[11];
            if (openTabs[int.Parse(x.ToString()), 2] == "saved")
            {
                openTabs[int.Parse(x.ToString()), 2] = "unsaved";
                if (tabPage.Text[tabPage.Text.Length - 1] != '*')
                    tabPage.Text += "*";
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tabControlMain.SelectedTab;
            int tabIndex = tabControlMain.SelectedIndex;

            RichTextBox contentsTextbox = null;
            foreach (Control ctrl in tabPage.Controls)
            {
                if (ctrl.GetType() == typeof(RichTextBox))
                {
                    contentsTextbox = ctrl as RichTextBox;
                    break;
                }
            }

            if (openTabs[tabIndex, 4] == "1")
            {
                if (openTabs[tabIndex, 3] == "1")
                {
                    StreamWriter sw = new StreamWriter(pathGlobal);
                    sw.Write(contentsTextbox.Text);
                    sw.Close();
                }
                else
                {
                    CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
                    var data = codeTableTA.GetData();
                    data[int.Parse(pathGlobal)][1] = contentsTextbox.Text;
                    codeTableTA.Update(data);
                }
                openTabs[tabIndex, 2] = "saved";
                openTabs[tabIndex, 4] = "0";
                tabPage.Text = tabPage.Text.Remove(tabPage.Text.Length - 1, 1);
            }
            else
            {
                var form = new FormSaveFileDialog(contentsTextbox.Text);
                form.FormClosed += (sdr, args) =>
                {
                    //MessageBox.Show(Properties.Settings.Default.OpenFileTitle);
                    tabPage.Text = Properties.Settings.Default.OpenFileTitle;
                    openTabs[tabIndex, 0] = Properties.Settings.Default.OpenFileTitle;
                    openTabs[tabIndex, 1] = contentsTextbox.Text;
                    openTabs[tabIndex, 2] = "saved";
                    openTabs[tabIndex, 3] = Properties.Settings.Default.OpenFileIsExternal;
                    openTabs[tabIndex, 4] = "1";
                };
                form.ShowDialog();
            }

            //Refresh();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            int tabIndex = 0;
            foreach (TabPage tabPage in tabControlMain.Controls)
            {
                RichTextBox contentsTextbox = null;
                foreach (Control ctrl in tabPage.Controls)
                {
                    if (ctrl.GetType() == typeof(RichTextBox))
                    {
                        contentsTextbox = ctrl as RichTextBox;
                        break;
                    }
                }
                if (openTabs[tabIndex, 2] == "unsaved")
                {
                    DialogResult result = MessageBox.Show("Do you want to save " + tabPage.Text + "?", "Save File", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        if (openTabs[tabIndex, 4] == "1")
                        {
                            if (openTabs[tabIndex, 3] == "1")
                            {
                                StreamWriter sw = new StreamWriter(pathGlobal);
                                sw.Write(contentsTextbox.Text);
                                sw.Close();
                            }
                            else
                            {
                                CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
                                var data = codeTableTA.GetData();
                                data[int.Parse(pathGlobal)][1] = contentsTextbox.Text;
                                codeTableTA.Update(data);
                            }
                            openTabs[tabIndex, 2] = "saved";
                            tabPage.Text = tabPage.Text.Remove(tabPage.Text.Length - 1, 1);
                        }
                        else
                        {
                            var form = new FormSaveFileDialog(contentsTextbox.Text);
                            form.FormClosed += (sdr, args) =>
                            {
                                //MessageBox.Show(Properties.Settings.Default.OpenFileTitle);
                                tabPage.Text = Properties.Settings.Default.OpenFileTitle;
                                openTabs[tabIndex, 0] = Properties.Settings.Default.OpenFileTitle;
                                openTabs[tabIndex, 1] = contentsTextbox.Text;
                                openTabs[tabIndex, 2] = "saved";
                                openTabs[tabIndex, 3] = Properties.Settings.Default.OpenFileIsExternal;
                                openTabs[tabIndex, 4] = "1";
                            };
                            form.ShowDialog();
                        }
                    }
                }
            }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tabIndex = 0;
            foreach (TabPage tabPage in tabControlMain.Controls)
            {
                RichTextBox contentsTextbox = null;
                foreach (Control ctrl in tabPage.Controls)
                {
                    if (ctrl.GetType() == typeof(RichTextBox))
                    {
                        contentsTextbox = ctrl as RichTextBox;
                        break;
                    }
                }

                if (openTabs[tabIndex, 3] == "1")
                {
                    StreamWriter sw = new StreamWriter(pathGlobal);
                    sw.Write(contentsTextbox.Text);
                    sw.Close();
                }
                else
                {
                    CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
                    var data = codeTableTA.GetData();
                    data[int.Parse(pathGlobal)][1] = contentsTextbox.Text;
                    codeTableTA.Update(data);
                }
                openTabs[tabIndex, 2] = "saved";
                tabPage.Text = tabPage.Text.Remove(tabPage.Text.Length - 1, 1);
                tabIndex++;
            }
        }

        private void addTab(string title, string path, string contents, string isexternal)
        {
            TabPage tabPage = new TabPage();

            tabPage.Text = title;
            tabPage.BackColor = Color.White;

            RichTextBox textBox = new RichTextBox();
            textBox.Text = contents;
            textBox.Name = "richTextBox" + tabsCounter;
            textBox.TextChanged += new EventHandler(richTextBoxCode_TextChanged);
            textBox.Dock = DockStyle.Fill;
            //tabPage.Font = new Font("Segoe UI", 10);

            tabPage.Controls.Add(textBox);

            GroupBox groupBox = new GroupBox();
            groupBox.Name = "groupBox" + tabsCounter;
            groupBox.BackColor = Color.White;
            groupBox.Text = "Error list";
            groupBox.Dock = DockStyle.Bottom;

            tabPage.Controls.Add(groupBox);

            openTabs[tabsCounter, 0] = title;
            openTabs[tabsCounter, 1] = contents;
            if (title == "Untitled*")
                openTabs[tabsCounter, 2] = "unsaved";
            else
                openTabs[tabsCounter, 2] = "saved";
            openTabs[tabsCounter, 3] = isexternal;

            if (title == "Untitled*")
                openTabs[tabsCounter, 4] = "0";
            else
                openTabs[tabsCounter, 4] = "1";

            tabControlMain.Controls.Add(tabPage);

            tabControlMain.SelectedTab = tabPage;

            //Refresh();

            tabsCounter++;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Titlu: " + titleGlobal + "\nContents: " + contentsGlobal);
            addTab(titleGlobal, pathGlobal, contentsGlobal, isExternalGlobal);
        }
    }
}
