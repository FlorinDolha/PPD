using System;
using System.Collections.Generic;

namespace Client
{
    class Client
    {
        private Controller ctrl;

        public Client(Controller ctrl)
        {
            this.ctrl = ctrl;
        }

        public void Start()
        {
            Console.WriteLine("Titlul spectacolului:");
            string titlu = Console.ReadLine();
            //Console.WriteLine("Lista locurilor libere:");
            //IList<int> locuriLibere = ctrl.GetLocuriLibere(titlu);
            //foreach (int loc in locuriLibere)
            //{
            //    Console.WriteLine(loc);
            //}
            Console.WriteLine("Cate bilete doriti?");
            int nrBilete = int.Parse(Console.ReadLine());
            Console.WriteLine("Locurile:");
            string[] locuriString = Console.ReadLine().Split(' ');
            List<int> locuri = new List<int>();
            foreach (string loc in locuriString)
            {
                locuri.Add(int.Parse(loc));
            }
            Console.WriteLine(ctrl.CumparaBilet(titlu, nrBilete, locuri));
        }
    }
}
