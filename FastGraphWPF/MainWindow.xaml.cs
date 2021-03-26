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
        string[] ChoiceMove = {"Объединение", "Пересечение" };
        string[] ChoiceMenu = { "Матрица смежности", "Матрица инцидентности" };
        public MainWindow()
        {
            InitializeComponent();
            UpdateView();
            Start();
            Graphs = new List<Graph>();
            ComboPoints.Items.Add(6);
            ComboPoints.Items.Add(3);
            ComboRibs.Items.Add("(3,6)");
            TextName.Text = "Graph";
        }

        void UpdateView()
        {
            if (Graphs != null)
            {
                GraphListView.Items.Clear();
                foreach (var graph in Graphs)
                {
                    var graphView = new GraphView(graph);
                    GraphListView.Items.Add(graphView);
                }
            }
        }
        void Start()
        {
            foreach (var choice in ChoiceMove)
                ComboMover.Items.Add(choice);
        }
        #region Contorls
        private void Ribs_ADD_Click(object sender, RoutedEventArgs e)
        {
            if (TextRibs.Text != null)
            {
                bool detected1 = false;
                bool detected2 = false;
                Match match = Regex.Match(TextRibs.Text, @"\((.*?),(.*?)\)");
                if (match.Success)
                {
                    foreach (var point in ComboPoints.Items)
                    {
                        if (int.Parse(match.Groups[1].Value) == (int)point)
                        {
                            detected1 = true;
                            break;
                        }
                    }
                    foreach (var point in ComboPoints.Items)
                    {
                        if (int.Parse(match.Groups[2].Value) == (int)point)
                        {
                            detected2 = true;
                            break;
                        }
                    }
                    if (detected1 == true && detected2 == true)
                    {
                        ComboRibs.Items.Add($"({match.Groups[1].Value},{match.Groups[2].Value})");
                    }
                    else
                        MessageBox.Show("Указанные вами точки не существуют в графе!", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
                bool detect = false;
                foreach (var point in ComboPoints.Items)
                {
                    if (int.Parse(TextPoints.Text) == (int)point)
                    {
                        detect = true;
                        break;
                    }
                }
                try
                {
                    if(!detect)
                        ComboPoints.Items.Add(Convert.ToInt32(TextPoints.Text));
                }
                catch (FormatException)
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

        private void But_ADD_Click(object sender, RoutedEventArgs e)
        {
            List<Rib> Ribs = new List<Rib>();
            List<int> Points = new List<int>();
            string Name = "";

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
                }
                catch (Exception)
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
            if (Name != null)
            {
                Graphs.Add(new Graph(Ribs, Points, Name));
            }
            else
                MessageBox.Show("Вы не ввели название графа", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            UpdateView();

        }

        private void But_DELETE_Click(object sender, RoutedEventArgs e)
        {
            if (GraphListView.SelectedItems != null && Graphs != null)
            {
                foreach (Graph graph in Graphs.ToArray())
                {
                    try
                    {
                        if (graph.Id == ((GraphView)GraphListView.SelectedItem).Id)
                        {
                            Graphs.Remove(graph);
                        }
                    }catch(Exception)
                    { }
                }
            }
            UpdateView();
        }

        private void But_UPDATE_Click(object sender, RoutedEventArgs e)
        {

        }

        private void But_Mover_Click(object sender, RoutedEventArgs e)
        {
            if (GraphListView.SelectedItems.Count == 2)
            {
                Graph graph1 = new Graph();
                Graph graph2 = new Graph();
                if (ChoiceMove[3] == (string)ComboMover.SelectedItem || ChoiceMove[2] == (string)ComboMover.SelectedItem)
                {
                    bool graph1Exist = false;
                    bool graph2Exist = false;
                    var graphView1 = (GraphView)GraphListView.SelectedItems[0];
                    var graphView2 = (GraphView)GraphListView.SelectedItems[1];

                    foreach (var graph in Graphs)
                    {
                        if (graphView1.Id == graph.Id)
                        {
                            graph1 = new Graph(graph.Ribs, graph.Points, graph.Name);
                            graph1Exist = true;
                        }
                        if (graphView2.Id == graph.Id)
                        {
                            graph2 = new Graph(graph.Ribs, graph.Points, graph.Name);
                            graph2Exist = true;
                        }
                        if (graph1Exist && graph2Exist)
                            break;
                    }

                    if (graph1Exist && graph2Exist && graph1.Points != null && graph2.Points !=null)
                    {
                        if (ChoiceMove[1] == (string)ComboMover.SelectedItem)
                        {
                            Graphs.Add(GraphOperation.Union(graph1, graph2));
                            UpdateView();
                        }
                        else if(ChoiceMove[0] == (string)ComboMover.SelectedItem)
                        {
                            Graphs.Add(GraphOperation.Crossing(graph1, graph2));
                            UpdateView();
                        }
                    }
                    else
                        MessageBox.Show("Граф(ы) не найдены", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Выберите только 2 графа!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void TextPoints_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(TextPoints.Text != null)
                {
                    try
                    {
                        ComboPoints.Items.Add(Convert.ToInt32(TextPoints.Text));
                        TextPoints.Text = null;
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Вы добавили не число!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        private void ComboMover_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(var choice in ChoiceMove)
            {
                if (choice == (string)ComboMover.SelectedItem)
                {
                    But_Mover.IsEnabled = true;
                    break;
                }
                else
                    But_Mover.IsEnabled = false;
            }
        }

        private void GraphListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(GraphListView.SelectedItem != null)
            {
                var mainGraph = new Graph();
                var graphView = (GraphView)GraphListView.SelectedItem;
                foreach (var graph in Graphs)
                {
                    if(graphView.Id == graph.Id)
                    {
                        mainGraph = new Graph(graph.Ribs, graph.Points, graph.Name);
                        break;
                    }
                }
                ComboPoints.Items.Clear();
                ComboRibs.Items.Clear();
                foreach (var point in mainGraph.Points)
                {
                    ComboPoints.Items.Add(point);
                }
                foreach (var ribs in mainGraph.Ribs)
                {
                    ComboRibs.Items.Add($"({ribs.x},{ribs.y})");
                }
                TextName.Text = mainGraph.Name;
            }
        }

        private void Click_matrixIncContext(object sender, RoutedEventArgs e)
        {
            //В РАЗРАБОТКЕ!

            //if (GraphListView.SelectedItems != null)
            //{
            //    foreach (var graph in Graphs)
            //    {
            //        if ((graph.Id == ((GraphView)GraphListView.SelectedItem).Id))
            //        {

            //            MainIncidence mainInc = new MainIncidence(GraphOperation.GetIncidenceMatrix(graph));
            //            mainInc.Show();
            //        } 
            //    }
            //}

            MessageBox.Show("Данная функция ещё не реализована!", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void Click_matrixAdjContext(object sender, RoutedEventArgs e)
        {
            if (GraphListView.SelectedItems != null)
            {
                foreach (var graph in Graphs)
                {
                    if ((graph.Id == ((GraphView)GraphListView.SelectedItem).Id))
                    {

                        MainIncidence mainInc = new MainIncidence(GraphOperation.GetAdjacencyMatrix(graph));
                        mainInc.Show();
                    }
                }
            }
        }
    }
}
