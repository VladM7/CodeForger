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
    public partial class FormMain : Form
    {
        string titleGlobal, contentsGlobal, pathGlobal;

        public FormMain(string title, string path, string contents)
        {
            InitializeComponent();
            titleGlobal = title;
            contentsGlobal = contents;
            pathGlobal = path;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Titlu: " + titleGlobal + "\nContents: " + contentsGlobal);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
