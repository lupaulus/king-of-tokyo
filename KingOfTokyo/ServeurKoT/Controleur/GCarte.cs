
using ServeurKoT.Modele;
using SimpleLogger;
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
            Logger.Log(Logger.Level.Info, "Import Cartes en cours");
            ListDesCartes = new Stack<Carte>();

            FactoryCarteAction factoryCarteAction = new FactoryCarteAction();
            FactoryCartePouvoir factoryCartePouvoir = new FactoryCartePouvoir();

            List<Carte> list = factoryCarteAction.AjouterCarte();
            int nbrAction = list.Count;
            list.AddRange(factoryCartePouvoir.AjouterCarte());

            //C'est sale mamene
            ListDesCartes = new Stack<Carte>(list);
            Logger.Log(Logger.Level.Info, 
                String.Format("Import Cartes fini; {0} Cartes au total",list.Count));
            Logger.Log(Logger.Level.Info,
                String.Format("{0} Cartes Actions | {1} Cartes Pouvoir", nbrAction,list.Count-nbrAction));
        }


    }
}