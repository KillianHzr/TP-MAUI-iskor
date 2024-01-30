using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_Skör.Models.Statistiques
{
    public class StatistiquesEquipePartie : StatistiquesEquipe
    {
        public int NombreDeKills { get; set; }
        public int NombreDeMorts { get; set; }
        public int NombreDAssistances { get; set; }
        public int ManchesGagnees { get; set; }
        public int ManchesPerdues { get; set; }
    }

}
