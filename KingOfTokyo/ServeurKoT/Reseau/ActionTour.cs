using ServeurKoT.Controleur;
using ServeurKoT.Modele;
using System;
using System.Collections.Generic;

namespace ServeurKoT.Reseau
{

    

    public class ActionTour : StreamObject
    {

        public bool FinTour { get; set; }

        public bool RerollDes { get; set; }

        public EtatLancerDes EtatDes { get; set; }

        public ValeurDes Des1 { get; set; }
        public ValeurDes Des2 { get; set; }
        public ValeurDes Des3 { get; set; }
        public ValeurDes Des4 { get; set; }
        public ValeurDes Des5 { get; set; }
        public ValeurDes Des6 { get; set; }




        public ActionTour(string s)
        {
            string[] tab = s.Split('|');
            FinTour = bool.Parse(tab[0]);
            RerollDes = bool.Parse(tab[1]);
            EtatDes = (EtatLancerDes)int.Parse(tab[2]);
            Des1 = (ValeurDes)int.Parse(tab[3]);
            Des2 = (ValeurDes)int.Parse(tab[4]);
            Des3 = (ValeurDes)int.Parse(tab[5]);
            Des4 = (ValeurDes)int.Parse(tab[6]);
            Des5 = (ValeurDes)int.Parse(tab[7]);
            Des6 = (ValeurDes)int.Parse(tab[8]);
        }

        public ActionTour()
        {
            this.FinTour = false;
            this.RerollDes = false;
            this.EtatDes = EtatLancerDes.PremierLance;

            this.Des1 = ValeurDes.Unknown;
            this.Des2 = ValeurDes.Unknown;
            this.Des3 = ValeurDes.Unknown;
            this.Des4 = ValeurDes.Unknown;
            this.Des5 = ValeurDes.Unknown;
            this.Des6 = ValeurDes.Unknown;
        }

        public override string IntoString()
        {
            return $"{FinTour.ToString()}|{RerollDes.ToString()}|{(int)EtatDes}|{(int)Des1}|{(int)Des2}|{(int)Des3}|{(int)Des4}|{(int)Des5}|{(int)Des6}";
        }

        public void RemplirDes(List<Des> list)
        {
            if(Des1 == ValeurDes.Unknown)
            {
                Des1 = list[0].Value;
            }
            if (Des2 == ValeurDes.Unknown)
            {
                Des2 = list[1].Value;
            }
            if (Des3 == ValeurDes.Unknown)
            {
                Des3 = list[2].Value;
            }
            if (Des4 == ValeurDes.Unknown)
            {
                Des4 = list[3].Value;
            }
            if (Des5 == ValeurDes.Unknown)
            {
                Des5 = list[4].Value;
            }
            if (Des6 == ValeurDes.Unknown)
            {
                Des6 = list[5].Value;
            }


        }
    }
}