using System.Net.Sockets;

namespace TcpServer
{
    public class TcpServer : INotifyAccept
    {
        Server server;
        public TcpServer()
        {
            server = new Server("Server", "127.0.0.1", 1300);
            server.Property = this;
            server.Start();
        }

        public void ConnactClient(TcpClient client)
        {
            Console.WriteLine("Connact " + server.TcpClients.Count);
        }

        public void DisconnectClient(TcpClient client)
        {
            Console.WriteLine("Disconnect");
        }

        public void GetMessage(string message, TcpClient client)
        {
            Console.WriteLine($"Message: {message}");
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            TcpServer server = new TcpServer();
        }
    }
}