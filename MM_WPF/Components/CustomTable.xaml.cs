using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MM_WPF.Components;

public partial class CustomTable : UserControl, INotifyPropertyChanged
{
    public ObservableCollection<TableItem> Items
    {
        get => (ObservableCollection<TableItem>) GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }
    
    public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register(
            nameof(Items),
            typeof(ObservableCollection<TableItem>),
            typeof(CustomTable),
            new PropertyMetadata(new ObservableCollection<TableItem>()));
    
    public static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register(
            nameof(Columns),
            typeof(int),
            typeof(CustomTable),
            new PropertyMetadata(default(int)));

    public int Columns
    {
        get => (int)GetValue(ColumnsProperty);
        set => SetValue(ColumnsProperty, value);
    }

    public static readonly DependencyProperty RowsProperty =
        DependencyProperty.Register(
            nameof(Rows),
            typeof(int),
            typeof(CustomTable),
            new PropertyMetadata(default(int)));

    public int Rows
    {
        get => (int)GetValue(RowsProperty);
        set => SetValue(RowsProperty, value);
    }
    
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(CustomTable),
            new PropertyMetadata(default(string)));

    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }
    
    public CustomTable()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void CustomTable_OnLoaded(object sender, RoutedEventArgs e)
    {
        GenerateColumns(Columns);
        GenerateCells(Rows, Columns);
    }

    private void GenerateColumns(int count)
    {
        MainDataGrid.Columns.Clear();
        for (var i = 0; i < count + 1; i++)
        {
            var headerBinding = new Binding("Header");
            headerBinding.Mode = BindingMode.OneTime;
            headerBinding.Path = new PropertyPath(Header);
            
            var itemBinding = new Binding($"{i}");
            headerBinding.Mode = BindingMode.TwoWay;
            headerBinding.Path = new PropertyPath(Items);
            
            MainDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = i == 0
                    ? Header
                    : $"{i}",
                Binding = i == 0
                    ? headerBinding
                    : itemBinding,
                IsReadOnly = false
            });
        }
    }

    private void GenerateCells(int count, int columnsCount)
    {
        Items?.Clear();
        for (var i = 0; i < count; i++)
        {
            var columns = new List<int>();
            for (int j = 0; j < columnsCount - 1; j++)
            {
                columns.Add(0);
            }

            Items?.Add(new TableItem()
            {
                Header = $"{i + 1}",
                Columns = columns
            });
        }
    }

    public void AddColumn(object sender, RoutedEventArgs e)
    {
        Columns += 1;
        GenerateColumns(Columns);
    }
    
    public void AddCell(object sender, RoutedEventArgs e)
    {
        Rows += 1;
        GenerateCells(Rows, Columns);
    }

    public int[,] GetValues()
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

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}