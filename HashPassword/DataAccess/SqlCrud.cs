using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using HashPassword.Models;

namespace HashPassword.DataAccess
{
    public class SqlCrud
    {
        private readonly string _connectionString;
        private SqlDataAccess db = new SqlDataAccess();

        public SqlCrud(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Users ReturnUserPassword(Users user)
        {
            var p = new DynamicParameters();
            p.Add("@FirstName", user.FirstName);
            p.Add("@LastName", user.LastName);
            p.Add("@Password", "", DbType.String, direction: ParameterDirection.Output);
            p.Add("@Salt",  "",DbType.String, direction: ParameterDirection.Output);
            db.ReturnPerson(user, p, _connectionString);
            return user;
        }

        public void CreateUserAndPassword(Users user)
        {
            var p = new DynamicParameters();
            p.Add("@FirstName", user.FirstName);
            p.Add("@LastName", user.LastName);
            p.Add("@Salt", user.Salt);
            p.Add("@Password", user.Password);
            db.CreatePerson(user, p, _connectionString);
            //user.Id = p.Get<int>("@id");
        }
    }
}