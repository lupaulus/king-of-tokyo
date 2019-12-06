
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Controleur{
    public class Partie {

        #region Properties

        public int Id { get; }
        public string nomPartie { get; }
        public Plateau Plateau { get; }
        public GMonstre GestionaryMonstre { get; }
        public GDes GestionaryDes { get;  }
        public GCarte GestionaryCarte { get;  }

        #endregion Properties

        #region Ctor
        public Partie(int idValue, string nom) {
            Id = idValue;
            nomPartie = nom;
            GestionaryMonstre = new GMonstre();
            GestionaryDes = new GDes();
            GestionaryCarte = new GCarte();
            Plateau = new Plateau(GestionaryMonstre.getListeMonstre());
        }

        

        #endregion Ctor

        #region Methodes

        public void demarerPartie() {
            // TODO implement here
        }

        public void finirPartie() {
            // TODO implement here
        }

        
        #endregion Methodes

    }
}