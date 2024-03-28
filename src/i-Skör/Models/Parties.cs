using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace i_Skör.Models
{
    public class Partie : INotifyPropertyChanged
    {
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        private Equipe _equipeA;
        public Equipe EquipeA
        {
            get => _equipeA;
            set
            {
                if (_equipeA != value)
                {
                    _equipeA = value;
                    OnPropertyChanged(nameof(EquipeA));
                }
            }
        }

        private int _scoreA;
        public int ScoreA
        {
            get => _scoreA;
            set
            {
                if (_scoreA != value)
                {
                    _scoreA = value;
                    OnPropertyChanged(nameof(ScoreA));
                }
            }
        }

        private Equipe _equipeB;
        public Equipe EquipeB
        {
            get => _equipeB;
            set
            {
                if (_equipeB != value)
                {
                    _equipeB = value;
                    OnPropertyChanged(nameof(EquipeB));
                }
            }
        }

        private int _scoreB;
        public int ScoreB
        {
            get => _scoreB;
            set
            {
                if (_scoreB != value)
                {
                    _scoreB = value;
                    OnPropertyChanged(nameof(ScoreB));
                }
            }
        }

        public void IncrementerPartiesGagneesPourEquipeGagnante()
        {
            Equipe equipeGagnante = ScoreA > ScoreB ? EquipeA : EquipeB;
            if (equipeGagnante != null)
            {
                equipeGagnante.IncrementerPartiesGagnees();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
