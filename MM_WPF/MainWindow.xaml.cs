using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace MM_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TableItem> Items { get; set; } = new ObservableCollection<TableItem>();
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            GenerateColumns(3);
            GenerateCells(3, 3);
            
            MainDataGrid.Columns[0].Header = "Заводы/Магазины";
        }

        private void GenerateColumns(int count)
        {
            for (var i = 0; i < count + 1; i++)
            {
                MainDataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = i,
                    Binding = i == 0 
                        ? new Binding("Header")
                        : new Binding($"{i}"),
                    IsReadOnly = false
                });
            }
        }
        
        private void GenerateCells(int count, int columnsCount)
        {
            for (var i = 0; i < count; i++)
            {
                var columns = new List<int>();
                for (int j = 0; j < columnsCount - 1; j++)
                {
                    columns.Add(0);
                }
                Items.Add(new TableItem()
                {
                    Header = $"{i + 1}",
                    Columns = columns
                });
            }
        }

        private int[,] GetValues()
        {
            var columnsCount = MainDataGrid.Columns.Count;
            var rowsCount = MainDataGrid.Items.Count;
            var array = new int[columnsCount - 1, rowsCount - 1];
            
            for (var i = 1; i < columnsCount; i++)
            {
                for (int j = 0; j < rowsCount; j++)
                {
                    if (MainDataGrid.Items[j] is TableItem tableItem)
                        array[i, j] = tableItem.Columns.ElementAtOrDefault(i);
                }
            }

            return array;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var temp = GetValues();
            var t = temp;
        }
    }
}