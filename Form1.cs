﻿using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonNewFile_Click(object sender, EventArgs e)
        {
            var form=new FormMain();
            form.Show();
        }

        private void inchidere()
        {
            this.Show();
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            var form = new FormAccount();
            form.Show();
            this.Hide();
            form.FormClosed += (s, args)=>
            {
                this.Show();
            };
        }
    }
}
