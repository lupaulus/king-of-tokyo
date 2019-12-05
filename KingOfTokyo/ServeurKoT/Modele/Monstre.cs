
using ServeurKoT.Controleur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Modele{
    public class Monstre {

        public Monstre(string nomDonne) {
            nom = nomDonne;
            pointVictoire = POINT_VICTOIRE_BASE;
            pointVie = POINT_VIE_BASE;
            energie = POINT_ENERGIE_BASE;
        }

        public string nom { get; }
        public int pointVie { get; set; } 
        public int pointVictoire { get; set; }
        public int energie { get; set; }

        private static int POINT_VIE_MIN = 0;

        private static int POINT_VICTOIRE_BASE = 0;

        private static int POINT_VIE_MAX = 12;

        private static int POINT_ENERGIE_MAX = 20;

        private static int POINT_ENERGIE_BASE = 0;

        private static int POINT_VIE_BASE = 10;
        private List<Carte> listeCartes { get; }
        private List<Carte> effetCartes { get; }

        /// <summary>
        /// Méthode pour rétirer la vie hors combat
        /// </summary>
        /// <param name="value">Valeur de vie à ajouter</param>
        public void retirerVie(int value)
        {
            pointVie -= value;
            if(pointVie < POINT_VIE_MIN)
            {
                pointVie = POINT_VIE_MIN;
            }
        }
        /// <summary>
        /// Méthode pour ajouter la vie hors combat
        /// </summary>
        /// <param name="value">Valeur de vie à retirer</param>
        public void ajouterVie(int value)
        {
            pointVie += value;
            if(pointVie > POINT_VIE_MAX)
            {
                pointVie = POINT_VIE_MAX;
            }
        }
        public void frapper(Monstre monstreAFrapper, int valeurDegat)
        {
            monstreAFrapper.seFaireFrapper(valeurDegat);
        }
        public void seFaireFrapper(int valeurDegat)
        {
            retirerVie(valeurDegat);
        }

        public void appliquerJeton() {
            // TODO implement here
        }

        public void appliquerCarte() {
            // TODO implement here
        }

        public void choisirDes() {
            // TODO implement here
        }

        public void achatCarte() {
            // TODO implement here
        }


    }
}