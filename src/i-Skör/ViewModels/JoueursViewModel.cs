using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using i_Skör.Models;
using i_Skör.Services;

namespace i_Skör.ViewModels
{
    public class JoueurViewModel : INotifyPropertyChanged
    {
        private Joueur _joueurSelectionne;

        public ObservableCollection<Joueur> Joueurs { get; } = new ObservableCollection<Joueur>();

        public Joueur JoueurSelectionne
        {
            get => _joueurSelectionne;
            set => SetProperty(ref _joueurSelectionne, value);
        }

        public ICommand AjouterJoueurCommand { get; }
        public ICommand SupprimerJoueurCommand { get; }
        public ICommand ModifierJoueurCommand { get; }

        public JoueurViewModel()
        {
            Joueurs = new ObservableCollection<Joueur>(DataCacheService.Instance.Joueurs);
            AjouterJoueurCommand = new Command<(string Nom, string Pseudo)>(tuple =>
                AjouterJoueur(tuple.Nom, tuple.Pseudo));
            SupprimerJoueurCommand = new Command<Joueur>(SupprimerJoueur);
            ModifierJoueurCommand = new Command<(Joueur Joueur, string NouveauNom, string NouveauPseudo)>(tuple =>
                ModifierJoueur(tuple.Joueur, tuple.NouveauNom, tuple.NouveauPseudo));
        }

        public (bool Success, string Message) AjouterJoueur(string nom, string pseudo)
        {
            if (string.IsNullOrWhiteSpace(nom))
            {
                return (false, "Le champ nom est vide.");
            }

            if (string.IsNullOrWhiteSpace(pseudo))
            {
                return (false, "Le champ pseudonyme est vide.");
            }

            var joueur = new Joueur { Nom = nom, Pseudo = pseudo };
            Joueurs.Add(joueur);
            DataCacheService.Instance.Joueurs.Add(joueur);
            OnPropertyChanged(nameof(Joueurs));

            return (true, "Joueur ajouté avec succès");
        }


        public void SupprimerJoueur(Joueur joueur)
        {
            if (Joueurs.Contains(joueur))
            {
                Joueurs.Remove(joueur);
                DataCacheService.Instance.Joueurs.Remove(joueur);

                if (JoueurSelectionne == joueur)
                    JoueurSelectionne = null;

                OnPropertyChanged(nameof(Joueurs));
            }
        }


        public void ModifierJoueur(Joueur joueur, string nouveauNom, string nouveauPseudo)
        {
            if (joueur != null)
            {
                joueur.Nom = nouveauNom;
                joueur.Pseudo = nouveauPseudo;
                OnPropertyChanged(nameof(Joueurs));
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
