using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NeuralM
{
    public partial class Form3 : Form
    {
        char c = '\0';
        public Form3()
        {
            InitializeComponent();
            label1.Text = "Define symbol for pattern " + Form1.tr + "/" + Form1.o + ":";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (c == '\0') { label2.ForeColor = Color.Red; label2.Text = "Invalid simbol"; }
            else { Form1.sim = textBox1.Text[0]; Form1.bb = 1; this.Dispose(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.bb = 1;
            this.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text!="") c = textBox1.Text[0];
            textBox1.Text = c+"";
        }
    }
}
