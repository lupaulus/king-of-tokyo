
using ServeurKoT.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Controleur{
    public abstract class FactoryCarte {

        public FactoryCarte() {}

        public abstract List<Carte> AjouterCarte();

    }
}