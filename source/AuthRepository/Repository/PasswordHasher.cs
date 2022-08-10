using System;
using System.Security.Cryptography;
using System.Text;

namespace AuthRepository
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string salt, string password)
        {
            string toHash = salt + password;

            byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(toHash));

            return Convert.ToBase64String(hash);
        }
    }
}
