namespace Client.Reseau
{
    internal class ConnexionPartie : StreamObject
    {
        private byte[] b;

        public ConnexionPartie(string s)
        {
            this.b = s;
        }

        public override byte IntoString()
        {
            throw new System.NotImplementedException();
        }
    }
}