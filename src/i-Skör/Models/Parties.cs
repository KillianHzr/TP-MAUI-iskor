using i_Skör.Models.Statistiques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_Skör.Models
{
    public class Partie
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Jeu { get; set; }
        public List<Equipe> Equipes { get; set; } = new List<Equipe>();
        public string Score { get; set; }
        public StatistiquesPartie Statistiques { get; set; }
    }
}
