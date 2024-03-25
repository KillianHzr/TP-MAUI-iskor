using i_Skör.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace i_Skör.ViewModels
{
    public class JoueurViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Joueur> Joueurs { get; } = new ObservableCollection<Joueur>();

        private Joueur _joueurSelectionne;
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

        public void AjouterJoueur(string nom, string pseudo)
        {
            if (!string.IsNullOrWhiteSpace(nom) && !string.IsNullOrWhiteSpace(pseudo))
            {
                Joueur nouveauJoueur = new Joueur { Nom = nom, Pseudo = pseudo };
                Joueurs.Add(nouveauJoueur);
                OnPropertyChanged(nameof(Joueurs));
            }
        }

        public void SupprimerJoueur(Joueur joueur)
        {
            if (joueur != null && Joueurs.Contains(joueur))
            {
                Joueurs.Remove(joueur);
                if (JoueurSelectionne == joueur)
                {
                    JoueurSelectionne = null;
                }
                OnPropertyChanged(nameof(Joueurs));
            }
        }


        public void ModifierJoueur(Joueur joueur, string nouveauNom, string nouveauPseudo)
        {
            var index = Joueurs.IndexOf(joueur);
            if (index != -1)
            {
                joueur.Nom = nouveauNom;
                joueur.Pseudo = nouveauPseudo;

                Joueurs[index] = joueur;

                OnPropertyChanged(nameof(Joueurs));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
