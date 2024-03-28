using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using i_Skör.Models;
using i_Skör.Services;

namespace i_Skör.ViewModels
{
    public class PartieViewModel : INotifyPropertyChanged
    {
        private Partie _partie;
        private ObservableCollection<Equipe> _equipes;
        private Equipe _equipeA;
        private Equipe _equipeB;
        private int _scoreEquipeA;
        private int _scoreEquipeB;
        private ObservableCollection<Partie> _parties;

        public PartieViewModel(Partie partie = null)
        {
            _partie = partie ?? new Partie();
            _equipes = new ObservableCollection<Equipe>(DataCacheService.Instance.Equipes);
            _parties = new ObservableCollection<Partie>(DataCacheService.Instance.Parties);

            AjouterPartieCommand = new Command(AddPartie);
            SupprimerPartieCommand = new Command<Partie>(DeletePartie);
            EquipeASelectionneeCommand = new Command<Equipe>(EquipeASelectionnee);
            EquipeBSelectionneeCommand = new Command<Equipe>(EquipeBSelectionnee);
        }

        public ObservableCollection<Equipe> Equipes
        {
            get => _equipes;
            set
            {
                _equipes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Partie> Parties
        {
            get => _parties;
            set
            {
                _parties = value;
                OnPropertyChanged();
            }
        }

        public Equipe EquipeA
        {
            get => _equipeA;
            set
            {
                _equipeA = value;
                OnPropertyChanged();
            }
        }

        public Equipe EquipeB
        {
            get => _equipeB;
            set
            {
                _equipeB = value;
                OnPropertyChanged();
            }
        }

        public int ScoreEquipeA
        {
            get => _scoreEquipeA;
            set
            {
                _scoreEquipeA = value;
                OnPropertyChanged();
            }
        }

        public int ScoreEquipeB
        {
            get => _scoreEquipeB;
            set
            {
                _scoreEquipeB = value;
                OnPropertyChanged();
            }
        }

        public ICommand AjouterPartieCommand { get; }
        public ICommand SupprimerPartieCommand { get; }
        public ICommand EquipeASelectionneeCommand { get; }
        public ICommand EquipeBSelectionneeCommand { get; }

        private void AddPartie()
        {
            if (EquipeA == null || EquipeB == null)
            {
                return;
            }

            var nouvellePartie = new Partie
            {
                Equipes = new List<Equipe> { EquipeA, EquipeB },
                Scores = new Dictionary<Equipe, int>
        {
            { EquipeA, ScoreEquipeA },
            { EquipeB, ScoreEquipeB }
        },
                Date = DateTime.Now
            };

            DataCacheService.Instance.Parties.Add(nouvellePartie);
            Parties.Add(nouvellePartie);

            EquipeA = null;
            EquipeB = null;
            ScoreEquipeA = 0;
            ScoreEquipeB = 0;
            Parties.Add(_partie);
        }


        private void DeletePartie(Partie partie)
        {
            DataCacheService.Instance.Parties.Remove(partie);
        }

        private void EquipeASelectionnee(Equipe equipe)
        {
            EquipeA = equipe;
        }

        private void EquipeBSelectionnee(Equipe equipe)
        {
            EquipeB = equipe;
        }

        public string ScoreEquipeAString
        {
            get
            {
                if (_partie != null && _partie.Scores != null && _partie.Scores.ContainsKey(EquipeA))
                {
                    return _partie.Scores[EquipeA].ToString();
                }
                return string.Empty;
            }
        }

        public string ScoreEquipeBString
        {
            get
            {
                if (_partie != null && _partie.Scores != null && _partie.Scores.ContainsKey(EquipeB))
                {
                    return _partie.Scores[EquipeB].ToString();
                }
                return string.Empty;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
