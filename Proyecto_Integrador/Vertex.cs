using System;
using System.Drawing;
using System.Collections.Generic;

namespace Proyecto_Integrador
{
    public class Vertex
    {
        int r;
        int id;
        Point p;
        public List<Edge> EdgL { get; set; }

        public Vertex( Point p, int r, int id )
        {
            this.p = p;
            this.r = r;
            this.id = id;
            EdgL = new List<Edge>();
        }

        public List<Edge> GetEdgL()
        {
            return EdgL;
        }

        public void SetId( int id )
        {
            this.id = id;
        }

        public void SetP( Point p )
        {
            this.p = p;
        }

        public void SetR( int r )
        {
            this.r = r;
        }

        public void SetLA( List<Edge> lA )
        {
            this.EdgL = lA;
        }

        public int GetX()
        {
            return p.X;
        }

        public int GetY()
        {
            return p.Y;
        }

        public int GetR()
        {
            return r;
        }

        public int GetId()
        {
            return id;
        }

        public Point GetPoint()
        {
            return p;
        }

        public override string ToString()
        {
            return string.Format("Vertice {0}({1},{2})", id, p.X, p.Y);
        }

        public bool IsLEave( List<Vertex> visited )
        {
            foreach(Edge edg in EdgL)
            {
                if(!visited.Contains(edg.GetDestino()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
