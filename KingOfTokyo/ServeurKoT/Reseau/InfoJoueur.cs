using ServeurKoT.Reseau;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeurKoT.Reseau
{
    public enum Monstre { UNKNOWN, J1, J2, J3, J4, J5, J6 }

    public class InfoJoueur : StreamObject
    {

        public Monstre IdJoueur { get; set; }
        public bool EstPret { get; set; }

        public string Pseudo { get; set; }
        public int PtsVie { get; set; }
        public int PtsVictoire { get; set; }
        public int PtsEnergie { get; set; }

        public bool AToiDeJouer { get; set; }

        public string ImageCarte1 { get; set; }
        public string ImageCarte2 { get; set; }
        public string ImageCarte3 { get; set; }


        public InfoJoueur()
        {
            this.IdJoueur = Monstre.UNKNOWN;
            this.Pseudo = "CLEARVOID";
            this.EstPret = false;

            this.PtsVie = -1;
            this.PtsVictoire = -1;
            this.PtsEnergie = -1;
            this.AToiDeJouer = false;

            this.ImageCarte1 = " ";
            this.ImageCarte2 = " ";
            this.ImageCarte3 = " ";
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
            this.AToiDeJouer = bool.Parse(tab[6]);

            this.ImageCarte1 = tab[7];
            this.ImageCarte2 = tab[8];
            this.ImageCarte3 = tab[9];
        }


        public override string IntoString()
        {
            return $"{(int)IdJoueur}|{Pseudo}|{EstPret.ToString()}|{PtsVie}|{PtsVictoire}|{PtsEnergie}|{AToiDeJouer}|{ImageCarte1}|{ImageCarte2}|{ImageCarte3}";
        }
    }
}
