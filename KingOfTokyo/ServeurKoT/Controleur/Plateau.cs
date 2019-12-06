
using ServeurKoT.Modele;
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
        public Plateau(List<Monstre> monstres) 
        {
            tousLesMonstres = new List<Monstre>(monstres);
        }

        private List<Monstre> monstreEnVille { get; }

        private List<Monstre> monstreHorsVille { get; }
        private List<Monstre> tousLesMonstres { get; }

        
        /// <summary>
        /// Methode pour rentrer dans la ville
        /// </summary>
        /// <param name="monstre">Monstre qui doit rentrer dans la ville</param>
        public void entrerVille(Monstre monstre)
        {
            monstreEnVille.Add(monstre);
        }

        /// <summary>
        /// Methode pour sortir de la ville
        /// </summary>
        /// <param name="monstre">Monstre qui doit sortir la ville</param>
        public void sortirVille(Monstre monstre)
        {
            monstreEnVille.Remove(monstre);
        }

        

        public List<Monstre> ListeDesMonstreEnVie(List<Monstre> monstres)
        {
            List<Monstre> res = new List<Monstre>();
            foreach(Monstre monstre in monstres)
            {
                if(!monstre.monstreMort())
                {
                    res.Add(monstre);
                }
            }
            return res;
        }

        public void frapperLaVille(int valueFrappe)
        {
            foreach(Monstre monstre in monstreEnVille)
            {
                monstre.seFaireFrapper(valueFrappe);
            }
        }


    }
}