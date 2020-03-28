using System;

namespace ServeurKoT.Reseau
{
    internal class ActionPartie : StreamObject
    {
        private byte[] b;

        public ActionPartie(string b)
        {
        }

        public override string IntoString()
        {
            return String.Empty;
        }
    }
}