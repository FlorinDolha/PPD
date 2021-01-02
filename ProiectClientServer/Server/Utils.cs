using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class Utils
    {
        public static DateTime RandomDay()
        {
            Random random = new Random();

            DateTime start = new DateTime(2021, 6, 1);
            int range = (start - DateTime.Today).Days;
            return start.AddDays(random.Next(range));
        }
    }
}
