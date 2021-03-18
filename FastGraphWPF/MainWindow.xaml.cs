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
        string[] ChoiceMove = {"Матрица Смежности", "Матрица Инцидентности", "Объединение", "Пересечение" };
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
            {
                ComboMover.Items.Add(choice);
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
                }catch(FormatException)
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
                
                foreach (var graphObj in GraphListView.SelectedItems)
                {
                    if(ChoiceMove[4] == (string)ComboMover.SelectedItem)
                    {

                    }
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
    }
}
