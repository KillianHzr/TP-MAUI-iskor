using i_Sk�r.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace i_Sk�r.ViewModels
{
    public class EquipeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Equipe> Equipes { get; } = new ObservableCollection<Equipe>();

        private Equipe _equipe;

        // Propri�t� Equipe qui g�re la notification de modification
        public Equipe Equipe
        {
            get => _equipe;
            set
            {
                if (_equipe != value)
                {
                    _equipe = value;
                    OnPropertyChanged(nameof(Equipe));

                    // Mise � jour de la collection Membres chaque fois que Equipe change
                    Membres.Clear();
                    if (_equipe.Membres != null)
                    {
                        foreach (var membre in _equipe.Membres)
                        {
                            Membres.Add(membre);
                        }
                    }
                }
            }
        }

        // Collection observable des membres de l'�quipe
        public ObservableCollection<Joueur> Membres { get; } = new ObservableCollection<Joueur>();

        public EquipeViewModel(Equipe equipe)
        {
            Equipe = equipe ?? new Equipe();
        }

        // M�thode pour ajouter un membre � l'�quipe
        public void AjouterMembre(Joueur joueur)
        {
            if (!Membres.Contains(joueur))
            {
                Membres.Add(joueur);
                _equipe.Membres.Add(joueur);
                OnPropertyChanged(nameof(Membres));
            }
        }

        // M�thode pour supprimer un membre de l'�quipe
        public void SupprimerMembre(Joueur joueur)
        {
            if (Membres.Contains(joueur))
            {
                Membres.Remove(joueur);
                _equipe.Membres.Remove(joueur);
                OnPropertyChanged(nameof(Membres));
            }
        }

        // M�thode pour modifier le nom de l'�quipe
        public void ModifierNomEquipe(string nouveauNom)
        {
            if (!string.Equals(Equipe.Nom, nouveauNom))
            {
                Equipe.Nom = nouveauNom;
                OnPropertyChanged(nameof(Equipe));
            }
        }

        // Impl�mentation de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CreerNouvelleEquipe(string nomEquipe)
        {
            if (!string.IsNullOrWhiteSpace(nomEquipe))
            {
                Equipe nouvelleEquipe = new Equipe { Nom = nomEquipe };
                Equipes.Add(nouvelleEquipe);
            }
        }

    }
}
