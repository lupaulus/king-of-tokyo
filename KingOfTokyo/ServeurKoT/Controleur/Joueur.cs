
using ServeurKoT.Controleur;
using ServeurKoT.Modele;
using ServeurKoT.Reseau;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ServeurKoT.Reseau
{
    public class Joueur 
    {
        private int id;
        public int Id { get => id; }

        public Monstre IdJoueur { get; set; }

        private string pseudo;
        public string Pseudo { get => pseudo; set => pseudo = value; }

        private bool estPret;
        public bool EstPret { get => estPret; set => estPret = value; }

        public int PtsVie { get; set; }
        public int PtsVictoire { get; set; }
        public int PtsEnergie { get; set; }

        public bool AToiDeJouer { get; set; }

        private static int POINT_VIE_MIN = 0;

        private static int POINT_VICTOIRE_BASE = 0;

        private static int POINT_VIE_MAX = 10;


        private static int POINT_ENERGIE_BASE = 0;

        private static int POINT_VIE_BASE = 10;

        private volatile string _messageReaded;
        private volatile string _messageToSend;
        private List<Carte> listeCartes;

        public string MessageReaded
        {
            get { return _messageReaded; }
            set { _messageReaded = value; }
        }

        public string MessageToSend
        {
            get { return _messageToSend; }
            set { _messageToSend = value; }
        }

       
        public Joueur(int id,string pseudo)
        {
            this.id = id;
            this.pseudo = pseudo;
            this.estPret = false;

            this.PtsVictoire = POINT_VICTOIRE_BASE;
            this.PtsVie = POINT_VIE_BASE;
            this.PtsEnergie = POINT_ENERGIE_BASE;

        }


        public InfoJoueur GenerateInfoJoueur()
        {
            InfoJoueur res = new InfoJoueur();
            res.Pseudo = pseudo;
            res.IdJoueur = IdJoueur;
            res.EstPret = EstPret;

            res.PtsVie = PtsVie;
            res.PtsVictoire = PtsVictoire;
            res.PtsEnergie = PtsEnergie;

            res.AToiDeJouer = AToiDeJouer;

            return res;
        }

        /// <summary>
        /// Méthode pour rétirer la vie hors combat
        /// </summary>
        /// <param name="value">Valeur de vie à ajouter</param>
        public void retirerVie(int value)
        {
            PtsVie -= value;
            if (PtsVie < POINT_VIE_MIN)
            {
                PtsVie = POINT_VIE_MIN;
            }
        }
        /// <summary>
        /// Méthode pour ajouter la vie hors combat
        /// </summary>
        /// <param name="value">Valeur de vie à retirer</param>
        public void ajouterVie(int value)
        {
            PtsVie += value;
            if (PtsVie > POINT_VIE_MAX)
            {
                PtsVie = POINT_VIE_MAX;
            }
        }

        public void ajouterPtsVictoire(int value)
        {
            PtsVictoire += value;
        }


        public void frapper(Joueur monstreAFrapper, int valeurDegat)
        {
            monstreAFrapper.seFaireFrapper(valeurDegat);
        }
        public void seFaireFrapper(int valeurDegat)
        {
            retirerVie(valeurDegat);
        }

        public void appliquerJeton()
        {
            // TODO implement here
        }



        public void achatCarte(Carte carteValue)
        {
            listeCartes.Add(carteValue);
        }

        public void utilisationCarte(Carte carteValue)
        {
            // TODO Application des effets
            listeCartes.Remove(carteValue);
        }

        /// <summary>
        /// Méthode qui ajoute le monstre mort dans la ville
        /// </summary>
        /// <param name="monstre"></param>
        public bool monstreMort()
        {
            return PtsVie == 0;
        }

        public void ajouterEnergie(int compteurEnergie)
        {
            PtsEnergie += compteurEnergie;
        }

        public bool enleverEnergie(int value)
        {
            if (POINT_ENERGIE_BASE < (PtsEnergie - value))
            {
                PtsEnergie -= value;
                return true;
            }
            return false;
        }



    }
}