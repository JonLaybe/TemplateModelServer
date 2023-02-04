using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class Server : OptionsServer // главный класс
    {
        public List<TcpClient> TcpClients
        {
            get
            {
                return _trackingActivity.TcpClients;
            }
        }
        public INotifyAccept Property { get; set; } // класс которуму будет приходить уведомления
        private TrackingActivity _trackingActivity; // проверяет подключен ли пользователь
        private ActionsClient _actionsClient; // принимает от пользователя сообщение

        public Server(string name_server, string ip, int port)
        {
            NameServer = name_server;
            Ip = ip;
            Port = port;
            IsStart = false;
        }

        private void Notify(TcpClient client)
        {
            Property.ConnactClient(client);
        }

        private void AcceptClient() // принимает клиентов
        {
            TcpClient client = TcpListener.AcceptTcpClient();
            _trackingActivity.AddClient(client);
            Task.Run(() =>
            {
                _actionsClient.ListenClient(client);
            });
            Notify(client);
        }

        private void Tracking() // проверяет подключен ли пользователь
        {
            _trackingActivity.Tracking();
        }

        public void Start() // запускает сервер
        {
            IsStart = true;

            TcpListener = new TcpListener(IPAddress.Any, Port);
            _trackingActivity = TrackingActivity.GetTrackingActivity(Property);
            _actionsClient = ActionsClient.GetActionsClient(Property);

            TcpListener.Start();
            Tracking();
            while(IsStart)
            {
                AcceptClient();
            }
            TcpListener.Stop();
        }
    }
}
