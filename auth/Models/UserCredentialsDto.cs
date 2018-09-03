using System;
using System.Collections.Generic;
using System.Text;

namespace auth.Models
{
    class UserCredentialsDto
    {
        
        public string Email { get; set; }
        public string Password { get; set; }

        public UserCredentialsDto()
        {
            Email = "";
            Password = "";
        }

        public UserCredentialsDto(string Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }

    }
}
