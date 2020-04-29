using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAnalyser
{
    class DijElemnt
    {
        public Vertex Ver { get; set; }
        public double DistAcu { get; set; }
        public bool Definitivo { get; set; }
        public Vertex Proveniente { get; set; }

        public DijElemnt(Vertex v)
        {
            Ver = v;
            DistAcu = double.MaxValue;
            Definitivo = false;
            Proveniente = null;
        }
    }
}
