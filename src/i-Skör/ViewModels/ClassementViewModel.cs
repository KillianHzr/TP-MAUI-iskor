using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using i_Skör.Models;
using i_Skör.Services;

namespace i_Skör.ViewModels
{
    public class ClassementViewModel : BaseViewModel
    {
        public ObservableCollection<Equipe> Equipes { get; } = new ObservableCollection<Equipe>();

        public ClassementViewModel()
        {
            Equipes = new ObservableCollection<Equipe>();
            UpdateEquipes();
        }

        private void UpdateEquipes()
        {
            var equipes = DataCacheService.Instance.Equipes;
            var parties = DataCacheService.Instance.Parties;

            foreach (var equipe in equipes)
            {
                equipe.PartiesGagnees = 0;
            }

            foreach (var partie in parties)
            {
                partie.IncrementerPartiesGagneesPourEquipeGagnante();
            }

            Equipes.Clear();
            foreach (var equipe in equipes.OrderByDescending(e => e.PartiesGagnees))
            {
                Equipes.Add(equipe);
            }
        }
    }
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
