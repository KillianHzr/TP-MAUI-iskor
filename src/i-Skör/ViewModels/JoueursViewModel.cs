using i_Skör.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace i_Skör.ViewModels
{
    public class JoueurViewModel : INotifyPropertyChanged
    {
        // Collection de joueurs
        public ObservableCollection<Joueur> Joueurs { get; } = new ObservableCollection<Joueur>();

        private Joueur _joueurSelectionne;

        // Joueur sélectionné dans l'interface utilisateur
        public Joueur JoueurSelectionne
        {
            get => _joueurSelectionne;
            set
            {
                if (_joueurSelectionne != value)
                {
                    _joueurSelectionne = value;
                    OnPropertyChanged();
                }
            }
        }

        public JoueurViewModel()
        {
        }

        // Ajoute un nouveau joueur à la collection
        public void AjouterJoueur(string nom, string pseudo)
        {
            if (!string.IsNullOrWhiteSpace(nom) && !string.IsNullOrWhiteSpace(pseudo))
            {
                Joueur nouveauJoueur = new Joueur { Nom = nom, Pseudo = pseudo };
                Joueurs.Add(nouveauJoueur);
                OnPropertyChanged(nameof(Joueurs));
            }
        }

        // Supprime le joueur sélectionné de la collection
        public void SupprimerJoueur()
        {
            if (JoueurSelectionne != null && Joueurs.Contains(JoueurSelectionne))
            {
                Joueurs.Remove(JoueurSelectionne);
                JoueurSelectionne = null; // Réinitialise le joueur sélectionné
                OnPropertyChanged(nameof(Joueurs));
            }
        }

        // Met à jour les informations du joueur sélectionné
        public void ModifierJoueur(string nom, string pseudo)
        {
            if (JoueurSelectionne != null)
            {
                JoueurSelectionne.Nom = nom;
                JoueurSelectionne.Pseudo = pseudo;
                OnPropertyChanged(nameof(JoueurSelectionne));
            }
        }

        // Implémentation de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
