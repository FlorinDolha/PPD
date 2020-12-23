using DataAccess;
using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        public const string IP_ADRESS = "127.0.0.1";
        public const int PORT = 1234;
        public static readonly IPAddress LOCAL_IP_ADRESS = IPAddress.Parse(IP_ADRESS);

        static void Main(string[] args)
        {
            Context context = Context.Instance;
            TcpListener serverSocket = new TcpListener(LOCAL_IP_ADRESS, PORT);
            serverSocket.Start();
        }
    }
}
