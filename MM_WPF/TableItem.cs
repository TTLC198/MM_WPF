using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MM_WPF;

public class TableItem : INotifyPropertyChanged
{
    private string? _value;
    
    public string? Value
    {
        get => _value;
        set
        {
            _value = value;
            OnPropertyChanged();
        }
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