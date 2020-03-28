
using ServeurKoT.Controleur;
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


        private volatile string _messageReaded;
        private volatile string _messageToSend;



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


    }
}