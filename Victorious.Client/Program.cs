using System.IO;
using System.Text;
using TOOLS;

namespace Victorious.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the IP of the Server: ");
            string ip = Console.ReadLine();
            Console.WriteLine("Starting TCP client...");
            TcpClient.Start(ip, 25575);

            byte[] guidBuffer = new byte[1024];
            int guidRead = TcpClient.Stream.Read(guidBuffer, 0, guidBuffer.Length);
            string guid = Encoding.UTF8.GetString(guidBuffer, 0, guidRead);
            TcpClient.Guid = new Guid(guid);

            Console.WriteLine($"You are: {guid}");
            Console.Write("Enter a Username: ");
            string username = Console.ReadLine();

            byte[] unameByte = Encoding.UTF8.GetBytes("#" + username);
            TcpClient.Stream.Write(unameByte, 0, unameByte.Length);

            Thread messageListener = new Thread(() => TcpClient.ListenForServerMessages());
            messageListener.IsBackground = true;
            messageListener.Start();

            while (true)
            {
                string message = Console.ReadLine();

                if (message?.ToLower() == "exit")
                {
                    TcpClient.Stop();
                    break;
                }

                Console.CursorTop--;
                TcpClient.Send(message);
            }
        }

    }
}
