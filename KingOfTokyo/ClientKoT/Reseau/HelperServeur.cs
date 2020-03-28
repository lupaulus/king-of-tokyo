
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace Client.Reseau
{
    public class HelperServeur {

        public string PseudoJoueur;
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
        private NetworkStream stream;
        private Thread ClientThread;
        private bool StopClient = false;

        private object lockRead = new object();
        private object lockSend = new object();
        private object lockInfo = new object();

        private string _messageReaded;
        private string _messageToSend;
        private List<InfoJoueur> _infoJoueurs;

        public List<InfoJoueur> ListInfoJoueur
        {
            get
            {
                lock (lockInfo)
                {
                    return _infoJoueurs;
                }
            }
            set
            {
                lock (lockInfo)
                {
                    _infoJoueurs = value;
                }
            }
        }

        public string MessageReaded
        {
            get 
            { 
                lock(lockRead)
                {
                    return _messageReaded;
                }
            }
            set
            {
                lock (lockRead)
                {
                    _messageReaded = value;
                }
            }
        }

        public string MessageToSend
        {
            get { 

                lock(lockSend)
                {
                    return _messageToSend;
                }
                
            }
            set
            {
                lock (lockSend)
                {
                    _messageToSend = value;
                }
            }
        }

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
            try
            {
                ClientTCP = new TcpClient(Adresse, Port);
                stream = ClientTCP.GetStream();
                EnvoyerReceptionPaquet();
                while (true)
                {
                    ReceptionPaquet();
                }


            }catch(Exception ex)
            {
                MessageBox.Show($"Erreur Thread Connexion : {ex.ToString()}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                ClientTCP?.Close();

            }
            

        }

        private void ReceptionPaquet()
        {
            // Bytes Array to receive Server Response.
            Byte[] data = new Byte[BYTES_SIZE];
            string info = String.Empty;
            // Read the Tcp Server Response Bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            info = Encoding.ASCII.GetString(data, 0, bytes);
            Debug.WriteLine("Received: {0}", info);
            PaquetDonnees p = new PaquetDonnees(info);

            if(p.commandeType == CommandeType.INFOJOUEUR)
            {
                InfoJoueur ij = (InfoJoueur)p.data;
                ListInfoJoueur.Add(ij);

                if(ij.IdJoueur == InfoJoueur.Monstre.UNKNOWN)
                {
                    ListInfoJoueur.Clear();
                }
            }
            
        }

        public void EnvoyerReceptionPaquet()
        {
            // Translate the Message into ASCII.
            Byte[] data = Encoding.ASCII.GetBytes(_messageToSend);
            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);
            Debug.WriteLine("Sent: {0}", _messageToSend);

            // Bytes Array to receive Server Response.
            data = new Byte[BYTES_SIZE];
            _messageReaded = String.Empty;
            // Read the Tcp Server Response Bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            _messageReaded = Encoding.ASCII.GetString(data, 0, bytes);
            Debug.WriteLine("Received: {0}", _messageReaded);
        }

        /// <summary>
        /// Fonction de verification si la connexion au serveur 
        /// à abouti
        /// </summary>
        /// <returns></returns>
        public bool CheckServeurRep()
        {
            for(int i=0;i<6;i++)
            {
                if(MessageReaded != null)
                {
                    PaquetDonnees p = new PaquetDonnees(MessageReaded);
                    if (p.commandeType == CommandeType.CONNEXIONSERVEUR)
                    {
                        ConnexionServeur c = (ConnexionServeur)p.data;
                        if (c.ConnexionOK)
                        {
                            return true;
                        }
                    }
                }
                Thread.Sleep(500);
            }
            return false;
            
        }

        public void InitConnexion()
        {
            // Preparation du paquet
            PaquetDonnees startConnection = new PaquetDonnees(Commande.POST, CommandeType.CONNEXIONSERVEUR, PseudoJoueur,
                new ConnexionServeur());
            _messageToSend = startConnection.ToString();

            // Lancement thread
            ClientThread = new Thread(new ThreadStart(RunClient));
            ClientThread.Start();

        }

        public void JoueurPret()
        {
            LancementPartie l = new LancementPartie();
            l.JoueurPret = true;
            PaquetDonnees startPartie = new PaquetDonnees(Commande.POST, CommandeType.LANCEMENTPARTIE, PseudoJoueur,l);

            _messageToSend = startPartie.ToString();
            EnvoyerReceptionPaquet();
        }

        public void JoueurPasPret()
        {

            PaquetDonnees startPartie = new PaquetDonnees(Commande.POST, CommandeType.LANCEMENTPARTIE, PseudoJoueur,
                new LancementPartie());
            _messageToSend = startPartie.ToString();
            EnvoyerReceptionPaquet();
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
