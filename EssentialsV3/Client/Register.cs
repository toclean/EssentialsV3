using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace Client
{
    public partial class Register : Form
    {

        private static UserFactory _userFactory = new UserFactory();
        private static FormProvider _formProvider;

        public Register(FormProvider formProvider)
        {
            InitializeComponent();
            _formProvider = formProvider;
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = usernameTb.Text,
                Password = passwordTb.Text
            };

            if (_userFactory.IsUser(user))
            {
                MessageBox.Show("User already exist!");
                usernameTb.Clear();
                passwordTb.Clear();
            }
            else
            {
                _userFactory.CreateUser(user, null);
                _formProvider.Register.Hide();
                _formProvider.Login.Show();
            }
        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formProvider.Login.Close();
        }
    }
}
