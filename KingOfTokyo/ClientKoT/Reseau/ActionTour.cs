namespace Client.Reseau
{
    internal class ActionTour : StreamObject
    {
        private byte[] b;

        public ActionTour(byte[] b)
        {
            this.b = b;
        }

        public override byte IntoString()
        {
            throw new System.NotImplementedException();
        }
    }
}