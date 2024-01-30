using i_Skör.Models;
using i_Skör.Models.Statistiques;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace i_Skör.ViewModels
{
    public class JoueursViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Joueur> Joueurs { get; set; } = new ObservableCollection<Joueur>();

        public ICommand AjouterJoueurCommand { get; private set; }
        public ICommand MettreAJourJoueurCommand { get; private set; }
        public ICommand SupprimerJoueurCommand { get; private set; }

        public JoueursViewModel()
        {
            AjouterJoueurCommand = new Command<Joueur>(AjouterJoueur);
            MettreAJourJoueurCommand = new Command<Joueur>(MettreAJourJoueur);
            SupprimerJoueurCommand = new Command<Joueur>(SupprimerJoueur);
        }

        private void AjouterJoueur(Joueur joueur)
        {
            Joueurs.Add(joueur);
            OnPropertyChanged(nameof(Joueurs));
        }

        private void MettreAJourJoueur(Joueur joueur)
        {
            var joueurExistant = Joueurs.FirstOrDefault(j => j.ID == joueur.ID);
            if (joueurExistant != null)
            {
                joueurExistant.Nom = joueur.Nom;
                joueurExistant.Pseudo = joueur.Pseudo;
                joueurExistant.Equipe = joueur.Equipe;
                OnPropertyChanged(nameof(Joueurs));
            }
        }

        private void SupprimerJoueur(Joueur joueur)
        {
            Joueurs.Remove(joueur);
            OnPropertyChanged(nameof(Joueurs));
        }

        public void MettreAJourStatistiquesJoueur(int joueurId, string jeu, StatistiquesJeu nouvellesStatistiques)
        {
            var joueur = Joueurs.FirstOrDefault(j => j.ID == joueurId);
            joueur?.Statistiques.AjouterOuMettreAJourStatistiques(jeu, nouvellesStatistiques);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
