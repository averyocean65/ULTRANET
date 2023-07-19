using System;
using System.Threading.Tasks;

namespace ULTRANET.Server
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            ushort port = 0;

            try
            {
                if (args.Length < 2)
                {
                    Console.Write("Enter Port: ");
                    port = ushort.Parse(Console.ReadLine() ?? string.Empty);

                    Console.Clear();
                }

                Console.WriteLine("ULTRANET Server");

                if (args.Length >= 1)
                {
                    port = ushort.Parse(args[1]);
                }

                await Server.Initialize(port);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}