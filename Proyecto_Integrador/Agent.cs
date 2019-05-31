using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto_Integrador
{
    public class Agent
    {
        private int id;
        private double DistAcu;
        private List<Edge> usadas;
        private Algorithms alg;

        public Graph Tree { get; set; }
        public List<Point> LP { get; set; }

        public int CONT_P { get; set; }
        public bool IsPrey { get; set; }

        public Point CurrPoint { get; set; }
        public LinkedListNode<Edge> CurrEdge { get; set; }

        //Random Path Agent
        public Agent( Vertex vOrg, int id )
        {
            CONT_P = 0;
            this.id = id;
            IsPrey = false;
            bool creandoCamino = true;
            Vertex vOrgCpy = vOrg;

            LP = new List<Point>();
            usadas = new List<Edge>();
            DistAcu = 0;

            while(creandoCamino)
            {
                Edge a_aleatoria = SeleccionaAristaAleatoria(vOrgCpy);

                if(a_aleatoria != null)
                {
                    usadas.Add(a_aleatoria);
                    foreach(Point p in a_aleatoria.GetLP())
                    {
                        LP.Add(p);
                    }

                    DistAcu += a_aleatoria.GetPonderacion();
                    vOrgCpy = a_aleatoria.GetDestino();
                } else
                {
                    creandoCamino = false;
                }
            }
        }
        //HUNTER
        public Agent( Vertex vOrg, List<Vertex> lV, int id_H )
        {
            id = id_H;
            CONT_P = 0;
            IsPrey = false;
            alg = new Algorithms(lV);
            LP = new List<Point>();
            Tree = alg.DFS(vOrg, LP);//Points Are added here

            if(Tree.EdgL.Count > 0)
            {
                CurrPoint = LP[0];//First point
                CurrEdge = Tree.EdgL.First;//First edge
            }
        }
        //PREY 
        public Agent( Vertex org, Vertex des, List<Vertex> lV, int id_P )
        {
            id = id_P;
            CONT_P = 0;
            IsPrey = true;
            alg = new Algorithms(lV);
            LP = new List<Point>();
            Tree = alg.Dijsktra(org, des, LP);

            if(Tree.EdgL.Count > 0)
            {
                CurrPoint = LP[0];//First point
                CurrEdge = Tree.EdgL.First;//First edge
            }
        }

        private Edge SeleccionaAristaAleatoria( Vertex v_origen )
        {
            int randAris;
            List<Edge> lA = v_origen.GetLA();
            List<Edge> candidates = new List<Edge>();
            for(int i = 0; i < lA.Count; i++)
            {
                int j;
                for(j = 0; j < usadas.Count; j++)
                    if(IsTheSame(usadas[j], lA[i]))
                        break;
                if(j == usadas.Count)
                    candidates.Add(lA[i]);
            }
            Random rand = new Random();

            System.Threading.Thread.Sleep(111);

            if(candidates.Count != 0)
            {
                randAris = rand.Next(candidates.Count);
                return candidates[randAris];
            }
            return null;
        }

        private bool IsTheSame( Edge arista1, Edge arista2 )
        {
            if(arista1.GetOrigen().GetId() == arista2.GetOrigen().GetId())
                if(arista1.GetDestino().GetId() == arista2.GetDestino().GetId())
                    return true;
            if(arista1.GetOrigen().GetId() == arista2.GetDestino().GetId())
                if(arista1.GetDestino().GetId() == arista2.GetOrigen().GetId())
                    return true;

            return false;
        }

        //Getters
        public int GetId()
        {
            return id;
        }
        public List<Edge> GetUsadas()
        {
            return usadas;
        }
        public List<Point> GetLPU()
        {
            return LP;
        }
        public double GetDistAcu()
        {
            return DistAcu;
        }

        public LinkedListNode<Edge> NextEdg()
        {
            return CurrEdge.Next;
        }
        public Point NextPoint()
        {
            CONT_P += 10;
            if(CONT_P < LP.Count - 2)
            {
                return LP[CONT_P];
            }
            return LP[LP.Count - 1];
        }
        public Agent IsInDanger( List<Agent> agentL, Edge edg )
        {
            foreach(Agent hun in agentL)
            {
                //if(hun.CurrEdge.Value == edg && !hun.IsPrey)
                if(SameEdge(hun.CurrEdge.Value, edg) && !hun.IsPrey)
                {
                    return hun;
                }
            }
            return null;
        }
        public bool IsInVertex()
        {
            Edge e = this.CurrEdge.Value;
            Point p = this.CurrPoint;
            Point pOrg = e.GetOrigen().GetPoint();
            Point pDes = e.GetDestino().GetPoint();

            double distOrg = GetDist(p, pOrg);
            double distDes = GetDist(p, pDes);

            return (distDes < e.GetDestino().GetR() || distOrg < e.GetOrigen().GetR());
        }
        public bool IsTouchingHunter( Agent agnDanger )
        {
            return GetDist(CurrPoint, agnDanger.CurrPoint) < 30 && !IsInVertex();
        }

        public bool SameEdge( Edge e_1, Edge e_2 )
        {
            if(e_1.GetDestino() == e_2.GetDestino() && e_1.GetOrigen() == e_2.GetOrigen())
            {
                return true;
            }
            if(e_1.GetDestino() == e_2.GetOrigen() && e_1.GetOrigen() == e_2.GetDestino())
            {
                return true;
            }
            return false;
        }
        public double GetDist( Point ini, Point fin )
        {
            int distX = Math.Abs((ini.X - fin.X));
            int distY = Math.Abs((ini.Y - fin.Y));
            return Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));
        }
    }
}
