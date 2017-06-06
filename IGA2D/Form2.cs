using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGA2D
{
    public partial class Form2 : Form
    {
        private Form1 BasicForm;
        public Form2(Form1 tmp)
        {
            InitializeComponent();
            BasicForm = tmp;
            textBox1.Text = BasicForm.Input;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            BasicForm.Input = textBox1.Text;
            string[] Blocks = BasicForm.Input.Split('#');
            //Order of NURBS
            int pX = Convert.ToInt32(Blocks[1].Split('\r')[1].Split('=')[1]);
            int pY = Convert.ToInt32(Blocks[1].Split('\r')[2].Split('=')[1]);
            //Knot Vector
            List<double> KnotVectorX = new List<double>(Array.ConvertAll(Blocks[2].Split('\r')[1].Split('=')[1].Split(','), i => Convert.ToDouble(i)));
            List<double> KnotVectorY = new List<double>(Array.ConvertAll(Blocks[2].Split('\r')[2].Split('=')[1].Split(','), i => Convert.ToDouble(i)));
            HashSet<double> NodeX = new HashSet<double>(KnotVectorX);
            HashSet<double> NodeY = new HashSet<double>(KnotVectorY);
            //Weights
            List<double> WeightsX = new List<double>(Array.ConvertAll(Blocks[3].Split('\r')[1].Split('=')[1].Split(','), i => Convert.ToDouble(i)));
            List<double> WeightsY = new List<double>(Array.ConvertAll(Blocks[3].Split('\r')[2].Split('=')[1].Split(','), i => Convert.ToDouble(i)));
            //Contorl Point
            List<double> CPx = new List<double>(Array.ConvertAll(Blocks[4].Split('\r')[1].Split('=')[1].Split(','), i => Convert.ToDouble(i)));
            List<double> CPy = new List<double>(Array.ConvertAll(Blocks[4].Split('\r')[2].Split('=')[1].Split(','), i => Convert.ToDouble(i)));
            //Nurbs
            NurbsShapeFunction NurbsX = new NurbsShapeFunction(KnotVectorX, WeightsX);
            NurbsShapeFunction NurbsY = new NurbsShapeFunction(KnotVectorY, WeightsY);


            List<double> DrawPx = new List<double> { };
            List<double> DrawPy = new List<double> { };
            foreach (double x in NodeX)
            {
                for (double y = KnotVectorY[0]; y < KnotVectorY.Last(); y += KnotVectorY.Last() / 50)
                {
                    double tmpx = 0, tmpy = 0;
                    int k = 0;
                    for (int j = 0; j != WeightsY.Count; ++j)
                        for (int i = 0; i != WeightsX.Count; ++i)
                        {
                            double NurbsR = NurbsX.R(0, pX, i, x) * NurbsY.R(0, pY, j, y);
                            tmpx += NurbsR * CPx[k];
                            tmpy += NurbsR * CPy[k];
                            ++k;
                        }
                    DrawPx.Add(tmpx);
                    DrawPy.Add(tmpy);
                }
            }
                
            /*
            for (double x = KnotVectorX.Min(); x < KnotVectorX.Max(); x += KnotVectorX.Max() / 500)
                for (double y = KnotVectorY.Min(); y < KnotVectorY.Max(); y += KnotVectorY.Max() / 50)
                {
                    double tmpx = 0, tmpy = 0;
                    int k = 0;
                    for (int j = 0; j != WeightsY.Count; ++j)
                        for (int i = 0; i != WeightsX.Count; ++i)
                        {
                            double NurbsR = NurbsX.R(0, pX, i, x) * NurbsY.R(0, pY, j, y);
                            tmpx += NurbsR * CPx[k];
                            tmpy += NurbsR * CPy[k];
                            ++k;
                        }
                    DrawPx.Add(tmpx);
                    DrawPy.Add(tmpy);
                }
                */
            List<List<double>> DrawP = new List<List<double>> { DrawPx, DrawPy };
            BasicForm.bmp[1] = new Bitmap(551, 551);
            Graphics g = Graphics.FromImage(BasicForm.bmp[1]);
            
            this.Close();
        }
    }
}
