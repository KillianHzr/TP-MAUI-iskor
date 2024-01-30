using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_Skör.Models.Statistiques
{
    public class StatistiquesPartie : Statistiques
    {
        public TimeSpan Duree { get; set; }
        public string Score { get; set; }
    }

}
