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
    public partial class Form1 : Form
    {
        public static int b = 0, bb = 0, b5 = 0;
        bool bbb = false, b4 = false;
        public static int o = 0, tr = 0, in2 = 0, hn = 0;
        public static double lr = 0, et = 0;
        public static char sim = '\0';
        private Point? _Previous = null;
        private Point[] niz = new Point[1000];
        public static double[,] mat;
        private static double[] pr;
        static double greska=o;
        static int brSt = 0;
        static double[] dO;
        static double[] dH;
        static int epoha = 0;
        static int[,] outmat;
        static double[] outNeur;
        static double[,] wH;
        static double[,] wO;
        static double[,] temp;
        static double[] prout;
        //ispitati formule, da li je kao u knjizi
        static Random randNum = new Random();
        char[] simb;
        Bitmap dd;
        Graphics g, g1;
        int i = 0, n = 0;
        Pen mypen = new Pen(Color.Black, 4);
        private Pen _Pen = new Pen(Color.Blue, 4);
        public Form1()
        {
            InitializeComponent();
            dd = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (label1.Text == "The net is ready!" && bbb)
            {
                i=0;
                _Previous = new Point(e.X, e.Y);
                pictureBox1_MouseMove(sender, e);
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (label1.Text == "The net is ready!" && bbb)
            {
                if (_Previous != null)
                {
                    if (pictureBox1.Image == null)
                    {
                        dd = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        using (g = Graphics.FromImage(dd))
                        {
                            g.Clear(Color.White);
                        }
                        pictureBox1.Image = dd;
                    }
                    using (g = Graphics.FromImage(pictureBox1.Image))
                    {
                        g.DrawLine(_Pen, _Previous.Value.X, _Previous.Value.Y, e.X, e.Y);
                        if (i == 999)
                        {
                            for (int j = 1; j < 1000; j++)
                            {
                                niz[(j + 1) / 2].X = (niz[j].X + niz[j + 1].X) / 2;
                                niz[(j + 1) / 2].Y = (niz[j].Y + niz[j + 1].Y) / 2;
                            }
                        }
                        niz[i++] = new Point((e.X + _Previous.Value.X) / 2, (e.Y + _Previous.Value.Y) / 2);
                    }
                    pictureBox1.Invalidate();
                    _Previous = new Point(e.X, e.Y);
                }
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = dd;
            g1 = Graphics.FromImage(dd);
            mypen = new Pen(Color.Red, 4);
            if (label1.Text == "The net is ready!" && bbb)
            {
                niz[i++] = new Point(_Previous.Value.X, _Previous.Value.Y);
                n = i;
                int k = n, s = 1;
                while (k > in2+1)
                {
                    for (i = 1; i < n && k > in2+1 && i+s<n; i = i + 2*s)
                    {
                            niz[i].X += niz[i + s].X;
                            niz[i].Y += niz[i + s].Y;
                            if (niz[i + s].X == 0 && niz[i + s].Y == 0) 
                            { 
                                k++; niz[i].X *= 2; niz[i].Y *= 2; 
                            }
                            niz[i + s].X = 0; niz[i + s].Y = 0;
                            niz[i].X /= 2; niz[i].Y /= 2;
                            k--;
                    }
                    s*=2;
                }
                k = 0;
                Point p = new Point(niz[0].X, niz[0].Y);
                g1.DrawLine(mypen, p.X - 2, p.Y - 2, p.X + 2, p.Y + 2);
                if (b4 == false)
                {
                    for (i = 1; i < n; i++)
                    {
                        if (niz[i].X != 0 || niz[i].Y != 0)
                        {
                            double d = Math.Sqrt((niz[i].X - p.X) * (niz[i].X - p.X) + (niz[i].Y - p.Y) * (niz[i].Y - p.Y));
                            mat[tr - 2, 2*k] = (double)(niz[i].X - p.X) / d;
                            mat[tr - 2, 2*k+1] = (double)(niz[i].Y - p.Y) / d;
                            p.X = niz[i].X; p.Y = niz[i].Y;
                            k++;
                            g1.DrawLine(mypen, niz[i].X - 2, niz[i].Y - 2, niz[i].X + 2, niz[i].Y + 2);
                            //bilo bi lepo da se te tacke prikazu dodatno
                        }
                    }
                }
                else
                {
                    pr = new double[in2*2];
                    for (i = 1; i < n; i++)
                    {
                        if (niz[i].X != 0 || niz[i].Y != 0)
                        {
                            double d = Math.Sqrt((niz[i].X - p.X) * (niz[i].X - p.X) + (niz[i].Y - p.Y) * (niz[i].Y - p.Y));
                            pr[2*k] = (niz[i].X - p.X) / d;
                            pr[2*k+1] = (niz[i].Y - p.Y) / d;
                            p.X = niz[i].X; p.Y = niz[i].Y;
                            k++;
                            g1.DrawLine(mypen, niz[i].X - 2, niz[i].Y - 2, niz[i].X + 2, niz[i].Y + 2);
                        }
                    }
                }
                _Previous = null;
                dd = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
                pictureBox1.Image = dd;
                bbb = false;
                if (b4 == false) t3();
                else t4();
            }
        }

        public static void ThreadProc()
        {
            Application.Run(new Form2());
        }

        public static void ThreadProc1()
        {
            Application.Run(new Form3());
        }

        public static void ThreadProc2()
        {
            Application.Run(new Form4());
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            b = 0; o = 0; in2 = 0; hn = 0; lr = 0; et = 0; b4 = false; bb = 0; bbb = false; b5 = 0;
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
            t.Start();
            while (b == 0)
            {
                for (int i = 0; i < 1000; i++) { }
            }
            if (b == 1 && o != 0 && in2 != 0 && hn != 0 && lr != 0 && et != 0)
            {
                simb = new char[o];
                dO = new double[o];
                dH = new double[hn];
                outmat = new int[o, o];
                outNeur = new double[hn];
                wH = new double[hn, in2*2+1];
                wO = new double[o, hn+1];
                temp = new double[hn, o];
                prout = new double[o];
                label1.Text = "The net is ready!";
                tr = 1;
                mat = new double[o, in2*2];
                t3();
            }
            if (b == -1) label1.Text = "The net is not ready!";
        }
        private void t3()
        {
            if (tr <= o)
            {
                bb = 0;
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc1));
                t.Start();
                int a = 0;
                while (bb == 0) { a++; a = a % 1000; }
                if (bb == 1 && sim == '\0') 
                { 
                    label1.Text = "The net is not ready!"; 
                }
                else if (bb == 1)
                {
                    simb[tr - 1] = sim;
                    outmat[tr - 1, tr - 1] = 1;
                    bbb = true; tr++;
                }
            }
            else
            {
                label1.Text = "Training...";
                epoha = 0;
                inicijalizuj();
                greska = 1;
                while (greska >= et || epoha < 1)
                {
                    for (int i = 0; i < o && greska>=et; i++)
                    {
                        greska = 0;
                        brSt = i;
                        treniraj();
                    }
                    epoha++;
                }
                label1.Text = "The net is ready!";
                b4 = true;
                bbb = true;
            }

        }

        private void t4()
        {
            probaj();
            for (int i = 0; i < o; i++)
            {
                if ((int)prout[i] == 1) { sim = simb[i];}
            }
            for (int i = 0; i < o; i++)
            {
                prout[i] = 0;
            }
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc2));
            t.Start();
            int a = 0;
            while (b5 == 0) { a++; a = a % 1000; }
            if (b5 == 1) bbb = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void inicijalizuj()
        {
            greska = o;
            for (int i = 0; i < o; i++)
            {
                for (int j = 0; j < hn+1; j++)
                {
                    wO[i, j] = randNum.NextDouble()-0.5;
                }
            }
            for (int k = 0; k < hn; k++)
            {
                for (int l = 0; l < in2*2+1; l++)
                {
                    wH[k, l] = randNum.NextDouble()-0.5;
                }
            } 
        }
 
        static int flag = 0; //da li ovo treba
        public static void treniraj()
        {
                for (int i = 0; i < hn; i++)
                {
                    outNeur[i] = 0;
                    for (int j = 0; j < in2*2; j++)
                    {
                        outNeur[i] = outNeur[i] + (mat[brSt, j] * wH[i, j]);
                    }
                    outNeur[i] += -1 * wH[i, in2 * 2];
                    outNeur[i] = 1/(1+Math.Exp(-1*outNeur[i]));
                }
                for (int i = 0; i < o; i++)
                {
                    flag = i;
                    double otemp = 0; 
                    for (int j = 0; j < hn; j++)
                    {
                        otemp = otemp + outNeur[j] * wO[i, j];
                    }
                    otemp += -1 * wO[i, hn];
                    otemp = 1 / (1 + Math.Exp(-1 * otemp));
                    dO[i] = otemp * (1 - otemp) * (outmat[brSt,i]-otemp);
                    if (dO[i] > greska) { greska = dO[i]; }
                    promeniwO(dO[i], i);
                }
                promeniwH();
        }
 
        public static void promeniwO(double _delta, int n)
        {
          for (int j = 0; j < hn; j++)
                {
                    double wtemp = lr * _delta * outNeur[j];
                    wO[n, j] = wtemp + wO[n, j];
                }
          wO[n, hn]+= lr * _delta * (-1);
        }
 
        public static void promeniwH()
        {
                for (int i = 0; i < hn; i++)
                {
                    double temp = outNeur[i] * (1 - outNeur[i]);
                    dH[i] = 0;
                    for (int j = 0; j < o; j++)
                    {
                        dH[i] += wO[j, i] * dO[j];
                    }
                    dH[i] = dH[i] * temp;
                }

                for (int l = 0; l < hn; l++)
                {
                    for (int k = 0; k < in2*2; k++)
                    {
                        wH[l, k] = (lr * dH[l] * mat[brSt, k]) + wH[l, k];
                    }
                    wH[l, in2*2] = wH[l, in2*2]+lr*dH[l]*(-1);
                }
        }
 
        public static void probaj()
        {
              for (int i = 0; i < hn; i++)
                {
                    outNeur[i] = 0;
                    for (int j = 0; j < in2*2; j++)
                    {
                        outNeur[i] = outNeur[i] + (pr[j] * wH[i, j]);
                    }
                    outNeur[i] += -1 * wH[i, in2 * 2];
                    outNeur[i] = 1 / (1 + Math.Exp(-1 * outNeur[i]));
                }
                for (int i = 0; i < o; i++)
                {
                    flag = i;
                    double otemp = 0;
                    for (int j = 0; j < hn; j++)
                    {
                        otemp = otemp + outNeur[j] * wO[i, j];
                    }
                    otemp += -1 * wO[i, hn];
                    otemp = 1 / (1 + Math.Exp(-1 * otemp));
                    prout[i] = otemp;
                }
            double max = prout[0]; 
            int ind = 0;
            for (int i = 1; i < o; i++) { if (max < prout[i]) { max = prout[i]; ind = i; } }
            for (int i = 0; i < o; i++) { if (i == ind) prout[i] = 1; else prout[i] = 0; }
        }
    }
}
