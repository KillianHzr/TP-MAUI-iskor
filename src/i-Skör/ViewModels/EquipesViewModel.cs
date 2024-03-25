using i_Skör.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace i_Skör.ViewModels
{
    public class EquipeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Equipe> Equipes { get; } = new ObservableCollection<Equipe>();

        private Equipe _equipe;

        // Propriété Equipe qui gère la notification de modification
        public Equipe Equipe
        {
            get => _equipe;
            set
            {
                if (_equipe != value)
                {
                    _equipe = value;
                    OnPropertyChanged(nameof(Equipe));

                    // Mise à jour de la collection Membres chaque fois que Equipe change
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

        // Collection observable des membres de l'équipe
        public ObservableCollection<Joueur> Membres { get; } = new ObservableCollection<Joueur>();

        public EquipeViewModel(Equipe equipe)
        {
            Equipe = equipe ?? new Equipe();
        }

        // Méthode pour ajouter un membre à l'équipe
        public void AjouterMembre(Joueur joueur)
        {
            if (!Membres.Contains(joueur))
            {
                Membres.Add(joueur);
                _equipe.Membres.Add(joueur);
                OnPropertyChanged(nameof(Membres));
            }
        }

        // Méthode pour supprimer un membre de l'équipe
        public void SupprimerMembre(Joueur joueur)
        {
            if (Membres.Contains(joueur))
            {
                Membres.Remove(joueur);
                _equipe.Membres.Remove(joueur);
                OnPropertyChanged(nameof(Membres));
            }
        }

        // Méthode pour modifier le nom de l'équipe
        public void ModifierNomEquipe(string nouveauNom)
        {
            if (!string.Equals(Equipe.Nom, nouveauNom))
            {
                Equipe.Nom = nouveauNom;
                OnPropertyChanged(nameof(Equipe));
            }
        }

        // Implémentation de INotifyPropertyChanged
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
