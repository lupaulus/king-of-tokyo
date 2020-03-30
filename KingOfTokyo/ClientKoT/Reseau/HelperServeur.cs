
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

        public event PartieStart_EventHandler PartieStart;
        public delegate void PartieStart_EventHandler(object sender, EventArgs args);

        public event PartieTourSuivant_EventHandler TourSuivant;
        public delegate void PartieTourSuivant_EventHandler(object sender, EventArgs args);

        public event ResultatDes_EventHandler ResultatDes;
        public delegate void ResultatDes_EventHandler(object sender, EventDesArgs args);

        public event UpdateInfoJoueur_EventHandler UpdateInfo;
        public delegate void UpdateInfoJoueur_EventHandler(object sender, EventArgs args);

        public Monstre ActualPlayer { get; set; }

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
        private bool okConnexion;
        private bool partieLancer;

        public string ImageCarte1 { get; set; }
        public string ImageCarte2 { get; set; }
        public string ImageCarte3 { get; set; }

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
            this.okConnexion = false;
            this.NbrJoueur = 0;
            this.Etat = EtatServeur.OK;
            this.ListInfoJoueur = new List<InfoJoueur>();
            this.partieLancer = false;

        }

        private void RunClient()
        { 
            try
            {
                ClientTCP = new TcpClient(Adresse, Port);
                stream = ClientTCP.GetStream();
                EnvoyerPaquet();
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
            _messageReaded = String.Empty;
            // Read the Tcp Server Response Bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            _messageReaded = Encoding.ASCII.GetString(data, 0, bytes);
            Debug.WriteLine($"Received: {_messageReaded}");
            if(_messageReaded.Equals(String.Empty))
            {
                return;
            }

            PaquetDonnees p = new PaquetDonnees(_messageReaded);

            if (p.commandeType == CommandeType.CONNEXIONSERVEUR)
            {
                ConnexionServeur c = (ConnexionServeur)p.data;
                if (c.ConnexionOK)
                {
                    okConnexion = true;
                }
            }

            else if(p.commandeType == CommandeType.LANCEMENTPARTIE)
            {
                LancementPartie c = (LancementPartie)p.data;
                ActualPlayer = c.JoueurActuel; 
                
            }

            else if (p.commandeType == CommandeType.INFOJOUEUR)
            {
                InfoJoueur ij = (InfoJoueur)p.data;
                ListInfoJoueur.Add(ij);

                if(ij.IdJoueur == Monstre.UNKNOWN)
                {
                    ListInfoJoueur.Clear();
                    ImageCarte1 = ij.ImageCarte1;
                    ImageCarte2 = ij.ImageCarte2;
                    ImageCarte3 = ij.ImageCarte3;
                    
                }
                if(!partieLancer && CheckIfAllPlayerAreReady())
                {
                    OnPartieStart(new EventArgs());
                    partieLancer = true;
                }
                
            }
            else if(p.commandeType == CommandeType.ACTIONTOUR)
            {
                ActionTour t = (ActionTour)p.data;
                if(t.RerollDes)
                {
                    OnResultatDes(new EventDesArgs(t));
                }
                OnUpdateInfo(new EventArgs());
            }
            else if(p.commandeType == CommandeType.FINTOUR)
            {
                OnProchainTour(new EventArgs());
            }
            
        }

        public bool CheckIfAllPlayerAreReady()
        {
            if (ListInfoJoueur.Count < 2)
            {
                return false;
            }

            foreach (InfoJoueur j in ListInfoJoueur)
            {
                if (!j.EstPret)
                {
                    return false;
                }
            }
            
            return true;
        }

        public void EnvoyerPaquet()
        {
            // Translate the Message into ASCII.
            Byte[] data = Encoding.ASCII.GetBytes(_messageToSend);
            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);
            Debug.WriteLine($"Sent: { _messageToSend}");

            
        }

        /// <summary>
        /// Fonction de verification si la connexion au serveur 
        /// à abouti
        /// </summary>
        /// <returns></returns>
        public bool CheckServeurRep()
        {   
            for(int i=0;i<5; i++)
            {
                if(okConnexion)
                {
                    return true;
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
            EnvoyerPaquet();
        }

        public void JoueurPasPret()
        {

            PaquetDonnees startPartie = new PaquetDonnees(Commande.POST, CommandeType.LANCEMENTPARTIE, PseudoJoueur,
                new LancementPartie());
            _messageToSend = startPartie.ToString();
            EnvoyerPaquet();
        }


        protected virtual void OnPartieStart(EventArgs e)
        {
            PartieStart_EventHandler handler = PartieStart;
            handler?.Invoke(this, e);
        }

        protected virtual void OnProchainTour(EventArgs e)
        {
            PartieTourSuivant_EventHandler handler = TourSuivant;
            handler?.Invoke(this, e);
        }


        protected virtual void OnResultatDes(EventDesArgs e)
        {
            ResultatDes_EventHandler handler = ResultatDes;
            handler?.Invoke(this, e);
        }

        protected virtual void OnUpdateInfo(EventArgs e)
        {
            UpdateInfoJoueur_EventHandler handler = UpdateInfo;
            handler?.Invoke(this, e);
        }


        public int NombreJoueurs() {
            return ListInfoJoueur.Count;
        }

        public void RollDes(ActionTour a)
        {
            a.RerollDes = true;
            a.FinTour = false;
            PaquetDonnees reroll = new PaquetDonnees(Commande.POST, CommandeType.ACTIONTOUR, PseudoJoueur,
                a);
            _messageToSend = reroll.ToString();
            EnvoyerPaquet();
        }

        public void FinTour()
        {
            PaquetDonnees finTour = new PaquetDonnees(Commande.POST, CommandeType.FINTOUR, PseudoJoueur,
                new TourFini()) ;
            _messageToSend = finTour.ToString();
            EnvoyerPaquet();
        }
    }

    public class EventDesArgs : EventArgs
    {
        public ActionTour Action { get;}
        public EventDesArgs(ActionTour a)
        {
            Action = a;
        }
    }
}
