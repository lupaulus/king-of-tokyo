using ServeurKoT.Controleur;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServeurKoT.Reseau
{
    internal class Serveur
    {
        #region Properties

        private TcpListener Server;

        /// <summary>
        /// Thread principal du serveur
        /// </summary>
        private Thread ServerLoop;

        /// <summary>
        /// Variable globale pour arreter le Thread
        /// </summary>
        private bool Quit;

        /// <summary>
        /// Liste des clients
        /// </summary>
        private Dictionary<TcpClient,Joueur> ListClients;

        /// <summary>
        /// Port du serveur
        /// </summary>
        private int localPort;

        /// <summary>
        /// Adresse IP du serveur
        /// </summary>
        private string localAddrString;

        /// <summary>
        /// Mutex
        /// </summary>
        private static readonly object Instancelock = new object();

        /// <summary>
        /// Instance Value
        /// </summary>
        private static Serveur InstanceValue = null;

        


        /// <summary>
        /// Singleton instance value
        /// </summary>
        public static Serveur Instance
        {
            get
            {
                lock (Instancelock)
                {
                    if (InstanceValue == null)
                    {
                        Logger.Log(Logger.Level.Error, "Singleton serveur pas initialisé");
                        throw new Exception("Singleton not initialised");
                    }
                    return InstanceValue;
                }
            }
        }

        public int BYTES_SIZE = 256;
        private int nextIdJoueur = 1;
        private bool updateTour;
        private bool gestionDes;
        private bool gestionVictoire;

        #endregion Properties

        #region Ctor

        public Serveur(string ipAdresse, int port)
        {
            // **** Initialisation ******
            // Variable de sortie
            Quit = false;
            // Ensemble des clients
            ListClients = new Dictionary<TcpClient, Joueur>();
            // Cast des clients
            localAddrString = ipAdresse;
            localPort = port;
            IPAddress localAddr = IPAddress.Parse(ipAdresse);
            // TcpListener server = new TcpListener(port);
            Server = new TcpListener(localAddr, port);
            updateTour = false;
            gestionDes = false;
        }

        #endregion Ctor

        /// <summary>
        /// Initialisation du serveur
        /// A faire au démarage du serveur
        /// </summary>
        /// <param name="IpAdresse">Adresse IP du serveur</param>
        /// <param name="Port">Port du serveur</param>
        public static void Init(string IpAdresse, int Port)
        {
            Logger.Log(Logger.Level.Info, "Initialisation du serveur");
            Serveur res = new Serveur(IpAdresse, Port);
            InstanceValue = res;
        }

        public void StartServer()
        {
            Logger.Log(Logger.Level.Info, "Lancement du serveur  ");
            Logger.Log(Logger.Level.Info, "Adresse : " + localAddrString);
            Logger.Log(Logger.Level.Info, "Port : " + localPort);
            ServerLoop = new Thread(new ThreadStart(RunServer));
            ServerLoop.Start();
        }

        private void RunServer()
        {
            try
            {
                // Start listening for client requests.
                Server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[BYTES_SIZE];

                // Liaison de la socket au point de communication

                Logger.Log(Logger.Level.Info, ("Serveur à l'écoute des connexions..."));
                while (!Quit)
                {
                    Logger.Log(Logger.Level.Info, "Dans l'attente d'une connexion... ");
                    // Méthode bloquante
                    TcpClient client = (TcpClient)Server.AcceptTcpClient();
                    // Ajout à la liste des clients
                    ListClients.Add(client, new Joueur(nextIdJoueur,""));
                    nextIdJoueur++;
                    // Start a thread to handle this client...
                    new Thread(() => HandleClient(client)).Start();
                }
            }
            catch (SocketException E)
            {
                Logger.Log(Logger.Level.Error, E.ToString());
            }
            // Shutdown and end connection
            Server.Stop();
            Logger.Log(Logger.Level.Info, "Fermeture du serveur");
        }

        private void HandleClient(TcpClient client)
        {
            
            Logger.Log(Logger.Level.Info, "Client Connecté");
            
            NetworkStream stream = client.GetStream();
            Byte[] bytes = new Byte[BYTES_SIZE];
            int i;
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    ListClients[client].MessageReaded = Encoding.ASCII.GetString(bytes, 0, i);
                    Logger.Log(Logger.Level.Debug, $"{Thread.CurrentThread.ManagedThreadId}: Received: {ListClients[client].MessageReaded}");

                    HandleMessageReceive(client);

                    if(!updateTour)
                    {
                        if(gestionDes || gestionVictoire)
                        {
                            gestionDes = false;
                            UpdateInfo(ListClients[client].MessageToSend);
                        }
                        else
                        {
                            Byte[] reply = Encoding.ASCII.GetBytes(ListClients[client].MessageToSend);
                            stream.Write(reply, 0, reply.Length);
                            Logger.Log(Logger.Level.Debug, $"{Thread.CurrentThread.ManagedThreadId}: Sent: {ListClients[client].MessageToSend}");
                            Thread.Sleep(500);
                        }
                    }
                    

                    UpdateInfoAllPlayers();
                    if(updateTour)
                    {
                        UpdateInfoTour(client);
                        updateTour = false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }
        }

        private void UpdateInfo(string msg)
        {
            foreach (TcpClient tcpClient in ListClients.Keys)
            {
                //Get stream
                NetworkStream stream = tcpClient.GetStream();

                Byte[] reply = Encoding.ASCII.GetBytes(msg.ToString());
                stream.Write(reply, 0, reply.Length);
                Logger.Log(Logger.Level.Debug, $"{Thread.CurrentThread.ManagedThreadId}: Sent: {msg.ToString()}");
                Thread.Sleep(200);
            }
        }

        private void HandleMessageReceive(TcpClient client)
        {
            PaquetDonnees p = new PaquetDonnees(ListClients[client].MessageReaded);
            // Connexion Client
            if (p.commandeType == CommandeType.CONNEXIONSERVEUR)
            {
                ConnexionServeur c = (ConnexionServeur)p.data;
                c.ConnexionOK = true;
                ListClients[client].Pseudo = p.pseudo;
                GPartie.Instance.PartieActuel.AjouterJoueur(ListClients[client]);
                Logger.Log(Logger.Level.Info, $"Joueur {p.pseudo} est connecté");
                Logger.Log(Logger.Level.Info, $"Nombre de joueur actuellement : {GPartie.Instance.PartieActuel.DicJeuMonstre.Count}");
                c.NbrJoueurActuellement = GPartie.Instance.PartieActuel.DicJeuMonstre.Count;
                p.data = c;
            }
            else if(p.commandeType == CommandeType.LANCEMENTPARTIE)
            {
                
                LancementPartie c = (LancementPartie)p.data;
                ListClients[client].EstPret = c.JoueurPret;
                string pret = c.JoueurPret ? "pret" : "pas prêt";
                Logger.Log(Logger.Level.Info, $"Joueur {p.pseudo} est {pret}");
                // Verification si tout le monde est pret 
                if (CheckIfAllPlayerAreReady())
                {
                    Logger.Log(Logger.Level.Info, $"La partie va débuter, tous les joueurs sont prets");
                    GPartie.Instance.PartieActuel.DemarerPartie();
                }
                // Indique le Joueur actuel
                c.JoueurActuel = ListClients[client].IdJoueur;
                c.NbrJoueur = ListClients.Count;
            }

            else if (p.commandeType == CommandeType.ACTIONTOUR)
            {
                ActionTour t = (ActionTour)p.data;
                if(t.RerollDes)
                {
                    gestionDes = true;
                    Logger.Log(Logger.Level.Info, $"Reroll Des : {t.EtatDes.ToString()}");
                    t.RemplirDes(GPartie.Instance.PartieActuel.LancerDes());
                    // SI dernier Lancer
                    if(t.EtatDes == EtatLancerDes.TroisiemeLance)
                    {
                        GPartie.Instance.PartieActuel.ResolutionDes(t);
                    }
                    // Test Joueur Gagnant 
                    Joueur joueurGagnant = GPartie.Instance.PartieActuel.CheckSiPartieFini();
                    if(joueurGagnant.IdJoueur != Monstre.UNKNOWN)
                    {
                        FinPartie f = new FinPartie();
                        f.Pseudo = joueurGagnant.Pseudo;
                        f.JoueurGagnant = joueurGagnant.IdJoueur;
                        Logger.Log(Logger.Level.Info, $"La partie est fini, {joueurGagnant.IdJoueur} à gagné");
                        p.commandeType = CommandeType.FINPARTIE;
                        p.data = f;
                        gestionVictoire = true;
                    }
                }
            }
            else if(p.commandeType  == CommandeType.FINTOUR)
            {
                GPartie.Instance.PartieActuel.ProchainTour();
                updateTour = true;
            }

            ListClients[client].MessageToSend = p.ToString();
        }

        private void UpdateInfoTour(TcpClient client)
        {
            foreach (TcpClient tcpClient in ListClients.Keys)
            {
                //Get stream
                NetworkStream stream = tcpClient.GetStream();

                // Clear list
                TourFini action = new TourFini();
                PaquetDonnees pa = new PaquetDonnees(Commande.POST, CommandeType.FINTOUR, "SERVEUR", action);
                Byte[] send = Encoding.ASCII.GetBytes(pa.ToString());
                stream.Write(send, 0, send.Length);
                Logger.Log(Logger.Level.Debug, $"{Thread.CurrentThread.ManagedThreadId}: Sent: {pa.ToString()}");
                Thread.Sleep(200);
            }
        }

        private void UpdateInfoAllPlayers()
        {
            foreach(TcpClient tcpClient in ListClients.Keys)
            {
                //Get stream
                NetworkStream stream = tcpClient.GetStream();

                // Clear list
                InfoJoueur clear = new InfoJoueur();
                // Rajout des informations des cartes si disponible
                clear.AjoutCartesChemins(GPartie.Instance.PartieActuel.ListCheminImgCartesPlateau());


                PaquetDonnees pa = new PaquetDonnees(Commande.POST, CommandeType.INFOJOUEUR, "CLEARLIST", clear);
                Byte[] send = Encoding.ASCII.GetBytes(pa.ToString());
                stream.Write(send, 0, send.Length);
                Logger.Log(Logger.Level.Debug, $"{Thread.CurrentThread.ManagedThreadId}: Sent: {pa.ToString()}");
                Thread.Sleep(200);

                foreach (Joueur j in ListClients.Values)
                {
                    PaquetDonnees p = new PaquetDonnees(Commande.POST, CommandeType.INFOJOUEUR, j.Pseudo, j.GenerateInfoJoueur());
                    Byte[] reply = Encoding.ASCII.GetBytes(p.ToString());
                    stream.Write(reply, 0, reply.Length);
                    Logger.Log(Logger.Level.Debug, $"{Thread.CurrentThread.ManagedThreadId}: Sent: {p.ToString()}");
                    Thread.Sleep(200);
                }
            }
            
        }

        private bool CheckIfAllPlayerAreReady()
        {
            if(GPartie.Instance.PartieActuel.DicJeuMonstre.Count < 2)
            {
                return false;
            }

            foreach(Joueur j in ListClients.Values) 
            {
                if(!j.EstPret)
                {
                    return false;
                }
            }
            return true;
        }
    }
}