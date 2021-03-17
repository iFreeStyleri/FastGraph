using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Graph> Graphs { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            UpdateView();
        }

        private void But_ADD_Click(object sender, RoutedEventArgs e)
        {
            List<Rib> Ribs = new List<Rib>();
            bool fall = false;
            foreach (var comborib in ComboRibs.Items)
            {
                Match match = Regex.Match((string)comborib, @"\((.*),(.*)\)");
                if (match.Groups[0] == null && match.Groups[1] == null)
                {
                    fall = true;
                    break;
                }
                else
                {
                    Ribs.Add(new Rib() { x = Convert.ToInt32(match.Groups[1].Value), y = Convert.ToInt32(match.Groups[2].Value) });
                }
            }

            if (fall == true || Ribs.Count == 0)
                MessageBox.Show("Вы некорректно указали ребро/рёбра графа", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else
            {

            }
            
        }

        private void But_DELETE_Click(object sender, RoutedEventArgs e)
        {

        }

        private void But_UPDATE_Click(object sender, RoutedEventArgs e)
        {

        }
        void UpdateView()
        {
            if (Graphs != null)
            {
                foreach (var graph in Graphs)
                {
                    GraphView graphView = new GraphView(graph);
                    GraphListView.Items.Add(graphView);
                }
            }
        }

        private void Ribs_ADD_Click(object sender, RoutedEventArgs e)
        {
            if (TextRibs.Text != null)
            {
                Match match = Regex.Match(TextRibs.Text, @"\((.*?),(.*?)\)");
                if (match.Success)
                {
                    ComboRibs.Items.Add($"({match.Groups[1].Value},{match.Groups[2].Value})");
                }
                else
                {
                    MessageBox.Show("Вы некорректно указали ребро графа", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }
    }
}
