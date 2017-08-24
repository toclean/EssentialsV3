using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class FormProvider
    {
        private static Login _login;
        private static Register _register;
        private static Chat _chat;

        public FormProvider(Login login)
        {
            _login = login;
            _register = new Register(this);
            _chat = new Chat(this);
        }

        public Login Login => _login;
        public Register Register => _register;
        public Chat Chat => _chat;
    }
}
