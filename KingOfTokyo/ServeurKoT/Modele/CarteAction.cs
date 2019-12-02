
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Modele{
    public class CarteAction : Carte , ICarte {

        public CarteAction() {
        }

        public override void ajouterCarte()
        {
            throw new NotImplementedException();
        }

        public void appliquerEffet(HashSet<ApplicationEffet> appEffet, HashSet<Effet> effet, HashSet<int> listeAppEffet, HashSet<int> listeEffet)
        {
            throw new NotImplementedException();
        }

        public void carteAction()
        {
            // TODO implement here
        }

    }
}