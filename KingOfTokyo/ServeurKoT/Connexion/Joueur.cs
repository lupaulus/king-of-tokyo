
using ServeurKoT.Controleur;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ServeurKoT.Connexion
{
    public class Joueur 
    {
        private int id;
        public int Id { get => id; }


        private string pseudo;
        public string Pseudo { get => pseudo; set => pseudo = value; }

        private bool estPret;
        public bool EstPret { get => estPret; set => estPret = value; }


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


        public void passerTour()
        {
            // TODO implement here
        }

        public void lancerDes()
        {
            // TODO implement here
        }
    }
}