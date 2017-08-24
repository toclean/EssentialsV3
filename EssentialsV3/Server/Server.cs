using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
        private static List<ClientConnection> _clientList = new List<ClientConnection>();

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

        private static void UserLoginConfirmation(Task<Socket> client, User user)
        {

        }

        public static bool AcceptConnection(Packet message)
        {
            var user = new User();
            user = new User
            {
                Username = message.Username,
                Password = message.Password
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
            if (_userFactory.IsUser(user))
            {
                client.Result.Disconnect(true);
                Console.WriteLine($"[REMOVE_CLIENT] -> {client.Result.RemoteEndPoint}");
            }
        }

        public static void BroadCastToClients(string message)
        {
            foreach (var client in _clientList)
            {
                client.Client.Send(Encoding.ASCII.GetBytes(message));
            }
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

                        while (true)
                        {
                            var message = JsonConvert.DeserializeObject<Packet>(ReadMessage(client));

                            if (message?.Type == "Connect")
                            {
                                AcceptConnection(message);
                                _clientList.Add(new ClientConnection
                                {
                                    Client = client.Result,
                                    Username = message.Username
                                });
                            }
                            else if (message?.Type == "Disconnect")
                            {
                                DisconnectConnection(new User
                                {
                                    Username = message.Username
                                }, client);
                                _clientList.RemoveAll(x => x.Client == client.Result && x.Username == message.Username);
                                break;
                            }else if (message?.Type == "Message")
                            {
                                BroadCastToClients("TEST");
                            }
                        }

                    }).Start();

                    clientCount++;
                }
            }).Start();


            Console.Read();
        }
    }
}