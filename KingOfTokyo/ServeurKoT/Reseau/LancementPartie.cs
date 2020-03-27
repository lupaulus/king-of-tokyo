namespace ServeurKoT.Connexion
{
    internal class LancementPartie : StreamObject
    {
        public bool JoueurPret { get; set; }
        public bool PartieVaDebuter { get; set; }

        public LancementPartie()
        {
            JoueurPret = false;
            PartieVaDebuter = false;

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