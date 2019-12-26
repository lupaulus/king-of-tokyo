
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Modele{

   

    public class Jeton {

        public EffetJeton effet { get; }
        public int value { get; }

        public Jeton(EffetJeton effetJeton, int valeur) {
            effet = effetJeton;
            value = valeur;
        }



    }
}