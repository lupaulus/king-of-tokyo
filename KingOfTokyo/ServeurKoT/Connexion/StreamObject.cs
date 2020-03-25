using System;

namespace ServeurKoT.Connexion
{
    public abstract class StreamObject
    {
        public int Length { get; set; }
        protected Joueur joueur { get; set; }

        public StreamObject()
        {
            joueur = null;
        }

        public static StreamObject FromString(string s, CommandeType type)
        {
            StreamObject res;
            switch (type)
            {
                case CommandeType.CONNEXIONSERVEUR:
                    res = new ConnexionServeur(s);
                    break;
                case CommandeType.CONNEXIONPARTIE:
                    res = new LancementPartie(s);
                    break;
                case CommandeType.ACTIONPARTIE:
                    res = new ActionPartie(s);
                    break;
                case CommandeType.ACTIONTOUR:
                    res = new ActionTour(s);
                    break;
                default:
                    throw new Exception("PAQUET INCONNU");
            }
            return res;
        }

        public abstract string IntoString();

    }
}



