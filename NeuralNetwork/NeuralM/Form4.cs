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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            label1.Text = "This simbol should be " + Form1.sim;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.b5 = 1;
            this.Dispose();
        }
    }
}
