using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;
using DataAccessLayer.Models;
using Information;
using Newtonsoft.Json;

namespace Client
{
    public partial class Login : Form
    {
        public static TcpClient Client;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            Client = new TcpClient();
        }

        private void SendServerMessage(string message)
        {
            var stream = Client.GetStream();
            var text = Encoding.ASCII.GetBytes(message);
            stream.Write(text, 0, text.Length);
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            ///if (string.IsNullOrEmpty(usernameTb.Text) || string.IsNullOrEmpty(passwordTb.Text)) return;
            //if (new UserFactory().GetUser(usernameTb.Text, passwordTb.Text).Count < 1) return;
            //if (User.Connected) return;
            if (!Client.Connected)
            {
                Client = new TcpClient();
                Client.Connect(Dns.GetHostAddresses("localhost"), 1400);
            }

            SendServerMessage(JsonConvert.SerializeObject(new PacketFactory().ConnectPacket(PacketType.Connect, new User
            {
                Username = usernameTb.Text,
                Password = passwordTb.Text
            })));
        }

        private void registerLl_Click(object sender, EventArgs e)
        {
            // Show registration form
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Client.Connected) return;
            SendServerMessage(JsonConvert.SerializeObject(new PacketFactory().DisconnectPacket(PacketType.Disconnect, new User
            {
                Username = usernameTb.Text,
                Password = passwordTb.Text
            })));
        }
    }
}
