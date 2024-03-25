using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Joueur : INotifyPropertyChanged
{
    private int _id;
    private string _nom;
    private string _pseudo;

    public int ID
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Nom
    {
        get => _nom;
        set => SetProperty(ref _nom, value);
    }

    public string Pseudo
    {
        get => _pseudo;
        set => SetProperty(ref _pseudo, value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
