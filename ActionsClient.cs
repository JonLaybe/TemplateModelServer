using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class ActionsClient //действия с клиентом
    {
        private static ActionsClient _actionsClient;
        private INotifyAccept _property;
        private ActionsClient(INotifyAccept property)
        {
            _property = property;
        }
        public static ActionsClient GetActionsClient(INotifyAccept property) => _actionsClient == null ? _actionsClient = new ActionsClient(property) : _actionsClient;

        public void ListenClient(TcpClient client) // принимает от пользователя сообщение
        {
            try
            {
                while (client.Client.Connected)
                {
                    NetworkStream stream;
                    stream = client.GetStream();
                    StreamReader reader = new StreamReader(stream);
                    string message = reader.ReadLine();
                    if (message != null && message != "")
                    {
                        _property.GetMessage(message, client);
                    }
                }
            }
            catch { }
        }
    }
}
