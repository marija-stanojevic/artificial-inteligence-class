using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OdlucitvenoDrevoID3
{
    public partial class Form1 : Form
    {
        class Elem
        {
            int param; //vprasanje
            double vr; //grana
            double ver; //verjetnost da pride v taj cvor
            public Elem[] s;
            public Elem otac;
            public Elem() { }
            public void br(int n) { s = new Elem[n];}
            public void par(int p) { param = p; }
            public void gr(double g) { vr = g; }
            public void verov(double v) { ver = v; }
            public int parametar() { return param; }
            public double grana() { return vr; }
            public double verjetnost() { return ver; }
        }
        double[][] podaci;
        int n;
        double ver=0;
        public Form1()
        {
            InitializeComponent();
            label4.Text = "";
        }

        private void obradi (Elem prvi)
        {
            if (prvi.parametar() == -3)
            {
                int gol = 0, nigol = 0;
                for (int i = 0; i < n; i++)
                {
                    if (podaci[i][6] == 1) { gol++; }
                    else nigol++;
                }
                double[] entr = new double[7];
                entr[6] = -((double)gol / n) * Math.Log(((double)gol / n), 2) - ((double)nigol / n) * Math.Log(((double)nigol / n), 2);
                //za svaku kolonu treba napraviti entropiju
                int[] tip = new int[25];
                int[] d = new int[25];
                double[] ent = new double[25];
                //entropija za prvu kolonu
                for (int j = 0; j < 25; j++)
                {
                    tip[j] = 0;
                    d[j] = 0;
                    ent[j] = 0;
                }
                for (int i = 0; i < n; i++)
                {
                    if (podaci[i][0] <= 1) { tip[0]++; if (podaci[i][6] == 1) d[0]++; }
                    if (podaci[i][0] <= 3 && podaci[i][0] > 1) { tip[1]++; if (podaci[i][6] == 1) d[1]++; }
                    if (podaci[i][0] <= 5 && podaci[i][0] > 3) { tip[2]++; if (podaci[i][6] == 1) d[2]++; }
                    if (podaci[i][0] <= 8 && podaci[i][0] > 5) { tip[3]++; if (podaci[i][6] == 1) d[3]++; }
                    if (podaci[i][0] <= 11 && podaci[i][0] > 8) { tip[4]++; if (podaci[i][6] == 1) d[4]++; }
                    if (podaci[i][0] <= 15 && podaci[i][0] > 11) { tip[5]++; if (podaci[i][6] == 1) d[5]++; }
                    if (podaci[i][0] <= 20 && podaci[i][0] > 15) { tip[6]++; if (podaci[i][6] == 1) d[6]++; }
                    if (podaci[i][0] <= 30 && podaci[i][0] > 20) { tip[7]++; if (podaci[i][6] == 1) d[7]++; }
                    if (podaci[i][0] > 30) { tip[8]++; if (podaci[i][6] == 1) d[8]++; }
                }
                bool b = false;
                Elem tek = prvi;
                if (tek.otac != null) { tek = tek.otac; }
                while (tek != null && !b) { if (tek.parametar() == 0) b = true; tek = tek.otac; }
                if (!b)
                {
                    entr[0] = entr[6];
                    for (int i = 0; i < 25; i++)
                    {
                        if (tip[i] != 0)
                        {
                            if (d[i] != 0) ent[i] += -((double)d[i] / tip[i]) * Math.Log(((double)d[i] / tip[i]), 2);
                            if (d[i] != tip[i]) ent[i] += -((double)(tip[i] - d[i]) / tip[i]) * Math.Log(((double)(tip[i] - d[i]) / tip[i]), 2);
                            entr[0] -= ent[i] * tip[i] / n;
                        }
                    }
                }
                else
                {
                    entr[0] = 0;
                }
                //entropija za drugu kolonu
                for (int j = 0; j < 25; j++)
                {
                    tip[j] = 0;
                    d[j] = 0;
                    ent[j] = 0;
                }
                for (int i = 0; i < n; i++)
                {
                    if (podaci[i][1] >= 70) { tip[0]++; if (podaci[i][6] == 1) d[0]++; }
                    if (podaci[i][1] >= 50 && podaci[i][1] < 70) { tip[1]++; if (podaci[i][6] == 1) d[1]++; }
                    if (podaci[i][1] >= 30 && podaci[i][1] < 50) { tip[2]++; if (podaci[i][6] == 1) d[2]++; }
                    if (podaci[i][1] >= 10 && podaci[i][1] < 30) { tip[3]++; if (podaci[i][6] == 1) d[3]++; }
                    if (podaci[i][1] >= -10 && podaci[i][1] < 10) { tip[4]++; if (podaci[i][6] == 1) d[4]++; }
                    if (podaci[i][1] >= -30 && podaci[i][1] < -10) { tip[5]++; if (podaci[i][6] == 1) d[5]++; }
                    if (podaci[i][1] >= -50 && podaci[i][1] < -30) { tip[6]++; if (podaci[i][6] == 1) d[6]++; }
                    if (podaci[i][1] >= -70 && podaci[i][1] < -50) { tip[7]++; if (podaci[i][6] == 1) d[7]++; }
                    if (podaci[i][1] < -70) { tip[8]++; if (podaci[i][6] == 1) d[8]++; }
                }
                b = false;
                tek = prvi;
                if (tek.otac != null) { tek = tek.otac; }
                while (tek != null && !b) { if (tek.parametar() == 1) b = true; tek = tek.otac; }
                if (!b)
                {
                    entr[1] = entr[6];
                    for (int i = 0; i < 25; i++)
                    {
                        if (tip[i] != 0)
                        {
                            if (d[i] != 0) ent[i] += -((double)d[i] / tip[i]) * Math.Log(((double)d[i] / tip[i]), 2);
                            if (d[i] != tip[i]) ent[i] += -((double)(tip[i] - d[i]) / tip[i]) * Math.Log(((double)(tip[i] - d[i]) / tip[i]), 2);
                            entr[1] -= ent[i] * tip[i] / n;
                        }
                    }
                }
                else
                {
                    entr[1] = 0;
                }
                //entropija za tretju kolonu
                for (int j = 0; j < 25; j++)
                {
                    tip[j] = 0;
                    d[j] = 0;
                    ent[j] = 0;
                }
                for (int i = 0; i < n; i++)
                {
                    if (podaci[i][2] == 1) { tip[0]++; if (podaci[i][6] == 1) d[0]++; }
                    if (podaci[i][2] == 2) { tip[1]++; if (podaci[i][6] == 1) d[1]++; }
                    if (podaci[i][2] == 3) { tip[2]++; if (podaci[i][6] == 1) d[2]++; }
                    if (podaci[i][2] == 4) { tip[3]++; if (podaci[i][6] == 1) d[3]++; }
                    if (podaci[i][2] == 5) { tip[4]++; if (podaci[i][6] == 1) d[4]++; }
                    if (podaci[i][2] == 6) { tip[5]++; if (podaci[i][6] == 1) d[5]++; }
                    if (podaci[i][2] == 7) { tip[6]++; if (podaci[i][6] == 1) d[6]++; }
                    if (podaci[i][2] == 8) { tip[7]++; if (podaci[i][6] == 1) d[7]++; }
                    if (podaci[i][2] == 9) { tip[8]++; if (podaci[i][6] == 1) d[8]++; }
                    if (podaci[i][2] == 10) { tip[9]++; if (podaci[i][6] == 1) d[9]++; }
                    if (podaci[i][2] == 11) { tip[10]++; if (podaci[i][6] == 1) d[10]++; }
                    if (podaci[i][2] == 12) { tip[11]++; if (podaci[i][6] == 1) d[11]++; }
                    if (podaci[i][2] == 13) { tip[12]++; if (podaci[i][6] == 1) d[12]++; }
                    if (podaci[i][2] == 14) { tip[13]++; if (podaci[i][6] == 1) d[13]++; }
                    if (podaci[i][2] == 15) { tip[14]++; if (podaci[i][6] == 1) d[14]++; }
                    if (podaci[i][2] == 16) { tip[15]++; if (podaci[i][6] == 1) d[15]++; }
                    if (podaci[i][2] == 17) { tip[16]++; if (podaci[i][6] == 1) d[16]++; }
                    if (podaci[i][2] == 18) { tip[17]++; if (podaci[i][6] == 1) d[17]++; }
                    if (podaci[i][2] == 19) { tip[18]++; if (podaci[i][6] == 1) d[18]++; }
                    if (podaci[i][2] == 20) { tip[19]++; if (podaci[i][6] == 1) d[19]++; }
                    if (podaci[i][2] == 21) { tip[20]++; if (podaci[i][6] == 1) d[20]++; }
                    if (podaci[i][2] == 22) { tip[21]++; if (podaci[i][6] == 1) d[21]++; }
                    if (podaci[i][2] == 23) { tip[22]++; if (podaci[i][6] == 1) d[22]++; }
                    if (podaci[i][2] == 24) { tip[23]++; if (podaci[i][6] == 1) d[23]++; }
                    if (podaci[i][2] == 25) { tip[24]++; if (podaci[i][6] == 1) d[24]++; }
                }
                b = false;
                tek = prvi;
                if (tek.otac != null) { tek = tek.otac; }
                while (tek != null && !b) { if (tek.parametar() == 2) b = true; tek = tek.otac; }
                if (!b)
                {
                    entr[2] = entr[6];
                    for (int i = 0; i < 25; i++)
                    {
                        if (tip[i] != 0)
                        {
                            if (d[i] != 0) ent[i] += -((double)d[i] / tip[i]) * Math.Log(((double)d[i] / tip[i]), 2);
                            if (d[i] != tip[i]) ent[i] += -((double)(tip[i] - d[i]) / tip[i]) * Math.Log(((double)(tip[i] - d[i]) / tip[i]), 2);
                            entr[2] -= ent[i] * tip[i] / n;
                        }
                    }
                }
                else
                {
                    entr[2] = 0;
                }
                //entropija za cetvrtu kolonu
                for (int j = 0; j < 25; j++)
                {
                    tip[j] = 0;
                    d[j] = 0;
                    ent[j] = 0;
                }
                for (int i = 0; i < n; i++)
                {
                    if (podaci[i][3] <= 4) { tip[0]++; if (podaci[i][6] == 1) d[0]++; }
                    if (podaci[i][3] <= 8 && podaci[i][3] > 4) { tip[1]++; if (podaci[i][6] == 1) d[1]++; }
                    if (podaci[i][3] <= 12 && podaci[i][3] > 8) { tip[2]++; if (podaci[i][6] == 1) d[2]++; }
                    if (podaci[i][3] <= 16 && podaci[i][3] > 12) { tip[3]++; if (podaci[i][6] == 1) d[3]++; }
                    if (podaci[i][3] <= 18 && podaci[i][3] > 16) { tip[4]++; if (podaci[i][6] == 1) d[4]++; }
                    if (podaci[i][3] <= 20 && podaci[i][3] > 18) { tip[5]++; if (podaci[i][6] == 1) d[5]++; }
                    if (podaci[i][3] <= 22 && podaci[i][3] > 20) { tip[6]++; if (podaci[i][6] == 1) d[6]++; }
                    if (podaci[i][3] <= 24 && podaci[i][3] > 22) { tip[7]++; if (podaci[i][6] == 1) d[7]++; }
                    if (podaci[i][3] <= 28 && podaci[i][3] > 24) { tip[8]++; if (podaci[i][6] == 1) d[8]++; }
                    if (podaci[i][3] <= 32 && podaci[i][3] > 28) { tip[9]++; if (podaci[i][6] == 1) d[9]++; }
                    if (podaci[i][3] <= 36 && podaci[i][3] > 32) { tip[10]++; if (podaci[i][6] == 1) d[10]++; }
                    if (podaci[i][3] <= 40 && podaci[i][3] > 36) { tip[11]++; if (podaci[i][6] == 1) d[11]++; }
                    if (podaci[i][3] >= 40) { tip[12]++; if (podaci[i][6] == 1) d[12]++; }
                }
                b = false;
                tek = prvi;
                if (tek.otac != null) { tek = tek.otac; }
                while (tek != null && !b) { if (tek.parametar() == 3) b = true; tek = tek.otac; }
                if (!b)
                {
                    entr[3] = entr[6];
                    for (int i = 0; i < 25; i++)
                    {
                        if (tip[i] != 0)
                        {
                            if (d[i] != 0) ent[i] += -((double)d[i] / tip[i]) * Math.Log(((double)d[i] / tip[i]), 2);
                            if (d[i] != tip[i]) ent[i] += -((double)(tip[i] - d[i]) / tip[i]) * Math.Log(((double)(tip[i] - d[i]) / tip[i]), 2);
                            entr[3] -= ent[i] * tip[i] / n;
                        }
                    }
                }
                else
                {
                    entr[3] = 0;
                }
                //entropija za petu kolonu
                for (int j = 0; j < 25; j++)
                {
                    tip[j] = 0;
                    d[j] = 0;
                    ent[j] = 0;
                }
                for (int i = 0; i < n; i++)
                {
                    if (podaci[i][4] <= 0.5) { tip[0]++; if (podaci[i][6] == 1) d[0]++; }
                    if (podaci[i][4] <= 1 && podaci[i][4] > 0.5) { tip[1]++; if (podaci[i][6] == 1) d[1]++; }
                    if (podaci[i][4] <= 2 && podaci[i][4] > 1) { tip[2]++; if (podaci[i][6] == 1) d[2]++; }
                    if (podaci[i][4] <= 3 && podaci[i][4] > 2) { tip[3]++; if (podaci[i][6] == 1) d[3]++; }
                    if (podaci[i][4] <= 4 && podaci[i][4] > 3) { tip[4]++; if (podaci[i][6] == 1) d[4]++; }
                    if (podaci[i][4] <= 5 && podaci[i][4] > 4) { tip[5]++; if (podaci[i][6] == 1) d[5]++; }
                    if (podaci[i][4] <= 6 && podaci[i][4] > 5) { tip[6]++; if (podaci[i][6] == 1) d[6]++; }
                    if (podaci[i][4] <= 7 && podaci[i][4] > 6) { tip[7]++; if (podaci[i][6] == 1) d[7]++; }
                    if (podaci[i][4] <= 8 && podaci[i][4] > 7) { tip[8]++; if (podaci[i][6] == 1) d[8]++; }
                    if (podaci[i][4] >= 8) { tip[9]++; if (podaci[i][6] == 1) d[9]++; }
                }
                b = false;
                tek = prvi;
                if (tek.otac != null) { tek = tek.otac; }
                while (tek != null && !b) { if (tek.parametar() == 4) b = true; tek = tek.otac; }
                if (!b)
                {
                    entr[4] = entr[6];
                    for (int i = 0; i < 25; i++)
                    {
                        if (tip[i] != 0)
                        {
                            if (d[i] != 0) ent[i] += -((double)d[i] / tip[i]) * Math.Log(((double)d[i] / tip[i]), 2);
                            if (d[i] != tip[i]) ent[i] += -((double)(tip[i] - d[i]) / tip[i]) * Math.Log(((double)(tip[i] - d[i]) / tip[i]), 2);
                            entr[4] -= ent[i] * tip[i] / n;
                        }
                    }
                }
                else
                {
                    entr[4] = 0;
                }
                //entropija za sestu kolonu
                for (int j = 0; j < 25; j++)
                {
                    tip[j] = 0;
                    d[j] = 0;
                    ent[j] = 0;
                }
                for (int i = 0; i < n; i++)
                {
                    if (podaci[i][5] >= 70) { tip[0]++; if (podaci[i][6] == 1) d[0]++; }
                    if (podaci[i][5] >= 50 && podaci[i][5] < 70) { tip[1]++; if (podaci[i][6] == 1) d[1]++; }
                    if (podaci[i][5] >= 30 && podaci[i][5] < 50) { tip[2]++; if (podaci[i][6] == 1) d[2]++; }
                    if (podaci[i][5] >= 10 && podaci[i][5] < 30) { tip[3]++; if (podaci[i][6] == 1) d[3]++; }
                    if (podaci[i][5] >= -10 && podaci[i][5] < 10) { tip[4]++; if (podaci[i][6] == 1) d[4]++; }
                    if (podaci[i][5] >= -30 && podaci[i][5] < -10) { tip[5]++; if (podaci[i][6] == 1) d[5]++; }
                    if (podaci[i][5] >= -50 && podaci[i][5] < -30) { tip[6]++; if (podaci[i][6] == 1) d[6]++; }
                    if (podaci[i][5] >= -70 && podaci[i][5] < -50) { tip[7]++; if (podaci[i][6] == 1) d[7]++; }
                    if (podaci[i][5] < -70) { tip[8]++; if (podaci[i][6] == 1) d[8]++; }
                }
                b = false;
                tek = prvi;
                if (tek.otac != null) { tek = tek.otac; }
                while (tek != null && !b) { if (tek.parametar() == 5) b = true; tek = tek.otac; }
                if (!b)
                {
                    entr[5] = entr[6];
                    for (int i = 0; i < 25; i++)
                    {
                        if (tip[i] != 0)
                        {
                            if (d[i] != 0) ent[i] += -((double)d[i] / tip[i]) * Math.Log(((double)d[i] / tip[i]), 2);
                            if (d[i] != tip[i]) ent[i] += -((double)(tip[i] - d[i]) / tip[i]) * Math.Log(((double)(tip[i] - d[i]) / tip[i]), 2);
                            entr[5] -= ent[i] * tip[i] / n;
                        }
                    }
                }
                else
                {
                    entr[5] = 0;
                }
                double max = 0;
                int k = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (entr[i] >= max) { max = entr[i]; k = i; }
                }
                k = k + 1;
                prvi.par(k-1);
                int m = 0;
                for (int j = 0; j < 25; j++)
                {
                    tip[j] = 0;
                    d[j] = 0;
                }
                if (k == 1)
                {
                    m = 9;
                    for (int i = 0; i < n; i++)
                    {
                        if (podaci[i][0] <= 1) { tip[0]++; if (podaci[i][6] == 1) d[0]++; }
                        if (podaci[i][0] <= 3 && podaci[i][0] > 1) { tip[1]++; if (podaci[i][6] == 1) d[1]++; }
                        if (podaci[i][0] <= 5 && podaci[i][0] > 3) { tip[2]++; if (podaci[i][6] == 1) d[2]++; }
                        if (podaci[i][0] <= 8 && podaci[i][0] > 5) { tip[3]++; if (podaci[i][6] == 1) d[3]++; }
                        if (podaci[i][0] <= 11 && podaci[i][0] > 8) { tip[4]++; if (podaci[i][6] == 1) d[4]++; }
                        if (podaci[i][0] <= 15 && podaci[i][0] > 11) { tip[5]++; if (podaci[i][6] == 1) d[5]++; }
                        if (podaci[i][0] <= 20 && podaci[i][0] > 15) { tip[6]++; if (podaci[i][6] == 1) d[6]++; }
                        if (podaci[i][0] <= 30 && podaci[i][0] > 20) { tip[7]++; if (podaci[i][6] == 1) d[7]++; }
                        if (podaci[i][0] > 30) { tip[8]++; if (podaci[i][6] == 1) d[8]++; }
                    }
                }
                if (k == 2)
                {
                    m = 9;
                    for (int i = 0; i < n; i++)
                    {
                        if (podaci[i][1] >= 70) { tip[0]++; if (podaci[i][6] == 1) d[0]++; }
                        if (podaci[i][1] >= 50 && podaci[i][1] < 70) { tip[1]++; if (podaci[i][6] == 1) d[1]++; }
                        if (podaci[i][1] >= 30 && podaci[i][1] < 50) { tip[2]++; if (podaci[i][6] == 1) d[2]++; }
                        if (podaci[i][1] >= 10 && podaci[i][1] < 30) { tip[3]++; if (podaci[i][6] == 1) d[3]++; }
                        if (podaci[i][1] >= -10 && podaci[i][1] < 10) { tip[4]++; if (podaci[i][6] == 1) d[4]++; }
                        if (podaci[i][1] >= -30 && podaci[i][1] < -10) { tip[5]++; if (podaci[i][6] == 1) d[5]++; }
                        if (podaci[i][1] >= -50 && podaci[i][1] < -30) { tip[6]++; if (podaci[i][6] == 1) d[6]++; }
                        if (podaci[i][1] >= -70 && podaci[i][1] < -50) { tip[7]++; if (podaci[i][6] == 1) d[7]++; }
                        if (podaci[i][1] < -70) { tip[8]++; if (podaci[i][6] == 1) d[8]++; }
                    }
                }
                if (k == 3)
                {
                    m = 25;
                    for (int i = 0; i < n; i++)
                    {
                        if (podaci[i][2] == 1) { tip[0]++; if (podaci[i][6] == 1) d[0]++; }
                        if (podaci[i][2] == 2) { tip[1]++; if (podaci[i][6] == 1) d[1]++; }
                        if (podaci[i][2] == 3) { tip[2]++; if (podaci[i][6] == 1) d[2]++; }
                        if (podaci[i][2] == 4) { tip[3]++; if (podaci[i][6] == 1) d[3]++; }
                        if (podaci[i][2] == 5) { tip[4]++; if (podaci[i][6] == 1) d[4]++; }
                        if (podaci[i][2] == 6) { tip[5]++; if (podaci[i][6] == 1) d[5]++; }
                        if (podaci[i][2] == 7) { tip[6]++; if (podaci[i][6] == 1) d[6]++; }
                        if (podaci[i][2] == 8) { tip[7]++; if (podaci[i][6] == 1) d[7]++; }
                        if (podaci[i][2] == 9) { tip[8]++; if (podaci[i][6] == 1) d[8]++; }
                        if (podaci[i][2] == 10) { tip[9]++; if (podaci[i][6] == 1) d[9]++; }
                        if (podaci[i][2] == 11) { tip[10]++; if (podaci[i][6] == 1) d[10]++; }
                        if (podaci[i][2] == 12) { tip[11]++; if (podaci[i][6] == 1) d[11]++; }
                        if (podaci[i][2] == 13) { tip[12]++; if (podaci[i][6] == 1) d[12]++; }
                        if (podaci[i][2] == 14) { tip[13]++; if (podaci[i][6] == 1) d[13]++; }
                        if (podaci[i][2] == 15) { tip[14]++; if (podaci[i][6] == 1) d[14]++; }
                        if (podaci[i][2] == 16) { tip[15]++; if (podaci[i][6] == 1) d[15]++; }
                        if (podaci[i][2] == 17) { tip[16]++; if (podaci[i][6] == 1) d[16]++; }
                        if (podaci[i][2] == 18) { tip[17]++; if (podaci[i][6] == 1) d[17]++; }
                        if (podaci[i][2] == 19) { tip[18]++; if (podaci[i][6] == 1) d[18]++; }
                        if (podaci[i][2] == 20) { tip[19]++; if (podaci[i][6] == 1) d[19]++; }
                        if (podaci[i][2] == 21) { tip[20]++; if (podaci[i][6] == 1) d[20]++; }
                        if (podaci[i][2] == 22) { tip[21]++; if (podaci[i][6] == 1) d[21]++; }
                        if (podaci[i][2] == 23) { tip[22]++; if (podaci[i][6] == 1) d[22]++; }
                        if (podaci[i][2] == 24) { tip[23]++; if (podaci[i][6] == 1) d[23]++; }
                        if (podaci[i][2] == 25) { tip[24]++; if (podaci[i][6] == 1) d[24]++; }
                    }
                }
                if (k == 5)
                {
                    m = 9;
                    for (int i = 0; i < n; i++)
                    {
                        if (podaci[i][3] <= 4) { tip[0]++; if (podaci[i][6] == 1) d[0]++; }
                        if (podaci[i][3] <= 8 && podaci[i][3] > 4) { tip[1]++; if (podaci[i][6] == 1) d[1]++; }
                        if (podaci[i][3] <= 12 && podaci[i][3] > 8) { tip[2]++; if (podaci[i][6] == 1) d[2]++; }
                        if (podaci[i][3] <= 16 && podaci[i][3] > 12) { tip[3]++; if (podaci[i][6] == 1) d[3]++; }
                        if (podaci[i][3] <= 18 && podaci[i][3] > 16) { tip[4]++; if (podaci[i][6] == 1) d[4]++; }
                        if (podaci[i][3] <= 20 && podaci[i][3] > 18) { tip[5]++; if (podaci[i][6] == 1) d[5]++; }
                        if (podaci[i][3] <= 22 && podaci[i][3] > 20) { tip[6]++; if (podaci[i][6] == 1) d[6]++; }
                        if (podaci[i][3] <= 24 && podaci[i][3] > 22) { tip[7]++; if (podaci[i][6] == 1) d[7]++; }
                        if (podaci[i][3] <= 28 && podaci[i][3] > 24) { tip[8]++; if (podaci[i][6] == 1) d[8]++; }
                        if (podaci[i][3] <= 32 && podaci[i][3] > 28) { tip[9]++; if (podaci[i][6] == 1) d[9]++; }
                        if (podaci[i][3] <= 36 && podaci[i][3] > 32) { tip[10]++; if (podaci[i][6] == 1) d[10]++; }
                        if (podaci[i][3] <= 40 && podaci[i][3] > 36) { tip[11]++; if (podaci[i][6] == 1) d[11]++; }
                        if (podaci[i][3] >= 40) { tip[12]++; if (podaci[i][6] == 1) d[12]++; }
                    }
                }
                if (k == 6)
                {
                    m = 9;
                    for (int i = 0; i < n; i++)
                    {
                        if (podaci[i][4] <= 0.5) { tip[0]++; if (podaci[i][6] == 1) d[0]++; }
                        if (podaci[i][4] <= 1 && podaci[i][4] > 0.5) { tip[1]++; if (podaci[i][6] == 1) d[1]++; }
                        if (podaci[i][4] <= 2 && podaci[i][4] > 1) { tip[2]++; if (podaci[i][6] == 1) d[2]++; }
                        if (podaci[i][4] <= 3 && podaci[i][4] > 2) { tip[3]++; if (podaci[i][6] == 1) d[3]++; }
                        if (podaci[i][4] <= 4 && podaci[i][4] > 3) { tip[4]++; if (podaci[i][6] == 1) d[4]++; }
                        if (podaci[i][4] <= 5 && podaci[i][4] > 4) { tip[5]++; if (podaci[i][6] == 1) d[5]++; }
                        if (podaci[i][4] <= 6 && podaci[i][4] > 5) { tip[6]++; if (podaci[i][6] == 1) d[6]++; }
                        if (podaci[i][4] <= 7 && podaci[i][4] > 6) { tip[7]++; if (podaci[i][6] == 1) d[7]++; }
                        if (podaci[i][4] <= 8 && podaci[i][4] > 7) { tip[8]++; if (podaci[i][6] == 1) d[8]++; }
                        if (podaci[i][4] > 8) { tip[8]++; if (podaci[i][6] == 1) d[9]++; }
                    }
                }
                prvi.br(m);
                for (int i = 0; i < m; i++)
                {
                    prvi.s[i] = new Elem();
                    prvi.s[i].verov((double)tip[i] / n * prvi.verjetnost());
                    prvi.s[i].gr(i);
                    prvi.s[i].otac = prvi;
                    if (d[i] == tip[i]) { prvi.s[i].par(-1); ver += prvi.s[i].verjetnost(); }
                    else if (d[i] == 0) { prvi.s[i].par(-2); }
                    else { prvi.s[i].par(-3); }
                    obradi(prvi.s[i]);
                }
            }
        }
        private bool Opodataka()
        {
            string[] text=System.IO.File.ReadAllLines(@openFileDialog1.FileName);
            n=text.Length;
            podaci=new double [n][];
            for (int i = 0; i < n; i++) 
            {
                string[] s = text[i].Split(' ');
                podaci[i] = new double[7];
                try 
                {
                    for (int j = 0; j < 7; j++)
                    {
                        podaci[i][j]=double.Parse(s[j]);
                    }
                }
                catch (Exception ee) 
                {
                    return false;
                }
            }
            //obrada podataka
            Elem prvi = new Elem();
            prvi.verov(1);
            prvi.gr(-1);
            prvi.par(-3);
            prvi.otac = null;
            obradi(prvi);
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            label4.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName != "")
            {
                if (Opodataka()==false) 
                {
                    label4.ForeColor = Color.Red;
                    label4.Text="Podaci so napačni";
                }
                else 
                {
                    label4.ForeColor = Color.Green;
                    label4.Text = "Verjetnost zadetka je "+ver+".";
                }
            }
            else
            {
                label4.ForeColor = Color.Red;
                label4.Text = "Niste naložili datoteko";
            }
        }
   }
}
