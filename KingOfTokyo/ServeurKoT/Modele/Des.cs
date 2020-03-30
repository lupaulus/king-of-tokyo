
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
        private Random rand;
        
        /// <summary>
        /// Constructeur d'un dés générer automatiquement sa valeur
        /// </summary>
        public Des(int id, Random random)
        {
            Id = id;
            rand = random;
            Roll();  
        }

        public void Roll()
        {
            
            Value = (ValeurDes) rand.Next(1, 7);
        }
    }
}