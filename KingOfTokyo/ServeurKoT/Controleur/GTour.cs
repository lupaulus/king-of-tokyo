using ServeurKoT.Modele;
using System.Collections.Generic;

namespace ServeurKoT.Controleur
{
    public class GTour
    {
        private Queue<MonstreJeu> ListOrdonneJoueur { get; set; }
        public MonstreJeu JoueurActuel { get; set; }

        public GTour(List<MonstreJeu> list)
        {
            ListOrdonneJoueur = new Queue<MonstreJeu>(list);
            JoueurActuel = ListOrdonneJoueur.Peek();
        }
        
        public void ProchainTour()
        {
            ListOrdonneJoueur.Enqueue(ListOrdonneJoueur.Dequeue());
            JoueurActuel = ListOrdonneJoueur.Peek();
        }
    }
}