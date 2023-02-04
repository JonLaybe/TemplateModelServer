using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class OptionsServer // настройки сервера
    {
        public TcpListener TcpListener { get; set; }
        public string NameServer { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public bool IsStart { get; set; }
    }
}
