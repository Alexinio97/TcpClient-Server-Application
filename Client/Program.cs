using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press ENTER to connect.");
            Console.ReadLine();
            Run().Wait();
            Console.ReadLine();
        }

        private static async Task Run()
        {
            using (var client = new TestClient("127.0.0.1", 30000))
            {
                await client.ConnectAsync();
                Console.WriteLine("Connected");
                var data = await client.ReceiveAsync(2);

                foreach (var b in data)
                {
                    Console.WriteLine(b);
                }
                var output = data.Select(b => (byte)(b + 10)).ToArray();

                await client.SendAsync(output);
            }
        }
    }
}
