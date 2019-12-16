
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Modele{
    public class CartePouvoir : Carte , ICarte {

        public CartePouvoir(string nom, int coutEnergie, string description)
            : base(nom, coutEnergie, description)
        {
        }

        public override void AjouterCarte()
        {
            throw new NotImplementedException();
        }

        public void AppliquerEffet(HashSet<ApplicationEffet> appEffet, HashSet<Effet> effet, HashSet<int> listeAppEffet, HashSet<int> listeEffet)
        {
            throw new NotImplementedException();
        }


    }
}