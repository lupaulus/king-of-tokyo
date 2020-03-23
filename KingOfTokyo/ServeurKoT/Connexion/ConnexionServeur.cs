using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeurKoT.Connexion
{
    class ConnexionServeur : StreamObject
    {
        int tailleIdJoueur = 1;
        bool check;


        public ConnexionServeur()
        {
            
        }

        public ConnexionServeur(string b)
        {
            
            this.check = Boolean.Parse(b[10].ToString());
        }



        public override string IntoString()
        {
            return String.Empty;
        }
    }
}
