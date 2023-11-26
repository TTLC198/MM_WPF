using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MM_WPF.Components;

public partial class CustomTable : UserControl, INotifyPropertyChanged
{
    public ObservableCollection<List<TableItem>> Items
    {
        get => (ObservableCollection<List<TableItem>>) GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register(
            nameof(Items),
            typeof(ObservableCollection<List<TableItem>>),
            typeof(CustomTable),
            new PropertyMetadata(new ObservableCollection<List<TableItem>>()));

    public static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register(
            nameof(Columns),
            typeof(int),
            typeof(CustomTable),
            new PropertyMetadata(default(int)));

    public int Columns
    {
        get => (int) GetValue(ColumnsProperty);
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
        get => (int) GetValue(RowsProperty);
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
        get => (string) GetValue(HeaderProperty);
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

    private void GenerateColumns(int columnsCount)
    {
        MainDataGrid.Columns.Clear();

        var headerBinding = new Binding("[0].Value")
        {
            Mode = BindingMode.OneTime
        };
        MainDataGrid.Columns.Add(new DataGridTextColumn()
        {
            Header = Header,
            Binding = headerBinding,
            IsReadOnly = true
        });

        for (var i = 1; i < columnsCount + 1; i++)
        {
            var itemBinding = new Binding($"[{i}].Value")
            {
                Mode = BindingMode.TwoWay
            };

            MainDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = $"{i}",
                Binding = itemBinding,
                IsReadOnly = false
            });
        }
    }

    private void GenerateCells(int count, int columnsCount)
    {
        Items?.Clear();
        for (var i = 0; i < count; i++)
        {
            var columns = new List<TableItem>()
            {
                new TableItem()
                {
                    Value = $"{i + 1}"
                }
            };
            for (var j = 1; j < columnsCount + 1; j++)
            {
                columns.Add(new TableItem()
                {
                    Value = $""
                });
            }

            Items?.Add(columns);
        }
    }

    public void AddColumn(object sender, RoutedEventArgs e)
    {
        Columns += 1;
        GenerateColumns(Columns);
        Items?.Clear();
        for (var i = 0; i < Rows; i++)
        {
            var columns = new List<TableItem>()
            {
                new ()
                {
                    Value = $"{i + 1}"
                }
            };
            for (var j = 1; j < Columns + 1; j++)
            {
                columns.Add(new TableItem()
                {
                    Value = $""
                });
            }

            Items?.Add(columns);
        }
    }

    public void AddCell(object sender, RoutedEventArgs e)
    {
        Rows += 1;
        var columns = new List<TableItem>()
        {
            new TableItem()
            {
                Value = $"{Rows}"
            }
        };
        for (var j = 1; j < Columns + 1; j++)
        {
            columns.Add(new TableItem()
            {
                Value = ""
            });
        }

        Items?.Add(columns);
    }

    public int[,] GetValues()
    {
        /*var columnsCount = MainDataGrid.Columns.Count;
        var rowsCount = MainDataGrid.Items.Count;
        var array = new int[columnsCount, rowsCount];

        for (var i = 1; i < columnsCount; i++)
        {
            for (int j = 0; j < rowsCount; j++)
            {
                if (MainDataGrid.Items[j] is TableItem tableItem)
                    array[i, j] = tableItem.Columns.ElementAtOrDefault(i);
            }
        }*/
        var rows = Items.Count;
        var columns = (Items.FirstOrDefault()?.Count ?? 0) - 1;

        var arrays = new int[rows, columns];
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                var t = Items[i][j + 1].Value;
                arrays[i, j] = Convert.ToInt32(string.IsNullOrEmpty(Items[i][j + 1].Value) ? "0" : Items[i][j + 1].Value);
            }
        }

        return arrays;
        /*return Items.Select(a => a
            .Select(i => Convert.ToInt32(
                string.IsNullOrEmpty(i)
                ? "0"
                : i))
            .ToArray()
        ).ToArray();*/
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