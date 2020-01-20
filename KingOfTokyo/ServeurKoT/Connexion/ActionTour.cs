namespace ServeurKoT.Connexion
{
    internal class ActionTour : StreamObject
    {
        private byte[] b;

        public ActionTour(byte[] b)
        {
            this.b = b;
        }
    }
}