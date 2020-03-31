using ServeurKoT.Controleur;
using ServeurKoT.Modele;
using System;
using System.Collections.Generic;

namespace ServeurKoT.Reseau
{

    public class FinPartie : StreamObject
    {
        public string Pseudo { get; set; }
        public Monstre JoueurGagnant { get; set; }

        public FinPartie()
        {
            this.Pseudo = " ";
            this.JoueurGagnant = Monstre.UNKNOWN;
        }

        public FinPartie(string s)
        {
            string[] tab = s.Split('|');
            Pseudo = tab[0];
            JoueurGagnant = (Monstre)int.Parse(tab[1]);
        }

        public override string IntoString()
        {
            return $"{Pseudo}|{(int)JoueurGagnant}";
        }

       
    }
}