using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServeurKoT.Connexion
{
    class Serveur
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
        private int Port = 13670;
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

        #endregion Properties

        #region Ctor
        public Serveur(string IpAdresse, int Port)
        {
            // **** Initialisation ******
            // Variable de sortie
            Quit = false;
            // Ensemble des clients
            ListClients = new List<TcpClient>();
            // Cast des clients
            IPAddress localAddr = IPAddress.Parse(IpAdresse);

            // TcpListener server = new TcpListener(port);
            Server = new TcpListener(localAddr, Port);
            
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
            Logger.Log(Logger.Level.Info, " Initialisation du serveur");
            Serveur res = new Serveur(IpAdresse,Port);
            InstanceValue = res;
        }

        


        public void StartServer()
        {
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
                Byte[] bytes = new Byte[256];
                String data = null;

                // Liaison de la socket au point de communication

                Logger.Log(Logger.Level.Info,("Server is listening..."));
                while (!Quit)
                {

                    Logger.Log(Logger.Level.Info, "Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = Server.AcceptTcpClient();

                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = Encoding.ASCII.GetString(bytes, 0, i);
                        Logger.Log(Logger.Level.Info,"Received: "+ data);

                        // Process the data sent by the client.
                        data = data.ToUpper();

                        byte[] msg = Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Logger.Log(Logger.Level.Info, "Sent: " + data);
                    }
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

        public static void SendMessages(Socket serverSocket, EndPoint clientEP, string msg, string pseudo)
        {
            // Encodage du string dans un buffer de bytes en ASCII
            byte[] buffer = new PaquetDonnees(Commande.POST, CommandeType.REPONSE, msg, pseudo).GetBytes();

            // Envoie du message aux clients
            int nBytes = serverSocket.SendTo(buffer, 0, buffer.Length, SocketFlags.None, clientEP);
        }

    }
}
