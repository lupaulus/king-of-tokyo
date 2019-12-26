
using ServeurKoT.Controleur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Modele{
    public abstract class Carte : FactoryCarte {

        #region Properties

        private string Nom;

        private int CoutEnEnergie;

        private string Description;

        #endregion Properties

        #region Ctor
        public Carte(string nom, int coutEnergie, string description) 
        {
            Nom = nom;
            CoutEnEnergie = coutEnergie;
            Description = description;
        }
        #endregion Ctor






    }
}