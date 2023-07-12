using System;
using System.Net;
using System.Threading.Tasks;

namespace ULTRANET.Server
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            IPAddress ip = null;
            ushort port = 0;

            try
            {
                if (args.Length < 2)
                {
                    Console.Write("Enter IP: ");
                    ip = IPAddress.Parse(Console.ReadLine() ?? string.Empty);

                    Console.Write("Enter Port: ");
                    port = ushort.Parse(Console.ReadLine() ?? string.Empty);

                    Console.Clear();
                }

                Console.WriteLine("ULTRANET Server");

                if (args.Length >= 2)
                {
                    ip = IPAddress.Parse(args[0]);
                    port = ushort.Parse(args[1]);
                }

                if (ip != null) await Server.Initialize(new IPEndPoint(ip, port));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}