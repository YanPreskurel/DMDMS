using MotorcycleDealership.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleDealership.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Status { get; set; }
    }

        public class UserManagement
    {
        private readonly DataBase dataBase;

        public UserManagement(DataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        public int AddUser(User user)
        {
            string query = "INSERT INTO [User] (Login, Password, Name, Surname, Status) " +
                           "OUTPUT INSERTED.User_Id " +
                           "VALUES (@Login, @Password, @Name, @Surname, @Status)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Login", user.Login),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Name", user.Name),
                new SqlParameter("@Surname", user.Surname),
                new SqlParameter("@Status", user.Status)
            };

            DataTable result = dataBase.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0 && result.Rows[0]["User_Id"] != DBNull.Value)
            {
                return Convert.ToInt32(result.Rows[0]["User_Id"]);
            }

            return 0;
        }

        public DataTable GetUserByLoginAndPassword(string login, string password)
        {
            string query = "SELECT * FROM [User] WHERE Login = @Login AND Password = @Password";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Login", login),
                new SqlParameter("@Password", password)
            };

            return dataBase.ExecuteQuery(query, parameters);
        }

        public DataTable GetUserByLogin(string login)
        {
            string query = "SELECT * FROM [User] WHERE Login = @Login";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Login", login),
            };

            return dataBase.ExecuteQuery(query, parameters);
        }
    }
}
