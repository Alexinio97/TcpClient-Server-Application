using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class TestServer
    {
        private int _port;

        public TestServer(int port)
        {
            _port = port;
        }

        public async Task Start()
        {
            var server = new TcpListener(new IPAddress(new byte[] { 0, 0, 0, 0 }), 30000);
            server.Start();
            Console.WriteLine("Accepting...");
            var clients = new List<Task>();
            while (true) // handle multiple clients
            {
                using (var client = await server.AcceptTcpClientAsync())
                {
                    //this actually spawns two tasks
                    var clientTask = await Task.Factory.StartNew(async state =>
                    {
                        using (var tcpClient = (TestClient)state)
                        {
                            //Thread.Sleep(6000);
                            await treatClient(tcpClient,new byte[] { 3, 4 });
                        }
                    }, new TestClient(client), TaskCreationOptions.LongRunning);

                    clientTask.Wait(); // wait for the current task
                    clients.Add(clientTask);
                    Console.WriteLine($"Client {clientTask.Id} has been completed.");

                }
                await Task.WhenAll(clients.ToArray());
            }
        }

        public async Task treatClient(TestClient client,byte[] dataToSend)
        {
            Console.WriteLine("Wait client name...");
            await client.SendAsync(dataToSend);
            var data = await client.ReceiveAsync(2);

            foreach (var b in data)
            {
                Console.WriteLine($"Data sent: {b}");
            }
        }
    }
}
