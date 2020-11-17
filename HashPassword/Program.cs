using System;
using System.IO;
using System.Security.Cryptography;
using HashPassword.DataAccess;
using HashPassword.Models;
using HashPassword.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace HashPassword
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlCrud sql = new SqlCrud(GetConnectionString());
            GeneratePasswordAndSalt generatePasswordAndSalt = new GeneratePasswordAndSalt();
            SqlDataAccess d = new SqlDataAccess();
            VerifyPassword verifyValidPassword = new VerifyPassword();

            // Creates a user
            Users user = new Users();

            Console.Write("Enter First Name: ");
            user.FirstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            user.LastName = Console.ReadLine();


            Console.Write("Enter a password: ");
            user.Password = Console.ReadLine();


            user.Salt = generatePasswordAndSalt.GenerateSalt();
            user.Password = generatePasswordAndSalt.GeneratePassword(user.Password, user.Salt);



            //generatePasswordAndSalt.GeneratePassword(user);


            // Creates a user wit password user ans saves it to the database
            sql.CreateUserAndPassword(user);


            Users u = new Users();
            Console.Write("Enter a password To Verify Login: ");
            String verifyPassword = Console.ReadLine();

            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u = sql.ReturnUserPassword(u);


            Console.WriteLine($"Password: {u.Password} Salt: {u.Salt}");

            Console.WriteLine(verifyValidPassword.VerifyValidPassword(verifyPassword, user, u));


            Console.WriteLine("Hello World!");
        }


        /// <summary>
        /// Gets the connection string from appsettings.json
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>Returns the string to the database</returns>
        private static string GetConnectionString(string connectionString = "Default")
        {
            string output = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            output = config.GetConnectionString(connectionString);

            return output;
        }
    }
}