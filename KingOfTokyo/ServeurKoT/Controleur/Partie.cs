
using ServeurKoT.Modele;
using ServeurKoT.Reseau;
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
        public GDes GestionaryDes { get;  }
        public GCarte GestionaryCarte { get;  }
        public GTour GestionnaireDesTours { get;  }
        public Dictionary<MonstreJeu, Joueur> DicJeuMonstre { get; }

        #endregion Properties

        #region Ctor
        public Partie(int idValue, string nom, int nbrJoueur) {
            Id = idValue;
            nomPartie = nom;
            NombreDeJoueurTotal = nbrJoueur;
            DicJeuMonstre = new Dictionary<MonstreJeu, Joueur>();
            GestionaryDes = new GDes();
            GestionaryCarte = new GCarte();
            GestionnaireDesTours = new GTour();
            Plateau = new Plateau();
        }

        #endregion Ctor

        #region Methodes
        public void AjouterJoueur(Joueur j)
        {
            // Si place encore dispo         
            if(DicJeuMonstre.Count < NombreDeJoueurTotal)
            {
                j.IdJoueur = (Monstre)(DicJeuMonstre.Count+1);
                MonstreJeu m = new MonstreJeu((Monstre)(DicJeuMonstre.Count + 1));
                DicJeuMonstre.Add(m, j);
            }
            
        }

        public void DemarerPartie() {
            if (DicJeuMonstre.Count < 2)
            {
                return;
            }

            // Initialisation des infos
            foreach(MonstreJeu m in DicJeuMonstre.Keys)
            {
                DicJeuMonstre[m].PtsVie = m.PointVie;
                DicJeuMonstre[m].PtsVictoire = m.PointVictoire;
                DicJeuMonstre[m].PtsEnergie = m.Energie;
            }

            // Joueur qui commence la partie
            //GestionnaireDesTours.JoueurActuel();
        }

        public void FinirPartie() {
            // Renvoyer le gagnant.
        }

        
        #endregion Methodes

    }
}