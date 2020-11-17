using System;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using HashPassword.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace HashPassword.Services
{
    public class GeneratePasswordAndSalt
    {
        public string GeneratePassword(string password, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            SHA256Managed sha256HashString = new SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);
            Console.WriteLine($"Password: {Convert.ToBase64String(hash)}"); // Salt converted to a string
            return Convert.ToBase64String(hash);


        }

        public string GenerateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[256 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}"); // Salt converted to a string
            return Convert.ToBase64String(salt);

        }
    }
}