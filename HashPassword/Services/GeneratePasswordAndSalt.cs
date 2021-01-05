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

        /// <summary>
        /// Generates salt byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateSaltArray()
        {
            using var randomNumberGenerator = new RNGCryptoServiceProvider();
            var salt = new byte[32];
            randomNumberGenerator.GetBytes(salt);
            return salt;
        }

        /// <summary>
        /// Hashes with iterations
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        public byte[] Rfc2898Hashing(string password, byte[] salt, int iterations)
        {
            var passwordToHash = Encoding.UTF8.GetBytes(password);
            using Rfc2898DeriveBytes verySecureHash = new Rfc2898DeriveBytes(passwordToHash, salt, iterations);
            byte[] result = verySecureHash.GetBytes(32);
            return result;

        }
    }
}