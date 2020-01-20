
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Client.Reseau{
    public class HelperServeur {

        private static int JOUEUR_MAX = 6;
        private static HelperServeur instance = null;
        private static readonly object padlock = new object();

        public static void Init(Joueur value, string hostName, int portNum)
        {
            instance = new HelperServeur(value,hostName,portNum);
        }

        public static HelperServeur Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        throw new NullReferenceException();
                    }
                    return instance;
                }
            }
        }


        

        /// <summary>
        /// @param value
        /// </summary>
        public HelperServeur(Joueur value, string hostName, int portNum) {
            try
            {
                Logger.Log(Logger.Level.Info, "Initialisation de la connexion vers le serveur");
                TcpClient client = new TcpClient(hostName, portNum);
                NetworkStream ns = client.GetStream();

                byte[] bytes = new byte[1024];
                int bytesRead = ns.Read(bytes, 0, bytes.Length);

                Logger.Log(Logger.Level.Info, Encoding.ASCII.GetString(bytes, 0, bytesRead));



            } catch (Exception e)
            {
                Logger.Log(Logger.Level.Error, e.ToString());
            }
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

        public string IpDuServeur() {
            // retourne l'ip du serveur
            string ip = "127.0.0.1";
            return ip;
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
