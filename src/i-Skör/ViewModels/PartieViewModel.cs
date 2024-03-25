using i_Skör.Models;
using i_Skör.Models.Statistiques;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace i_Skör.ViewModels
{
    public class PartieViewModel : INotifyPropertyChanged
    {
        private Partie _partie;
        public Partie Partie
        {
            get => _partie;
            set
            {
                _partie = value;
                OnPropertyChanged(nameof(Partie));
                LoadFromModel();
            }
        }

        public ObservableCollection<EquipeViewModel> Equipes { get; }

        public int ID
        {
            get => Partie.ID;
            set
            {
                if (Partie.ID != value)
                {
                    Partie.ID = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        public DateTime Date
        {
            get => Partie.Date;
            set
            {
                if (Partie.Date != value)
                {
                    Partie.Date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public string Jeu
        {
            get => Partie.Jeu;
            set
            {
                if (Partie.Jeu != value)
                {
                    Partie.Jeu = value;
                    OnPropertyChanged(nameof(Jeu));
                }
            }
        }

        public string Score
        {
            get => Partie.Score;
            set
            {
                if (Partie.Score != value)
                {
                    Partie.Score = value;
                    OnPropertyChanged(nameof(Score));
                }
            }
        }

        public TimeSpan Duree
        {
            get => Partie.Statistiques.Duree;
            set
            {
                if (Partie.Statistiques.Duree != value)
                {
                    Partie.Statistiques.Duree = value;
                    OnPropertyChanged(nameof(Duree));
                }
            }
        }

        public PartieViewModel(Partie partie)
        {
            _partie = partie ?? throw new ArgumentNullException(nameof(partie));
            Equipes = new ObservableCollection<EquipeViewModel>();
            LoadFromModel();
        }

        private void LoadFromModel()
        {
            Equipes.Clear();
            foreach (var equipe in Partie.Equipes)
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
            var equipeViewModelToRemove = Equipes.FirstOrDefault(vm => vm.Equipe.ID == equipe.ID);
            if (equipeViewModelToRemove != null)
            {
                Partie.Equipes.Remove(equipe);
                Equipes.Remove(equipeViewModelToRemove);
                OnPropertyChanged(nameof(Equipes));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
