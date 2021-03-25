using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Rib> ribs = new List<Rib>();
            ribs.Add(new Rib() { x = 2, y = 1 });
            ribs.Add(new Rib() { x = 4, y = 2 });
            ribs.Add(new Rib() { x = 3, y = 1 });
            ribs.Add(new Rib() { x = 2, y = 3 });

            List<Rib> ribs1 = new List<Rib>();
            ribs1.Add(new Rib() { x = 2, y = 1 });
            ribs1.Add(new Rib() { x = 5, y = 1 });
            ribs1.Add(new Rib() { x = 3, y = 3 });
            ribs1.Add(new Rib() { x = 5, y = 3 });

            List<int> points = new List<int>() 
            {1,2,3,4 };

            List<int> points1 = new List<int>()
            { 1,3,5,2,4};

            Graph graph = new Graph(ribs, points);
            Graph graph1 = new Graph(ribs1, points1);
            var MainGraph = GraphOperation.Union(graph,graph1);
            Console.Write("Вершины: ");
            foreach (var point in MainGraph.Points)
            {
                Console.Write($"{point} ");
            }
            Console.WriteLine();
            Console.Write("Рёбра:");
            foreach (var rib in MainGraph.Ribs)
            {
                Console.Write($" ({rib.x},{rib.y}) ");
            }
            Console.WriteLine();

            var graphes = GraphOperation.GetAdjacencyMatrix(graph1);
            Console.WriteLine(GetDataTableMatrixAdjacency(graphes));
            GraphOperation.GetIncidenceMatrix(graphes);
            Console.WriteLine();
            Console.WriteLine(GetDataTableMatrixIncidence(graphes));
            Console.ReadLine();
        }

        private static string GetDataTableMatrixAdjacency(Graph graph)
        {
            string main = " ";

            for(int i = 0; i < graph.Points.Count; ++i)
            {
                main += $" {graph.Points[i]}";
            }
            for(int j = 0; j < graph.Points.Count; ++j)
            {
                main += $"\n{graph.Points[j]}";
                for(int k = 0; k < graph.Points.Count; ++k)
                {
                    main += $" {graph.AdjancenceMatrix[j,k]}";
                }
            }
            return main;
        }
        
        private static string GetDataTableMatrixIncidence(Graph graph)
        {
            string main = "";

            for (int o = 0; o < graph.Ribs.Count; ++o)
                main += $" {o+1}:({graph.Ribs[o].x}, {graph.Ribs[o].y})";

            main += "\n ";
            for(int k = 0; k < graph.Ribs.Count; ++k)
            {
                main += $" {k+1}";
            }

            for(int i = 0; i <graph.Points.Count; ++i)
            {
                main += $"\n{graph.Points[i]}";
                for(int j = 0; j < graph.Ribs.Count; ++j)
                {
                    main += $" {graph.IncidenceMatrix[i,j]}";
                }
            }

            return main;
        }
    }
}
