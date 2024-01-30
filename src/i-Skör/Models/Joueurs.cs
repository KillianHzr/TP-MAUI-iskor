using i_Skör.Models.Statistiques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_Skör.Models
{
    public class Joueur
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Pseudo { get; set; }
        public Equipe Equipe { get; set; }
        public StatistiquesJoueur Statistiques { get; set; }
    }
}
