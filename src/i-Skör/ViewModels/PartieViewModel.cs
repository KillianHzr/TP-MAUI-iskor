using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using i_Skör.Models;

namespace i_Skör.ViewModels
{
    public class PartieViewModel : INotifyPropertyChanged
    {
        private Partie _partie;

        public ObservableCollection<EquipeViewModel> Equipes { get; } = new ObservableCollection<EquipeViewModel>();

        public Partie Partie
        {
            get => _partie;
            set
            {
                if (SetProperty(ref _partie, value))
                {
                    LoadFromModel();
                }
            }
        }


        public ICommand AjouterEquipeCommand { get; }
        public ICommand SupprimerEquipeCommand { get; }

        public PartieViewModel(Partie partie)
        {
            _partie = partie ?? throw new System.ArgumentNullException(nameof(partie));
            AjouterEquipeCommand = new Command<Equipe>(AjouterEquipe);
            SupprimerEquipeCommand = new Command<Equipe>(SupprimerEquipe);
            LoadFromModel();
        }

        public void LoadFromModel()
        {
            Equipes.Clear();
            foreach (var equipe in _partie.Equipes)
            {
                Equipes.Add(new EquipeViewModel(equipe));
            }
        }

        public void AjouterEquipe(Equipe equipe)
        {
            if (!Partie.Equipes.Contains(equipe))
            {
                Partie.Equipes.Add(equipe);
                Equipes.Add(new EquipeViewModel(equipe));
                OnPropertyChanged(nameof(Equipes));
            }
        }

        public void SupprimerEquipe(Equipe equipe)
        {
            var toRemove = Equipes.FirstOrDefault(vm => vm.Equipe == equipe);
            if (toRemove != null)
            {
                Partie.Equipes.Remove(equipe);
                Equipes.Remove(toRemove);
                OnPropertyChanged(nameof(Equipes));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
