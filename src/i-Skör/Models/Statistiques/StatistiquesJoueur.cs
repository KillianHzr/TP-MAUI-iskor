using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_Skör.Models.Statistiques
{
    public class StatistiquesJoueur
    {
        public Dictionary<string, StatistiquesJeu> StatistiquesParJeu { get; set; } = new Dictionary<string, StatistiquesJeu>();

        public void AjouterOuMettreAJourStatistiques(string jeu, StatistiquesJeu nouvellesStatistiques)
        {
            if (StatistiquesParJeu.ContainsKey(jeu))
            {
                StatistiquesParJeu[jeu].MettreAJour(nouvellesStatistiques);
            }
            else
            {
                StatistiquesParJeu.Add(jeu, nouvellesStatistiques);
            }
        }
    }

    public class StatistiquesJeu
    {
        public int Kills { get; set; }
        public int Morts { get; set; }
        public int Assistances { get; set; }

        public void MettreAJour(StatistiquesJeu nouvellesStatistiques)
        {
            Kills += nouvellesStatistiques.Kills;
            Morts += nouvellesStatistiques.Morts;
            Assistances += nouvellesStatistiques.Assistances;
        }
    }
}
