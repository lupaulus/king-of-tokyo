
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Controleur{
    /// <summary>
    /// Singleton
    /// </summary>
    public class GPartie {

        #region Properties

        public static int NOMBRE_JOUEUR_MAX = 6;

        public static int NOMBRE_JOUEUR_MIN = 2;

        private int nextId = 1;

        private List<Partie> listeDesParties;

        private static GPartie InstanceValue = null;
        public static GPartie Instance
        {
            get
            {
                if (InstanceValue == null)
                {
                    InstanceValue = new GPartie();
                }
                return InstanceValue;
            }
        }

        #endregion Properties


        #region Ctor
        private GPartie()
        {
            listeDesParties = new List<Partie>();
        }
        #endregion Ctor

        public int CreerPartie(string nomPartie) {
            Logger.Log(Logger.Level.Info, 
                String.Format("Création de la partie : {0} ({1})", nomPartie, nextId));
            int res = nextId;
            Partie partie = new Partie(nextId, nomPartie);
            listeDesParties.Add(partie);
            return res;
        }

        public Partie TrouverPartie(int numPartie)
        {
            foreach(Partie p in listeDesParties)
            {
                if(numPartie == p.Id)
                {
                    return p;
                }
            }
            throw new InvalidOperationException();
        }
        private void SupprimerPartie(int id) {
            Partie partie = TrouverPartie(id);
            listeDesParties.Remove(partie);
            Logger.Log(Logger.Level.Info,
                String.Format("Suppresion partie : {0} ({1})", partie.nomPartie, id));
        }

    }
}