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
            this.joueur = (Joueur) StreamObject.FromBytes(b,CommandeType.CONNEXIONSERVEUR);
            this.check = Boolean.Parse(b[10].ToString());
        }



        public override byte IntoBytes()
        {
            return new byte();
        }
    }
}
