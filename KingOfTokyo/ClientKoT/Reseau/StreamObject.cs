using System;

namespace Client.Reseau
{
    public abstract class StreamObject
    {
        public int Length { get; set; }
        protected Joueur joueur { get; set; }

        public StreamObject()
        {
            joueur = null;
        }

        public static StreamObject FromString(string b, CommandeType type)
        {
            StreamObject res;
            switch(type)
            {
                case CommandeType.CONNEXIONSERVEUR:
                    res = new ConnexionServeur(b);
                    break;
                case CommandeType.CONNEXIONPARTIE:
                    res = new ConnexionPartie(b);
                    break;
                case CommandeType.ACTIONPARTIE:
                    res = new ActionPartie(b);
                    break;
                case CommandeType.ACTIONTOUR:
                    res = new ActionTour(b);
                    break;
                default:
                    throw new Exception("PAQUET INCONNU");
            }
            return res;
        }

        public abstract byte IntoString();

    }
}



