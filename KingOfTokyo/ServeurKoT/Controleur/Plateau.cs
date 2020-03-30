
using ServeurKoT.Modele;
using ServeurKoT.Reseau;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Controleur{
    /// <summary>
    /// Singleton
    /// </summary>
    public class Plateau {



        /// <summary>
        /// Création du plateau
        /// </summary>
        /// <param name="monstres">Liste des monstres qui compose le plateau</param>
        public Plateau() 
        {}

        
        /// <summary>
        /// Methode pour rentrer dans la ville
        /// </summary>
        /// <param name="monstre">Monstre qui doit rentrer dans la ville</param>
        public void entrerVille(Joueur monstre)
        {
            monstre.EstEnVille = true;
        }

        /// <summary>
        /// Methode pour sortir de la ville
        /// </summary>
        /// <param name="monstre">Monstre qui doit sortir la ville</param>
        public void sortirVille(Joueur monstre)
        {
            monstre.EstEnVille = false;
        }

        


        public void frapperLaVille(List<Joueur> j, int valueFrappe)
        {
            foreach(Joueur monstre in j)
            {
                if(monstre.EstEnVille)
                {
                    monstre.seFaireFrapper(valueFrappe);
                }
            }
        }

        public void frapperHorsVille(List<Joueur> j, int valueFrappe)
        {
            foreach (Joueur monstre in j)
            {
                if (!monstre.EstEnVille)
                {
                    monstre.seFaireFrapper(valueFrappe);
                }
            }
        }


    }
}