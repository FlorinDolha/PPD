using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using DataAccess;
using DataAccess.Dto;

namespace Client
{
    public class Controller
    {
        private TcpClient tcpClient;
        private IFormatter formatter;

        public Controller(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            this.formatter = new BinaryFormatter();
        }


        public void sendMessage(NetworkStream stream, RequestVanzare vanzare)
        {
            formatter.Serialize(stream, vanzare);
            stream.Flush();
        }

        private Result receiveResponse(NetworkStream stream)
        {
            Result rezultat = (Result)formatter.Deserialize(stream);
            return rezultat;
        }

        public string CumparaBilet(string titlu, int nrBilete)
        {
            RequestVanzare request = new RequestVanzare();
            UnitOfWork unitOfWork = new UnitOfWork();

            IList<Spectacol> spectacole = unitOfWork.SpectacolRepository.Get().ToList();

            unitOfWork.Dispose();

            foreach (Spectacol sp in spectacole)
            {
                if (sp.Titlu == titlu)
                {
                    request.SpectacolId = sp.Id;
                }
            }
            request.NrBileteVandute = nrBilete;
            //request.Locuri = locuri;

            sendMessage(tcpClient.GetStream(), request);


            Result response = receiveResponse(tcpClient.GetStream());
            return response.ToString();
        }
    }
}
