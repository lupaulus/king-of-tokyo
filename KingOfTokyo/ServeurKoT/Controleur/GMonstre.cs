
using ServeurKoT.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Controleur
{
    public class GMonstre
    {
        #region Properties
        /// <summary>
        /// Variable de gestion des ID
        /// </summary>
        private int nextId;
        /// <summary>
        /// Nombre de joueur dans la partie
        /// </summary>
        private int NombreJoueur;
        /// <summary>
        /// Liste des montres dans la partie
        /// </summary>
        private List<Monstre> listeMonstre;
        #endregion Properties

        #region Ctor
        public GMonstre(int nbrJoueur)
        {
            nextId = 1;
            NombreJoueur = nbrJoueur;
            listeMonstre = new List<Monstre>();
        }
        #endregion Ctor

        #region Methods
        public void ajouterMonstre(NomMonstre nom)
        {
            Monstre monstre = new Monstre(nextId,nom);
            nextId++;
            listeMonstre.Add(monstre);
        }

        public void supprimerMonstre(Monstre monstre)
        {
            listeMonstre.Remove(monstre);
        }

        public List<Monstre> getListeMonstre()
        {
            return new List<Monstre>(listeMonstre);
        }
        #endregion Methods
    }
}