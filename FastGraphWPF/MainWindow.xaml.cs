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
            List<int> Points = new List<int>();
            string Name;

            if (Ribs.Count != 0 && Points.Count != 0)
            {
                #region Add Ribs
                bool fallRibs = false;
                bool fallPoints = false;
                foreach (var comborib in ComboRibs.Items)
                {
                    Match match = Regex.Match((string)comborib, @"\((.*),(.*)\)");
                    if (match.Groups[0] == null && match.Groups[1] == null)
                    {
                        fallRibs = true;
                        break;
                    }
                    else
                    {
                        Ribs.Add(new Rib() { x = Convert.ToInt32(match.Groups[1].Value), y = Convert.ToInt32(match.Groups[2].Value) });
                    }
                }

                if (fallRibs == true || Ribs.Count == 0)
                {
                    fallRibs = false;
                    MessageBox.Show("Вы некорректно указали ребро/рёбра графа", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                #endregion

                #region Add Points

                foreach (var combopoint in ComboPoints.Items)
                {
                    try
                    {
                        Points.Add((int)combopoint);
                    } catch (Exception ex)
                    {
                        fallPoints = true;
                    }
                }

                if (Points.Count == 0 || fallPoints == true)
                {
                    fallPoints = false;
                    MessageBox.Show("Вы некорректно указали точки графа", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                #endregion

                #region Add Name
                if (TextName.Text != null)
                    Name = TextName.Text;
                #endregion


            }
            else
                MessageBox.Show("Вы некорректно указали точки графа \nВы некорректно указали ребро/рёбра графа\n Вы некорректно указали название графа", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);

        }

        private void But_DELETE_Click(object sender, RoutedEventArgs e)
        {
            if(GraphListView.SelectedItems != null && Graphs != null)
            {
                foreach(var graph in Graphs)
                {
                    if(graph.Id == ((GraphView)GraphListView.SelectedItems).Id)
                    {
                        Graphs.Remove(graph);
                    }
                    else
                    {
                        MessageBox.Show("Граф не найден!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
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
        #region Contorls
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

        private void Points_ADD_Click(object sender, RoutedEventArgs e)
        {
            if (TextPoints.Text != null)
            {
                try
                {
                    ComboPoints.Items.Add(Convert.ToInt32(TextPoints.Text));
                }catch(FormatException fx)
                {
                    MessageBox.Show("Вы добавили не число!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Points_DEL_Click(object sender, RoutedEventArgs e)
        {
            if(ComboPoints.SelectedItem != null)
            {
                ComboPoints.Items.Remove(ComboPoints.SelectedItem);
            }
        }

        private void Ribs_DEL_Click(object sender, RoutedEventArgs e)
        {
            if(ComboRibs.SelectedItem != null)
            {
                ComboRibs.Items.Remove(ComboRibs.SelectedItem);
            }
        }
        #endregion
    }
}
