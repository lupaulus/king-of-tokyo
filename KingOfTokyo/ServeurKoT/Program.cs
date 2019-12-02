using ServeurKoT.Connexion;
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
        static void Main(string[] args)
        {
            // Mise en place du logger
            Logger.LoggerHandlerManager
            .AddHandler(new ConsoleLoggerHandler())
            .AddHandler(new FileLoggerHandler());

            // Lancement de l'application
            Logger.Log(Logger.Level.Info, "*** Serveur King Of Tokyo ***");


            // Mise en place d'une connexion client
            Serveur.Init("127.0.0.1", 13670);





        }
    }
}
