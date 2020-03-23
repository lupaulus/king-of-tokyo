using ServeurKoT.Connexion;
using ServeurKoT.Controleur;
using SimpleLogger;
using SimpleLogger.Logging.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServeurKoT
{
    class Program
    {
        private static string ADRESSE_SERVEUR = "127.0.0.1";
        private static int PORT_SERVEUR = 13670;

        static void Main(string[] args)
        {
            // Mise en place du logger
            Logger.LoggerHandlerManager
            .AddHandler(new ConsoleLoggerHandler())
            .AddHandler(new FileLoggerHandler());

            // Lancement de l'application
            Logger.Log(Logger.Level.Info, "*** Serveur King Of Tokyo ***");


            // Mise en place d'une connexion client
            Serveur.Init(ADRESSE_SERVEUR, PORT_SERVEUR);
            Serveur.Instance.StartServer();

            int idPartie =  GPartie.Instance.CreerPartie("TEST1",6);





        }
    }
}
