
using ServeurKoT.Controleur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ServeurKoT.Modele{
    public abstract class Carte {

        #region Properties


        protected string Nom { get; set; }

        protected int CoutEnEnergie { get; set; }

        protected string Description { get; set; }

        public string CheminImgCarte { get; set; }

        #endregion Properties

        #region Ctor
        public Carte(string nom, int coutEnergie, string description, string cheminImg) 
        {
            Nom = nom;
            CoutEnEnergie = coutEnergie;
            Description = description;
            CheminImgCarte = cheminImg;
        }
        #endregion Ctor






    }
}