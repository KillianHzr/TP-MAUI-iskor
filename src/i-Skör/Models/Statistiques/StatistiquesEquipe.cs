using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_Skör.Models.Statistiques
{
    public class StatistiquesEquipe : Statistiques
    {
        public int NombreDeMatchs { get; set; }
        public int Victoires { get; set; }
        public int Defaites => NombreDeMatchs - Victoires;
        public double TauxDeVictoire => (double)Victoires / NombreDeMatchs;
    }
}
