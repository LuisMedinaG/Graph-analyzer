using System;
using System.Collections.Generic;
using System.Drawing;

namespace Proyecto_Integrador
{
    public class Edge
    {
        int id;
        double ponderacion;
        Vertex destino, origen;
        List<Point> lP;

        public Edge( Vertex origen, Vertex destino, double pond, int id, List<Point> lP )
        {
            this.id = id;
            this.origen = origen;
            this.destino = destino;
            this.ponderacion = pond;
            this.lP = lP;
        }
        public void SetId( int id )
        {
            this.id = id;
        }
        public int GetId()
        {
            return id;
        }

        public void SetPonderarcion( double value )
        {
            ponderacion = value;
        }
        public double GetPonderacion()
        {
            return ponderacion;
        }

        public override string ToString()
        {
            return string.Format("Arista({0}):  {1} -> {2}", id, origen.GetId(), destino.GetId());
        }

        public String ToTree()
        {
            return String.Format("{0} -> {1}", origen.GetId(), destino.GetId());
        }

        public String ToTreeR()
        {
            return String.Format("{1} -> {0}", origen.GetId(), destino.GetId());
        }

        public Vertex GetOrigen()
        {
            return origen;
        }

        public Vertex GetDestino()
        {
            return destino;
        }

        public void SetDestino( Vertex destino )
        {
            this.destino = destino;
        }

        public void SetOrigen( Vertex origen )
        {
            this.origen = origen;
        }

        internal List<Point> GetLP()
        {
            return lP;
        }
    }
}
