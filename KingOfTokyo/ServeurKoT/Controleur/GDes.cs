
using ServeurKoT.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServeurKoT.Controleur{

    public enum EtatLancerDes
    {
        PremierLance,
        DeuxiemeLance,
        TroisiemeLance
    }

    public class GDes {

        #region Properties 

        /// <summary>
        /// Variable de gestion des ID
        /// </summary>
        private int NextId;

        /// <summary>
        /// Nombre de dés à lancer par tour [valeur par défaut]
        /// </summary>
        private const int NOMBRE_DES_PAR_TOUR = 6;

        private List<Des> EnsembleDes;

        private Random seed;

        #endregion Properties

        #region Ctor
        public GDes() {
            NextId = 1;
            EnsembleDes = new List<Des>();
            seed = new Random();
        }
        #endregion Ctor

        #region Methodes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nbrDes"></param>
        /// <returns></returns>
        public List<Des> LancementDes(int nbrDes = NOMBRE_DES_PAR_TOUR) {
            List<Des> listDes = new List<Des>();
            for(int i = 0 ; i<nbrDes ; i++)
            {
                Des des = new Des(NextId,seed);
                listDes.Add(des);
                EnsembleDes.Add(des);
                NextId++;
            }
            return listDes;
        }

        private Des TrouverDes(List<Des> listDes, int id)
        {
            foreach(Des d in listDes)
            {
                if(id == d.Id)
                {
                    return d;
                }
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Méthode qui permet de reroll les des indiques dans la deuxieme list avec les ID
        /// respectifs
        /// </summary>
        /// <param name="list">Liste de Dés</param>
        /// <param name="idDesReroll">Liste remplis des ID des Dés à reroll </param>
        public void RerollDes(List<Des> list, List<int> idDesReroll)
        {
            foreach(int id in idDesReroll)
            {
                Des d = TrouverDes(list, id);
                d.Roll();
            }
        }
        #endregion Methodes

    }
}