using System;
using System.Collections.Generic;
using i_Skör.Models;
using i_Skör.Models.Statistiques;

namespace i_Skör.Models
{
    public class Partie
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Jeu { get; set; }
        public List<Equipe> Equipes { get; set; } = new List<Equipe>();
        public Dictionary<Equipe, int> Scores { get; set; } = new Dictionary<Equipe, int>();
        public StatistiquesPartie Statistiques { get; set; }
    }
}
