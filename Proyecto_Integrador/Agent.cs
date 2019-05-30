using System;
using System.Collections.Generic;
using System.Drawing;

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

        public Point CurrPoint { get; set; }
        public Edge CurrEdge { get; set; }

        //Random Path Agent
        public Agent( Vertex vOrg, int id )
        {
            this.id = id;
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
        public Agent( Vertex vOrg )
        {
            alg = new Algorithms();
            LP = new List<Point>();
            Tree = alg.DFS(vOrg, LP);//Points Are added here

            if(Tree.EdgL.Count > 0)
            {
                CurrPoint = LP[0];//First point
                CurrEdge = Tree.EdgL[0];//First edge
            }
        }
        //PREY 
        public Agent( Vertex org, Vertex des )
        {
            alg = new Algorithms();
            LP = new List<Point>();
            Tree = alg.Dijsktra(org, des, LP);

            if(Tree.EdgL.Count > 0)
            {
                CurrPoint = LP[0];//First point
                CurrEdge = Tree.EdgL[0];//First edge
            }
        }

        private Edge SeleccionaAristaAleatoria( Vertex v_origen )
        {
            int randAris;
            List<Edge> lA = v_origen.getLA();
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
    }
}
