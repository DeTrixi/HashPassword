using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using HashPassword.Models;

namespace HashPassword.DataAccess
{
    public class SqlDataAccess
    {
        public List<T> LoadData<T, TU>(string sqlStatement, TU parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameters);
            }
        }


        /// <summary>
        /// Connects to stored procedure
        /// </summary>
        /// <param name="User"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public Users CreatePerson(Users User, DynamicParameters p, string connectionString)
        {
            using (IDbConnection connection =
                new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Execute("dbo.spCreateUserAndPassword", p, commandType: CommandType.StoredProcedure);
                //User.Id = p.Get<int>("@id");
                return User;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <param name="p"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public Users ReturnPerson(Users user,DynamicParameters p, string connectionString)
        {
            using (IDbConnection connection =
                new System.Data.SqlClient.SqlConnection(connectionString))
            {

                connection.Execute("dbo.spReturnUser", p, commandType: CommandType.StoredProcedure);
                user.Password = p.Get<string>("@Password");
                user.Salt = p.Get<string>("@Salt");
                return user;
            }
        }

    }
}