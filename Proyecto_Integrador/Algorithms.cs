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

        public Algorithms() { }
        public Algorithms( List<Vertex> lC )
        {
            VertexL = lC;
            EdgesL = new List<Edge>();
            MyGraph = new Graph();

            foreach(Vertex cir in lC)
                foreach(Edge ari in cir.getLA())
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
            MyGraph.LVer.Add(v_o);

            DFS(pila, visitados, MyGraph, lP);
            return MyGraph;
        }
        public void DFS( Stack<Vertex> pila, HashSet<Vertex> visited, Graph g, List<Point> lP )
        {
            v_act = pila.Pop();
            foreach(Edge e in v_act.getLA())
            {
                des = e.GetDestino();
                if(!visited.Contains(des))
                {
                    g.LVer.Add(des);
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
            var distL = new List<double>();
            var S = new List<Vertex>();
            var Q = new List<Vertex>();
            //double dist;
            //Vertex u;

            for(int i = 0; i < VertexL.Count; i++)
            {
                distL.Add(double.MaxValue);
            }

            //dist = 0;
            distL[org.GetId()] = 0;
            Q.Add(org);

            //while (Q.Count != 0) {
            //	u = minDist(Q, dist);

            //	foreach (Edge e in u.getLA()) {
            //		if (distAnt > dist + w(u,v)) {
            //          lP.AddRange(e.GetLP());//<----------------Agrgando puntos
            //		}

            //	}

            //}
            return null;
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
            int numOfVer = DFS(v_i, lP).LVer.Count;
            var aCandidatas = new List<Edge>();
            var vVistitados = new List<Vertex>();

            while(MyGraph.LVer.Count < numOfVer - 1)
            {//O(n)
                vVistitados.Add(vertex);

                foreach(Edge a in vertex.getLA())//O(n)
                    aCandidatas.Add(a);

                ariMin = SelecAriMin(aCandidatas, vVistitados);//O(n^2)

                vertex = ariMin.GetDestino();
                aCandidatas.Remove(ariMin);
                MyGraph.EdgL.Add(ariMin);
            }
            return MyGraph;
        }

        //Dijkstra
        Vertex MinDist( List<Vertex> q, double dist )
        {
            return null;
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
