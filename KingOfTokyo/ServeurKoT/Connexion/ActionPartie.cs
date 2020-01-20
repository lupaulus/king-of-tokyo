namespace ServeurKoT.Connexion
{
    internal class ActionPartie : StreamObject
    {
        private byte[] b;

        public ActionPartie(byte[] b)
        {
            this.b = b;
        }
    }
}