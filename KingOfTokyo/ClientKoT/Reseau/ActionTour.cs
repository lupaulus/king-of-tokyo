using System.Collections.Generic;

namespace Client.Reseau
{
    public class ActionTour : StreamObject
    {
        public int ValeurDes;

        public bool FinTour { get; set; }

        public ActionTour(string s)
        {
            string[] tab = s.Split('|');
            FinTour = bool.Parse(tab[0]);
            
        }

        public ActionTour()
        {
            this.FinTour = false;
            this.ValeurDes = -1;
        }

        public override string IntoString()
        {
            return $"{FinTour.ToString()}|{ValeurDes}";
        }
    }
}