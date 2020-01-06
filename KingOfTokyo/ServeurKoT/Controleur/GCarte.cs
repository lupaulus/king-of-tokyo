
using ServeurKoT.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

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

            List<Carte> list = factoryCarteAction.AjouterCarte();
            list.AddRange(factoryCartePouvoir.AjouterCarte());

            //C'est sale mamene
            ListDesCartes = (Stack<Carte>) list.Cast<Carte>();
        }
     
    }
}