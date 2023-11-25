using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace MM_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<string> SuppliesItems
        {
            get => _suppliesItems;
            set
            {
                _suppliesItems = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _suppliesItems;
        
        public ObservableCollection<string> DemandsItems
        {
            get => _demandsItems;
            set
            {
                _demandsItems = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _demandsItems;
        
        public ObservableCollection<List<string>> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<List<string>> _items = new ObservableCollection<List<string>>();
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var values = MainTable.GetValues();
            var t = SuppliesItems.GetValues();
            var tt = DemandsItems.GetValues();
            //var solver = new TransportProblemSolver()
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

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {

            var sItems = new List<string>();
            for (int i = 0; i < 2; i++)
            {
                sItems.Add("");
            }
            
            var dItems = new List<string>();
            for (int i = 0; i < 2; i++)
            {
                dItems.Add("");
            }
                
            SuppliesItems = new ObservableCollection<string>(sItems);
            DemandsItems = new ObservableCollection<string>(dItems);

            SuppliesTable.ItemsSource = SuppliesItems;
            DemandsTable.ItemsSource = DemandsItems;
        }
        
        public void AddColumn(object sender, RoutedEventArgs e)
        {
            MainTable.AddColumn(sender, e);
            DemandsItems.Add("");
        }
    
        public void AddCell(object sender, RoutedEventArgs e)
        {
            MainTable.AddCell(sender, e);
            SuppliesItems.Add("");
        }
    }
}