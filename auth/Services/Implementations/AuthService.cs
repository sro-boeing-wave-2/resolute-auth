using auth.Context;
using auth.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using JWT.Builder;
using JWT.Algorithms;
using JWT.Serializers;
using JWT;
using auth.Utils;
using Newtonsoft.Json;
using auth.Models;
using Chilkat;
using Consul;
using System.Threading.Tasks;

namespace auth.Services.Implementations
{   
    class AuthService : IAuthService
    {

        private UserCredentialContext userCredentialContext;

        public AuthService(UserCredentialContext userCredentialContext)
        {
            this.userCredentialContext = userCredentialContext;
        }

        public bool AddUserCreadentials(string email, string password)
        {
            var random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            random.GetNonZeroBytes(salt);
            Console.WriteLine("Email: " + email);
            Console.WriteLine("Password: " + password);
            Console.WriteLine("Salt: " + Convert.ToBase64String(salt));
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            string hash = String.Concat(String.Concat(Convert.ToBase64String(salt), "$"), Convert.ToBase64String(pbkdf2.GetBytes(20)));
            Console.WriteLine("Password Hash: " + hash);
            userCredentialContext.UserCredentials.Add(new UserCredentials(email, hash));
            int result = userCredentialContext.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<string> Login(string email, string password)
        {
            if (!KeyUtils._isCreated)
            {
                KeyUtils.CreateKey();
            }
            Console.WriteLine("Verify Email: " + email);
            Console.WriteLine("Verify Password: " + password);
            UserCredentials userCredential = userCredentialContext.UserCredentials.Where(credential => credential.email.
                Equals(email)).FirstOrDefault();
            if (userCredential == null)
            {
                throw new UnauthorizedAccessException();
            }
            Console.WriteLine("User Credential password: " + userCredential.passwordHash);
            byte[] salt = Convert.FromBase64String(userCredential.passwordHash.Split("$")[0]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            string hash = String.Concat(String.Concat(Convert.ToBase64String(salt), "$"), Convert.ToBase64String(pbkdf2.GetBytes(20)));
            Console.WriteLine("Verification Hash: " + hash);
            if (hash != null && hash != "" && hash.Equals(userCredential.passwordHash))
            {
                OnboardingUtility onboardingUtility = new OnboardingUtility();
                JsonObject jwtHeader = new JsonObject();
                jwtHeader.AppendString("alg", "RS256");
                jwtHeader.AppendString("typ", "JWT");
                JsonObject claims = new JsonObject();
                Models.Agent agent = await onboardingUtility.GetAgentDetails(email);
                claims.AppendString("organisationid", "1");
                claims.AppendString("agentid", agent.Id.ToString());
                claims.AppendString("name", agent.Name);
                claims.AppendString("profileimageurl", agent.Profile_img_url);
                claims.AppendString("organisationname", "Boeing");
                claims.AppendString("email", email);

                Jwt jwt = new Jwt();
                string token = jwt.CreateJwtPk(jwtHeader.Emit(), claims.Emit(), KeyUtils.GetPrivateKey());
                return token;
            }
            throw new UnauthorizedAccessException();
        }

        public UserHeaders VerifyUserToken(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            var json = decoder.Decode(token, PasswordUtils.secretKey, verify: true);
            Console.Write("Decoded token: " + json);
            UserHeaders decodedHeaders = JsonConvert.DeserializeObject<UserHeaders>(json);
            Console.Write("Decoded headers: " + decodedHeaders);
            return decodedHeaders;
        }
    }
}
