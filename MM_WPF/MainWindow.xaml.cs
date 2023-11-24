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
        public ObservableCollection<TableItem> Items
        {
            get => _items;
            set
            {
                _items = Items;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TableItem> _items = new ObservableCollection<TableItem>();
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var temp = MainTable.GetValues();
            var t = temp;
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
}