namespace Client.Reseau
{
    internal class ActionPartie : StreamObject
    {
        private byte[] b;

        public ActionPartie(byte[] b)
        {
            this.b = b;
        }

        public override byte IntoBytes()
        {
            throw new System.NotImplementedException();
        }
    }
}