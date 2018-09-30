using auth.Models;
using System;
using System.Threading.Tasks;

namespace auth.Services
{
    public interface IAuthService
    {

        /**
         * Method for user login
         */
        Task<string> Login(string email, string password);

        /**
         * Method to save user credentials
         */
        Boolean AddUserCreadentials(string email, string password);

        /**
         * Method to verify user session token.
         */
        UserHeaders VerifyUserToken(string token);
    }
}
