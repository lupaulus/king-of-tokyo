
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Modele{
    public interface ICarte {


        /// <summary>
        /// @param appEffet 
        /// @param effet 
        /// @param listeAppEffet 
        /// @param listeEffet
        /// </summary>
        void appliquerEffet(HashSet<ApplicationEffet> appEffet, HashSet<Effet> effet, HashSet<int> listeAppEffet, HashSet<int> listeEffet);

    }
}