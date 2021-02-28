﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Barbuuuda.Core.Data {
    public sealed class AuthOptions {
        public const string ISSUER = "MyAuthServer"; // Издатель токена
        public const string AUDIENCE = "MyAuthClient"; // Потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // Ключ для шифрования
        public const int LIFETIME = 10; // Время жизни токена

        public static SymmetricSecurityKey GetSymmetricSecurityKey() {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
