
using ServeurKoT.Controleur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ServeurKoT.Modele{
    public abstract class Carte {

        #region Properties

        [XmlElement("Nom")]
        private string Nom;
        [XmlElement("CoutEnEnergie")]
        private int CoutEnEnergie;
        [XmlElement("Description")]
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