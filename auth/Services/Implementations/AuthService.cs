﻿using auth.Context;
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
            userCredentialContext.UserCredentials.Add(new UserCredentials(email, password));
            int result = userCredentialContext.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public string Login(string email, string password)
        {
            UserCredentials userCredential = userCredentialContext.UserCredentials.Where(credential => credential.email.
                Equals(email)).FirstOrDefault();
            if (userCredential == null)
            {
                string token = new JwtBuilder().WithAlgorithm(new HMACSHA256Algorithm()).
                    WithSecret(PasswordUtils.secretKey).AddClaim("UserCredentials", JsonConvert.
                        SerializeObject(new UserHeaders(1, 1))).Build();
                return token;
                //return "";
            }
            byte[] salt = Encoding.ASCII.GetBytes(userCredential.passwordHash.Split("$")[0]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            string hash = String.Concat(String.Concat(salt.ToString(), "$"), pbkdf2.GetBytes(20).ToString());

            if (hash != null && hash != "" && hash.Equals(userCredential.passwordHash))
            {
                string token = new JwtBuilder().WithAlgorithm(new HMACSHA256Algorithm()).
                    WithSecret(PasswordUtils.secretKey).AddClaim("UserCredentials", JsonConvert.
                        SerializeObject(new UserHeaders(1 ,1))).Build();
                return token;
            }
            return "";
        }

        public UserHeaders VerifyUserToken(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            var json = decoder.Decode(token, PasswordUtils.secretKey, verify: true);
            UserHeaders decodedHeaders = JsonConvert.DeserializeObject<UserHeaders>(json);
            return decodedHeaders;
        }
    }
}
