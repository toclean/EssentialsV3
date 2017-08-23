using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Models;
using Information;
using Newtonsoft.Json;
using Information.Models;
using Microsoft.Win32;

namespace Server
{
    public class Server
    {
        private static TcpListener _listener;
        private static UserFactory _userFactory;

        public static Task<Socket> AcceptClient(TcpListener listener)
        {
            return listener.AcceptSocketAsync();
        }

        public static string ReadMessage(Task<Socket> client)
        {
            var buffer = new byte[1024];
            string message = string.Empty;
            client.Result.Receive(buffer);
            foreach (var b in buffer)
            {
                message += Convert.ToChar(b);
            }

            return message.Trim('\0');
        }

        public static bool AcceptConnection(string message)
        {
            var user = new User();
            var raw = JsonConvert.DeserializeObject<Connect>(message);
            user = new User
            {
                Username = raw.Username,
                Password = raw.Password
            };

            if (_userFactory.IsUser(user))
            {
                // TODO: Add log user in
                return true;
            }

            return false;
        }

        public static void DisconnectConnection(User user, Task<Socket> client)
        {
            
        }

        public static void Main(string[] args)
        {
            _listener = new TcpListener(IPAddress.Any, 1400);
            _userFactory = new UserFactory();

            _listener.Start();

            new Task(() =>
            {
                int clientCount = 0;
                // TODO: Figure out when to exit this loop (max user count?)
                while (clientCount < 10)
                {
                    var client = AcceptClient(_listener);
                    Console.WriteLine($"[ACCEPT_CLIENT] -> {client.Result.RemoteEndPoint}");

                    // Login user
                    new Task(() =>
                    {
                        var message = JsonConvert.DeserializeObject(ReadMessage(client)) as Packet;
                        if (message?.Type == "Connect")
                        {
                            AcceptConnection(message.ToString());
                        }else if (message?.Type == "Disconnect")
                        {
                            DisconnectConnection(new User
                                {
                                    Username = ((Connect) message).Username,
                                    Password = ((Connect) message).Password
                                },
                                client);
                        }
                    }).Start();

                    clientCount++;
                }
            }).Start();


            Console.Read();
        }
    }
}