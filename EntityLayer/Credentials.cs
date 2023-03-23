using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Credentials(string username, string password, string token)
        {
            Username = username;
            Password = password;
            Token = token;
        }
    }
}
