using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeurKoT.Connexion
{
    class ConnexionServeur : StreamObject
    {
        public int NbrJoueurActuellement { get; set; }
        public bool ConnexionOK { get; set; }


        public ConnexionServeur()
        {
            this.NbrJoueurActuellement = 0;
            this.ConnexionOK = false;
        }

        public ConnexionServeur(string b)
        {
            string[] tab = b.Split('|');
            ConnexionOK = bool.Parse(tab[0]);
            NbrJoueurActuellement = int.Parse(tab[1]);

        }


        public override string IntoString()
        {
            return $"{ConnexionOK.ToString()}|{NbrJoueurActuellement}";
        }
    }
}
