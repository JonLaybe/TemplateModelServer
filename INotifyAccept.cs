using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public interface INotifyAccept
    {
        public void ConnactClient(TcpClient client);
        public void GetMessage(string message, TcpClient client);
        public void DisconnectClient(TcpClient client);
    }
}
