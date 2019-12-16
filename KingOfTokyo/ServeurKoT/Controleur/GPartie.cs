
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
        /// <summary>
        /// Variable de gestion des ID 
        /// </summary>
        private int NextId = 1;
        /// <summary>
        /// Constante indiquant le nombre de joueur max
        /// </summary>
        public static int NOMBRE_JOUEUR_MAX = 6;
        /// <summary>
        /// Constante indiquant le nombre de joueur min
        /// </summary>
        public static int NOMBRE_JOUEUR_MIN = 2;
        /// <summary>
        /// List<> Contenant l'ensemble des parties
        /// </summary>
        private List<Partie> listeDesParties;
        /// <summary>
        /// Variable de l'instance
        /// </summary>
        private static GPartie InstanceValue = null;
        /// <summary>
        /// Propriétes pour récuperer l'instance
        /// </summary>
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

        public int CreerPartie(string nomPartie, int nbrJoueur) {

            if(nbrJoueur<NOMBRE_JOUEUR_MIN || nbrJoueur>NOMBRE_JOUEUR_MAX)
            {
                throw new InvalidOperationException();
            }

            Logger.Log(Logger.Level.Info, 
                String.Format("Création de la partie : {0} ({1})", nomPartie, NextId));
            int res = NextId;
            Partie partie = new Partie(NextId, nomPartie, nbrJoueur);
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