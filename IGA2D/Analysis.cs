using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Integration;

namespace IGA2D
{
    public class Node : ICloneable
    {
        public int i;
        public int j;
        public int n;
        public double x;
        public double y;
        public Node(int tmpn, int tmpi, int tmpj, double tmpx, double tmpy)
        {
            n = tmpn;
            i = tmpi;
            j = tmpj;
            x = tmpx;
            y = tmpy;
        }
        public object Clone() { return new Node(n, i, j, x, y) as object; }
    }
    public class Material
    {
        private int n;
        public double rho;
        public double E;
        public double v;
        public Material(int tmpn, double tmprho, double tmpE, double tmpv)
        {
            n = tmpn;
            rho = tmprho;
            E = tmpE;
            v = tmpv;
        }
    }
    public class IGAElement
    {
        public int n;
        public List<Node> Nodes;
        private Material Mat;
        private Matrix<double> D;
        private NurbsShapeFunction NurbsX;
        private NurbsShapeFunction NurbsY;
        private double t;
        public Matrix<double> N(double r, double s)
        {
            List<double> Row1 = new List<double>();
            List<double> Row2 = new List<double>();
            foreach(Node CP in Nodes)
            {
                double Ni = NurbsX.R(0, CP.n / NurbsY.Count, r) * NurbsY.R(0, CP.n % NurbsY.Count, s);
                Row1.Add(Ni); Row1.Add(0);
                Row2.Add(0); Row2.Add(Ni);
            }
            double[][] Rows = new double[2][];
            Rows[0] = Row1.ToArray();
            Rows[1] = Row2.ToArray();
            return Matrix<double>.Build.DenseOfRowArrays(Rows);
        }
        public Matrix<double> B(double r, double s)
        {
            Matrix<double> Ji = J(r, s).Inverse();
            List<double> Row1 = new List<double>();
            List<double> Row2 = new List<double>();
            List<double> Row3 = new List<double>();
            foreach (Node CP in Nodes)
            {
                double dRr = NurbsX.R(1, CP.n / NurbsY.Count, r) * NurbsY.R(0, CP.n % NurbsY.Count, s);
                double dRs = NurbsX.R(0, CP.n / NurbsY.Count, r) * NurbsY.R(1, CP.n % NurbsY.Count, s);
                double dRx = Ji[0, 0] * dRr + Ji[0, 1] * dRs;
                double dRy = Ji[1, 0] * dRr + Ji[1, 1] * dRs;
                Row1.Add(dRx); Row1.Add(0);
                Row2.Add(0); Row2.Add(dRy);
                Row3.Add(dRy); Row3.Add(dRx);
            }
            double[][] Rows = new double[3][];
            Rows[0] = Row1.ToArray();
            Rows[1] = Row2.ToArray();
            Rows[2] = Row3.ToArray();
            return Matrix<double>.Build.DenseOfRowArrays(Rows);
        }
        public Matrix<double> J(double r, double s)
        {
            double J11 = 0;
            double J12 = 0;
            double J21 = 0;
            double J22 = 0;
            foreach (Node CP in Nodes)
            {
                J11 += NurbsX.R(1, CP.n / NurbsY.Count, r) * NurbsY.R(0, CP.n % NurbsY.Count, s) * CP.x;
                J12 += NurbsX.R(1, CP.n / NurbsY.Count, r) * NurbsY.R(0, CP.n % NurbsY.Count, s) * CP.y;
                J21 += NurbsX.R(0, CP.n / NurbsY.Count, r) * NurbsY.R(1, CP.n % NurbsY.Count, s) * CP.x;
                J22 += NurbsX.R(0, CP.n / NurbsY.Count, r) * NurbsY.R(1, CP.n % NurbsY.Count, s) * CP.y;
            }
            return Matrix<double>.Build.DenseOfArray(new double[,] {
                       { J11, J12 },
                       { J21, J22 } });
        }
        public Matrix<double> K()
        {
            Matrix<double> tmpK = Matrix<double>.Build.Dense(2 * Nodes.Count, 2 * Nodes.Count, 0);
            List<double> ParaSpaceX = (new HashSet<double>(NurbsX.KnotVector)).ToList();
            List<double> ParaSpaceY = (new HashSet<double>(NurbsY.KnotVector)).ToList();
            GaussLegendreRule IntegrateR = new GaussLegendreRule(ParaSpaceX[n / (ParaSpaceY.Count - 1)], ParaSpaceX[(n / (ParaSpaceY.Count - 1)) + 1], NurbsX.Order + 1);
            GaussLegendreRule IntegrateS = new GaussLegendreRule(ParaSpaceY[n % (ParaSpaceY.Count - 1)], ParaSpaceY[(n % (ParaSpaceY.Count - 1)) + 1], NurbsY.Order + 1);
            for (int i = 0; i != IntegrateR.Abscissas.Count(); ++i)
                for (int j = 0; j != IntegrateS.Abscissas.Count(); ++j)
                {
                    Matrix<double> tmpB = B(IntegrateR.Abscissas[i], IntegrateS.Abscissas[j]);
                    tmpK += IntegrateR.Weights[i] * IntegrateS.Weights[j] * tmpB.Transpose() * D * tmpB * J(IntegrateR.Abscissas[i], IntegrateS.Abscissas[j]).Determinant() * t;
                }
            return tmpK;
        }
        private Vector<double> Stress(double r, double s, Vector<double> Disp)
        {
            Matrix<double> S = D * B(r, s);
            Vector<double> Sigma = Vector<double>.Build.Dense(3, 0);
            for (int i = 0; i != Nodes.Count; ++i)
            {
                Sigma[0] += S[0, i * 2] * Disp[Nodes[i].n * 2];
                Sigma[1] += S[1, i * 2 + 1] * Disp[Nodes[i].n * 2 + 1];
                Sigma[2] += S[2, i * 2] * Disp[Nodes[i].n * 2] + S[2, i * 2 + 1] * Disp[Nodes[i].n * 2 + 1];
            }
            return Sigma;
        }
        public List<List<double>> StressSample(Vector<double> Disp)
        {
            List<List<double>> Pxys = new List<List<double>>();
            List<double> ParaSpaceX = (new HashSet<double>(NurbsX.KnotVector)).ToList();
            List<double> ParaSpaceY = (new HashSet<double>(NurbsY.KnotVector)).ToList();
            GaussLegendreRule IntegrateR = new GaussLegendreRule(ParaSpaceX[n / (ParaSpaceY.Count - 1)], ParaSpaceX[(n / (ParaSpaceY.Count - 1)) + 1], NurbsX.Order + 1);
            GaussLegendreRule IntegrateS = new GaussLegendreRule(ParaSpaceY[n % (ParaSpaceY.Count - 1)], ParaSpaceY[(n % (ParaSpaceY.Count - 1)) + 1], NurbsY.Order + 1);
            for (int i = 0; i != IntegrateR.Abscissas.Count(); ++i)
                for (int j = 0; j != IntegrateS.Abscissas.Count(); ++j)
                    Pxys.Add(new List<double> {
                        IntegrateR.Abscissas[i],
                        IntegrateS.Abscissas[j],
                        Stress(IntegrateR.Abscissas[i], IntegrateS.Abscissas[j], Disp)[0],
                        Stress(IntegrateR.Abscissas[i], IntegrateS.Abscissas[j], Disp)[1],
                        Stress(IntegrateR.Abscissas[i], IntegrateS.Abscissas[j], Disp)[2] });
            return Pxys;
        }
        public IGAElement(int tmpn, List<Node> tmpNodes, Material tmpMat, double tmpt, NurbsShapeFunction tmpNurbsX, NurbsShapeFunction tmpNurbsY)
        {
            n = tmpn;
            Nodes = tmpNodes;
            Mat = tmpMat;
            NurbsX = tmpNurbsX;
            NurbsY = tmpNurbsY;
            t = tmpt;
            D = Mat.E / (1 - Mat.v * Mat.v) * Matrix<double>.Build.DenseOfArray(new double[,] {
                                                                 { 1.0, Mat.v, 0.0 }, 
                                                                 { Mat.v, 1.0, 0.0 }, 
                                                                 { 0.0, 0.0, (1 - Mat.v) / 2 } });
        }
    }
    public class Load
    {
        private int n;
        private Node m_LoadNode;
        private string m_Dir;
        private double m_Mag;
        public Vector<double> Assembly(Vector<double> F)
        {
            if (m_Dir == "x") F[(m_LoadNode.n + 1) * 2 - 2] = m_Mag;
            else F[(m_LoadNode.n + 1) * 2 - 1] = m_Mag;
            return F;
        }
        public Load(int tmpn, Node LoadNode, string Dir, double Mag)
        {
            n = tmpn;
            m_LoadNode = LoadNode;
            m_Dir = Dir;
            m_Mag = Mag;
        }
    }
    public class Restraint
    {
        private int n;
        private Node m_ResNode;
        private string m_Dir;
        public Matrix<double> KProcessing(Matrix<double> K)
        {
            int dof;
            if (m_Dir == "x") dof = (m_ResNode.n + 1) * 2 - 2;
            else dof = (m_ResNode.n + 1) * 2 - 1;
            Vector<double> Zero = Vector<double>.Build.Dense(K.RowCount, 0);
            K.SetRow(dof, Zero);
            K.SetColumn(dof, Zero);
            K[dof, dof] = 1;
            return K;
        }
        public Vector<double> ForceProcessing(Vector<double> F)
        {
            if (m_Dir == "x") F[(m_ResNode.n + 1) * 2 - 2] = 0;
            else F[(m_ResNode.n + 1) * 2 - 1] = 0;
            return F;
        }
        public Restraint(int tmpn, Node ResNode, string Dir)
        {
            n = tmpn;
            m_ResNode = ResNode;
            m_Dir = Dir;
        }
    }
    public class Instance
    {
        public List<List<Node>> Nodes;
        public List<IGAElement> Eles;
        private List<Load> m_Loads;
        private List<Restraint> m_Restraints;
        private Matrix<double> m_GlobalK;
        private Matrix<double> m_GlobalK_Pro;
        public Vector<double> Disp;
        public Vector<double> Force;
        private Vector<double> m_Force_Pro;
        public Vector<double> Reaction;
        public void Calculate()
        {
            int Dim = 2 * Nodes.Count * Nodes[0].Count;
            m_GlobalK = Matrix<double>.Build.Dense(Dim, Dim, 0);
            Force = Vector<double>.Build.Dense(Dim, 0);
            Disp = Vector<double>.Build.Dense(Dim, 0);
            //Global Stiffness Matrix
            foreach (IGAElement Ele in Eles)
            {
                Matrix<double> LocalK = Ele.K();
                for (int i = 0; i != Ele.Nodes.Count; ++i)
                    for (int j = 0; j != Ele.Nodes.Count; ++j)
                    {
                        m_GlobalK[Ele.Nodes[i].n * 2, Ele.Nodes[j].n * 2] += LocalK[i * 2, j * 2];
                        m_GlobalK[Ele.Nodes[i].n * 2, Ele.Nodes[j].n * 2 + 1] += LocalK[i * 2, j * 2 + 1];
                        m_GlobalK[Ele.Nodes[i].n * 2 + 1, Ele.Nodes[j].n * 2] += LocalK[i * 2 + 1, j * 2];
                        m_GlobalK[Ele.Nodes[i].n * 2 + 1, Ele.Nodes[j].n * 2 + 1] += LocalK[i * 2 + 1, j * 2 + 1];
                    }
            }
            //Force Vector
            foreach (Load load in m_Loads)
                Force = load.Assembly(Force);
            //Processing
            m_Force_Pro = Vector<double>.Build.DenseOfVector(Force);
            m_GlobalK_Pro = Matrix<double>.Build.DenseOfMatrix(m_GlobalK);
            foreach (Restraint res in m_Restraints)
            {
                m_Force_Pro = res.ForceProcessing(m_Force_Pro);
                m_GlobalK_Pro = res.KProcessing(m_GlobalK_Pro);
            }
            //Calculate
            Disp = m_GlobalK_Pro.QR().Solve(m_Force_Pro);
            Vector<double> tmpForce = m_GlobalK * Disp;
            Reaction = tmpForce - Force;
        }
        public Instance(List<List<Node>> tmpNodes, NurbsShapeFunction NurbsX, NurbsShapeFunction NurbsY, Material Mat, double t, List<Load> Loads, List<Restraint> Restraints)
        {
            Nodes = tmpNodes;
            Eles = new List<IGAElement>();
            m_Loads = Loads;
            m_Restraints = Restraints;
            //Create IGAElement
            List<double> ParaSpaceX = (new HashSet<double>(NurbsX.KnotVector)).ToList();
            List<double> ParaSpaceY = (new HashSet<double>(NurbsY.KnotVector)).ToList();
            Dictionary<double, int> mi = new Dictionary<double, int>();
            Dictionary<double, int> mj = new Dictionary<double, int>();
            foreach (double value in NurbsX.KnotVector)
                if (mi.ContainsKey(value)) mi[value] += 1;
                else mi.Add(value, 1);
            foreach (double value in NurbsY.KnotVector)
                if (mj.ContainsKey(value)) mj[value] += 1;
                else mj.Add(value, 1);
            for (int i = 0; i != ParaSpaceX.Count - 1; ++i)
                for (int j = 0; j != ParaSpaceY.Count - 1; ++j)
                {
                    List<Node> tmpEleNodes = new List<Node>();
                    int rEnd = 0;
                    int sEnd = 0;
                    for (int h = 0; h != i + 1; ++h)
                        rEnd += mi[ParaSpaceX[h]];
                    for (int h = 0; h != j + 1; ++h)
                        sEnd += mj[ParaSpaceY[h]];
                    int rBegin = rEnd - (NurbsX.Order + 1);
                    int sBegin = sEnd - (NurbsY.Order + 1);
                    for (int r =rBegin; r != rEnd; ++r) 
                        for (int s = sBegin; s != sEnd; ++s)
                            tmpEleNodes.Add(tmpNodes[r][s]);
                    Eles.Add(new IGAElement(i * (ParaSpaceY.Count - 1) + j, tmpEleNodes,Mat, t, NurbsX, NurbsY));
                }
        }
    }
}
