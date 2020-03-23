namespace Client.Reseau
{
    internal class ConnexionPartie : StreamObject
    {
        private byte[] b;

        public ConnexionPartie(byte[] b)
        {
            this.b = b;
        }

        public override byte IntoString()
        {
            throw new System.NotImplementedException();
        }
    }
}