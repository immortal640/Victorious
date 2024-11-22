using TOOLS;

namespace Victorious
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Starting TCP server...");
            TcpServer.Start(25575);

            while (true)
            {
                Console.Write("");
            }
        }

        static void StartMC()
        {
            //
        }
    }
}
