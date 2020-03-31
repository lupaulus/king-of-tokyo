
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
        public GTour GestionnaireDesTours { get; set; }
        public List<Joueur> DicJeuMonstre { get; }

        #endregion Properties

        #region Ctor
        public Partie(int idValue, string nom, int nbrJoueur) {
            Id = idValue;
            nomPartie = nom;
            NombreDeJoueurTotal = nbrJoueur;
            DicJeuMonstre = new List<Joueur>();
            GestionaryDes = new GDes();
            GestionaryCarte = new GCarte();
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
                DicJeuMonstre.Add(j);
            }
            
        }

        public void DemarerPartie() {
            if (DicJeuMonstre.Count < 2)
            {
                return;
            }


            GestionnaireDesTours = new GTour(DicJeuMonstre);

            // Monstre dans la ville au début
            Plateau.entrerVille(GestionnaireDesTours.JoueurActuel);

            // Joueur qui commence la partie
            GestionnaireDesTours.JoueurActuel.AToiDeJouer = true;
        }


        public void ProchainTour()
        {
            GestionnaireDesTours.JoueurActuel.AToiDeJouer = false;
            GestionnaireDesTours.ProchainTour();
            GestionnaireDesTours.JoueurActuel.AToiDeJouer = true;
        }
        
        public Joueur CheckSiPartieFini()
        {
            int nbrAlive = 0;
            Joueur res = new Joueur(99,"PersoTest");
            // Check Point de victoire
            foreach(Joueur j in DicJeuMonstre)
            {
                if(j.PtsVictoire >= Joueur.POINT_VICTOIRE && !j.monstreMort())
                {
                    return j;
                }
                if(!j.monstreMort())
                {
                    nbrAlive++;
                    res = j;
                }
            }

            if(nbrAlive == 1)
            {
                return res;
            }
            res = new Joueur(99, "PersoTest");
            return res;
        }

        public string[] ListCheminImgCartesPlateau()
        {
            string[] tab = new string[3];
            for(int i=0;i<3;i++)
            {
                tab[i] = GestionaryCarte.CartePlateau[i].CheminImgCarte;
            }
            return tab;
        }

        public List<Des> LancerDes()
        {
            return GestionaryDes.LancementDes();
        }

        public void ResolutionDes(ActionTour t)
        {
            List<ValeurDes> list = new List<ValeurDes>();
            list.Add(t.Des1);
            list.Add(t.Des2);
            list.Add(t.Des3);
            list.Add(t.Des4);
            list.Add(t.Des5);
            list.Add(t.Des6);
            EffetDes(list);
        }

        private void EffetDes(List<ValeurDes> des)
        {
            int compteurUn = 0;
            int compteurDeux = 0;
            int compteurTrois = 0;
            int compteurBaffe = 0;
            int compteurEnergie = 0;
            int compteurSoin = 0;
            foreach(ValeurDes d in des)
            {
                switch(d)
                {
                    case ValeurDes.Un:
                        compteurUn++;
                        break;
                    case ValeurDes.Deux:
                        compteurDeux++;
                        break;
                    case ValeurDes.Trois:
                        compteurTrois++;
                        break;
                    case ValeurDes.Baffe:
                        compteurBaffe++;
                        break;
                    case ValeurDes.Soin:
                        compteurSoin++;
                        break;
                    case ValeurDes.Energie:
                        compteurEnergie++;
                        break;
                }
            }
            if(compteurUn >= 3)
            {
                int value = 1 + (compteurUn - 3);
                GPartie.Instance.PartieActuel.GestionnaireDesTours.JoueurActuel.ajouterPtsVictoire(value);
            }
            if(compteurDeux >= 3)
            {
                int value = 2 + (compteurDeux - 3);
                GPartie.Instance.PartieActuel.GestionnaireDesTours.JoueurActuel.ajouterPtsVictoire(value);
            }
            if(compteurTrois >= 3)
            {
                int value = 3 + (compteurTrois - 3);
                GPartie.Instance.PartieActuel.GestionnaireDesTours.JoueurActuel.ajouterPtsVictoire(value);
            }
            GPartie.Instance.PartieActuel.GestionnaireDesTours.JoueurActuel.ajouterVie(compteurSoin);
            GPartie.Instance.PartieActuel.GestionnaireDesTours.JoueurActuel.ajouterEnergie(compteurEnergie);
            if(compteurBaffe > 0)
            {
                if(GPartie.Instance.PartieActuel.GestionnaireDesTours.JoueurActuel.EstEnVille)
                {
                    GPartie.Instance.PartieActuel.Plateau.frapperHorsVille(GPartie.Instance.PartieActuel.DicJeuMonstre, compteurBaffe);
                }
                else
                {
                    GPartie.Instance.PartieActuel.Plateau.frapperLaVille(GPartie.Instance.PartieActuel.DicJeuMonstre, compteurBaffe);
                }
            }
                
        }

        #endregion Methodes

    }
}