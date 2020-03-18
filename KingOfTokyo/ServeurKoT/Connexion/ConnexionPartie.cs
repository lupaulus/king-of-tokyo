namespace ServeurKoT.Connexion
{
    internal class ConnexionPartie : StreamObject
    {
        private byte[] b;

        public ConnexionPartie(byte[] b)
        {
            this.b = b;
        }

        public override byte IntoBytes()
        {
            throw new System.NotImplementedException();
        }
    }
}