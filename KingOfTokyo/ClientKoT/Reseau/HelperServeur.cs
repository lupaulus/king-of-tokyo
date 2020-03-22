
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client.Reseau
{
    public class HelperServeur {

        public enum EtatServeur { OK, FULL , OUT }

        /// <summary>
        /// Nom du serveur
        /// </summary>
        public string Nom { get; private set; }
        /// <summary>
        /// Addresse de l'hote
        /// </summary>
        public string Adresse { get; private set; }
        /// <summary>
        /// Port de l'hote
        /// </summary>
        public int Port { get; private set; }
        
        /// <summary>
        /// Nombre de joueur actuellement
        /// </summary>
        public int NbrJoueur {get; private set;}

        /// <summary>
        /// Etat du serveur actuellement
        /// </summary>
        public EtatServeur Etat { get; private set; }

        /// <summary>
        /// Nombre de joueur max.
        /// </summary>
        private static int JOUEUR_MAX = 6;

        private TcpClient ClientTCP;

        private Thread ClientThread;
        private bool StopClient = false;

        public HelperServeur(string name, string hostName, int portNum)
        {
            this.Nom = name;
            this.Adresse = hostName;
            this.Port = portNum;

            this.NbrJoueur = 0;
            this.Etat = EtatServeur.OK;
            
        }

        private void RunClient()
        { 

            ClientTCP = new TcpClient(Adresse, Port);
            while(true)
            {
                NetworkStream ns = ClientTCP.GetStream();
                byte[] bytes = new byte[1024];
                int bytesRead = ns.Read(bytes, 0, bytes.Length);
            }
        }

        public void InitConnexion()
        {
            ClientThread = new Thread(new ThreadStart(RunClient));
            ClientThread.Start();
        }

        internal object GetListePartieParDefaut()
        {
            throw new NotImplementedException();
        }


        internal void CreePartie()
        {
            throw new NotImplementedException();
        }

        public void DeconnexionServeur() {
            // TODO implement here
        }

        public void ReadListeParties() {
            // Renvoie la liste des parties: Nom; Nb joueurs;
        }
        
        public void NomDuJoueur() {
            // Obtenu à partir du formulaire dans Menu.xaml
        }


        public int NombreJoueurs() {
            // retourne le nombre de joueur actuel dans la partie
            int nbJoueurs = 0;
            return nbJoueurs;
        }

        // Monstre 

        public List<int> LancerDes()
        {
            return new List<int>();
        }

        public void GarderDes(List<int> idDesGarder)
        {

        }

        public void DonnerBaffes(List<int> idMonstres,List<int> nbrBaffes)
        {

        }




    }
}
