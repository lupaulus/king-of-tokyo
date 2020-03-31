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
        POST, GET, HELP, QUIT, STOPSERVEUR, SUBSCRIBE, UNSUBSCRIBE
    };

    public enum CommandeType { SERVEURCONNEXION, CONNEXIONSERVEUR, LANCEMENTPARTIE, ACTIONPARTIE, ACTIONTOUR, INFOJOUEUR, FINTOUR, FINPARTIE };
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
            string[] tab = s.Split(';');
            this.commande = (Commande)int.Parse(tab[0]);
            this.commandeType = (CommandeType)int.Parse(tab[1]);
            this.pseudo = tab[2];
            this.data = StreamObject.FromString(tab[3], this.commandeType);
        }


        public override string ToString()
        {
            return (int)commande + ";" + (int)commandeType + ";" + pseudo + ";" + data.IntoString();
        }

    }
}
