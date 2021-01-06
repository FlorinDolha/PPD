using System;
using System.Collections.Generic;

namespace DataAccess.Dto
{
    [Serializable]
    public class RequestVanzare
    {
        public int NrBileteVandute { get; set; }

        public int SpectacolId { get; set; }

        public List<int> Locuri { get; set; }
    }
}
