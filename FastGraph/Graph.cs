using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastGraph
{
    public struct Rib
    {
        public int x;
        public int y;
    }
    public class Graph
    {
        public byte[,] AdjancenceMatrix { get; set; }
        public byte[,] IncidenceMatrix { get; set; }
        public List<Rib> Ribs { get; set; }
        public List<int> Points { get; set; }

        public Graph() { }
        public Graph(List<Rib>ribs, List<int>points)
        {
            if(Ribs != null)
                Ribs.Clear();
            if(Points != null)
                Points.Clear();
            Ribs = ribs;
            Points = points;
        }

        public Graph(Rib[]ribs,int[] points)
        {
            if (Ribs != null)
                Ribs.Clear();
            if (Points != null)
                Points.Clear();
            Ribs.AddRange(ribs);
            Points.AddRange(points);
        }
    }
}
