using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeurKoT.Connexion
{
    #region Enum
    public enum Commande
    {
        POST, GET, HELP, QUIT, STOPSERVEUR, SUBSCRIBE, UNSUBSCRIBE
    };

    public enum CommandeType { REQUETE, REPONSE };
    #endregion Enum

    class PaquetDonnees
    {
        /// <summary>
        /// Taille du Buffer
        /// </summary>
        public const int bufferSize = 1500;

        /// Position des différents éléments dans le buffer
        private const int positionCommande = 0;
        private const int positionCommandeType = 1;
        private const int positionJoueur = 2;
        private const int positionStreamObject = 3;


        public Commande commande;               // commande
        public CommandeType commandeType;       // type (Requête/Réponse)
        public Joueur joueur;                   // Joueur
        public int dataSize;                    // taille de la donnée
        public StreamObject data;               // Objet envoyé
                        

        public PaquetDonnees(Commande commande, CommandeType type, Joueur j, StreamObject data)
        {
            this.commande = commande;
            this.commandeType = type;
            this.joueur = j;
            this.dataSize = data.Length;
            this.data = data;
            
        }

        public PaquetDonnees(byte[] buffer)
        {
            this.commande = (Commande)buffer[0];
            this.commandeType = (CommandeType)buffer[1];
            this.joueur = Joueur.FromBytes(buffer[2]);
            this.data = StreamObject.FromBytes(buffer[3]);
        }


        public byte[] GetBytes()
        {
            byte[] buffer = new byte[bufferSize];

            buffer[0] = (byte)commande;
            buffer[1] = (byte)commandeType;
            buffer[2] = joueur.IntoBytes();
            buffer[3] = data.IntoBytes();


            return buffer;
        }

        public override string ToString()
        {
            return "[" + commande + "\"," + commandeType + "\"," + joueur.ToString() + "\"," + dataSize + ",\"" + data + "\"]";
        }

    }
}
