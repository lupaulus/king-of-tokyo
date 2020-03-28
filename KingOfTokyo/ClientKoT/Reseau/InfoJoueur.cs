using Client.Reseau;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Reseau
{
    public class InfoJoueur : StreamObject
    {
        public enum Monstre { UNKNOWN, J1, J2, J3, J4, J5, J6 }
        public Monstre IdJoueur { get; set; }
        public bool EstPret { get; set; }
        public string Pseudo { get; set; }
        public int PtsVie { get; set; }
        public int PtsVictoire { get; set; }
        public int PtsEnergie { get; set; }


        public InfoJoueur()
        {
            this.IdJoueur = Monstre.UNKNOWN;
            this.Pseudo = String.Empty;
            this.EstPret = false;
 
            this.PtsVie = -1;
            this.PtsVictoire = -1;
            this.PtsEnergie = -1; 

        }

        public InfoJoueur(string b)
        {
            string[] tab = b.Split('|');
            this.IdJoueur = (Monstre)int.Parse(tab[0]);
            this.Pseudo = tab[1];
            this.EstPret = bool.Parse(tab[2]);

            this.PtsVie = int.Parse(tab[3]);
            this.PtsVictoire = int.Parse(tab[4]);
            this.PtsEnergie = int.Parse(tab[5]);
            

        }


        public override string IntoString()
        {
            return $"{(int)IdJoueur}|{Pseudo}|{EstPret.ToString()}|{PtsVie}|{PtsVictoire}|{PtsEnergie}";
        }
    }
}
