using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Information.Models
{
    public class ClientConnection
    {
        public string Username { get; set; }
        public Socket Client { get; set; }
    }
}
