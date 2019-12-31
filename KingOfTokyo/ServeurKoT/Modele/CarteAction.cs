
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ServeurKoT.Modele{
    public class CarteAction : Carte , ICarte {

        public CarteAction(string nom, int coutEnergie, string description) 
            : base(nom,coutEnergie,description) {
            
        }


        public void AppliquerEffet(HashSet<ApplicationEffet> appEffet, HashSet<Effet> effet, HashSet<int> listeAppEffet, HashSet<int> listeEffet)
        {
            throw new NotImplementedException();
        }


    }

    [Serializable()]
    [XmlRoot("CarteActionCollection")]
    public class CarteActionCollection
    {
        [XmlArray("Cartes")]
        [XmlArrayItem("CarteAction", typeof(CarteAction))]
        public CarteAction[] Cartes { get; set; }
    }
}