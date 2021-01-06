using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const string SERVERADRESS = "127.0.0.1";
            const int PORT = 1234;
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(SERVERADRESS, PORT);
            Controller ctrl = new Controller(tcpClient);
            Client client = new Client(ctrl);
            while (true)
            {
                try
                {
                    client.Start();
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
                Thread.Sleep(2000);
            }

            Console.ReadLine();
        }
    }
}
