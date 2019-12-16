
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Controleur{
    public class Partie {

        #region Properties

        public int Id { get; }
        public int NombreDeJoueur { get; }
        public string nomPartie { get; }
        public Plateau Plateau { get; }
        public GMonstre GestionaryMonstre { get; }
        public GDes GestionaryDes { get;  }
        public GCarte GestionaryCarte { get;  }

        #endregion Properties

        #region Ctor
        public Partie(int idValue, string nom, int nbrJoueur) {
            Id = idValue;
            nomPartie = nom;
            NombreDeJoueur = nbrJoueur; 
            GestionaryMonstre = new GMonstre(nbrJoueur);
            GestionaryDes = new GDes();
            GestionaryCarte = new GCarte();
            Plateau = new Plateau(GestionaryMonstre.getListeMonstre());
        }

        

        #endregion Ctor

        #region Methodes

        public void DemarerPartie() {
            // Choix du premier Joueur à jouer

        }

        public void FinirPartie() {
            // Renvoyer le gagnant.
        }

        
        #endregion Methodes

    }
}