
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Modele{
    
    #region Enum
    public enum ValeurDes
    {
        Un,
        Deux,
        Trois,
        Energie,
        Soin,
        Baffe
    }
    #endregion Enum

    public class Des
    {
        /// <summary>
        /// Propriété representant la valeur du dés
        /// </summary>
        public ValeurDes Value { get; set; }
        
        /// <summary>
        /// Constructeur d'un dés générer automatiquement sa valeur
        /// </summary>
        public Des()
        {
            Random rand = new Random();
            Value = (ValeurDes) rand.Next(0, 5);
        }

        public void Reroll()
        {
            Random rand = new Random();
            Value = (ValeurDes) rand.Next(0, 5);
        }
    }
}