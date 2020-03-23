using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServeurKoT.Connexion
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
        private List<TcpClient> ListClients;

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
        private byte[] bytesReaded;
        private byte[] byteToWrite;

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

        #endregion Properties

        #region Ctor

        public Serveur(string ipAdresse, int port)
        {
            // **** Initialisation ******
            // Variable de sortie
            Quit = false;
            // Ensemble des clients
            ListClients = new List<TcpClient>();
            // Cast des clients
            localAddrString = ipAdresse;
            localPort = port;
            IPAddress localAddr = IPAddress.Parse(ipAdresse);
            // TcpListener server = new TcpListener(port);
            Server = new TcpListener(localAddr, port);
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
                    TcpClient client = Server.AcceptTcpClient();
                    // Ajout à la liste des clients
                    ListClients.Add(client);
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

            var stream = client.GetStream();
            string imei = String.Empty;
            string data = null;
            Byte[] bytes = new Byte[BYTES_SIZE];
            int i;
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Logger.Log(Logger.Level.Debug,$"{Thread.CurrentThread.ManagedThreadId}: Received: {data}");
                    string str = "Hey Device!";
                    Byte[] reply = System.Text.Encoding.ASCII.GetBytes(str);
                    stream.Write(reply, 0, reply.Length);
                    Logger.Log(Logger.Level.Debug, $"{Thread.CurrentThread.ManagedThreadId}: Sent: {data}");
                    Thread.Sleep(500);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }
        }
    }
}