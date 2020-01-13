<<<<<<< HEAD
﻿using System;

namespace ServeurKoT.Connexion
{
    public class StreamObject
    {
        public int Length { get; internal set; }

        internal static StreamObject FromBytes(byte v)
        {
            throw new NotImplementedException();
        }

        internal byte IntoBytes()
        {
            throw new NotImplementedException();
        }
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
