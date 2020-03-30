using ServeurKoT.Modele;
using ServeurKoT.Reseau;
using System.Collections.Generic;

namespace ServeurKoT.Controleur
{
    public class GTour
    {
        private Queue<Joueur> ListOrdonneJoueur { get; set; }
        public Joueur JoueurActuel { get; set; }

        public GTour(List<Joueur> list)
        {
            ListOrdonneJoueur = new Queue<Joueur>(list);
            JoueurActuel = ListOrdonneJoueur.Peek();
        }
        
        public void ProchainTour()
        {
            // Changement de position
            ListOrdonneJoueur.Enqueue(ListOrdonneJoueur.Dequeue());
            JoueurActuel = ListOrdonneJoueur.Peek();
        }
    }
}