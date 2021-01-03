using DataAccess;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        private const string IP_ADRESS = "127.0.0.1";
        private const int PORT = 1234;
        private static readonly IPAddress LOCAL_IP_ADRESS = IPAddress.Parse(IP_ADRESS);
        IFormatter formatter;

        TcpListener serverSocket;
        IList<TcpClient> clients;

        public Server()
        {
            clients = new List<TcpClient>();
            serverSocket = new TcpListener(LOCAL_IP_ADRESS, PORT);
            formatter = new BinaryFormatter();
        }

        public void Start()
        {
            serverSocket.Start();
            Console.WriteLine("Server started...\n");
            StartVerificare();

            TcpClient client;
            while (true)
            {
                client = serverSocket.AcceptTcpClient();
                clients.Add(client);
                Task taskWork = Task.Factory.StartNew(() => Work(client));
            }
        }

        private async void StartVerificare()
        {
            try
            {
                await Task.Factory.StartNew(Verifica);
            }
            catch (DoneException dex)
            {
                Console.WriteLine(dex.Message);
                foreach (TcpClient client in clients)
                {
                    SendMessage(Result.Closed, client.GetStream());
                }
                throw dex;
            }
        }

        private void Verifica()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (true)
            {
                if (watch.Elapsed.TotalSeconds >= 120)
                {
                    throw new DoneException("Server has stopped");
                }
                Thread.Sleep(5000);

                //Partea de verificare
                Valideaza();
            }
        }

        private void Valideaza()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Console.WriteLine("Validez");
            var spectacole = unitOfWork.SpectacolRepository.Get().ToList();
            foreach (Spectacol spectacol in spectacole) 
            {
                var vanzari = unitOfWork.VanzareRepository.Get(vanzare => vanzare.SpectacolId == spectacol.Id).ToList();

                int nrLocuriOcupate = 0;
                int nrLocuriVandute = 0;
                double suma = 0;

                foreach (Vanzare vanzare in vanzari)
                {
                    int locuriMaxime = unitOfWork.SalaRepository.GetByID(1).NrLocuri;
                    var locuriVandute = unitOfWork.VanzariLocuriRepository.Get(locVandut => locVandut.VanzareId == vanzare.Id);

                    nrLocuriOcupate += locuriVandute.GroupBy(locVandut => locVandut.Loc).Count();
                    nrLocuriVandute += vanzare.NrBileteVandute;

                    suma += vanzare.Suma;
                }

                string status = "corect";

                if (nrLocuriOcupate != nrLocuriVandute || suma != spectacol.Sold)
                {
                    status = "incorect";
                }

                unitOfWork.VerificareRepository.Insert(new Verificare
                {
                    SpectacolId = spectacol.Id,
                    Data = DateTime.Now,
                    Sold = spectacol.Sold,
                    Status = status,
                });
            }

            unitOfWork.Save();
            unitOfWork.Dispose();
        }

        private void Work(TcpClient client)
        {
            Process(client.GetStream());
            clients.Remove(client);
        }

        private void Process(NetworkStream networkStream)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            RequestVanzare requestVanzare = Receive(networkStream);

            if (requestVanzare == null)
            {
                SendMessage(Result.Fail, networkStream);
                return;
            }

            Spectacol spectacol = unitOfWork.SpectacolRepository.GetByID(requestVanzare.SpectacolId);

            if (spectacol == null)
            {
                SendMessage(Result.Fail, networkStream);
                return;
            }

            double pret = spectacol.Pret;


            ///Adauga vanzare
            unitOfWork.VanzareRepository.Insert(new Vanzare
            {
                Data = DateTime.Now,
                NrBileteVandute = requestVanzare.NrBileteVandute,
                SpectacolId = requestVanzare.SpectacolId,
                Suma = pret * requestVanzare.NrBileteVandute,
            });

            unitOfWork.Save();

            ///Actualizeaza sold
            spectacol.Sold += pret * requestVanzare.NrBileteVandute;
            unitOfWork.SpectacolRepository.Update(spectacol);
            unitOfWork.Save();


            int vanzareId = unitOfWork.VanzareRepository.Get().OrderByDescending(vanzare => vanzare.Id).First().Id;

            foreach (int loc in requestVanzare.Locuri)
            {
                unitOfWork.VanzariLocuriRepository.Insert(new VanzariLocuri
                {
                    Loc = loc,
                    VanzareId = vanzareId,
                });

                unitOfWork.Save();
            }

            unitOfWork.Save();
            unitOfWork.Dispose();

            SendMessage(Result.Success, networkStream);
        }

        public void SendMessage(Result result, NetworkStream stream)
        {
            formatter.Serialize(stream, result);
            stream.Flush();
        }

        private RequestVanzare Receive(NetworkStream stream)
        {
            RequestVanzare vanzare = formatter.Deserialize(stream) as RequestVanzare;

            return vanzare;
        }
    }
}
