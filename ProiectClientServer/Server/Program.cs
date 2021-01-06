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
            unitOfWork.SalaRepository.DeleteAll();

            unitOfWork.Save();

            Spectacol spectacol = new Spectacol
            {
                Data = Utils.RandomDay(),
                Pret = 100,
                Titlu = "Spectacol0",
                Sold = 0,
            };

            unitOfWork.SpectacolRepository.Insert(spectacol);

            spectacol = new Spectacol
            {
                Data = Utils.RandomDay(),
                Pret = 200,
                Titlu = "Spectacol1",
                Sold = 0,
            };

            unitOfWork.SpectacolRepository.Insert(spectacol);

            spectacol = new Spectacol
            {
                Data = Utils.RandomDay(),
                Pret = 150,
                Titlu = "Spectacol2",
                Sold = 0,
            };

            unitOfWork.SpectacolRepository.Insert(spectacol);

            unitOfWork.Save();

            unitOfWork.SalaRepository.Insert(new Sala
            {
                NrLocuri = 100
            });

            unitOfWork.Save();

            unitOfWork.Dispose();

            Server server = new Server();
            server.Start();
        }
    }
}
