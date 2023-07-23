using CodeForger.CodeForgerDBDataSetTableAdapters;
//using DefineIMyInterface;
using FastColoredTextBoxNS;
using AutocompleteMenuNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutocompleteMenu = AutocompleteMenuNS.AutocompleteMenu;
using System.Runtime.Remoting.Channels;
using CodeForger.Properties;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms.VisualStyles;
using extensions;

namespace CodeForger
{
    public partial class FormMain : Form
    {
        string titleGlobal, contentsGlobal, pathGlobal, isExternalGlobal, typeGlobal;

        string[,] openTabs = new string[100, 100];
        int tabsCounter = 0;

        private void toolStripButtonOpenFile_Click(object sender, EventArgs e)
        {
            var form1 = new FormOpenFileDialog();
            form1.Owner = this;
            form1.FormClosed += (sdr, args) =>
            {
                //MessageBox.Show(Properties.Settings.Default.OpenFileTitle);
                addTab(Properties.Settings.Default.OpenFileTitle, Properties.Settings.Default.OpenFilePath, Properties.Settings.Default.OpenFileContents, Properties.Settings.Default.OpenFileIsExternal, Properties.Settings.Default.OpenFileType);
                tabControlMain.Invalidate();
            };
            form1.ShowDialog();
        }

        private void toolStripButtonNewFile_Click(object sender, EventArgs e)
        {
            addTab("Untitled*", null, null, null, null);
            tabControlMain.Invalidate();
        }

        public FormMain(string title, string path, string contents, string isExternal, string type)
        {
            InitializeComponent();
            titleGlobal = title;
            contentsGlobal = contents;
            pathGlobal = path;
            isExternalGlobal = isExternal;
            typeGlobal = type;
        }

        Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);

        private void richTextBoxCode_TextChanged(object sender, EventArgs e)
        {
            FastColoredTextBox richTextBox = sender as FastColoredTextBox;
            object tabPageAux = richTextBox.Parent;
            TabPage tabPage = tabPageAux as TabPage;

            foreach (Control ctrl in tabPage.Controls)
                if (ctrl.GetType() == typeof(StatusBar))
                {
                    StatusBar statusBar = ctrl as StatusBar;
                    var pnls = statusBar.Panels;
                    var pnl = pnls[2];
                    var place = richTextBox.Selection.Start;
                    pnl.Text = "Lin " + (place.iLine + 1) + ", Col " + (place.iChar + 1);
                }

            char x = richTextBox.Name[11];
            if (openTabs[int.Parse(x.ToString()), 2] == "saved")
            {
                openTabs[int.Parse(x.ToString()), 2] = "unsaved";
                if (tabPage.Text[tabPage.Text.Length - 1] != '*')
                    tabPage.Text += "*";
                tabPage.Refresh();
                tabControlMain.Invalidate();
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
                MessageBox.Show(ctrl.GetType().ToString());
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
                tabControlMain.Invalidate();
                tabPage.Text = tabPage.Text.Remove(tabPage.Text.Length - 1, 1);
            }
            else
            {
                var form = new FormSaveFileDialog(contentsTextbox.Text);
                form.FormClosed += (sdr, args) =>
                {
                    //MessageBox.Show(Properties.Settings.Default.OpenFileTitle);
                    if (Properties.Settings.Default.OpenFileTitle != "null")
                    {
                        tabPage.Text = Properties.Settings.Default.OpenFileTitle;
                        openTabs[tabIndex, 0] = Properties.Settings.Default.OpenFileTitle;
                        openTabs[tabIndex, 1] = contentsTextbox.Text;
                        openTabs[tabIndex, 2] = "saved";
                        openTabs[tabIndex, 3] = Properties.Settings.Default.OpenFileIsExternal + Properties.Settings.Default.OpenFilePath;
                        openTabs[tabIndex, 4] = "1";
                        openTabs[tabIndex, 5] = Properties.Settings.Default.OpenFileType;
                        tabControlMain.Invalidate();
                    }
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
            tabControlMain.Invalidate();
            //Refresh();
        }

        private void askToSaveFile(TabPage tabPage, int tabIndex, FastColoredTextBox contentsTextbox)
        {
            DialogResult result = MessageBox.Show("Do you want to save " + tabPage.Text + "?", "Save File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                if (openTabs[tabIndex, 4] == "1")
                {
                    if (openTabs[tabIndex, 3][0] == '1')
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

        private void deleteTempFiles()
        {
            // Delete all files in a directory    
            string[] files = Directory.GetFiles(Path.GetFullPath(Path.Combine(System.Windows.Forms.Application.StartupPath, @"..\..\tmp\")));
            foreach (string file in files)
                File.Delete(file);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            deleteTempFiles();

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
                    askToSaveFile(tabPage, tabIndex, contentsTextbox);
                }
                tabIndex++;
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
                    tabControlMain.Invalidate();
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
                        tabControlMain.Invalidate();
                    };
                    form.ShowDialog();
                }
                openTabs[tabIndex, 2] = "saved";
                tabControlMain.Invalidate();
                tabIndex++;
            }
        }

        public static string[,] RemoveRow(string[,] matrix, int rowToRemove)
        {
            int numRows = matrix.GetLength(0);
            int numCols = matrix.GetLength(1);

            // Create a new matrix with one fewer row
            string[,] newMatrix = new string[numRows - 1, numCols];

            // Copy the rows before the removed row
            for (int i = 0; i < rowToRemove; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }

            // Copy the rows after the removed row
            for (int i = rowToRemove + 1; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    newMatrix[i - 1, j] = matrix[i, j];
                }
            }

            return newMatrix;
        }

        private void showOpenTabs()
        {
            string afish = null;
            int numRows = openTabs.GetLength(0);
            int numCols = openTabs.GetLength(1);
            for (int j = 0; j < numRows; j++)
            {
                for (int k = 0; k < numCols; k++)
                    afish += openTabs[j, k] + " ";
                afish += "\n";
            }
            MessageBox.Show(afish);
        }

        private void tabControlMain_MouseDown(object sender, MouseEventArgs e)
        {
            for (var i = 0; i < this.tabControlMain.TabPages.Count; i++)
            {
                var tabRect = this.tabControlMain.GetTabRect(i);
                tabRect.Inflate(-2, -2);
                var closeImage = Resources.DeleteButton_Image;
                var imageRect = new Rectangle(
                    (tabRect.Right - closeImage.Width),
                    tabRect.Top + (tabRect.Height - closeImage.Height) / 2,
                    closeImage.Width,
                    closeImage.Height);
                if (imageRect.Contains(e.Location))
                {
                    if (openTabs[i, 2] == "unsaved")
                    {
                        //TODO show dialog message to save the file or not
                        TabPage tbpg = tabControlMain.TabPages[i];
                        FastColoredTextBox fst = tbpg.Controls.OfType<FastColoredTextBox>().FirstOrDefault();
                        askToSaveFile(tbpg, i, fst);
                    }
                    this.tabControlMain.TabPages.RemoveAt(i);
                    openTabs = RemoveRow(openTabs, i);
                    tabsCounter--;
                    break;
                }
            }
            tabControlMain.Invalidate();
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
        int current = 0;

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
            if (tkn.type == "paren" && tkn.value == "(")
            {
                tkn = tokens[++current];
                node expression;
                expression.type = "CallExpression";
                expression.value = tkn.value;
                expression.par = null;
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

        private bool IsValidPath(string path)
        {
            return path != null && path != "";
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
            }
            return null;
        }

        private void toolStripButtonRun_Click(object sender, EventArgs e)
        {
            //tokenizer("(add 8 (sub 8 6))");
            //foreach (var token in tokens)
            //MessageBox.Show("Type: " + token.type + "\nValue: " + token.value);

            int tab = tabControlMain.SelectedIndex;

            string path;
            if (openTabs[tab, 3][0] == '0')
            {
                string extension = parseFileTypeName(openTabs[tab, 5]);
                path = Path.GetFullPath(Path.Combine(System.Windows.Forms.Application.StartupPath, @"..\..\tmp\" + titleGlobal + extension));
            }
            else
            {
                path = openTabs[tab, 3].ToString();
                path = path.Substring(1).Trim();
            }

            //MessageBox.Show(path);

            if (!IsValidPath(path))
            {
                MessageBox.Show("Invalid file path or file doesn't exist yet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string folderPath = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);

            if (openTabs[tab, 2] == "unsaved")
            {
                var result = MessageBox.Show("Do you want to save the file before compiling?", "Unsaved File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    saveFileToolStripMenuItem_Click(null, null);
                else if (result == DialogResult.Cancel)
                    return;
            }

            if (openTabs[tab, 5] != "C" && openTabs[tab, 5] != "Brainfuck")
            {
                MessageBox.Show(openTabs[tab, 5]);
                MessageBox.Show("For now, only .c and .bf files are supported for compiling", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //string path = Path.GetDirectoryName(Application.ExecutablePath);
            string compilerPath;

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            if (openTabs[tab, 5] == "C")
            {
                compilerPath = Path.GetFullPath(Path.Combine(System.Windows.Forms.Application.StartupPath, @"..\..\compilers\c"));
                startInfo.Arguments = "/c c4 " + path + " & pause";
            }
            else if (openTabs[tab, 5] == "Brainfuck")
            {
                if (!File.Exists(folderPath + @"\" + name + ".exe"))
                {
                    var result = MessageBox.Show("The file has not been built yet, do you want to build it now?", "Run", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                        toolStripButtonBuildRun_Click(null, null);
                    return;
                }
                compilerPath = Path.GetFullPath(Path.Combine(System.Windows.Forms.Application.StartupPath, @"..\..\compilers\brainfuck\bfcc"));
                startInfo.Arguments = "/c " + folderPath + @"\" + name + " & pause";
            }
            else
                throw new Exception("Invalid file type");
            //startInfo.Arguments = "/c " + path + " & pause";
            startInfo.WorkingDirectory = compilerPath;

            //MessageBox.Show(Application.StartupPath);

            Process.Start(startInfo);

            Label lbl = this.Controls.Find("outputLabel" + tab, true)[0] as Label;
            lbl.Text += "\nRun started      at      " + DateTime.Now.ToString("HH:mm:ss");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormAbout();
            form.ShowDialog();
        }

        private void updateLayout()
        {
            PrefsTableTableAdapter prefsTableTA = new PrefsTableTableAdapter();
            var data = prefsTableTA.GetData();

            foreach (var row in data)
            {
                if (int.Parse(row[7].ToString()) == Properties.Settings.Default.AccountLogin)
                {
                    if (int.Parse(row[2].ToString()) == 1)
                    {
                        foreach (TabPage tab in tabControlMain.TabPages)
                            foreach (Control control in tab.Controls)
                                if (control.GetType() == typeof(FastColoredTextBox))
                                {
                                    FastColoredTextBox txt = control as FastColoredTextBox;
                                    txt.WordWrap = true;
                                }
                    }
                    else
                    {
                        foreach (TabPage tab in tabControlMain.TabPages)
                            foreach (Control control in tab.Controls)
                                if (control.GetType() == typeof(FastColoredTextBox))
                                {
                                    FastColoredTextBox txt = control as FastColoredTextBox;
                                    txt.WordWrap = false;
                                }
                    }

                    if (int.Parse(row[6].ToString()) == 0)
                        toolStripOptions.Visible = false;
                    else
                        toolStripOptions.Visible = true;
                }
            }
        }

        private void generalOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormSettings(0);
            form.FormClosed += (sdr, args) =>
            {
                updateLayout();
            };
            form.ShowDialog();
        }

        private void manageDatabaseFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormSettings(1);
            form.ShowDialog();
        }

        private void releaseNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/VladM7/CodeForger/releases");
        }

        private void toolStripButtonFormat_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tabControlMain.SelectedTab;
            foreach (Control control in tabPage.Controls)
                if (control.GetType() == typeof(FastColoredTextBox))
                {
                    FastColoredTextBox fastColoredTextBox = control as FastColoredTextBox;
                    fastColoredTextBox.DoAutoIndent();
                }
        }

        private void closeAllButCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControlMain.TabPages)
            {
                int index = tabControlMain.TabPages.IndexOf(tab);
                if (index != tabControlMain.SelectedIndex)
                    closetab(index);
            }
        }

        private void tabPage_TextChanged(object sender, EventArgs e)
        {
            TabPage tabPage = sender as TabPage;
            tabPage.Refresh();
            tabControlMain.Invalidate();
        }

        private void richTextBox_ZoomChanged(object sender, EventArgs e)
        {
            TabPage currentTabPage = tabControlMain.SelectedTab;
            FastColoredTextBox fastColoredTextBox = sender as FastColoredTextBox;

            int zoom = fastColoredTextBox.Zoom;

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

        private void createErrorsDataGridView(TabPage tabPage)
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;

            dataGridView.BackgroundColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.GridColor = Color.White;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView.ReadOnly = true;

            // dataGridView.CellDoubleClick += dataGridView_CellDoubleClick;

            dataGridView.ColumnCount = 4;
            dataGridView.Columns[0].Name = "Type";
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[1].Name = "Description";
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[2].Name = "File";
            dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[3].Name = "Line";
            dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            tabPage.Controls.Add(dataGridView);
        }

        private void addTab(string title, string path, string contents, string isexternal, string type)
        {
            TabPage tabPage = new TabPage();

            tabPage.Text = title;
            tabPage.BackColor = Color.White;
            tabPage.TextChanged += tabPage_TextChanged;

            FastColoredTextBox textBox = new FastColoredTextBox();
            textBox.Name = "richTextBox" + tabsCounter;
            textBox.Dock = DockStyle.Fill;
            textBox.Font = new Font("Consolas", 14);
            textBox.CommentPrefix = "//";
            textBox.WordWrap = true;
            textBox.AddStyle(GreenStyle);
            textBox.SelectionChanged += TextBox_SelectionChanged;
            textBox.AutoIndent = true;
            textBox.Language = Language.CSharp;
            textBox.AutoCompleteBrackets = true;
            textBox.BackColor = Color.Transparent;
            textBox.BackgroundImage = Properties.Resources.White1;
            textBox.BackgroundImageLayout = ImageLayout.Stretch;
            //textBox.A;
            //textBox.Add;
            AutocompleteMenu autocompleteMenuC = new AutocompleteMenu();
            autocompleteMenuC.Items = new string[]
            {
"auto", "double", "int", "struct", "break", "else", "long", "switch", "case", "enum", "register", "typedef", "char", "extern", "return", "union", "continue", "for", "signed", "void", "do", "if", "static", "while", "default", "goto", "sizeof", "volatile", "const", "float", "short", "unsigned"
            };

            Colors clrs = new Colors();
            clrs.HighlightingColor = Color.FromArgb(101, 98, 252);
            clrs.SelectedBackColor = Color.FromArgb(101, 98, 252);
            autocompleteMenuC.Colors = clrs;

            autocompleteMenuC.Show(textBox, false);

            ContextMenuStrip ctx = new ContextMenuStrip();
            ctx.Items.Add(new ToolStripMenuItem("Cut", Resources.cut_icon, cutToolStripMenuItem_Click));
            ctx.Items.Add(new ToolStripMenuItem("Copy", Resources.copy_icon, copyToolStripMenuItem_Click));
            ctx.Items.Add(new ToolStripMenuItem("Paste", Resources.paste_icon, pasteToolStripMenuItem_Click));
            ctx.Items.Add(new ToolStripSeparator());
            ctx.Items.Add(new ToolStripMenuItem("Format", Resources.format_icon, toolStripButtonFormat_Click));
            textBox.ContextMenuStrip = ctx;

            tabPage.Controls.Add(textBox);

            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Bottom;
            tabControl.Height = 150;

            TabPage tabPageErrorList = new TabPage();
            tabPageErrorList.Text = "Error List";
            tabPageErrorList.BackColor = Color.White;
            createErrorsDataGridView(tabPageErrorList);

            TabPage tabPageOutput = new TabPage();
            tabPageOutput.Text = "Output";
            tabPageOutput.BackColor = Color.White;

            Label outputLabel = new Label();
            outputLabel.Dock = DockStyle.Fill;
            outputLabel.Text = "==============================";
            outputLabel.Name = "outputLabel" + tabsCounter;
            tabPageOutput.Controls.Add(outputLabel);

            tabControl.TabPages.Add(tabPageOutput);
            tabControl.TabPages.Add(tabPageErrorList);
            tabPage.Controls.Add(tabControl);

            /*
            GroupBox groupBox = new GroupBox();
            groupBox.Name = "groupBox" + tabsCounter;
            groupBox.BackColor = Color.White;
            groupBox.Text = "Error list";
            groupBox.Dock = DockStyle.Bottom;

            tabPage.Controls.Add(groupBox);
            */

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
            languagePanel.Text = type;
            //languagePanel.AutoSize= StatusBarPanelAutoSize.Spring;
            //statusPanel.Alignment = HorizontalAlignment.Left;
            lineColPanel.Text = "Lin ?, Col ?";
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
            openTabs[tabsCounter, 5] = type;

            textBox.ZoomChanged += new EventHandler(richTextBox_ZoomChanged);
            textBox.TextChanged += new EventHandler<TextChangedEventArgs>(richTextBoxCode_TextChanged);
            textBox.Text = contents;
            openTabs[tabsCounter, 2] = "saved";
            tabControlMain.Invalidate();

            tabControlMain.Controls.Add(tabPage);
            //tabControlMain.TabPages.Insert(tabControlMain.TabCount - 1, tabPage);

            tabControlMain.SelectedTab = tabPage;

            //Refresh();

            tabControlMain.Invalidate();

            tabsCounter++;
        }

        private void toolStripButtonResetZoom_Click(object sender, EventArgs e)
        {
            TabPage currentTabPage = tabControlMain.SelectedTab;
            int zoom = 100;

            foreach (Control ctrl in currentTabPage.Controls)
            {
                if (ctrl.GetType() == typeof(FastColoredTextBox))
                {
                    FastColoredTextBox textBox = ctrl as FastColoredTextBox;
                    textBox.Zoom = zoom;
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

        private void TextBox_SelectionChanged(object sender, EventArgs e)
        {
            FastColoredTextBox richTextBox = sender as FastColoredTextBox;
            object tabPageAux = richTextBox.Parent;
            TabPage tabPage = tabPageAux as TabPage;

            foreach (Control ctrl in tabPage.Controls)
                if (ctrl.GetType() == typeof(StatusBar))
                {
                    StatusBar statusBar = ctrl as StatusBar;
                    var pnls = statusBar.Panels;
                    var pnl = pnls[2];
                    var place = richTextBox.Selection.Start;
                    pnl.Text = "Lin " + (place.iLine + 1) + ", Col " + (place.iChar + 1);
                }
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.AccountLogin = -1;
            toolStripMenuItemUser.Text = "(user)";
            toolStripMenuItemUser.Enabled = false;
        }

        /*
        private void tabControlMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = this.tabControlMain.TabPages[e.Index];
            var tabRect = this.tabControlMain.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);
            var closeImage = Properties.Resources.DeleteButton_Image;
            e.Graphics.DrawImage(closeImage,
                (tabRect.Right - closeImage.Width),
                tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                tabRect, tabPage.ForeColor, TextFormatFlags.Left);
        }
        */

        private void tabControlMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = this.tabControlMain.TabPages[e.Index];
            var tabRect = this.tabControlMain.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);
            var closeImage = Properties.Resources.DeleteButton_Image;
            e.Graphics.DrawImage(closeImage,
                (tabRect.Right - closeImage.Width),
                tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
            if (openTabs[e.Index, 2] == "unsaved" && openTabs[e.Index, 0] != "Untitled*")
                TextRenderer.DrawText(e.Graphics, openTabs[e.Index, 0] + "*", tabPage.Font,
                tabRect, tabPage.ForeColor, TextFormatFlags.Left);
            else
                TextRenderer.DrawText(e.Graphics, openTabs[e.Index, 0], tabPage.Font,
                    tabRect, tabPage.ForeColor, TextFormatFlags.Left);
        }

        private void toolStripButtonBuildRun_Click(object sender, EventArgs e)
        {
            int tab = tabControlMain.SelectedIndex;

            string path = openTabs[tab, 3].ToString();
            path = path.Substring(1).Trim();

            //MessageBox.Show(path);

            if (!IsValidPath(path))
            {
                MessageBox.Show("Invalid file path or file doesn't exist yet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string folderPath = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);

            if (openTabs[tab, 2] == "unsaved")
            {
                var result = MessageBox.Show("Do you want to save the file before compiling?", "Unsaved File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    saveFileToolStripMenuItem_Click(null, null);
                else if (result == DialogResult.Cancel)
                    return;
            }

            if (openTabs[tab, 5] != "C" && openTabs[tab, 5] != "Brainfuck")
            {
                MessageBox.Show(openTabs[tab, 5]);
                MessageBox.Show("For now, only .c and .bf files are supported for compiling", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //string path = Path.GetDirectoryName(Application.ExecutablePath);
            string compilerPath;

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            if (openTabs[tab, 5] == "C")
            {
                compilerPath = Path.GetFullPath(Path.Combine(System.Windows.Forms.Application.StartupPath, @"..\..\compilers\c"));
                startInfo.Arguments = "/c c4 " + path + " & pause";
            }
            else if (openTabs[tab, 5] == "Brainfuck")
            {
                compilerPath = Path.GetFullPath(Path.Combine(System.Windows.Forms.Application.StartupPath, @"..\..\compilers\brainfuck\bfcc"));
                startInfo.Arguments = "/c bfcc -backend=c -run " + path + " " + folderPath + @"\" + name + " & pause";
            }
            else
                throw new Exception("Invalid file type");
            //startInfo.Arguments = "/c " + path + " & pause";
            startInfo.WorkingDirectory = compilerPath;

            //MessageBox.Show(Application.StartupPath);

            Process.Start(startInfo);

            Label lbl = this.Controls.Find("outputLabel" + tab, true)[0] as Label;
            lbl.Text += "\nBuild started        at      " + DateTime.Now.ToString("HH:mm:ss");
            lbl.Text += "\nRun started      at      " + DateTime.Now.ToString("HH:mm:ss");
        }

        private void toolStripButtonBuild_Click(object sender, EventArgs e)
        {
            int tab = tabControlMain.SelectedIndex;

            string path = openTabs[tab, 3].ToString();
            path = path.Substring(1).Trim();

            //MessageBox.Show(path);

            if (!IsValidPath(path))
            {
                MessageBox.Show("Invalid file path or file doesn't exist yet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string folderPath = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);

            if (openTabs[tab, 2] == "unsaved")
            {
                var result = MessageBox.Show("Do you want to save the file before compiling?", "Unsaved File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    saveFileToolStripMenuItem_Click(null, null);
                else if (result == DialogResult.Cancel)
                    return;
            }

            if (openTabs[tab, 5] != "C" && openTabs[tab, 5] != "Brainfuck")
            {
                MessageBox.Show(openTabs[tab, 5]);
                MessageBox.Show("For now, only .c and .bf files are supported for compiling", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //string path = Path.GetDirectoryName(Application.ExecutablePath);
            string compilerPath;

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            if (openTabs[tab, 5] == "C")
            {
                compilerPath = Path.GetFullPath(Path.Combine(System.Windows.Forms.Application.StartupPath, @"..\..\compilers\c"));
                startInfo.Arguments = "/c c4 " + path + " & pause";
            }
            else if (openTabs[tab, 5] == "Brainfuck")
            {
                compilerPath = Path.GetFullPath(Path.Combine(System.Windows.Forms.Application.StartupPath, @"..\..\compilers\brainfuck\bfcc"));
                startInfo.Arguments = "/c bfcc -backend=c " + path + " " + folderPath + @"\" + name;
            }
            else
                throw new Exception("Invalid file type");
            //startInfo.Arguments = "/c " + path + " & pause";
            startInfo.WorkingDirectory = compilerPath;

            //MessageBox.Show(Application.StartupPath);

            Process.Start(startInfo);

            Label lbl = this.Controls.Find("outputLabel" + tab, true)[0] as Label;
            lbl.Text += "\nBuild started        at      " + DateTime.Now.ToString("HH:mm:ss");
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(null, null);
        }

        private void saveAllFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAllToolStripMenuItem_Click(null, null);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonNewFile_Click(null, null);
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonOpenFile_Click(null, null);
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonFind_Click(null, null);
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonReplace_Click(null, null);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonUndo_Click(null, null);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonRedo_Click(null, null);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tabControlMain.SelectedTab;
            FastColoredTextBox fst = tabPage.Controls.OfType<FastColoredTextBox>().FirstOrDefault();
            fst.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tabControlMain.SelectedTab;
            FastColoredTextBox fst = tabPage.Controls.OfType<FastColoredTextBox>().FirstOrDefault();
            Clipboard.SetText(fst.SelectedText);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tabControlMain.SelectedTab;
            FastColoredTextBox fst = tabPage.Controls.OfType<FastColoredTextBox>().FirstOrDefault();
            fst.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tabControlMain.SelectedTab;
            FastColoredTextBox fst = tabPage.Controls.OfType<FastColoredTextBox>().FirstOrDefault();
            fst.Paste();
        }

        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormAccountSettings();
            form.ShowDialog();
        }

        private void closetab(int tabIndex)
        {
            if (openTabs[tabIndex, 2] == "unsaved")
            {
                TabPage tbpg = tabControlMain.TabPages[tabIndex];
                FastColoredTextBox fst = tbpg.Controls.OfType<FastColoredTextBox>().FirstOrDefault();
                askToSaveFile(tbpg, tabIndex, fst);
            }
            this.tabControlMain.TabPages.RemoveAt(tabIndex);
            openTabs = RemoveRow(openTabs, tabIndex);
            tabsCounter--;
        }

        private void closeAllTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControlMain.TabPages)
            {
                int index = tabControlMain.TabPages.IndexOf(tab);
                closetab(index);
            }
        }

        private void closeCurrentTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closetab(tabControlMain.SelectedIndex);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tabControlMain.SelectedTab;
            FastColoredTextBox fst = tabPage.Controls.OfType<FastColoredTextBox>().FirstOrDefault();
            fst.SelectedText = "";
        }

        private void findAccount()
        {
            //MessageBox.Show(Properties.Settings.Default.AccountLogin.ToString());
            if (Properties.Settings.Default.AccountLogin == -1)
            {
                toolStripMenuItemUser.Text = "(user)";
                toolStripMenuItemUser.Enabled = false;
                return;
            }

            UsersTableTableAdapter usersTA = new UsersTableTableAdapter();
            var data = usersTA.GetData();
            foreach (var row in data)
            {
                if (int.Parse(row[0].ToString()) == Settings.Default.AccountLogin)
                {
                    toolStripMenuItemUser.Text = row[1].ToString();
                    return;
                }
            }
        }

        private void resize()
        {
            foreach (ToolStripItem toolStripItem in toolStripOptions.Items)
            {
                toolStripItem.AutoSize = false;
                //toolStripItem.Width =;
                toolStripItem.Height = 30;
                toolStripItem.Width = (int)(0.775 * toolStripItem.Width);
                toolStripItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }

            toolStripButtonBuildRun.Width = (int)(1.125 * toolStripButtonBuildRun.Width);

            toolStripMenuItemUser.AutoSize = false;
            toolStripMenuItemUser.Height = 22;
            toolStripMenuItemUser.Width = (int)(0.70 * toolStripMenuItemUser.Width);

            /*
            saveAllToolStripMenuItem.AutoSize = false;
            saveAllToolStripMenuItem.Height = 30;
            saveAllToolStripMenuItem.Width = (int)(0.775 * saveAllToolStripMenuItem.Width);
            saveAllToolStripMenuItem.TextAlign = ContentAlignment.MiddleCenter;

            saveToolStripMenuItem.AutoSize = false;
            saveToolStripMenuItem.Height = 30;
            saveToolStripMenuItem.Width = (int)(0.775 * saveToolStripMenuItem.Width);
            saveToolStripMenuItem.TextAlign = ContentAlignment.MiddleCenter;
            */
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Focus();

            updateLayout();

            if (Settings.Default.AccountLogin == -1)
            {
                accountSettingsToolStripMenuItem.Enabled = false;
            }

            this.tabControlMain.Padding = new Point(12, 4);
            //this.tabControlMain.DrawMode = TabDrawMode.OwnerDrawFixed;
            //this.tabControlMain.TabPages[this.tabControlMain.TabCount - 1].Text = "";
            this.tabControlMain.SizeMode = TabSizeMode.Fixed;
            //this.tabControlMain.Selecting += tabControlMain_Selecting;
            //tabControlMain.HandleCreated += tabControlMain_HandleCreated;

            //this.tabControlMain.DrawItem += tabControlMain_DrawItem;
            //MessageBox.Show("Title: " + titleGlobal + "\nContents: " + contentsGlobal);
            //MessageBox.Show(tabControlMain.TabCount.ToString());

            //resize();
            findAccount();
            addTab(titleGlobal, pathGlobal, contentsGlobal, isExternalGlobal, typeGlobal);

            //toolStripMenuItemUser.Image = new Bitmap(Resources.user_icon, new Size(16, 16));
        }
    }
}

/*
 * TODO
 * -saving files in DB is buggy
 * -closing an unsaved tab/file prompt
 * -compiling a file saved in DB
 * -set compiler path in settings
 * -syntax highlighting doesn't work after opening a file
 * -output tab
 * -error list tab
 * -compilers
 * -menuStrip functions (Go To.., Fullscreen, etc)
 */

namespace extensions
{
    public static class extension
    {
        public static void AddContextMenu(this RichTextBox rtb)
        {
            if (rtb.ContextMenuStrip == null)
            {
                ContextMenuStrip cms = new ContextMenuStrip()
                {
                    ShowImageMargin = false
                };

                ToolStripMenuItem tsmiUndo = new ToolStripMenuItem("Undo");
                tsmiUndo.Click += (sender, e) => rtb.Undo();
                cms.Items.Add(tsmiUndo);

                ToolStripMenuItem tsmiRedo = new ToolStripMenuItem("Redo");
                tsmiRedo.Click += (sender, e) => rtb.Redo();
                cms.Items.Add(tsmiRedo);

                cms.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem tsmiCut = new ToolStripMenuItem("Cut");
                tsmiCut.Click += (sender, e) => rtb.Cut();
                cms.Items.Add(tsmiCut);

                ToolStripMenuItem tsmiCopy = new ToolStripMenuItem("Copy");
                tsmiCopy.Click += (sender, e) => rtb.Copy();
                cms.Items.Add(tsmiCopy);

                ToolStripMenuItem tsmiPaste = new ToolStripMenuItem("Paste");
                tsmiPaste.Click += (sender, e) => rtb.Paste();
                cms.Items.Add(tsmiPaste);

                ToolStripMenuItem tsmiDelete = new ToolStripMenuItem("Delete");
                tsmiDelete.Click += (sender, e) => rtb.SelectedText = "";
                cms.Items.Add(tsmiDelete);

                cms.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem tsmiSelectAll = new ToolStripMenuItem("Select All");
                tsmiSelectAll.Click += (sender, e) => rtb.SelectAll();
                cms.Items.Add(tsmiSelectAll);

                cms.Opening += (sender, e) =>
                {
                    tsmiUndo.Enabled = !rtb.ReadOnly && rtb.CanUndo;
                    tsmiRedo.Enabled = !rtb.ReadOnly && rtb.CanRedo;
                    tsmiCut.Enabled = !rtb.ReadOnly && rtb.SelectionLength > 0;
                    tsmiCopy.Enabled = rtb.SelectionLength > 0;
                    tsmiPaste.Enabled = !rtb.ReadOnly && Clipboard.ContainsText();
                    tsmiDelete.Enabled = !rtb.ReadOnly && rtb.SelectionLength > 0;
                    tsmiSelectAll.Enabled = rtb.TextLength > 0 && rtb.SelectionLength < rtb.TextLength;
                };

                rtb.ContextMenuStrip = cms;
            }
        }
    }
}