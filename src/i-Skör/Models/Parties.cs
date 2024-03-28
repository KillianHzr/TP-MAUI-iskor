using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using i_Skör.Models;
using i_Skör.Models.Statistiques;

namespace i_Skör.Models
{
    public class Partie
    {
        private int _id;
        private DateTime _date;
        private Equipe _equipea;
        private int _scorea;
        private Equipe _equipeb;
        private int _scoreb;

        public int ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        public Equipe EquipeA
        {
            get => _equipea;
            set => SetProperty(ref _equipea, value);
        }
        public int ScoreA
        {
            get => _scorea;
            set => SetProperty(ref _scorea, value);
        }
        public Equipe EquipeB
        {
            get => _equipeb;
            set => SetProperty(ref _equipeb, value);
        }
        public int ScoreB
        {
            get => _scoreb;
            set => SetProperty(ref _scoreb, value);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}