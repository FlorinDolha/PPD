using System;

namespace DataAccess.Dto
{
    [Serializable]
    public class RequestVanzare
    {
        public int NrBileteVandute { get; set; }

        public int SpectacolId { get; set; }
    }
}
