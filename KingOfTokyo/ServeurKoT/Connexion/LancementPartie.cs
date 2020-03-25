namespace ServeurKoT.Connexion
{
    internal class LancementPartie : StreamObject
    {
        public bool JoueurPret { get; set; }

        public LancementPartie()
        {
            JoueurPret = false;

        }

        public LancementPartie(string b)
        {
            JoueurPret = bool.Parse(b);
        }

        public override string IntoString()
        {
            return $"{JoueurPret.ToString()}";
        }
    }
}