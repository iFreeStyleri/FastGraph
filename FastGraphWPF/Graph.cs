﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastGraphWPF
{
    public struct Rib
    {
        public int x;
        public int y;
    }
    public class Graph
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public byte[,] AdjancenceMatrix { get; set; }
        public byte[,] IncidenceMatrix { get; set; }
        public List<Rib> Ribs { get; set; }
        public List<int> Points { get; set; }

        public Graph() { }
        public Graph(List<Rib>ribs, List<int>points, string name)
        {
            Ribs = ribs;
            Points = points;
            Name = name;
            Id = Guid.NewGuid().ToString();
        }

        public Graph(Rib[]ribs,int[] points)
        {
            Ribs.Clear();
            Points.Clear();
            Ribs.AddRange(ribs);
            Points.AddRange(points);
            Id = Guid.NewGuid().ToString();
        }
        
    }
}
