using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServeurKoT.Connexion
{
    #region Enum
    public enum Commande
    {
        POST, GET, HELP, QUIT, STOPSERVEUR, SUBSCRIBE, SUBSCRIBEv2, UNSUBSCRIBE
    };

    public enum CommandeType { REQUETE, REPONSE };
    #endregion Enum
    class PaquetDonnees
    {
        

        public const int bufferSize = 1500;

        public Commande commande;               // commande
        public CommandeType commandeType;       // type (Requête/Réponse)
        public int dataSize;                    // taille de la donnée
        public String data;                     // données de la commande
        public String pseudo;                   // Pseudo du joueur

        public PaquetDonnees(Commande commande, CommandeType type, String pseudo, String data)
        {
            this.commande = commande;
            this.commandeType = type;
            this.dataSize = data.Length;
            this.data = data;
            this.pseudo = pseudo;
        }

        public PaquetDonnees(byte[] buffer)
        {
            this.commande = (Commande)buffer[0];
            this.commandeType = (CommandeType)buffer[1];

            //On lit le pseudo--------------------------------------------------------------------------------
            //Etape taille
            int sizeP = BitConverter.ToInt32(buffer, 2);

            //Etape data (offset : 2 --> command+ commandType + 4 --> taille pseudo )
            this.pseudo = Encoding.ASCII.GetString(buffer, 6, sizeP);

            //Etape message ---------------------------------------------------------------------------------------------
            //Etape taille(offset : 2 --> command+ commandType + 4 --> taille pseudo + pseudo.Length --> nb de carac pseudo)
            this.dataSize = BitConverter.ToInt32(buffer, 2 + 4 + sizeP);

            //Etape data(offset : 2 --> command+ commandType + 4 --> taille pseudo + pseudo.Length --> nb de carac pseudo + 4 --> taille message)
            this.data = Encoding.ASCII.GetString(buffer, 2 + 4 + 4 + sizeP, dataSize);


        }


        public byte[] GetBytes()
        {
            byte[] buffer = new byte[bufferSize];

            buffer[0] = (byte)commande;
            buffer[1] = (byte)commandeType;

            //On rentre d'abord le pseudo--------------------------------------------------------------------------------
            //Etape taille
            byte[] bufferPseudo = BitConverter.GetBytes(pseudo.Length);
            Buffer.BlockCopy(bufferPseudo, 0, buffer, 2, bufferPseudo.Length);

            //Etape pseudo (offset : 2 --> command+ commandType + 4 --> taille pseudo )
            Encoding.ASCII.GetBytes(pseudo, 0, pseudo.Length, buffer, 6);

            //Etape message ---------------------------------------------------------------------------------------------
            //Etape taille(offset : 2 --> command+ commandType + 4 --> taille pseudo + pseudo.Length --> nb de carac pseudo)
            Byte[] bufferTemp = BitConverter.GetBytes(dataSize);
            Buffer.BlockCopy(bufferTemp, 0, buffer, 2 + 4 + pseudo.Length, bufferTemp.Length);

            //Etape data (offset : 2 --> command+ commandType + 4 --> taille pseudo + pseudo.Length --> nb de carac pseudo + 4 --> taille message)
            Encoding.ASCII.GetBytes(data, 0, dataSize, buffer, 2 + 4 + pseudo.Length + 4);

            return buffer;
        }

        public override string ToString()
        {
            return "[" + commande + "," + commandeType + ",\"" + pseudo + "\"," + dataSize + ",\"" + data + "\"]";
        }

    }
}
