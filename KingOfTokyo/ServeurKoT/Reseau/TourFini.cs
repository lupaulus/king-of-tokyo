using ServeurKoT.Controleur;
using ServeurKoT.Modele;
using System;
using System.Collections.Generic;

namespace ServeurKoT.Reseau
{

    public class TourFini : StreamObject
    {
        private string s;

        public TourFini()
        {
        }

        public TourFini(string s)
        {
            this.s = s;
        }

        public override string IntoString()
        {
            return $"";
        }

       
    }
}