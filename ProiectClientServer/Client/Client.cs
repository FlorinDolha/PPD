using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    class Client
    {
        private Controller ctrl;

        private IList<string> Spectacole = new List<string>()
        {
            "Spectacol0",
            "Spectacol1",
            "Spectacol2",
            "Spectacol3",
            "Spectacol4",
        };

        public Client(Controller ctrl)
        {
            this.ctrl = ctrl;
        }

        //public void Start()
        //{
        //    Console.WriteLine("Titlul spectacolului:");
        //    string titlu = Console.ReadLine();
        //    //Console.WriteLine("Lista locurilor libere:");
        //    //IList<int> locuriLibere = ctrl.GetLocuriLibere(titlu);
        //    //foreach (int loc in locuriLibere)
        //    //{
        //    //    Console.WriteLine(loc);
        //    //}
        //    Console.WriteLine("Cate bilete doriti?");
        //    int nrBilete = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Locurile:");
        //    string[] locuriString = Console.ReadLine().Split(' ');
        //    List<int> locuri = new List<int>();
        //    foreach (string loc in locuriString)
        //    {
        //        locuri.Add(int.Parse(loc));
        //    }
        //    Console.WriteLine(ctrl.CumparaBilet(titlu, nrBilete, locuri));
        //}

        public void Start()
        {
            Random random = new Random();
            int index = random.Next(5);
            string titlu = Spectacole[index];

            IList<int> locuriLibere = ctrl.GetLocuriLibere(titlu);

            int nrBilete = random.Next(10);

            IList<int> locuri = GenerateRandom(locuriLibere, nrBilete);

            Result result = ctrl.CumparaBilet(titlu, nrBilete, locuri.ToList());

            Console.WriteLine(result.ToString());

            if (result == Result.Closed)
            {
                throw new Exception("Server closed");
            }
        }

        public static IList<int> GenerateRandom(IList<int> collection, int count)
        {
            Random random = new Random();
            return collection.OrderBy(d => random.Next()).Take(count).ToList();
        }
    }
}
