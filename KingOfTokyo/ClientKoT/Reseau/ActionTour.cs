using System.Collections.Generic;

namespace Client.Reseau
{
    class ActionTour : StreamObject
    {
        public int valeurDes;

        public bool FinTour { get; set; }

        public ActionTour(string s)
        {
            string[] tab = s.Split('|');
            FinTour = bool.Parse(tab[0]);
            
        }

        public override string IntoString()
        {
            return $"{FinTour.ToString()}|{valeurDes}";
        }
    }
}