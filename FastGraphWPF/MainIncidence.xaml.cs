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
using System.Windows.Shapes;

namespace FastGraphWPF
{
    public partial class MainIncidence : Window
    {
        public MainIncidence(Graph graph)
        {
            InitializeComponent();
            if(graph.IncidenceMatrix !=null)
                GetIncidence(graph);
            if (graph.AdjancenceMatrix != null)
                GetAdjancency(graph);
        }

        private void GetIncidence(Graph graph)
        {
            string main = "";

            for (int o = 0; o < graph.Ribs.Count; ++o)
                main += $" {o + 1}:({graph.Ribs[o].x}, {graph.Ribs[o].y})";

            main += "\n ";
            for (int k = 0; k < graph.Ribs.Count; ++k)
            {
                main += $" {k + 1}";
            }

            for (int i = 0; i < graph.Points.Count; ++i)
            {
                main += $"\n{graph.Points[i]}";
                for (int j = 0; j < graph.Ribs.Count; ++j)
                {
                    main += $" {graph.IncidenceMatrix[i, j]}";
                }
            }
            DataList.Items.Add(main);
        }
        private void GetAdjancency(Graph graph)
        {
            string main = " ";

            for (int i = 0; i < graph.Points.Count; ++i)
            {
                main += $" {graph.Points[i]}";
            }
            for (int j = 0; j < graph.Points.Count; ++j)
            {
                main += $"\n{graph.Points[j]}";
                for (int k = 0; k < graph.Points.Count; ++k)
                {
                    main += $" {graph.AdjancenceMatrix[j, k]}";
                }
            }
            DataList.Items.Add(main);
        }
    }
}
