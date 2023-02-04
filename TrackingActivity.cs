using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class TrackingActivity // проверяет подключен ли пользователь
    {
        private static TrackingActivity? _trackingActivity;
        private INotifyAccept _property;
        public List<TcpClient> TcpClients { get; private set; }

        private TrackingActivity(INotifyAccept property)
        {
            _property = property;
            TcpClients = new List<TcpClient>();
        }

        public static TrackingActivity GetTrackingActivity(INotifyAccept property) => _trackingActivity == null ? _trackingActivity = new TrackingActivity(property) : _trackingActivity;

        public void AddClient(TcpClient client) => TcpClients.Add(client);

        public void Tracking()
        {
            Task.Run(() =>
            {
                int index = 0;
                while (true)
                {
                    if (TcpClients.Count > 0)
                    {
                        if (index >= TcpClients.Count)
                        {
                            index = 0;
                        }
                        if (index < TcpClients.Count)
                        {
                            if (!TcpClients[index].Client.Connected)
                            {
                                Task.Run(() =>
                                {
                                    TcpClients[index].Client.Close();
                                    TcpClients.Remove(TcpClients[index]);
                                });
                                _property.DisconnectClient(TcpClients[index]);
                                Thread.Sleep(1000);
                            }
                        }
                        index++;
                    }
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
