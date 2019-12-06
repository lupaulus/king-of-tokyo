
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Controleur{
    public class Partie {

        #region Properties

        private int Id { get; }
        public Plateau Plateau { get; }
        private GMonstre GestionaryMonstre { get; }
        private GDes GestionaryDes { get;  }
        private GCarte GestionaryCarte { get;  }

        #endregion Properties

        #region Ctor
        public Partie(int idValue) {
            Id = idValue;
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