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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                if (n > 2) { textBox1.Text = --n + ""; }
                else { textBox1.Text = "2"; }
            }
            catch (Exception ex) { textBox1.Text = "2"; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                if (n < 8) { textBox1.Text = ++n + ""; }
                else { textBox1.Text = "8"; }
            }
            catch (Exception ex) { textBox1.Text = "8"; }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                if (n > 8) { textBox1.Text = --n + ""; }
                else { textBox1.Text = "8"; }
            }
            catch (Exception ex) { textBox1.Text = "8"; }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                if (n < 30) { textBox1.Text = ++n + ""; }
                else { textBox1.Text = "30"; }
            }
            catch (Exception ex) { textBox1.Text = "30"; }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                if (n > 4) { textBox1.Text = --n + ""; }
                else { textBox1.Text = "4"; }
            }
            catch (Exception ex) { textBox1.Text = "4"; }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                if (n < 20) { textBox1.Text = ++n + ""; }
                else { textBox1.Text = "20"; }
            }
            catch (Exception ex) { textBox1.Text = "20"; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                if (n <2) { textBox1.Text = "2"; }
                if (n > 8) { textBox1.Text = "8"; }
            }
            catch (Exception ex) { textBox1.Text = "2"; }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                if (n < 2) { textBox1.Text = "8"; }
                if (n > 8) { textBox1.Text = "30"; }
            }
            catch (Exception ex) { textBox1.Text = "8"; }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                if (n < 2) { textBox1.Text = "4"; }
                if (n > 8) { textBox1.Text = "20"; }
            }
            catch (Exception ex) { textBox1.Text = "4"; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.o = int.Parse(textBox1.Text);
                Form1.in2 = int.Parse(textBox2.Text);
                Form1.hn = int.Parse(textBox3.Text);
                Form1.lr = double.Parse(textBox4.Text);
                Form1.et = double.Parse(textBox5.Text);
            }
            catch (Exception ex)
            {
                Form1.o = 0; Form1.in2 = 0; Form1.hn = 0; Form1.lr = 0; Form1.et = 0;
            }
            Form1.b = 1;
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Form1.b = -1;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                if (n == 0) { textBox5.Text = "0.01"; }
            }
            catch (Exception ex) { textBox1.Text = "0.01"; }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                if (n == 0) { textBox5.Text = "0.5"; }
            }
            catch (Exception ex) { textBox1.Text = "0.5"; }
        }
    }
}
