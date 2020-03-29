
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Modele{
    
    #region Enum
    public enum ValeurDes
    {
        Unknown,
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
        /// Propriéte identifiant le dés
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Propriété representant la valeur du dés
        /// </summary>
        public ValeurDes Value { get; private set; }
        
        /// <summary>
        /// Constructeur d'un dés générer automatiquement sa valeur
        /// </summary>
        public Des(int id)
        {
            Id = id;
            Roll();
        }

        public void Roll()
        {
            Random rand = new Random();
            Value = (ValeurDes) rand.Next(0, 5);
        }
    }
}