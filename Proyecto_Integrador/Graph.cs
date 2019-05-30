using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto_Integrador
{
    public class Graph
    {
        private List<Vertex> lVer;
        private List<Edge> lEdg;

        public Graph() {
            lVer = new List<Vertex>();
            lEdg = new List<Edge>();
        }
        public Graph( List<Vertex> lVer, List<Edge> lEdg)
        {
            this.lVer = lVer;
            this.lEdg = lEdg;
        }

        public List<Vertex> VerL {
            get { return lVer; }
            set { lVer = value; }
        }

        public List<Edge> EdgL {
            get { return lEdg; }
            set { lEdg = value; }
        }
    }
}
