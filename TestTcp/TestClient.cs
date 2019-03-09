using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class TestClient: IDisposable
    {
        private readonly TcpClient _tcpClient;
        private NetworkStream _stream;

        public NetworkStream Stream { get => _stream; set => _stream = value; }

        public TestClient(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _stream = _tcpClient.GetStream();
        }
        
        public Task SendAsync(byte[] data)
        {
            return _stream.WriteAsync(data, 0, data.Length);
        }

        public async Task<byte[]> ReceiveAsync(int count)
        {
            byte[] data = new byte[count];
            var i = 0;
            while ((i += await _stream.ReadAsync(data, i, count - i)) < count) ;
            return data;
        }

        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
            if (_tcpClient != null)
            {
                _tcpClient.Dispose();
            }
        }
    }
}
