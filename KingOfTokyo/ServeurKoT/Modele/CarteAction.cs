
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ServeurKoT.Modele{
    public class CarteAction : Carte , ICarte {


        public Dictionary<Effet, int> ValeurEffet { get; set; }
        public ApplicationEffet EffetCarteCible { get; set; }

        public int Speciale { get; set; }


        public CarteAction(string nom, int coutEnergie, string description, string cheminImg,
             Dictionary<Effet, int> valeurEffet, ApplicationEffet application, int speciale)
            : base(nom, coutEnergie, description, cheminImg)
        {

            // base
            //this.Nom = nom;
            //this.CoutEnEnergie = coutEnergie;
            //this.Description = description;


            // Spécifique cartes Actions
            this.ValeurEffet = valeurEffet;
            this.EffetCarteCible = application;
            this.Speciale = speciale;

        }



        public void AppliquerEffet(HashSet<ApplicationEffet> appEffet, HashSet<Effet> effet, HashSet<int> listeAppEffet, HashSet<int> listeEffet)
        {
            throw new NotImplementedException();
        }


    }

   
}