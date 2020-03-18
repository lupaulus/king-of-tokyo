
using ServeurKoT.Controleur;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ServeurKoT.Connexion
{
    [Serializable()]
    public class Joueur
    {
        private int id;

        private string adresseIP;

        private string pseudo;

        public Joueur(int id, string adresseIP, string pseudo)
        {
            this.id = id;
            this.adresseIP = adresseIP;
            this.pseudo = pseudo;
        }

        


        public void passerTour()
        {
            // TODO implement here
        }

        public void lancerDes()
        {
            // TODO implement here
        }

        public static Joueur FromBytes(byte[] v)
        {
            return Utilitaire.Deserializer<Joueur>(v);
        }

        public byte[] IntoBytes()
        {
            return Utilitaire.Serializer(this);
        }
    }
}