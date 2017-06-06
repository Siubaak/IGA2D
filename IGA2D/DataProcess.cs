using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace IGA2D
{
    public class DataProcess
    {
        private NurbsShapeFunction NurbsX;
        private NurbsShapeFunction NurbsY;
        public Bitmap DispPicture;
        public Bitmap SxPicture;
        public Bitmap SyPicture;
        public Bitmap TxyPicture;
        public Instance CreateInstance(string Input)
        {
            string[] Blocks = Input.Split('#');
            //Modeling Parameters
            //Order of NURBS
            int pX = Convert.ToInt32(Blocks[1].Split('\r')[1].Split('=')[1]);
            int pY = Convert.ToInt32(Blocks[1].Split('\r')[2].Split('=')[1]);
            //Knot Vector
            List<double> KnotVectorX = new List<double>(Array.ConvertAll(Blocks[2].Split('\r')[1].Split('=')[1].Split(','), i => Convert.ToDouble(i)));
            List<double> KnotVectorY = new List<double>(Array.ConvertAll(Blocks[2].Split('\r')[2].Split('=')[1].Split(','), i => Convert.ToDouble(i)));
            //Weights
            List<double> WeightsX = new List<double>(Array.ConvertAll(Blocks[3].Split('\r')[1].Split('=')[1].Split(','), i => Convert.ToDouble(i)));
            List<double> WeightsY = new List<double>(Array.ConvertAll(Blocks[3].Split('\r')[2].Split('=')[1].Split(','), i => Convert.ToDouble(i)));
            //Nurbs
            NurbsX = new NurbsShapeFunction(KnotVectorX, WeightsX, pX);
            NurbsY = new NurbsShapeFunction(KnotVectorY, WeightsY, pY);
            //Contorl Point
            string[] Ps = Blocks[4].Split('(');
            List<List<Node>>CPs = new List<List<Node>>();
            for (int k = 1; k != Ps.Count(); k += NurbsY.Count)
            {
                List<Node> CPj = new List<Node>();
                for (int j = 0; j != NurbsY.Count; ++j)
                    CPj.Add(new Node(k + j - 1, (k - 1) / NurbsY.Count, j, Convert.ToDouble(Ps[k + j].Split(')')[0].Split(',')[0]), Convert.ToDouble(Ps[k + j].Split(')')[0].Split(',')[1])));
                CPs.Add(CPj);
            }
            //Analysis Parameters
            //Material
            string[] MatPara = Blocks[5].Split('\r');
            Material Mat = new Material(0, Convert.ToDouble(MatPara[1].Split('=')[1]), Convert.ToDouble(MatPara[2].Split('=')[1]), Convert.ToDouble(MatPara[3].Split('=')[1]));
            double t = Convert.ToDouble(MatPara[4].Split('=')[1]);
            //Load
            string[] LoadPara = Blocks[6].Split('\r');
            List<Load> Loads = new List<Load>();
            for (int k = 1; k != LoadPara.Count() - 2; ++k)
                Loads.Add(new Load(k - 1, CPs[Convert.ToInt32(LoadPara[k].Split(',')[0].Split('\n')[1]) / NurbsY.Count][Convert.ToInt32(LoadPara[k].Split(',')[0].Split('\n')[1]) % NurbsY.Count], LoadPara[k].Split(',')[1], Convert.ToDouble(LoadPara[k].Split(',')[2])));
            //Restriant
            string[] ResPara = Blocks[7].Split('\r');
            List<Restraint> Restraints = new List<Restraint>();
            for (int k = 1; k != ResPara.Count() - 2; ++k)
                Restraints.Add(new Restraint(k - 1, CPs[Convert.ToInt32(ResPara[k].Split(',')[0].Split('\n')[1]) / NurbsY.Count][Convert.ToInt32(ResPara[k].Split(',')[0].Split('\n')[1]) % NurbsY.Count], ResPara[k].Split(',')[1]));
            //Create Instance
            return new Instance(CPs, NurbsX, NurbsY, Mat, t, Loads, Restraints);
        }
        private List<double> xy(double r, double s, List<List<Node>> CPs)
        {
            List<double> xys = new List<double>();
            xys.Add(0);
            xys.Add(0);
            for (int i = 0; i != NurbsX.Count; ++i)
                for (int j = 0; j != NurbsY.Count; ++j)
                {
                    double NurbsR = NurbsX.R(0, i, r) * NurbsY.R(0, j, s);
                    xys[0] += NurbsR * CPs[i][j].x;
                    xys[1] += NurbsR * CPs[i][j].y;
                }
            return xys;
        }
        private List<List<double>> ModelDraw(List<List<Node>> tmpCPs)
        {
            List<List<double>> Draw = new List<List<double>>();
            List<double> DrawLX = (new HashSet<double>(NurbsX.KnotVector)).ToList();
            List<double> DrawLY = (new HashSet<double>(NurbsY.KnotVector)).ToList();
            //Draw Vertical Lines
            foreach (double Cox in DrawLX)
            {
                List<double> Pxy = new List<double>();
                for (double Coy = DrawLY[0]; Coy < DrawLY.Last(); Coy += DrawLY.Last() / 48)
                {
                    List<double> tmpxy = xy(Cox, Coy, tmpCPs);
                    Pxy.Add(tmpxy[0]);
                    Pxy.Add(tmpxy[1]);
                }
                List<double> ttmpxy = xy(Cox, DrawLY.Last(), tmpCPs);
                Pxy.Add(ttmpxy[0]);
                Pxy.Add(ttmpxy[1]);
                Draw.Add(Pxy);
            }
            //Draw Horizontal Lines
            foreach (double Coy in DrawLY)
            {
                List<double> Pxy = new List<double>();
                for (double Cox = DrawLX[0]; Cox < DrawLX.Last(); Cox += DrawLX.Last() / 48)
                {
                    List<double> tmpxy = xy(Cox, Coy, tmpCPs);
                    Pxy.Add(tmpxy[0]);
                    Pxy.Add(tmpxy[1]);
                }
                List<double> ttmpxy = xy(DrawLX.Last(), Coy, tmpCPs);
                Pxy.Add(ttmpxy[0]);
                Pxy.Add(ttmpxy[1]);
                Draw.Add(Pxy);
            }
            return Draw;
        }
        public void Draw(Instance Ins, double ScaleF)
        {
            //Obtain Data
            List<List<double>> OriginDraw = ModelDraw(Ins.Nodes);
            List<List<Node>> tmpCPs = new List<List<Node>>();
            foreach(List<Node> item in Ins.Nodes)
            {
                List<Node> tmpItem = new List<Node>();
                item.ForEach(i => tmpItem.Add((Node)i.Clone()));
                tmpCPs.Add(tmpItem);
            }
            for (int i = 0; i != tmpCPs.Count; ++i)
                for (int j = 0; j != tmpCPs[0].Count; ++j)
                {
                    tmpCPs[i][j].x += ScaleF * Ins.Disp[tmpCPs[i][j].n * 2];
                    tmpCPs[i][j].y += ScaleF * Ins.Disp[tmpCPs[i][j].n * 2 + 1];
                }
            List<List<double>> DeformedDraw = ModelDraw(tmpCPs);
            List<List<List<double>>> StressDraw = new List<List<List<double>>>();
            foreach (IGAElement Ele in Ins.Eles)
                StressDraw.Add(Ele.StressSample(Ins.Disp));
            double Sxmin = StressDraw[0][0][2];
            double Sxmax = StressDraw[0][0][2];
            double Symin = StressDraw[0][0][3];
            double Symax = StressDraw[0][0][3];
            double Txymin = StressDraw[0][0][4];
            double Txymax = StressDraw[0][0][4];
            foreach (List<List<double>> EleStress in StressDraw)
                foreach (List<double> Pxys in EleStress)
                {
                    Sxmin = Sxmin > Pxys[2] ? Pxys[2] : Sxmin;
                    Sxmax = Sxmax < Pxys[2] ? Pxys[2] : Sxmax;
                    Symin = Symin > Pxys[3] ? Pxys[3] : Symin;
                    Symax = Symax < Pxys[3] ? Pxys[3] : Symax;
                    Txymin = Txymin > Pxys[4] ? Pxys[4] : Txymin;
                    Txymax = Txymax < Pxys[4] ? Pxys[4] : Txymax;
                }
            double ScaleSx = 255 / (Sxmax - Sxmin);
            double ScaleSy = 255 / (Symax - Symin);
            double ScaleTxy = 255 / (Txymax - Txymin);
            foreach (List<List<double>> EleStress in StressDraw)
                for (int h = 0; h != EleStress.Count; ++h)
                {
                    List<double> tmpxy = xy(EleStress[h][0], EleStress[h][1], tmpCPs);
                    EleStress[h][0] = tmpxy[0];
                    EleStress[h][1] = tmpxy[1];
                }
            //Initial
            int Size = 500;
            int Margin = 50;
            Bitmap DispPic = new Bitmap(Size, Size);
            Bitmap SxPic = new Bitmap(Size, Size);
            Bitmap SyPic = new Bitmap(Size, Size);
            Bitmap TxyPic = new Bitmap(Size, Size);
            Graphics GDisp = Graphics.FromImage(DispPic);
            Graphics GSx = Graphics.FromImage(SxPic);
            Graphics GSy = Graphics.FromImage(SyPic);
            Graphics GTxy = Graphics.FromImage(TxyPic);
            GDisp.SmoothingMode = SmoothingMode.AntiAlias;
            GSx.SmoothingMode = SmoothingMode.AntiAlias;
            GSy.SmoothingMode = SmoothingMode.AntiAlias;
            GTxy.SmoothingMode = SmoothingMode.AntiAlias;
            LinearGradientBrush linGrBrush = new LinearGradientBrush(new Point(124, 0), new Point(376, 0), Color.FromArgb(0, 0, 255), Color.FromArgb(255, 0, 0));
            GSx.FillRectangle(linGrBrush, 125, 470, 250, 20);
            GSy.FillRectangle(linGrBrush, 125, 470, 250, 20);
            GTxy.FillRectangle(linGrBrush, 125, 470, 250, 20);
            Rectangle StringBoxLeft = new Rectangle(0, 470, 125, 21);
            Rectangle StringBoxRight = new Rectangle(377, 470, 125, 21);
            StringFormat StringFormatLeft = new StringFormat();
            StringFormatLeft.Alignment = StringAlignment.Far;
            StringFormatLeft.LineAlignment = StringAlignment.Center;
            StringFormat StringFormatRight = new StringFormat();
            StringFormatRight.Alignment = StringAlignment.Near;
            StringFormatRight.LineAlignment = StringAlignment.Center;
            GSx.DrawString(Sxmin.ToString("E"), new Font("宋体", 9), new SolidBrush(Color.Black), StringBoxLeft, StringFormatLeft);
            GSx.DrawString(Sxmax.ToString("E"), new Font("宋体", 9), new SolidBrush(Color.Black), StringBoxRight, StringFormatRight);
            GSy.DrawString(Symin.ToString("E"), new Font("宋体", 9), new SolidBrush(Color.Black), StringBoxLeft, StringFormatLeft);
            GSy.DrawString(Symax.ToString("E"), new Font("宋体", 9), new SolidBrush(Color.Black), StringBoxRight, StringFormatRight);
            GTxy.DrawString(Txymin.ToString("E"), new Font("宋体", 9), new SolidBrush(Color.Black), StringBoxLeft, StringFormatLeft);
            GTxy.DrawString(Txymax.ToString("E"), new Font("宋体", 9), new SolidBrush(Color.Black), StringBoxRight, StringFormatRight);
            //Origin Offset
            GDisp.Transform = new Matrix(1, 0, 0, -1, 0, 0);
            GSx.Transform = new Matrix(1, 0, 0, -1, 0, 0);
            GSy.Transform = new Matrix(1, 0, 0, -1, 0, 0);
            GTxy.Transform = new Matrix(1, 0, 0, -1, 0, 0);
            double minX = OriginDraw[0][0], maxX = OriginDraw[0][0], minY = OriginDraw[0][1], maxY = OriginDraw[0][1];
            foreach (List<double> Line in OriginDraw)
                for (int h = 0; h != Line.Count; h += 2)
                {
                    minX = minX > Line[h] ? Line[h] : minX;
                    maxX = maxX < Line[h] ? Line[h] : maxX;
                    minY = minY > Line[h + 1] ? Line[h + 1] : minY;
                    maxY = maxY < Line[h + 1] ? Line[h + 1] : maxY;
                }
            foreach (List<double> Line in DeformedDraw)
                for (int h = 0; h != Line.Count; h += 2)
                {
                    minX = minX > Line[h] ? Line[h] : minX;
                    maxX = maxX < Line[h] ? Line[h] : maxX;
                    minY = minY > Line[h + 1] ? Line[h + 1] : minY;
                    maxY = maxY < Line[h + 1] ? Line[h + 1] : maxY;
                }
            double Scale = (Size - 2 * Margin) / ((maxX - minX) > (maxY - minY) ? (maxX - minX) : (maxY - minY));
            float OffsetX = (float)(Size / 2) + System.Math.Abs((float)(Scale * minX)) - (float)(Scale * (maxX - minX) / 2);
            float OffsetY = (float)(Size / 2) + System.Math.Abs((float)(Scale * maxY)) - (float)(Scale * (maxY - minY) / 2);
            GDisp.TranslateTransform(OffsetX, -OffsetY);
            GSx.TranslateTransform(OffsetX, -OffsetY);
            GSy.TranslateTransform(OffsetX, -OffsetY);
            GTxy.TranslateTransform(OffsetX, -OffsetY);
            //Draw Picture
            foreach (List<double> Line in OriginDraw)
                for (int h = 0; h != Line.Count - 2; h += 2)
                    GDisp.DrawLine(new Pen(Color.Blue), (float)(Line[h] * Scale), (float)(Line[h + 1] * Scale), (float)(Line[h + 2] * Scale), (float)(Line[h + 3] * Scale));
            int L = 10;
            foreach (List<List<double>> EleStress in StressDraw)
                foreach(List<double> Pxys in EleStress)
                {
                    GSx.FillEllipse(new SolidBrush(Color.FromArgb((int)(ScaleSx * (Sxmax - Pxys[2])), 0, (int)(ScaleSx * (Pxys[2] - Sxmin)))), (float)(Pxys[0] * Scale - L/2), (float)(Pxys[1] * Scale - L/2), L, L);
                    GSy.FillEllipse(new SolidBrush(Color.FromArgb((int)(ScaleSy * (Symax - Pxys[3])), 0, (int)(ScaleSy * (Pxys[3] - Symin)))), (float)(Pxys[0] * Scale - L/2), (float)(Pxys[1] * Scale - L/2), L, L);
                    GTxy.FillEllipse(new SolidBrush(Color.FromArgb((int)(ScaleTxy * (Txymax - Pxys[4])), 0, (int)(ScaleTxy * (Pxys[4] - Txymin)))), (float)(Pxys[0] * Scale - L/2), (float)(Pxys[1] * Scale - L/2), L, L);
                }
            foreach (List<double> Line in DeformedDraw)
                for (int h = 0; h != Line.Count - 2; h += 2)
                {
                    GDisp.DrawLine(new Pen(Color.Red), (float)(Line[h] * Scale), (float)(Line[h + 1] * Scale), (float)(Line[h + 2] * Scale), (float)(Line[h + 3] * Scale));
                    GSx.DrawLine(new Pen(Color.Black), (float)(Line[h] * Scale), (float)(Line[h + 1] * Scale), (float)(Line[h + 2] * Scale), (float)(Line[h + 3] * Scale));
                    GSy.DrawLine(new Pen(Color.Black), (float)(Line[h] * Scale), (float)(Line[h + 1] * Scale), (float)(Line[h + 2] * Scale), (float)(Line[h + 3] * Scale));
                    GTxy.DrawLine(new Pen(Color.Black), (float)(Line[h] * Scale), (float)(Line[h + 1] * Scale), (float)(Line[h + 2] * Scale), (float)(Line[h + 3] * Scale));
                }
            //Save
            DispPicture = DispPic;
            SxPicture = SxPic;
            SyPicture = SyPic;
            TxyPicture = TxyPic;
        }
        public string Output(Instance Ins)
        {
            string OP;
            OP = "IGA-2D v1.0 Result Info\r\n" +
                     "Unit(N, m)\r\n" +
                     "\r\n" +
                     "[Result]\r\n" +
                     "# Displacement\r\n";
            for (int i = 0; i != Ins.Disp.Count; ++i)
                if (i % 2 == 0) OP += Ins.Disp[i].ToString("E") + ",";
                else OP += Ins.Disp[i].ToString("E") + "\r\n";
            return OP;
        }
    }
}
