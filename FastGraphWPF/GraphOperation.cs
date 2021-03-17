using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastGraphWPF
{
    static class GraphOperation
    {
        public static Graph Union(Graph graph1, Graph graph2)
        {
            var graph = new Graph();

            graph.Ribs = graph1.Ribs;
            graph.Ribs.AddRange(graph2.Ribs);
            graph.Points = graph1.Points;
            graph.Points.AddRange(graph2.Points);

            graph.Points = graph.Points.Distinct().ToList();
            graph.Ribs = graph.Ribs.Distinct().ToList();
            return graph;
        }

        public static Graph Crossing(Graph graph1, Graph graph2)
        {
            var graph = new Graph();

            var ribes = graph1.Ribs;
            ribes.AddRange(graph2.Ribs);
            var pointes = graph1.Points;
            pointes.AddRange(graph2.Points);

            var noribs = ribes.Distinct().ToList();
            var points = pointes.Distinct().ToList();

            graph.Ribs = ribes;
            graph.Points = pointes;
            for(int i = 0; i < noribs.Count; ++i)
            {
                graph.Ribs.Remove(noribs[i]);
            }
            for (int j = 0; j < points.Count; ++j)
            {
                graph.Points.Remove(points[j]);
            }
            return graph;
        }

        public static Graph GetAdjacencyMatrix(Graph graph)
        {
            graph.AdjancenceMatrix = new byte[graph.Points.Count, graph.Points.Count];

            for(int i = 0; i < graph.Points.Count; ++i)
            {
                for(int j = 0; j < graph.Points.Count; ++j)
                {
                    foreach (var rib in graph.Ribs)
                    {
                        if (rib.x == graph.Points[i] && rib.y == graph.Points[j] || rib.x == graph.Points[j] && rib.y == graph.Points[i])
                        {
                            graph.AdjancenceMatrix[i, j] = 1;
                        }
                    }
                }
            }
            graph.Points.Sort();
            return graph;
        }

        public static Graph GetIncidenceMatrix(Graph graph)
        {
            graph.IncidenceMatrix = new byte[graph.Points.Max(), graph.Ribs.Count];

            for(int i = 0; i < graph.Ribs.Count; ++i)
            {
                 graph.IncidenceMatrix[graph.Ribs[i].x - 1, i] = 1;
                 graph.IncidenceMatrix[graph.Ribs[i].y - 1, i] = 1;
            }
            graph.Points.Sort();
            return graph;
        }
    }
}
