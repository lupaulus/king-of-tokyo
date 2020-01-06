
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ServeurKoT.Modele{
    public class CartePouvoir : Carte , ICarte {

        public CartePouvoir(string nom, int coutEnergie, string description, string cheminImg)
            : base(nom, coutEnergie, description,cheminImg)
        {
            // base
            //this.Nom = nom;
            //this.CoutEnEnergie = coutEnergie;
            //this.Description = description;


            // Spécifique cartes Pouvoir

            
        }

        public void AppliquerEffet(HashSet<ApplicationEffet> appEffet, HashSet<Effet> effet, HashSet<int> listeAppEffet, HashSet<int> listeEffet)
        {
            throw new NotImplementedException();
        }


        [XmlRoot("CartePouvoirCollection")]
        public class CartePouvoirCollection
        {
            [XmlArray("Cartes")]
            [XmlArrayItem("CartePouvoir", typeof(CartePouvoir))]
            public CartePouvoir[] Cartes { get; set; }
        }


    }
}