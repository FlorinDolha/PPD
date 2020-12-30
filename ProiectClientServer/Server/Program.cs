using DataAccess;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context = Context.Instance;
            context.ClearDatabase();
            context.GenerateData();

            Server server = new Server();
            server.Start();
        }
    }
}
