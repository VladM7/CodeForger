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
    public partial class FormSettings : Form
    {
        private int selTabGlobal;

        public FormSettings(int selTab)
        {
            InitializeComponent();
            selTabGlobal = selTab;
        }

        private void tabControlSettings_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControlSettings.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControlSettings.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Black);
                g.FillRectangle(Brushes.LightGray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Microsoft Sans Serif", 10.0f, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void updateDataGridViewDBData()
        {
            dataGridViewDB.Rows.Clear();

            CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
            var data = codeTableTA.GetData();

            foreach (var row in data)
            {
                if (int.Parse(row[4].ToString()) == Properties.Settings.Default.AccountLogin)
                {
                    dataGridViewDB.Rows.Add(row[5].ToString(), row[3].ToString(), row[2].ToString(), "delete", "rename");
                    dataGridViewDB.Rows[dataGridViewDB.Rows.Count - 2].Cells[0].Tag = row[0].ToString();
                    //MessageBox.Show("Row number: " + (dataGridViewDB.Rows.Count - 2) + "\nTag: " + dataGridViewDB.Rows[dataGridViewDB.Rows.Count - 2].Cells[0].Tag);
                }
            }
        }

        private void updateCheckboxes()
        {
            checkBoxIconScaling.Checked = Properties.Settings.Default.Scaling;
            PrefsTableTableAdapter prefsTableTA = new PrefsTableTableAdapter();
            var data = prefsTableTA.GetData();

            foreach (var row in data)
            {
                if (int.Parse(row[7].ToString()) == Properties.Settings.Default.AccountLogin)
                {
                    if (int.Parse(row[1].ToString()) == 1)
                        radioButtonLight.Checked = true;
                    else radioButtonDark.Checked = true;

                    if (int.Parse(row[2].ToString()) == 1)
                        checkBoxWordWrap.Checked = true;
                    else checkBoxWordWrap.Checked = false;

                    if (int.Parse(row[3].ToString()) == 1)
                        checkBoxErrorSquiggles.Checked = true;
                    else checkBoxErrorSquiggles.Checked = false;

                    if (int.Parse(row[4].ToString()) == 1)
                        checkBoxShowOutput.Checked = true;
                    else checkBoxShowOutput.Checked = false;

                    if (int.Parse(row[5].ToString()) == 1)
                        checkBoxShowErrorList.Checked = true;
                    else checkBoxShowErrorList.Checked = false;

                    if (int.Parse(row[6].ToString()) == 1)
                        checkBoxShowNavbar.Checked = true;
                    else checkBoxShowNavbar.Checked = false;
                }
            }
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            this.Location = new Point(
    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

            tabControlSettings.SelectedIndex = selTabGlobal;

            updateCheckboxes();
            updateDataGridViewDBData();
        }

        private void dataGridViewDB_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 3)
            {
                var message = MessageBox.Show("Are you sure do you want to delete " + dataGridViewDB.Rows[e.RowIndex].Cells[0].Value.ToString() + "?", "Delete", MessageBoxButtons.YesNo);
                if (message == DialogResult.Yes)
                {
                    CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
                    var data = codeTableTA.GetData();

                    int count = 0;
                    foreach (var row in data)
                    {
                        if (int.Parse(row[0].ToString()) == int.Parse(dataGridViewDB.Rows[e.RowIndex].Cells[0].Tag.ToString()))
                        {
                            data[count].Delete();
                            break;
                        }
                        count++;
                    }

                    codeTableTA.Update(data);
                    updateDataGridViewDBData();
                    return;
                }
            }
            else if (e.ColumnIndex == 4)
            {
                string UserAnswer = Microsoft.VisualBasic.Interaction.InputBox("Rename " + dataGridViewDB.Rows[e.RowIndex].Cells[0].Value.ToString() + " into:", "Rename", "");
                if (UserAnswer.Trim() != null)
                {
                    CodeTableTableAdapter codeTableTA = new CodeTableTableAdapter();
                    var data = codeTableTA.GetData();

                    int count = 0;
                    foreach (var row in data)
                    {
                        if (int.Parse(row[0].ToString()) == int.Parse(dataGridViewDB.Rows[e.RowIndex].Cells[0].Tag.ToString()))
                        {
                            data[count][5] = UserAnswer;
                            break;
                        }
                        count++;
                    }

                    codeTableTA.Update(data);
                    updateDataGridViewDBData();
                    return;
                }
            }
        }

        private void checkBoxShowNavbar_CheckedChanged(object sender, EventArgs e)
        {
            PrefsTableTableAdapter prefsTableTA = new PrefsTableTableAdapter();
            var data = prefsTableTA.GetData();

            int counter = 0;
            foreach (var row in data)
            {
                if (int.Parse(row[7].ToString()) == Properties.Settings.Default.AccountLogin)
                {
                    if (checkBoxShowNavbar.Checked == true)
                        data[counter][6] = 1;
                    else
                        data[counter][6] = 0;
                    prefsTableTA.Update(data);
                    return;
                }
                counter++;
            }
        }

        private void checkBoxShowErrorList_CheckedChanged(object sender, EventArgs e)
        {
            PrefsTableTableAdapter prefsTableTA = new PrefsTableTableAdapter();
            var data = prefsTableTA.GetData();

            int counter = 0;
            foreach (var row in data)
            {
                if (int.Parse(row[7].ToString()) == Properties.Settings.Default.AccountLogin)
                {
                    if (checkBoxShowErrorList.Checked == true)
                        data[counter][5] = 1;
                    else
                        data[counter][5] = 0;
                    prefsTableTA.Update(data);
                    return;
                }
                counter++;
            }
        }

        private void checkBoxShowOutput_CheckedChanged(object sender, EventArgs e)
        {
            PrefsTableTableAdapter prefsTableTA = new PrefsTableTableAdapter();
            var data = prefsTableTA.GetData();

            int counter = 0;
            foreach (var row in data)
            {
                if (int.Parse(row[7].ToString()) == Properties.Settings.Default.AccountLogin)
                {
                    if (checkBoxShowOutput.Checked == true)
                        data[counter][4] = 1;
                    else
                        data[counter][4] = 0;
                    prefsTableTA.Update(data);
                    return;
                }
                counter++;
            }
        }

        private void radioButtonDark_CheckedChanged(object sender, EventArgs e)
        {
            PrefsTableTableAdapter prefsTableTA = new PrefsTableTableAdapter();
            var data = prefsTableTA.GetData();

            int counter = 0;
            foreach (var row in data)
            {
                if (int.Parse(row[7].ToString()) == Properties.Settings.Default.AccountLogin)
                {
                    if (radioButtonDark.Checked == true)
                        data[counter][1] = 0;
                    else
                        data[counter][1] = 1;
                    prefsTableTA.Update(data);
                    return;
                }
                counter++;
            }
        }

        private void checkBoxWordWrap_CheckedChanged(object sender, EventArgs e)
        {
            PrefsTableTableAdapter prefsTableTA = new PrefsTableTableAdapter();
            var data = prefsTableTA.GetData();

            int counter = 0;
            foreach (var row in data)
            {
                if (int.Parse(row[7].ToString()) == Properties.Settings.Default.AccountLogin)
                {
                    if (checkBoxWordWrap.Checked == true)
                        data[counter][2] = 1;
                    else
                        data[counter][2] = 0;
                    prefsTableTA.Update(data);
                    return;
                }
                counter++;
            }
        }

        private void checkBoxErrorSquiggles_CheckedChanged(object sender, EventArgs e)
        {
            PrefsTableTableAdapter prefsTableTA = new PrefsTableTableAdapter();
            var data = prefsTableTA.GetData();

            int counter = 0;
            foreach (var row in data)
            {
                if (int.Parse(row[7].ToString()) == Properties.Settings.Default.AccountLogin)
                {
                    if (checkBoxErrorSquiggles.Checked == true)
                        data[counter][3] = 1;
                    else
                        data[counter][3] = 0;
                    prefsTableTA.Update(data);
                    return;
                }
                counter++;
            }
        }

        private void checkBoxIconScaling_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Scaling = checkBoxIconScaling.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
