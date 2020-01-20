using System;

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