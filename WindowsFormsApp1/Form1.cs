﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Checks;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
           /* MessageBox.Show("Hello World!");*/
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Class1.Proverka();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int num = rand.Next(1, 100);
           /* label1.Text = num.ToString();*/
        }
    }
}
