using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nQueens
{
    public partial class Form1 : Form
    {
        Bitmap d;
        int n;
        Graphics g, g1, g2, g3, g4, g5;
        Brush brb = new SolidBrush(Color.Black), brw=new SolidBrush(Color.White), brr=new SolidBrush(Color.Red);
        bool[,] qu;
        bool b=false;
        int xx;
        public Form1()
        {
            InitializeComponent();
            d=new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = "";
            try
            {
                n = Int32.Parse(textBox1.Text);
                b = true;
                if (n <= 12 && n >= 4) 
                {
                    NarisiTablo(n);
                    qu = new bool[n, n];
                    for (int i=0; i<n; i++)
                        for (int j = 0; j < n; j++) 
                        {
                            qu[i, j] = false;
                        }
                    PostaviKraljice(n);
                }
               else label2.Text = "Vnesite število med 4 in 12.";
            }
            catch (Exception ee)
            {
                if (ee is ArgumentNullException|| ee is FormatException || ee is OverflowException) {label2.Text="Napačni vnos";} else {throw;} 
            }
        }

        private void NarisiTablo(int n) 
        {
            xx=580/n;
            pictureBox1.Image = d;
            g = Graphics.FromImage(pictureBox1.Image);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if ((i + j) % 2 == 0) 
                    {
                        g.FillRectangle(brb, new System.Drawing.Rectangle(i * xx, j * xx, (i + 1) * xx, (j + 1) * xx));
                    }
                    else g.FillRectangle(brw, new System.Drawing.Rectangle(i * xx, j * xx, (i + 1) * xx, (j + 1) * xx));
                }
        }
        private void PostaviKraljice(int n)
        {
            g1 = Graphics.FromImage(pictureBox1.Image);
            Random rnd = new Random();
            for (int j = 0; j < n; j++)
            {
                int i = rnd.Next(0, n - 1);
               qu[i, j] = true;
                g1.FillEllipse(brr, j * xx + (int)(xx / 4), i * xx + (int)(xx / 4), (int)(xx / 2), (int)(xx / 2));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = d;
            g2 = Graphics.FromImage(pictureBox1.Image);
            int br, min, ia, ja, brN;
            int[,]cur = new int[n, n];
            br=0;
            if (b)
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        if (qu[i, j] == true)
                        {
                            for (int k = 0; k < n; k++)
                            {
                                if (qu[i, k] == true) { br++; }
                            }
                            for (int k = 0; k < n; k++)
                            {
                                if (((k + j - i) >= 0) && ((k + j - i) < n))
                                {
                                    if (qu[k, k + j - i] == true) { br++; }
                                }
                            }
                            for (int k = 0; k < n; k++)
                            {
                                if (((i + j - k) >= 0) && ((i + j - k) < n))
                                {
                                    if (qu[k, i + j - k] == true) { br++; }
                                }
                            }
                            br -= 3;
                        }
                    }
                br = br / 2;
                min = n * n;
                int itr = 0;
                while ((itr < 10 * n) && br != 0 && min != br)
                {
                    if (min != n * n) { br = min; }
                    for (int j = 0; j < n; j++)
                    {
                        int s = 0;
                        for (int m = 0; m < n; m++)
                        {
                            if (qu[m, j] == true)
                            {
                                for (int k = 0; k < n; k++)
                                {
                                    if (qu[m, k] == true) { s--; }
                                }
                                for (int k = 0; k < n; k++)
                                {
                                    if (((k + m - j) >= 0) && ((k + m - j) < n))
                                    {
                                        if (qu[k + m - j, k] == true) { s--; }
                                    }
                                }
                                for (int k = 0; k < n; k++)
                                {
                                    if (((j + m - k) >= 0) && ((j + m - k) < n))
                                    {
                                        if (qu[j + m - k, k] == true) { s--; }
                                    }
                                }
                                s += 3;
                            }

                        }
                        for (int i = 0; i < n; i++)
                        {
                            brN = br;
                            if (qu[i, j] != true)
                            {
                                for (int k = 0; k < n; k++)
                                {
                                    if (qu[i, k] == true) { brN++; }
                                }
                                for (int k = 0; k < n; k++)
                                {
                                    if (((k + i - j) >= 0) && ((k + i - j) < n))
                                    {
                                        if (qu[k + i - j, k] == true) { brN++; }
                                    }
                                }
                                for (int k = 0; k < n; k++)
                                {
                                    if (((i + j - k) >= 0) && ((i + j - k) < n))
                                    {
                                        if (qu[i + j - k, k] == true) { brN++; }
                                    }
                                }
                                brN += s;
                                cur[i, j] = brN;
                            }
                            else cur[i, j] = br;
                        }
                    }
                    min = cur[0, 0]; ia = 0; ja = 0;
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                        {
                            if (cur[i, j] < min) { min = cur[i, j]; ia = i; ja = j; }
                        }
                    if (min < br)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (qu[i, ja] == true)
                            {
                                qu[i, ja] = false;
                                if ((i + ja) % 2 == 0)
                                { g2.FillEllipse(brb, (int)(ja * xx + xx / 4), (int)(i * xx + xx / 4), (int)(xx / 2), (int)(xx / 2)); }
                                else { g2.FillEllipse(brw, ja * xx + (int)(xx / 4), i * xx + (int)(xx / 4), (int)(xx / 2), (int)(xx / 2)); }
                                qu[ia, ja] = true;
                                g2.FillEllipse(brr, ja * xx + (int)(xx / 4), ia * xx + (int)(xx / 4), (int)(xx / 2), (int)(xx / 2));
                                label2.Text = "Trenutni minimum je:" + min;
                            }
                        }
                        for (int i = 0; i < n; i++)
                            for (int j = 0; j < n; j++)
                            {
                                if (qu[i, j] == true) cur[i, j] = min;
                            }
                    }
                    itr++;
                }
            }
            else label2.Text = "Pogresan vnos.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = d;
            g3 = Graphics.FromImage(pictureBox1.Image);
            int br, min, ia, ja, brN;
            double T = 5, dT=0.1;
            Random r=new Random();
            int[,] cur = new int[n, n];
            br = 0;
            if (b)
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        if (qu[i, j] == true)
                        {
                            for (int k = 0; k < n; k++)
                            {
                                if (qu[i, k] == true) { br++; }
                            }
                            for (int k = 0; k < n; k++)
                            {
                                if (((k + j - i) >= 0) && ((k + j - i) < n))
                                {
                                    if (qu[k, k + j - i] == true) { br++; }
                                }
                            }
                            for (int k = 0; k < n; k++)
                            {
                                if (((i + j - k) >= 0) && ((i + j - k) < n))
                                {
                                    if (qu[k, i + j - k] == true) { br++; }
                                }
                            }
                            br -= 3;
                        }
                    }
                br = br / 2;
                min = n * n;
                while (T > 0 && br != 0 && min != 0)
                {
                    T = T - dT;
                    if (min != n * n) { br = min; }
                    for (int j = 0; j < n; j++)
                    {
                        int s = 0;
                        for (int m = 0; m < n; m++)
                        {
                            if (qu[m, j] == true)
                            {
                                for (int k = 0; k < n; k++)
                                {
                                    if (qu[m, k] == true) { s--; }
                                }
                                for (int k = 0; k < n; k++)
                                {
                                    if (((k + m - j) >= 0) && ((k + m - j) < n))
                                    {
                                        if (qu[k + m - j, k] == true) { s--; }
                                    }
                                }
                                for (int k = 0; k < n; k++)
                                {
                                    if (((j + m - k) >= 0) && ((j + m - k) < n))
                                    {
                                        if (qu[j + m - k, k] == true) { s--; }
                                    }
                                }
                                s += 3;
                            }

                        }
                        for (int i = 0; i < n; i++)
                        {
                            brN = br;
                            if (qu[i, j] != true)
                            {
                                for (int k = 0; k < n; k++)
                                {
                                    if (qu[i, k] == true) { brN++; }
                                }
                                for (int k = 0; k < n; k++)
                                {
                                    if (((k + i - j) >= 0) && ((k + i - j) < n))
                                    {
                                        if (qu[k + i - j, k] == true) { brN++; }
                                    }
                                }
                                for (int k = 0; k < n; k++)
                                {
                                    if (((i + j - k) >= 0) && ((i + j - k) < n))
                                    {
                                        if (qu[i + j - k, k] == true) { brN++; }
                                    }
                                }
                                brN += s;
                                cur[i, j] = brN;
                            }
                            else cur[i, j] = br;
                        }
                    }
                    min = n * n; ia = 0; ja = 0;
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                        {
                            if (!qu[i, j])
                            {
                                if (cur[i, j] < min) { min = cur[i, j]; ia = i; ja = j; }
                            }
                        }
                    double next = (double)r.Next(0, n) / n;
                    double diff = Math.Exp((min - br) / T);
                    if (br != 0 || min < br || diff > r.Next(0, 1))
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (qu[i, ja] == true)
                            {
                                qu[i, ja] = false;
                                if ((i + ja) % 2 == 0)
                                { g3.FillEllipse(brb, (int)(ja * xx + xx / 4), (int)(i * xx + xx / 4), (int)(xx / 2), (int)(xx / 2)); }
                                else { g3.FillEllipse(brw, ja * xx + (int)(xx / 4), i * xx + (int)(xx / 4), (int)(xx / 2), (int)(xx / 2)); }
                                qu[ia, ja] = true;
                                g3.FillEllipse(brr, ja * xx + (int)(xx / 4), ia * xx + (int)(xx / 4), (int)(xx / 2), (int)(xx / 2));
                                label2.Text = "Trenutni minimum je:" + min;
                            }
                        }
                        for (int i = 0; i < n; i++)
                            for (int j = 0; j < n; j++)
                            {
                                if (qu[i, j] == true) cur[i, j] = min;
                            }
                    }
                }
            }
            else label2.Text = "Pogresan vnos.";
        }
    }
}
