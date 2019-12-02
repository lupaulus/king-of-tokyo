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
    class Connecteur
    {
        /// <summary>
        /// Thread principal du serveur
        /// </summary>
        private static Thread serverLoop;
        /// <summary>
        /// Variable globale pour arreter le Thread
        /// </summary>
        private static bool quit;
        private List<Socket> sockets;

        public Connecteur()
        {
            quit = false;
            sockets = new List<Socket>();
        }

        public static Socket Start()
        {
            Socket listenSocket = new Socket(
                      AddressFamily.InterNetwork,
                      SocketType.Stream,
                      ProtocolType.Tcp);
            return listenSocket;
        }


        public static void StartServer()
        {
            serverLoop = new Thread(new ThreadStart(RunServer));
            serverLoop.Start();
        }

        private static void RunServer()
        {
            // **** Initialisation ******

            // Création de la socket d'écoute UDP
            Socket serverSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Tcp);

            // Liaison de la socket au point de communication
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 34343));

            // Reception message client
            EndPoint clientEP = new IPEndPoint(IPAddress.Any, 34343);
            byte[] buffer = new byte[PaquetDonnees.bufferSize];      //Le buffer pour recevoir les messages

            Logger.Log(Logger.Level.Info,("Server is listening..."));
            while (!quit)
            {

                try
                {
                    int nBytes = serverSocket.ReceiveFrom(buffer, buffer.Length, SocketFlags.None, ref clientEP);
                    // Decodage du buffer de bytes en ASCII vers un string
                    PaquetDonnees msg = new PaquetDonnees(buffer);

                    switch (msg.commande)
                    {
                        case Commande.POST:
                            // Decodage du buffer de bytes en ASCII vers un string
                            String message = msg.pseudo + " : " + msg.data;
                            messagesServeur.Add(message);
                            Console.WriteLine(message);
                            foreach (EndPoint client in clientsSub)
                            {
                                sendMessages(serverSocket, client, messagesServeur[messagesServeur.Count - 1], msg.pseudo);
                            }
                            break;
                        case Commande.STOPSERVEUR:
                            quit = true;
                            break;
                        case Commande.SUBSCRIBE:
                            clientsSub.Add(clientEP);
                            Console.WriteLine(clientEP + " vient de s'abonner");
                            break;
                    }
                }
                catch (SocketException E)
                {
                    Console.WriteLine(E.ToString());
                }
            }
            //************************************************************** Conclusion
            // Fermeture socket
            Console.WriteLine("Fermeture Socket...");
            serverSocket.Close();   //Ferme le Socket

            Console.WriteLine(" : Fermeture du serveur");
            Console.ReadKey();
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
