
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
        public Plateau() 
        {
        }

        private List<MonstreJeu> monstreEnVille { get; }

        private List<MonstreJeu> monstreHorsVille { get; }
        private List<MonstreJeu> tousLesMonstres { get; }

        
        /// <summary>
        /// Methode pour rentrer dans la ville
        /// </summary>
        /// <param name="monstre">Monstre qui doit rentrer dans la ville</param>
        public void entrerVille(MonstreJeu monstre)
        {
            monstreEnVille.Add(monstre);
        }

        /// <summary>
        /// Methode pour sortir de la ville
        /// </summary>
        /// <param name="monstre">Monstre qui doit sortir la ville</param>
        public void sortirVille(MonstreJeu monstre)
        {
            monstreEnVille.Remove(monstre);
        }

        

        public List<MonstreJeu> ListeDesMonstreEnVie(List<MonstreJeu> monstres)
        {
            List<MonstreJeu> res = new List<MonstreJeu>();
            foreach(MonstreJeu monstre in monstres)
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
            foreach(MonstreJeu monstre in monstreEnVille)
            {
                monstre.seFaireFrapper(valueFrappe);
            }
        }


    }
}