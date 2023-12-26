using MotorcycleDealership.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleDealership.Entities
{
    public class AdminManagement
    {
        private readonly DataBase dataBase;

        public AdminManagement(DataBase dataBase)
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

        public void DeleteUser(int userId) // удаление пользователя
        {
            string query = "DELETE FROM [User] WHERE User_Id = @UserId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId)
            };

            dataBase.ExecuteNonQuery(query, parameters);
        }

        public DataTable ViewTheJournal()
        {
            string query = "SELECT * FROM Journal";
            return dataBase.ExecuteQuery(query);
        }

        public void DisplayTable(DataTable table, string tableName)
        {
            Console.WriteLine($"\n{tableName}\n");

            foreach (DataColumn column in table.Columns)
            {
                Console.Write($"{column.ColumnName,-20}");
            }

            Console.WriteLine();

            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write($"{item,-20}");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public DataTable ViewManagerList()
        {
            string query = "SELECT * FROM [User] WHERE Status = 'Manager'";
            return dataBase.ExecuteQuery(query);
        }

        public DataTable ViewClientList()
        {
            string query = "SELECT * FROM [User] WHERE Status = 'Client'";
            return dataBase.ExecuteQuery(query);
        }
        public DataTable GetAllUsers()
        {
            string query = "SELECT * FROM [User]";
            return dataBase.ExecuteQuery(query);
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

        public void AddAdmin(int userId)
        {
            string query = "INSERT INTO [Admin] (User_Id) VALUES (@UserId)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId),
            };

            dataBase.ExecuteNonQuery(query, parameters);
        }
    }
}