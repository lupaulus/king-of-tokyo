using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServeurKoT.Connexion
{
    class Connecteur
    {
        public static Socket Start()
        {
            Socket listenSocket = new Socket(
                      AddressFamily.InterNetwork,
                      SocketType.Stream,
                      ProtocolType.Tcp);
            return listenSocket;
        }

    }
}
