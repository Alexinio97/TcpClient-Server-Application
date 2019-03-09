using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Linq;
using Server;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
             Run().Wait();
           
        }

        private static async Task Run()
        {
            Console.WriteLine("Listening...");
            var server = new TestServer(30000);
            await server.Start();
            //Console.ReadLine();
            //TODO: extract the sending of message and display of message from commented code bellow so that SRP is met

            //var server = new TcpListener(new IPAddress(new byte[] { 0, 0, 0, 0 }), 30000);
            //server.Start();
            //Console.WriteLine("Accepting...");
            //using (var client = await server.AcceptTcpClientAsync())
            //{
            //    using (var clientStream = client.GetStream())
            //    {
            //        Console.WriteLine("Wait client name...");
            //        await clientStream.WriteAsync(new byte[] { 1, 2 });
            //        //var writer = new StreamWriter(clientStream);
            //        byte[] data = new byte[2];
            //        var i = 0;
            //        while ((i += await clientStream.ReadAsync(data, i, 2 - i)) < 2) ;
            //        foreach (var b in data)
            //        {
            //            Console.WriteLine(b);
            //        }
            //    }
            //}
        }
    }
}








