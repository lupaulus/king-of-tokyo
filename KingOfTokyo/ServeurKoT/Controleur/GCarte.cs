
using ServeurKoT.Modele;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ServeurKoT.Controleur{
    public class GCarte {

        private Stack<Carte> ListDesCartes;

        public List<Carte> CartePlateau { get; set; }

        public GCarte() {
            ImportCartes();
            Randomize();
            CartePlateau = GetTroisCartes();
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

        private void Randomize()
        {
            ListDesCartes = Shuffle<Carte>(ListDesCartes);
        }

        public static Stack<T> Shuffle<T>(Stack<T> stack)
        {
            Random rnd = new Random();
            return new Stack<T>(stack.OrderBy(x => rnd.Next()));
        }

        public List<Carte>  GetTroisCartes()
        {
            List<Carte> res = new List<Carte>();
            for(int i =0;i<3;i++)
            {
                res.Add(ListDesCartes.Pop());
            }
            
            return res;
        }

    }
}