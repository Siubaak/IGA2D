using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGA2D
{
    public class BSplineShapeFunction
    {
        private List<double> m_KnotVector;
        public int Order;
        public int Count;
        public BSplineShapeFunction(List<double> KnotVector, int p)
        {
            m_KnotVector = KnotVector;
            Order = p;
            Count = KnotVector.Count - p - 1;
        }
        //计算B样条基函数及其导数值
        //k阶导数          （从0阶导数开始）
        //第i项基函数    （从第0项开始）
        //当x=t时
        public double N(int k, int i, double t)
        {
            return N(k, Order, i, t);
        }
        public double N(int k, int p, int i, double t)
        {
            if (k == 0)
            {
                if (p == 0)
                {
                    if (t >= m_KnotVector[i] && t <= m_KnotVector[i + 1]) return 1;
                    else return 0;
                }
                else
                {
                    if ((m_KnotVector[i + p] - m_KnotVector[i]) == 0 && (m_KnotVector[i + p + 1] - m_KnotVector[i + 1]) == 0)
                        return 0;
                    else if ((m_KnotVector[i + p] - m_KnotVector[i]) == 0)
                        return (m_KnotVector[i + p + 1] - t) / (m_KnotVector[i + p + 1] - m_KnotVector[i + 1]) * N(0, p - 1, i + 1, t);
                    else if ((m_KnotVector[i + p + 1] - m_KnotVector[i + 1]) == 0)
                        return (t - m_KnotVector[i]) / (m_KnotVector[i + p] - m_KnotVector[i]) * N(0, p - 1, i, t);
                    else
                        return (t - m_KnotVector[i]) / (m_KnotVector[i + p] - m_KnotVector[i]) * N(0, p - 1, i, t) + (m_KnotVector[i + p + 1] - t) / (m_KnotVector[i + p + 1] - m_KnotVector[i + 1]) * N(0, p - 1, i + 1, t);
                }
            }
            else
            {
                if ((m_KnotVector[i + p] - m_KnotVector[i]) == 0 && (m_KnotVector[i + p + 1] - m_KnotVector[i + 1]) == 0)
                    return 0;
                else if ((m_KnotVector[i + p] - m_KnotVector[i]) == 0)
                    return p * (-N(k - 1, p - 1, i + 1, t) / (m_KnotVector[i + p + 1] - m_KnotVector[i + 1]));
                else if ((m_KnotVector[i + p + 1] - m_KnotVector[i + 1]) == 0)
                    return p * (N(k - 1, p - 1, i, t) / (m_KnotVector[i + p] - m_KnotVector[i]));
                else
                    return p * (N(k - 1, p - 1, i, t) / (m_KnotVector[i + p] - m_KnotVector[i]) - N(k - 1, p - 1, i + 1, t) / (m_KnotVector[i + p + 1] - m_KnotVector[i + 1]));
            }
        }
    }
    public class NurbsShapeFunction
    {
        private BSplineShapeFunction m_BSplinesShapeFunction;
        private List<double> m_Weights;
        public int Order;
        public List<double> KnotVector;
        public int Count;
        public NurbsShapeFunction(List<double> tmpKnotVector, List<double> Weights, int p)
        {
            m_BSplinesShapeFunction = new BSplineShapeFunction(tmpKnotVector, p);
            KnotVector = tmpKnotVector;
            m_Weights = Weights;
            Order = p;
            Count = tmpKnotVector.Count - p - 1;
        }
        //阶乘函数
        private int Fac(int n)
        {
            if (n < 2) return 1;
            else return n * Fac(n - 1);
        }
        //（k，j）算符函数
        private double Bin(int k, int j)
        {
            return Fac(k) / (Fac(j) * Fac(k - j));
        }
        //计算求和变量W
        private double W(int k, int p, double t)
        {
            double Ws = 0;
            for (int j = 0; j != m_Weights.Count; ++j)
                Ws += m_Weights[j] * m_BSplinesShapeFunction.N(k, p, j, t);
            return Ws;
        }
        //计算NURBS基函数及其导数值
        //k阶导数          （从0阶导数开始）
        //第i项基函数    （从第0项开始）
        //当x=t时
        public double R(int k, int i, double t)
        {
            return R(k, Order, i, t);
        }
        public double R(int k, int p, int i, double t)
        {
            double A = m_Weights[i] * m_BSplinesShapeFunction.N(k, p, i, t);
            for (int j = 0; j != k; ++j) A -= Bin(k, j + 1) * W(j + 1, p, t) * R(k - j - 1, p, i, t);
            return A / W(0, p, t);
        }
        
    }
}
