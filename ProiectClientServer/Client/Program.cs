using System;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const string SERVERADRESS = "127.0.0.1";
            const int PORT = 1234;
            TcpClient tcpClient = new TcpClient();
            bool connect = true;
            while (connect)
            {
                try
                {
                    tcpClient.Connect(SERVERADRESS, PORT);
                    connect = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Controller ctrl = new Controller(tcpClient);

            Client ui = new Client(ctrl);
            ui.Start();

            Console.ReadLine();
        }
    }
}
