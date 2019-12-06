
using ServeurKoT.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Controleur
{
    public class GMonstre
    {
        private int nextId; 
        private List<Monstre> listeMonstre;
        public GMonstre()
        {
            nextId = 1;
            listeMonstre = new List<Monstre>();
        }

        public void ajouterMonstre(NomMonstre nom)
        {
            Monstre monstre = new Monstre(nextId,nom);
            nextId++;
            listeMonstre.Add(monstre);
        }

        public void supprimerMonstre(Monstre monstre)
        {
            listeMonstre.Remove(monstre);
        }

        public List<Monstre> getListeMonstre()
        {
            return new List<Monstre>(listeMonstre);
        }



    }
}