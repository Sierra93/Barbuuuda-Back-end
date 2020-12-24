using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Barbuuuda.Core.Data {
    public sealed class HashMD5 {
        public static string HashPassword(string password) {
            string getHashPassword = GetHash(password);

            return getHashPassword;
        }

        static string GetHash(string password) {
            byte[] hash = Encoding.ASCII.GetBytes(password);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";

            foreach (var b in hashenc) {
                result += b.ToString("x2");
            }

            return result;
        }
    }
}
