namespace Client.Reseau
{
    internal class ActionPartie : StreamObject
    {
        private byte[] b;

        public ActionPartie(string s)
        {
            this.b = s;
        }

        public override byte IntoString()
        {
            throw new System.NotImplementedException();
        }
    }
}