using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using i_Skör.Models;
using i_Skör.ViewModels;

namespace i_Skör.Services
{
    public class DataCacheService
    {
        private static DataCacheService _instance;

        public static DataCacheService Instance => _instance ?? (_instance = new DataCacheService());

        public List<Equipe> Equipes { get; } = new List<Equipe>();
        public List<Joueur> Joueurs { get; } = new List<Joueur>();
        public List<Partie> Parties { get; } = new List<Partie>();

        private DataCacheService()
        {
        }
    }
}
