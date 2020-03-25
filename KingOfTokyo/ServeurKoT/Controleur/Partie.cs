
using ServeurKoT.Connexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Controleur{
    public class Partie {

        #region Properties

        public int Id { get; }
        public int NombreDeJoueurTotal { get; }
        public string nomPartie { get; }
        public Plateau Plateau { get; }
        public GMonstre GestionaryMonstre { get; }
        public GDes GestionaryDes { get;  }
        public GCarte GestionaryCarte { get;  }
        public Queue<Joueur> ListeDesJoueurs { get; }

        #endregion Properties

        #region Ctor
        public Partie(int idValue, string nom, int nbrJoueur) {
            Id = idValue;
            nomPartie = nom;
            NombreDeJoueurTotal = nbrJoueur;
            ListeDesJoueurs = new Queue<Joueur>();
            GestionaryMonstre = new GMonstre(nbrJoueur);
            GestionaryDes = new GDes();
            GestionaryCarte = new GCarte();
            Plateau = new Plateau(GestionaryMonstre.getListeMonstre());
        }

        #endregion Ctor

        #region Methodes
        public void AjouterJoueur(Joueur j)
        {
            // Si place encore dispo
            if(ListeDesJoueurs.Count < NombreDeJoueurTotal)
            {
                ListeDesJoueurs.Enqueue(j);
            }
            
        }

        public void DemarerPartie() {
            if (ListeDesJoueurs.Count > 2)
            {

            }

        }

        public void FinirPartie() {
            // Renvoyer le gagnant.
        }

        
        #endregion Methodes

    }
}