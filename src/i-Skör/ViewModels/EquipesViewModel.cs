using System;
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
        private string _nomEquipe;
        public string NomEquipe
        {
            get => _nomEquipe;
            set => SetProperty(ref _nomEquipe, value);
        }

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

        public ICommand SupprimerEquipeCommand { get; }
        public ICommand ModifierNomEquipeCommand { get; }
        public ICommand CreerNouvelleEquipeCommand { get; }

        public EquipeViewModel(Equipe equipe)
        {
            _equipe = equipe;
            SupprimerEquipeCommand = new Command<Equipe>(SupprimerEquipe);
            ModifierNomEquipeCommand = new Command<(Equipe Equipe, string NouveauNom)>(ModifierNomEquipe);
            CreerNouvelleEquipeCommand = new Command<string>(CreerNouvelleEquipe);
        }

        public event EventHandler EquipeModified;

        public void ModifierNomEquipe((Equipe Equipe, string NouveauNom) tuple)
        {
            if (tuple.Equipe != null && !string.IsNullOrWhiteSpace(tuple.NouveauNom))
            {
                int index = Equipes.IndexOf(tuple.Equipe);
                if (index != -1)
                {
                    Equipe nouvelleEquipe = new Equipe { ID = tuple.Equipe.ID, Nom = tuple.NouveauNom, Membres = tuple.Equipe.Membres };
                    Equipes[index] = nouvelleEquipe;
                    NomEquipe = tuple.NouveauNom;
                    EquipeModified?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        public void CreerNouvelleEquipe(string nomEquipe)
        {
            if (!string.IsNullOrWhiteSpace(nomEquipe))
            {
                Equipe nouvelleEquipe = new Equipe { ID = DataCacheService.Instance.Equipes.Count + 1, Nom = nomEquipe };
                DataCacheService.Instance.Equipes.Add(nouvelleEquipe);
                Equipes.Add(nouvelleEquipe);
                OnPropertyChanged(nameof(Equipes));
            }
        }

        public void SupprimerEquipe(Equipe equipe)
        {
            if (equipe != null)
            {
                Equipes.Remove(equipe);
                DataCacheService.Instance.Equipes.Remove(equipe);
                OnPropertyChanged(nameof(Equipes)); 
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
