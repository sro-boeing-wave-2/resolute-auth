using System;
using System.Collections.Generic;
using System.Text;

namespace auth.Models
{
    public class UserCredentialsDto
    {
        
        public string Username { get; set; }
        public string Password { get; set; }

        public UserCredentialsDto()
        {
            Username = "";
            Password = "";
        }

        public UserCredentialsDto(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

    }
}
