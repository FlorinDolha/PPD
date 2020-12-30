using System;

namespace Server
{
    public class DoneException : Exception
    {
        public DoneException(string message) : base(message)
        {
        }
    }
}
