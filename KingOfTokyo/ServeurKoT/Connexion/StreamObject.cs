<<<<<<< HEAD
<<<<<<< HEAD
﻿using System;

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

        public static StreamObject FromBytes(byte[] b, CommandeType type)
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

        public abstract byte IntoBytes();

    }
}
||||||| merged common ancestors
=======
﻿namespace ServeurKoT.Connexion
{
    public class StreamObject
    {
        public int Length { get; internal set; }
    }
}
>>>>>>> Avancement de l'envoi de paquets génériques
||||||| merged common ancestors
=======
﻿using System;

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

        public static StreamObject FromBytes(byte[] b, CommandeType type)
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

        public abstract byte IntoBytes();

    }
}
>>>>>>> fb53fe27883a94ebc5ff8b51c32bdc0e11ef7eec
