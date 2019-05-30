using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Proyecto_Integrador
{
    class Algorithms
    {
        public List<Vertex> VertexL { get; set; }
        public List<Edge> EdgesL { get; set; }
        public Graph MyGraph { get; set; }
        private List<Point> lPR;
        private Vertex v_act, des;

        public Algorithms( List<Vertex> lV )
        {
            VertexL = lV;
            EdgesL = new List<Edge>();
            MyGraph = new Graph();

            foreach(Vertex cir in lV)
                foreach(Edge ari in cir.GetLA())
                    if(!EdgesL.Contains(ari))
                        EdgesL.Add(ari);
        }

        public Graph DFS( Vertex v_o, List<Point> lP )
        {
            var pila = new Stack<Vertex>();
            var visitados = new HashSet<Vertex>();

            MyGraph = new Graph();

            pila.Push(v_o);
            visitados.Add(v_o);
            MyGraph.VerL.Add(v_o);

            DFS(pila, visitados, MyGraph, lP);
            return MyGraph;
        }
        public void DFS( Stack<Vertex> pila, HashSet<Vertex> visited, Graph g, List<Point> lP )
        {
            v_act = pila.Pop();
            foreach(Edge e in v_act.GetLA())
            {
                des = e.GetDestino();
                if(!visited.Contains(des))
                {
                    g.VerL.Add(des);
                    if(!g.EdgL.Contains(e))
                        g.EdgL.Add(e);

                    visited.Add(des);
                    pila.Push(des);

                    lPR = e.GetLP();
                    lP.AddRange(e.GetLP());

                    DFS(pila, visited, g, lP);

                    lPR = new List<Point>(e.GetLP());
                    lPR.Reverse();
                    lP.AddRange(lPR);
                }
            }
        }

        public Graph Dijsktra( Vertex org, Vertex des, List<Point> lP )
        {
            var g = new Graph();
            var S = new List<DijElemnt>();
            var Q = new Queue<DijElemnt>();
            var DijEleL = new List<DijElemnt>();
            DijElemnt u, v;

            for(int i = 0; i < VertexL.Count; i++)
            {
                DijEleL.Add(new DijElemnt(VertexL[i]));
            }
            u = DijEleL.Find(x => x.Ver.GetId() == org.GetId());

            u.DistAcu = 0;
            u.Definitivo = true;

            while(!SolucionDij(DijEleL))//Todos no este definitivos
            {
                foreach(Edge e in u.Ver.GetLA())
                {
                    v = DijEleL.Find(x => x.Ver.GetId() == e.GetDestino().GetId());

                    if(v.DistAcu > u.DistAcu + e.GetPonderacion())
                    {
                        v.DistAcu = u.DistAcu + e.GetPonderacion();
                        v.Proveniente = u.Ver;
                        //lP.AddRange(e.GetLP());//<-----Agregando puntos
                    }

                }
                u = MinDist(DijEleL);
                u.Definitivo = true;
                
                if(u.Ver.GetId() == des.GetId())
                {
                    break;
                }
            }

            //Provenientes construir camino
            DijElemnt aux = u;//Des
            List<Point> lPR;
            g.VerL.Add(aux.Ver);

            while(aux.Ver.GetId() != org.GetId())
            {
                foreach(Edge e in aux.Proveniente.GetLA())
                {
                    if(aux.Ver.GetId() == e.GetDestino().GetId())
                    {
                        //e.GetLP().Reverse();//Porque las aristas se van agregando del final al inicio
                        lPR = new List<Point>(e.GetLP());
                        lPR.Reverse();
                        lP.AddRange(lPR);
                        g.EdgL.Add(e);
                        break;
                    }
                }
                aux = DijEleL.Find(x => x.Ver.GetId() == aux.Proveniente.GetId());
                g.VerL.Add(aux.Ver);
            }

            lP.Reverse();//Begint to end
            return g;
        }

        private bool SolucionDij( List<DijElemnt> dEL )
        {
            for(int i = 0; i < dEL.Count; i++)
            {
                if(dEL[i].Definitivo == false)
                    return false;
            }
            return true;
        }

        public Graph Kruskal()
        {
            Edge edge;
            int a_1, a_2, n;
            var lLP = new List<List<Point>>();
            var link = new List<int>();
            var size = new List<double>();

            n = VertexL.Count;
            EdgesL.Sort(( x, y ) => x.GetPonderacion().CompareTo(y.GetPonderacion()));//O(nlog(n))

            for(int i = 0; i < n; i++) link.Add(i);//O(n)
            for(int i = 0; i < n; i++) size.Add(1);//O(n)

            for(int i = 0; i < EdgesL.Count; i++)
            {//O(n^2)
                edge = EdgesL[i];
                a_1 = edge.GetOrigen().GetId() - 1;
                a_2 = edge.GetDestino().GetId() - 1;

                if(!Same(a_1, a_2, link))
                {
                    Unite(a_1, a_2, link, size);
                    MyGraph.EdgL.Add(edge);
                }
            }
            return MyGraph;
        }
        public Graph Prim( Vertex v_i )
        {
            Edge ariMin;
            Vertex vertex = v_i;
            List<Point> lP = new List<Point>();
            int numOfVer = DFS(v_i, lP).VerL.Count;
            var aCandidatas = new List<Edge>();
            var vVistitados = new List<Vertex>();

            while(MyGraph.VerL.Count < numOfVer - 1)
            {//O(n)
                vVistitados.Add(vertex);

                foreach(Edge a in vertex.GetLA())//O(n)
                    aCandidatas.Add(a);

                ariMin = SelecAriMin(aCandidatas, vVistitados);//O(n^2)

                vertex = ariMin.GetDestino();
                aCandidatas.Remove(ariMin);
                MyGraph.EdgL.Add(ariMin);
            }
            return MyGraph;
        }

        //Dijkstra
        DijElemnt MinDist( List<DijElemnt> Q )
        {
            DijElemnt u = new DijElemnt(null);
            for(int i = 0; i < Q.Count; i++)
            {
                if(Q[i].DistAcu < u.DistAcu && Q[i].Definitivo == false)
                    u = Q[i];
            }
            return u;
        }
        //Kruskal
        private int Find( int x, List<int> link )
        {
            while(x != link[x])
                x = link[x];
            return x;
        }
        private bool Same( int a, int b, List<int> link )
        {
            return Find(a, link) == Find(b, link);
        }
        private void Unite( int a, int b, List<int> link, List<double> size )
        {
            int aux;
            a = Find(a, link);
            b = Find(b, link);
            if(size[a] < size[b])
            {
                aux = a;
                a = b;
                b = aux;
            }
            size[a] += size[b];
            link[b] = a;
        }
        //Prim
        private bool Solucion( List<Edge> ARM_T )
        {
            //bool isConexo = depthFirstSearch(v_i);//Se puede conectar a todos los vertices
            return (ARM_T.Count == VertexL.Count - 1);
        }
        private Edge SelecAriMin( List<Edge> aCandidatas, List<Vertex> vVistitados )
        {
            Edge ariMin = new Edge(null, null, int.MaxValue, -1, null);

            foreach(Edge a in aCandidatas)
            {
                if(!vVistitados.Contains(a.GetDestino()))
                    if(a.GetPonderacion() < ariMin.GetPonderacion())
                        ariMin = a;
            }
            return ariMin;

        }
    }
}
