using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Information;
using Information.Models;
using Newtonsoft.Json;

namespace WindowsServer
{
    class Server
    {
        public static TcpListener Listner;

        private static string ReadMessage(Socket client)
        {
            var buffer = new byte[1024];

            var message = string.Empty;
            client.Receive(buffer);

            foreach (var b in buffer)
            {
                message += Convert.ToChar(b);
            }

            return message.Trim('\0');
        }

        private static Packet FilterMessage(string message, Socket client)
        {
            switch (type)
            {
                    case PacketType.Connect:
                        return JsonConvert.DeserializeObject<Connect>(message);
            }
            return new Packet();
        }
        
        public static void Main(string[] args)
        {
            Listner = new TcpListener(IPAddress.Any, 1400);
            Listner.Start();

            while (true)
            {

                var client = Listner.AcceptSocket();

                Console.WriteLine($"[SOCKET_ACCEPT] -> {client.RemoteEndPoint}");
                new Task(() =>
                {
                    while (client.Connected)
                    {
                        FilterMessage(ReadMessage(client), client);
                    }
                }).Start();
            }
        }
    }
}
