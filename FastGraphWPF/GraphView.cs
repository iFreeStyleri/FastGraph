using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FastGraphWPF
{
    class GraphView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int CountPoints { get; set; }
        public int CountRibs { get; set; }
        public GraphView(Graph graph)
        {
            Id = graph.Id;
            Name = graph.Name;
            CountPoints = graph.Points.Count;
            CountRibs = graph.Ribs.Count;
        }
    }
}
