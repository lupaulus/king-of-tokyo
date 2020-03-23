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

        public ConnexionServeur(byte[] b)
        {
            this.joueur = (Joueur) StreamObject.FromString(b,CommandeType.CONNEXIONSERVEUR);
            this.check = Boolean.Parse(b[10].ToString());
        }



        public override byte IntoString()
        {
            return new byte();
        }
    }
}
