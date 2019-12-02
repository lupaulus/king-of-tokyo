
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Modele{
    public class CartePouvoir : Carte , ICarte {

        public CartePouvoir() {
        }

        public override void ajouterCarte()
        {
            throw new NotImplementedException();
        }

        public void appliquerEffet(HashSet<ApplicationEffet> appEffet, HashSet<Effet> effet, HashSet<int> listeAppEffet, HashSet<int> listeEffet)
        {
            throw new NotImplementedException();
        }

        public void cartePouvoir() {
            // TODO implement here
        }

    }
}