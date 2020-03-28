namespace ServeurKoT.Reseau
{
    internal class LancementPartie : StreamObject
    {
        public bool JoueurPret { get; set; }
        public Monstre JoueurActuel { get; set; }

        public LancementPartie()
        {
            JoueurPret = false;
            JoueurActuel = Monstre.UNKNOWN;

        }
        public LancementPartie(string b)
        {
            string[] tab = b.Split('|');
            JoueurPret = bool.Parse(tab[0]);
            JoueurActuel = (Monstre)int.Parse(tab[1]);

        }

        public override string IntoString()
        {
            return $"{JoueurPret.ToString()}|{(int)JoueurActuel}";
        }
    }
}