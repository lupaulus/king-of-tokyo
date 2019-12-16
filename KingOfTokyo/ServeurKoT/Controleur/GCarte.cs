
using ServeurKoT.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Controleur{
    public class GCarte {


        public Stack<Carte> ListDesCartes;

        public GCarte() {
            ImportCartes();
        }

        private void ImportCartes()
        {
            ListDesCartes = new Stack<Carte>();
            FactoryCarteAction factoryCarteAction = new FactoryCarteAction();
            FactoryCartePouvoir factoryCartePouvoir = new FactoryCartePouvoir();

        }
    }
}