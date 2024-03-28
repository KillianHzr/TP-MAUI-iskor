using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using i_Skör.Models;
using i_Skör.Models.Statistiques;
using i_Skör.Services;

namespace i_Skör.ViewModels
{
    public class PartieViewModel : INotifyPropertyChanged
    {
        private Partie _partieSelectionnee;
        private Equipe _equipeASelectionnee;
        private Equipe _equipeBSelectionnee;

        public ObservableCollection<Partie> Parties { get; } = new ObservableCollection<Partie>(DataCacheService.Instance.Parties);
        public ObservableCollection<Equipe> Equipes { get; } = new ObservableCollection<Equipe>(DataCacheService.Instance.Equipes);

        public Partie PartieSelectionnee
        {
            get => _partieSelectionnee;
            set => SetProperty(ref _partieSelectionnee, value);
        }

        public Equipe EquipeASelectionnee
        {
            get => _equipeASelectionnee;
            set => SetProperty(ref _equipeASelectionnee, value);
        }

        public Equipe EquipeBSelectionnee
        {
            get => _equipeBSelectionnee;
            set => SetProperty(ref _equipeBSelectionnee, value);
        }

        public ICommand AjouterPartieCommand { get; }
        public ICommand SupprimerPartieCommand { get; }
        public ICommand ModifierPartieCommand { get; }

        public PartieViewModel()
        {
            AjouterPartieCommand = new Command<(DateTime Date, Equipe EquipeA, int ScoreA, Equipe EquipeB, int ScoreB)>(tuple =>
                AjouterPartie(tuple.Date, tuple.EquipeA, tuple.ScoreA, tuple.EquipeB, tuple.ScoreB));
            SupprimerPartieCommand = new Command<Partie>(SupprimerPartie);
            ModifierPartieCommand = new Command<(Partie Partie, DateTime NouvelleDate, Equipe NouvelleEquipeA, int NouveauScoreA, Equipe NouvelleEquipeB, int NouveauScoreB)>(tuple =>
                ModifierPartie(tuple.Partie, tuple.NouvelleDate, tuple.NouvelleEquipeA, tuple.NouveauScoreA, tuple.NouvelleEquipeB, tuple.NouveauScoreB));
        }

        public (bool Success, string Message) AjouterPartie(DateTime date, Equipe equipeA, int scoreA, Equipe equipeB, int scoreB)
        {
            if (equipeA == null || equipeB == null)
            {
                return (false, "Veuillez sélectionner deux équipes différentes.");
            }

            if (equipeA == equipeB)
            {
                return (false, "Les deux équipes sélectionnées sont identiques.");
            }

            var partie = new Partie { Date = date, EquipeA = equipeA, ScoreA = scoreA, EquipeB = equipeB, ScoreB = scoreB };
            Parties.Add(partie);
            DataCacheService.Instance.Parties.Add(partie);
            OnPropertyChanged(nameof(Parties));

            return (true, "Partie ajoutée avec succès");
        }

        public void SupprimerPartie(Partie partie)
        {
            if (Parties.Contains(partie))
            {
                Parties.Remove(partie);
                DataCacheService.Instance.Parties.Remove(partie);

                if (PartieSelectionnee == partie)
                    PartieSelectionnee = null;

                OnPropertyChanged(nameof(Parties));
            }
        }

        public void ModifierPartie(Partie partie, DateTime nouvelleDate, Equipe nouvelleEquipeA, int nouveauScoreA, Equipe nouvelleEquipeB, int nouveauScoreB)
        {
            if (partie != null)
            {
                partie.Date = nouvelleDate;
                partie.EquipeA = nouvelleEquipeA;
                partie.ScoreA = nouveauScoreA;
                partie.EquipeB = nouvelleEquipeB;
                partie.ScoreB = nouveauScoreB;
                OnPropertyChanged(nameof(Parties));
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
