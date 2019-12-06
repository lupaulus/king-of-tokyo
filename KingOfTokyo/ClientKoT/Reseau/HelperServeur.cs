
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Client.Reseau{
    public class HelperServeur {

        public HelperServeur() {
        }

        private static int JOUEUR_MAX = 6;

        private static string IP_SERVEUR;

        /// <summary>
        /// @param value
        /// </summary>
        public void ConnexionServeur(Joueur value, string hostName, int portNum) {
            try
            {
                Logger.Log(Logger.Level.Info, " Initialisation de la connexion vers le serveur");
                TcpClient client = new TcpClient(hostName, portNum);
                NetworkStream ns = client.GetStream();

                byte[] bytes = new byte[1024];
                int bytesRead = ns.Read(bytes, 0, bytes.Length);
                
                Logger.Log(Logger.Level.Info,Encoding.ASCII.GetString(bytes, 0, bytesRead));

                

            }catch(Exception e)
            {
                Logger.Log(Logger.Level.Error, e.ToString());
            }
        }

        public void DeconnexionServeur() {
            // TODO implement here
        }

    }
}