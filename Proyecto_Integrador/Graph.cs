using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAnalyser
{
    public class Graph
    {
        private List<Vertex> lVer;
        private LinkedList<Edge> lEdg;

        public Graph()
        {
            lVer = new List<Vertex>();
            lEdg = new LinkedList<Edge>();
        }
        public Graph( List<Vertex> lVer, LinkedList<Edge> lEdg )
        {
            this.lVer = lVer;
            this.lEdg = lEdg;
        }

        public List<Vertex> VerL
        {
            get { return lVer; }
            set { lVer = value; }
        }

        public LinkedList<Edge> EdgL
        {
            get { return lEdg; }
            set { lEdg = value; }
        }
    }
}
