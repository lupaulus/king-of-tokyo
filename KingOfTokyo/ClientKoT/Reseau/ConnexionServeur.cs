using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Reseau
{
    class ConnexionServeur : StreamObject
    {
        int tailleIdJoueur = 1;
        bool check;


        public ConnexionServeur()
        {
            
        }

        public ConnexionServeur(string s)
        {
            
        }



        public override string IntoString()
        {
            return $"{check.ToString()};"
        }
    }
}
