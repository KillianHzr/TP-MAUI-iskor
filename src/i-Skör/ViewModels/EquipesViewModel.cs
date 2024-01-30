using i_Skör.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace i_Skör.ViewModels
{
    public class EquipeViewModel : INotifyPropertyChanged
    {
        private Equipe _equipe;

        public Equipe Equipe
        {
            get => _equipe;
            set
            {
                if (_equipe != value)
                {
                    _equipe = value;
                    OnPropertyChanged(nameof(Equipe));
                    Membres.Clear();
                    foreach (var membre in _equipe.Membres)
                    {
                        Membres.Add(membre);
                    }
                }
            }
        }

        public ObservableCollection<Joueur> Membres { get; }

        public EquipeViewModel(Equipe equipe)
        {
            Equipe = equipe;
            Membres = new ObservableCollection<Joueur>(equipe.Membres);
        }

        public void AjouterMembre(Joueur joueur)
        {
            if (!Membres.Contains(joueur))
            {
                Membres.Add(joueur);
                Equipe.Membres.Add(joueur);
                OnPropertyChanged(nameof(Membres));
            }
        }

        public void SupprimerMembre(Joueur joueur)
        {
            if (Membres.Contains(joueur))
            {
                Membres.Remove(joueur);
                Equipe.Membres.Remove(joueur);
                OnPropertyChanged(nameof(Membres));
            }
        }

        public void ModifierNomEquipe(string nouveauNom)
        {
            if (!string.Equals(Equipe.Nom, nouveauNom))
            {
                Equipe.Nom = nouveauNom;
                OnPropertyChanged(nameof(Equipe));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
