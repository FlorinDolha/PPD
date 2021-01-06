using DataAccess;
using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            unitOfWork.VerificareRepository.DeleteAll();
            unitOfWork.VanzariLocuriRepository.DeleteAll();
            unitOfWork.VanzareRepository.DeleteAll();
            unitOfWork.SpectacolRepository.DeleteAll();

            unitOfWork.Save();

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                Spectacol spectacol = new Spectacol
                {
                    Data = Utils.RandomDay(),
                    Pret = random.Next(5, 30) + Math.Round(random.NextDouble(), 2),
                    Titlu = $"Spectacol{i}",
                    Sold = 0,
                };

                unitOfWork.SpectacolRepository.Insert(spectacol);
            }

            Sala sala = new Sala
            {
                NrLocuri = 250
            };
            unitOfWork.SalaRepository.Insert(sala);
            Sala sala2 = new Sala
            {
                NrLocuri = 270
            };
            unitOfWork.SalaRepository.Insert(sala2);

            unitOfWork.Save();
            unitOfWork.Dispose();

            Server server = new Server();
            server.Start();
        }
    }
}
