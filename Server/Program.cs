using System;
using System.Net;
using System.Threading.Tasks;

namespace ULTRANET.Server
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: ULTRANET.Server <ip> <port>");
                Console.WriteLine("Example: ULTRANET.Server 127.0.0.1 1234");
                return;
            }

            try
            {
                Console.WriteLine("ULTRANET Server");

                IPAddress ip = IPAddress.Parse(args[0]);
                ushort port = ushort.Parse(args[1]);

                await Server.Initialize(new IPEndPoint(ip, port));
                Console.WriteLine("Server stopped");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}