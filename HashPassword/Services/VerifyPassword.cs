using System;
using System.Security.Cryptography;
using System.Text;
using HashPassword.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace HashPassword.Services
{
    public class VerifyPassword
    {
        public bool VerifyValidPassword(string password, Users user, Users u)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + u.Salt);
            SHA256Managed sha256HashString = new SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);
            u.Password = Convert.ToBase64String(hash);
            Console.WriteLine($"u.Password: {Convert.ToBase64String(hash)}"); // Salt converted to a string

            if (Equals(user.Password, u.Password))
            {
                return true;
            }


            //return Convert.ToBase64String(hash);


            return false;
        }
    }
}