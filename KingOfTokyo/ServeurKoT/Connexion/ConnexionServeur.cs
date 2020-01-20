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

        public ConnexionServeur(byte[] b)
        {
            this.joueur = Joueur.FromBytes(b);
            this.check = true;
        }



        public override byte IntoBytes()
        {
            byte 
            return
        }
    }
}
