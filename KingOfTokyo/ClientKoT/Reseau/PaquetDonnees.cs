using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Reseau
{
    #region Enum
    public enum Commande
    {
        POST, GET, HELP, QUIT, STOPSERVEUR, SUBSCRIBE, SUBSCRIBEv2, UNSUBSCRIBE
    };

    public enum CommandeType { REQUETE, REPONSE, ACTIONTOUR, ACTIONPARTIE, CONNEXIONPARTIE, CONNEXIONSERVEUR };
    #endregion Enum
    class PaquetDonnees
    {
      

        public Commande commande;               // commande
        public CommandeType commandeType;       // type (Requête/Réponse)                   
        public StreamObject data;               // données de la commande
        public String pseudo;                   // Pseudo du joueur

        public PaquetDonnees(Commande commande, CommandeType type, String pseudo, StreamObject data)
        {
            this.commande = commande;
            this.commandeType = type;
            this.data = data;
            this.pseudo = pseudo;
        }

        public PaquetDonnees(string s)
        {

        }


        public override string ToString()
        {
            return "[" + commande + ";" + commandeType + ";" + pseudo + ";" + data + "]";
        }

    }
}
