using i_Skör.Models.Statistiques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_Skör.Models
{
    public class Equipe
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public List<Joueur> Membres { get; set; } = new List<Joueur>();
        public StatistiquesEquipe Statistiques { get; set; }
        public int PartiesGagnees { get; set; }

        public void IncrementerPartiesGagnees()
        {
            PartiesGagnees++;
        }
    }
}
