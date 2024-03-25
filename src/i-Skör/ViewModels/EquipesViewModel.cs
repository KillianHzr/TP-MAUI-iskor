using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using i_Skör.Models;
using i_Skör.Services;

namespace i_Skör.ViewModels
{
    public class EquipeViewModel : INotifyPropertyChanged
    {
        private Equipe _equipe;

        public ObservableCollection<Equipe> Equipes { get; } = new ObservableCollection<Equipe>(DataCacheService.Instance.Equipes);
        public ObservableCollection<Joueur> Membres { get; } = new ObservableCollection<Joueur>();

        public Equipe Equipe
        {
            get => _equipe;
            set => SetProperty(ref _equipe, value, onChanged: () =>
            {
                Membres.Clear();
                foreach (var membre in _equipe.Membres)
                {
                    Membres.Add(membre);
                }
            });
        }

        public ICommand AjouterMembreCommand { get; }
        public ICommand SupprimerMembreCommand { get; }
        public ICommand ModifierNomEquipeCommand { get; }
        public ICommand CreerNouvelleEquipeCommand { get; }

        public EquipeViewModel(Equipe equipe)
        {
            Equipe = equipe ?? new Equipe();
            AjouterMembreCommand = new Command<Joueur>(AjouterMembre);
            SupprimerMembreCommand = new Command<Joueur>(SupprimerMembre);
            ModifierNomEquipeCommand = new Command<string>(ModifierNomEquipe);
            CreerNouvelleEquipeCommand = new Command<string>(CreerNouvelleEquipe);
        }

        public void AjouterMembre(Joueur joueur)
        {
            if (!Membres.Contains(joueur))
            {
                Membres.Add(joueur);
                _equipe.Membres.Add(joueur);
                OnPropertyChanged(nameof(Membres));
            }
        }

        public void SupprimerMembre(Joueur joueur)
        {
            if (Membres.Contains(joueur))
            {
                Membres.Remove(joueur);
                _equipe.Membres.Remove(joueur);
                DataCacheService.Instance.Joueurs.Remove(joueur);
                OnPropertyChanged(nameof(Membres));
            }
        }

        public void ModifierNomEquipe(string nouveauNom)
        {
            if (_equipe.Nom != nouveauNom)
            {
                _equipe.Nom = nouveauNom;
                OnPropertyChanged(nameof(Equipe));
            }
        }

        public void CreerNouvelleEquipe(string nomEquipe)
        {
            if (!string.IsNullOrWhiteSpace(nomEquipe))
            {
                Equipe nouvelleEquipe = new Equipe { Nom = nomEquipe };
                DataCacheService.Instance.Equipes.Add(nouvelleEquipe);
                Equipes.Add(nouvelleEquipe);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) =>
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
