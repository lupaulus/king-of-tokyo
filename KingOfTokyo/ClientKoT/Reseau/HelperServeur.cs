
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private const int BYTES_SIZE = 256;

        /// <summary>
        /// Nombre de joueur max.
        /// </summary>
        private static int JOUEUR_MAX = 6;

        private TcpClient ClientTCP;

        private Thread ClientThread;
        private bool StopClient = false;

        private string messageReaded;
        private string messageToSend;

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
            NetworkStream stream = ClientTCP.GetStream();
            while (true)
            {
                // Translate the Message into ASCII.
                Byte[] data = Encoding.ASCII.GetBytes(messageToSend);
                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
                Debug.WriteLine("Sent: {0}", messageToSend);
                // Bytes Array to receive Server Response.
                data = new Byte[256];
                messageReaded = String.Empty;
                // Read the Tcp Server Response Bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                messageReaded = Encoding.ASCII.GetString(data, 0, bytes);
                Debug.WriteLine("Received: {0}", messageReaded);
                Thread.Sleep(500);
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
