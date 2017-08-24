using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using DataAccessLayer;
using DataAccessLayer.Models;
using Information;
using Newtonsoft.Json;

namespace Client
{
    public partial class Login : Form
    {
        private static TcpClient _client;
        private static FormProvider _formProvider;
        //private static SqlConnection _connection = DataBase.Connect();
        private static readonly UserFactory UserFactory = new UserFactory();
        private static readonly PacketFactory PacketFactory = new PacketFactory();

        public Login()
        {
            InitializeComponent();
            _formProvider = new FormProvider(this);
            _formProvider.Login.Show();
        }

        public TcpClient GetTcpClient()
        {
            return _client;
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            _client = new TcpClient();
        }

        private void SendServerMessage(string message)
        {
            var stream = _client.GetStream();
            var text = Encoding.ASCII.GetBytes(message);
            stream.Write(text, 0, text.Length);
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(usernameTb.Text) || string.IsNullOrEmpty(passwordTb.Text)) return;
            //if (UserFactory.GetUser(new User{Username = usernameTb.Text, Password = passwordTb.Text}).Count < 1) return;
            //if (!_client.Connected)
            //{
            //    _client = new TcpClient();
            //    _client.Connect(Dns.GetHostAddresses("localhost"), 1400);
            //}

            //SendServerMessage(JsonConvert.SerializeObject(PacketFactory.ConnectPacket(PacketType.Connect, new User
            //{
            //    Username = usernameTb.Text,
            //    Password = passwordTb.Text
            //})));

            _formProvider.Login.Hide();
            _formProvider.Chat.Show();
        }

        private void registerLl_Click(object sender, EventArgs e)
        {
            _formProvider.Login.Hide();
            _formProvider.Register.Show();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_client.Connected) return;
            SendServerMessage(JsonConvert.SerializeObject(PacketFactory.DisconnectPacket(PacketType.Disconnect, new User
            {
                Username = usernameTb.Text,
                Password = passwordTb.Text
            })));
            _formProvider.Register.Close();
            _formProvider.Chat.Close();
        }
    }
}
