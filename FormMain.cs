using CodeForger.CodeForgerDBDataSetTableAdapters;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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

        Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);

        private void richTextBoxCode_TextChanged(object sender, EventArgs e)
        {
            FastColoredTextBox richTextBox = sender as FastColoredTextBox;
            object tabPageAux = richTextBox.Parent;
            TabPage tabPage = tabPageAux as TabPage;
            char x = richTextBox.Name[11];
            if (openTabs[int.Parse(x.ToString()), 2] == "saved")
            {
                openTabs[int.Parse(x.ToString()), 2] = "unsaved";
                if (tabPage.Text[tabPage.Text.Length - 1] != '*')
                    tabPage.Text += "*";
                //tabPage.Refresh();
            }

            TextChangedEventArgs er = e as TextChangedEventArgs;
            //clear style of changed range
            er.ChangedRange.ClearStyle(GreenStyle);
            //comment highlighting
            er.ChangedRange.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tabControlMain.SelectedTab;
            int tabIndex = tabControlMain.SelectedIndex;

            MessageBox.Show("Title: " + openTabs[tabIndex, 0] + "\nContents: " + openTabs[tabIndex, 1] + "\nSaved: " + openTabs[tabIndex, 2] + "\nPath(1) or DB(0): " + openTabs[tabIndex, 3] + "\nExists or not: " + openTabs[tabIndex, 4]);

            FastColoredTextBox contentsTextbox = null;
            foreach (Control ctrl in tabPage.Controls)
            {
                if (ctrl.GetType() == typeof(FastColoredTextBox))
                {
                    contentsTextbox = ctrl as FastColoredTextBox;
                    break;
                }
            }

            if (openTabs[tabIndex, 4] == "1")
            {
                if (openTabs[tabIndex, 3][0] == '1')
                {
                    string path = openTabs[tabIndex, 3];
                    path = path.Remove(0, 1);
                    StreamWriter sw = new StreamWriter(path);
                    sw.Write(contentsTextbox.Text);
                    sw.Close();
                }
                else
                {
                    CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
                    var data = codeTableTA.GetData();
                    data[int.Parse(pathGlobal)][1] = contentsTextbox.Text;
                    data[int.Parse(pathGlobal)][3] = DateTime.Now;
                    codeTableTA.Update(data);
                }
                openTabs[tabIndex, 2] = "saved";
                //openTabs[tabIndex, 4] = "0";
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
                    openTabs[tabIndex, 3] = Properties.Settings.Default.OpenFileIsExternal + Properties.Settings.Default.OpenFilePath;
                    openTabs[tabIndex, 4] = "1";
                };
                form.ShowDialog();
                foreach (Control ctrl in tabPage.Controls)
                {
                    if (ctrl.GetType() == typeof(StatusBar))
                    {
                        StatusBar statusBar = (StatusBar)ctrl;
                        foreach (StatusBarPanel panel in statusBar.Panels)
                            if (string.Equals(panel.Name, "filePanel"))
                            {
                                if (openTabs[tabIndex, 3][0] == '0')
                                    panel.Text = "DB/" + openTabs[tabIndex, 0];
                                else
                                    panel.Text = openTabs[tabIndex, 3].Remove(0, 1);
                            }
                    }
                }
            }
            openTabs[tabIndex, 2] = "saved";
            //Refresh();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            int tabIndex = 0;
            foreach (TabPage tabPage in tabControlMain.Controls)
            {
                FastColoredTextBox contentsTextbox = null;
                foreach (Control ctrl in tabPage.Controls)
                {
                    if (ctrl.GetType() == typeof(FastColoredTextBox))
                    {
                        contentsTextbox = ctrl as FastColoredTextBox;
                        break;
                    }
                }
                if (openTabs[tabIndex, 2] == "unsaved")
                {
                    DialogResult result = MessageBox.Show("Do you want to save " + tabPage.Text + "?", "Save File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
                FastColoredTextBox contentsTextbox = null;
                foreach (Control ctrl in tabPage.Controls)
                {
                    if (ctrl.GetType() == typeof(FastColoredTextBox))
                    {
                        contentsTextbox = ctrl as FastColoredTextBox;
                        break;
                    }
                }

                if (openTabs[tabIndex, 4] == "1")
                {
                    if (openTabs[tabIndex, 3][0] == '1')
                    {
                        string path = openTabs[tabIndex, 3];
                        path = path.Remove(0, 1);
                        StreamWriter sw = new StreamWriter(path);
                        sw.Write(contentsTextbox.Text);
                        sw.Close();
                    }
                    else
                    {
                        CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
                        var data = codeTableTA.GetData();
                        data[int.Parse(pathGlobal)][1] = contentsTextbox.Text;
                        data[int.Parse(pathGlobal)][3] = DateTime.Now;
                        codeTableTA.Update(data);
                    }
                    openTabs[tabIndex, 2] = "saved";
                    //openTabs[tabIndex, 4] = "0";
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
                        openTabs[tabIndex, 3] = Properties.Settings.Default.OpenFileIsExternal + Properties.Settings.Default.OpenFilePath;
                        openTabs[tabIndex, 4] = "1";
                    };
                    form.ShowDialog();
                }
                openTabs[tabIndex, 2] = "saved";
                tabIndex++;
            }
        }

        private void tabControlMain_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            TabPage currentTabPage = tabControlMain.SelectedTab;
            foreach (Control ctrl in currentTabPage.Controls)
                if (ctrl.GetType() == typeof(FastColoredTextBox))
                {
                    FastColoredTextBox textBox = ctrl as FastColoredTextBox;
                    textBox.Undo();
                }
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            TabPage currentTabPage = tabControlMain.SelectedTab;
            foreach (Control ctrl in currentTabPage.Controls)
                if (ctrl.GetType() == typeof(FastColoredTextBox))
                {
                    FastColoredTextBox textBox = ctrl as FastColoredTextBox;
                    textBox.Redo();
                }
        }

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            TabPage currentTabPage = tabControlMain.SelectedTab;
            int zoom = 100;

            foreach (Control ctrl in currentTabPage.Controls)
            {
                if (ctrl.GetType() == typeof(FastColoredTextBox))
                {
                    FastColoredTextBox textBox = ctrl as FastColoredTextBox;
                    textBox.Zoom += 10;
                    zoom = textBox.Zoom;
                }
            }
            foreach (Control ctrl in currentTabPage.Controls)
            {
                if (ctrl.GetType() == typeof(StatusBar))
                {
                    StatusBar statusBar = ctrl as StatusBar;
                    foreach (StatusBarPanel statusBarPanel in statusBar.Panels)
                    {
                        if (string.Equals(statusBarPanel.Name, "zoomPanel"))
                            statusBarPanel.Text = zoom.ToString() + "%";
                    }
                }
            }
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            TabPage currentTabPage = tabControlMain.SelectedTab;
            int zoom = 100;

            foreach (Control ctrl in currentTabPage.Controls)
            {
                if (ctrl.GetType() == typeof(FastColoredTextBox))
                {
                    FastColoredTextBox textBox = ctrl as FastColoredTextBox;
                    textBox.Zoom -= 10;
                    zoom = textBox.Zoom;
                }
            }
            foreach (Control ctrl in currentTabPage.Controls)
            {
                if (ctrl.GetType() == typeof(StatusBar))
                {
                    StatusBar statusBar = ctrl as StatusBar;
                    foreach (StatusBarPanel statusBarPanel in statusBar.Panels)
                    {
                        if (string.Equals(statusBarPanel.Name, "zoomPanel"))
                            statusBarPanel.Text = zoom.ToString() + "%";
                    }
                }
            }
        }

        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            TabPage currentTabPage = tabControlMain.SelectedTab;

            foreach (Control ctrl in currentTabPage.Controls)
            {
                if (ctrl.GetType() == typeof(FastColoredTextBox))
                {
                    FastColoredTextBox textBox = ctrl as FastColoredTextBox;
                    textBox.ShowFindDialog();
                }
            }
        }

        private void toolStripButtonReplace_Click(object sender, EventArgs e)
        {
            TabPage currentTabPage = tabControlMain.SelectedTab;

            foreach (Control ctrl in currentTabPage.Controls)
            {
                if (ctrl.GetType() == typeof(FastColoredTextBox))
                {
                    FastColoredTextBox textBox = ctrl as FastColoredTextBox;
                    textBox.ShowReplaceDialog();
                }
            }
        }

        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            var form = new FormAbout();
            form.ShowDialog();
        }

        struct token
        {
            public string type;
            public string value;
        };

        List<token> tokens = new List<token>();

        private void tokenizer(string input)
        {
            int n = input.Length;
            for (int i = 0; i < n; i++)
            {
                char c = input[i];

                if (c == '(' || c == ')')
                {
                    token aux = new token();
                    aux.type = "paren";
                    aux.value = c.ToString();
                    tokens.Add(aux);
                    continue;
                }
                if (c >= 'a' && c <= 'z')
                {
                    string value = "";
                    while (c >= 'a' && c <= 'z')
                    {
                        value += c;
                        if (i < n - 1)
                            c = input[++i];
                        else break;
                    }
                    i--;
                    token aux = new token();
                    aux.type = "name";
                    aux.value = value;
                    tokens.Add(aux);
                    continue;
                }
                if (c == ' ')
                    continue;
                if (c >= '0' && c <= '9')
                {
                    string value = "";
                    while (c >= '0' && c <= '9')
                    {
                        value += c;
                        if (i < n - 1)
                            c = input[++i];
                        else break;
                    }
                    i--;
                    token aux = new token();
                    aux.type = "number";
                    aux.value = value;
                    tokens.Add(aux);
                    continue;
                }
                //TODO throw error
            }
        }

        struct ast
        {
            public string type;
            public node[] body;
        }

        struct node
        {
            public string type;
            public string value;
            public node[] par;
        }

        ast structure;
        int current=0;

        private node Walk()
        {
            token tkn = tokens[current];
            if (tkn.type == "number")
            {
                current++;
                node aux;
                aux.type = "NumberLiteral";
                aux.value = tokens[current].value;
                aux.par = null;
                return aux;
            }
            if(tkn.type == "paren" && tkn.value == "(")
            {
                tkn = tokens[++current];
                node expression;
                expression.type = "CallExpression";
                expression.value = tkn.value;
                expression.par= null;
                tkn = tokens[current];
                while (tkn.value != ")")
                {
                    expression.par.Append(Walk());
                    tkn = tokens[current];
                }
                current++;
                return expression;
            }
            throw new Exception("Unknown token type");
        }

        private void parser()
        {
            structure.type = "Program";
            structure.body.Append(Walk());
        }

        private void toolStripButtonRun_Click(object sender, EventArgs e)
        {
            tokenizer("(add 8 (sub 8 6))");
            foreach (var token in tokens)
                MessageBox.Show("Type: " + token.type + "\nValue: " + token.value);
        }

        private void addTab(string title, string path, string contents, string isexternal)
        {
            TabPage tabPage = new TabPage();

            tabPage.Text = title;
            tabPage.BackColor = Color.White;

            FastColoredTextBox textBox = new FastColoredTextBox();
            textBox.Text = contents;
            textBox.Name = "richTextBox" + tabsCounter;
            textBox.TextChanged += new EventHandler<TextChangedEventArgs>(richTextBoxCode_TextChanged);
            textBox.Dock = DockStyle.Fill;
            textBox.Font = new Font("Consolas", 14);
            textBox.CommentPrefix = "//";
            textBox.WordWrap = true;
            textBox.AddStyle(GreenStyle);
            textBox.SelectionChanged += TextBox_SelectionChanged;

            tabPage.Controls.Add(textBox);

            GroupBox groupBox = new GroupBox();
            groupBox.Name = "groupBox" + tabsCounter;
            groupBox.BackColor = Color.White;
            groupBox.Text = "Error list";
            groupBox.Dock = DockStyle.Bottom;

            tabPage.Controls.Add(groupBox);

            StatusBar statusBar = new StatusBar();
            statusBar.Dock = DockStyle.Bottom;

            StatusBarPanel filePanel = new StatusBarPanel();
            StatusBarPanel languagePanel = new StatusBarPanel();
            StatusBarPanel lineColPanel = new StatusBarPanel();
            StatusBarPanel zoomPanel = new StatusBarPanel();
            StatusBarPanel linebreakPanel = new StatusBarPanel();
            StatusBarPanel encodingPanel = new StatusBarPanel();

            if (isexternal == "0")
                filePanel.Text = "DB/" + title;
            else
                filePanel.Text = path;
            filePanel.AutoSize = StatusBarPanelAutoSize.Spring;
            filePanel.Name = "filePanel";
            languagePanel.Text = "LISP";
            //languagePanel.AutoSize= StatusBarPanelAutoSize.Spring;
            //statusPanel.Alignment = HorizontalAlignment.Left;
            lineColPanel.Text = "Lin 2, Col 4";
            //lineColPanel.Alignment = HorizontalAlignment.Right;
            zoomPanel.Text = "100%";
            zoomPanel.Name = "zoomPanel";
            //zoomPanel.Alignment = HorizontalAlignment.Right;
            linebreakPanel.Text = "Windows CRLF";
            //linebreakPanel.Alignment = HorizontalAlignment.Right;
            encodingPanel.Text = "UTF-8";
            //encodingPanel.Alignment = HorizontalAlignment.Right;

            statusBar.Panels.Add(filePanel);
            statusBar.Panels.Add(languagePanel);
            statusBar.Panels.Add(lineColPanel);
            statusBar.Panels.Add(zoomPanel);
            statusBar.Panels.Add(linebreakPanel);
            statusBar.Panels.Add(encodingPanel);

            statusBar.ShowPanels = true;

            tabPage.Controls.Add(statusBar);

            openTabs[tabsCounter, 0] = title;
            openTabs[tabsCounter, 1] = contents;
            if (title == "Untitled*")
                openTabs[tabsCounter, 2] = "unsaved";
            else
                openTabs[tabsCounter, 2] = "saved";
            if (isexternal == "0")
                openTabs[tabsCounter, 3] = isexternal;
            else
                openTabs[tabsCounter, 3] = "1" + path;

            if (title == "Untitled*")
                openTabs[tabsCounter, 4] = "0";
            else
                openTabs[tabsCounter, 4] = "1";

            tabControlMain.Controls.Add(tabPage);
            //tabControlMain.TabPages.Insert(tabControlMain.TabCount - 1, tabPage);

            tabControlMain.SelectedTab = tabPage;

            //Refresh();

            tabsCounter++;
        }

        private void TextBox_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.tabControlMain.Padding = new Point(12, 4);
            //this.tabControlMain.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControlMain.SizeMode = TabSizeMode.Fixed;

            //this.tabControlMain.DrawItem += tabControlMain_DrawItem;
            //MessageBox.Show("Titlu: " + titleGlobal + "\nContents: " + contentsGlobal);
            //MessageBox.Show(tabControlMain.TabCount.ToString());
            addTab(titleGlobal, pathGlobal, contentsGlobal, isExternalGlobal);
        }
    }
}
