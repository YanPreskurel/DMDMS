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
    public class ClientManagement
    {
        private readonly DataBase dataBase;

        public ClientManagement(DataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        public void DeleteAccount(int userId) // удаление пользователя или акканута клиента
        {
            string query = "DELETE FROM [User] WHERE User_Id = @UserId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId)
            };

            dataBase.ExecuteNonQuery(query, parameters);
        }

        public DataTable ViewMotorcycleList() // + Model
        {
            string query = "SELECT M.*, Model.[Name] AS ModelName " +
                           "FROM Motorcycle M " +
                           "INNER JOIN Model ON M.Model_Id = Model.Model_Id";
            return dataBase.ExecuteQuery(query);
        }

        public DataTable ViewOrderList(int userId)
        {
            string query = @"
                             SELECT 
                                 O.Order_Id,
                                 O.Date_of_order,
                                 O.Date_of_delivery,
                                 O.Amount,
                                 M.Motorcycle_Id
                             FROM [Order] O
                             INNER JOIN Motorcycle M ON O.Motorcycle_Id = M.Motorcycle_Id
                             WHERE O.Client_Id = (SELECT Client_Id FROM [Client] WHERE User_Id = @UserId)";

            SqlParameter[] parameters = { new SqlParameter("@UserId", userId) };
            return dataBase.ExecuteQuery(query, parameters);
        }

        public DataTable ViewMototrcyclePartList(int motorcycleId)
        {
            string query = "SELECT * FROM Motorcycle_Part WHERE Motorcycle_Id = @MotorcycleId";
            SqlParameter[] parameters = { new SqlParameter("@MotorcycleId", motorcycleId) };
            return dataBase.ExecuteQuery(query, parameters);
        }

        public void CreateOrder(decimal amount, int userId, int motorcycleId, int managerId)
        {
            string orderQuery = "INSERT INTO [Order] (Date_of_order, Date_of_delivery, Amount, Client_Id, Manager_Id, Motorcycle_Id) " +
                                "VALUES (GETDATE(), DATEADD(DAY, 2, GETDATE()), @Amount, " +
                                "(SELECT Client_Id FROM [Client] WHERE User_Id = @UserId), @ManagerId, @MotorcycleId)";

            SqlParameter[] orderParameters =
            {
                new SqlParameter("@Amount", amount),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@ManagerId", managerId),
                new SqlParameter("@MotorcycleId", motorcycleId)
            };

            dataBase.ExecuteNonQuery(orderQuery, orderParameters);
        }

        public DataTable ViewDetailedInfoForUserOrder(int userId) // подробный список заказов текущего пользователя 
        {
            string query = @"
                             SELECT 
                                 O.Order_Id,
                                 O.Date_of_order,
                                 O.Date_of_delivery,
                                 O.Amount,
                                 M.Motorcycle_Id,
                                 M.Color,
                                 M.Year_of_manufacture,
                                 Mo.[Name] AS Model_Name,
                                 Mo.Price AS Model_Price,
                                 Mo.Characteristics AS Model_Characteristics,
                                 C.[Name] AS Certificate_Name,
                                 C.Date_of_issue AS Certificate_Date_of_issue,
                                 C.Date_of_expiration AS Certificate_Date_of_expiration,
                                 Org.[Name] AS Organization_Name,
                                 Org.Phone AS Organization_Phone,
                                 Org.[Description] AS Organization_Description,
                                 S.[Name] AS Supplier_Name,
                                 S.Address AS Supplier_Address,
                                 S.Phone AS Supplier_Phone
                             FROM [Order] O
                             INNER JOIN Motorcycle M ON O.Motorcycle_Id = M.Motorcycle_Id
                             INNER JOIN Model Mo ON M.Model_Id = Mo.Model_Id
                             LEFT JOIN [Certificate] C ON Mo.Organization_Id = C.Organization_Id
                             LEFT JOIN Organization Org ON Mo.Organization_Id = Org.Organization_Id
                             LEFT JOIN Supplier S ON Org.Supplier_Id = S.Supplier_Id
                             INNER JOIN Client Cl ON O.Client_Id = Cl.Client_Id
                             WHERE Cl.User_Id = @UserId";
           
            SqlParameter[] parameters = { new SqlParameter("@UserId", userId) };
            return dataBase.ExecuteQuery(query, parameters);
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

        public bool IsMotorcycleIdValid(int motorcycleId)
        {
            string query = "SELECT COUNT(*) FROM Motorcycle WHERE Motorcycle_Id = @MotorcycleId";
            SqlParameter[] parameters = { new SqlParameter("@MotorcycleId", motorcycleId) };

            DataTable resultTable = dataBase.ExecuteQuery(query, parameters);

            if (resultTable.Rows.Count > 0)
            {
                int count = Convert.ToInt32(resultTable.Rows[0][0]);
                return count > 0;
            }

            return false;
        }

        public DataTable ViewManagerList()
        {
            string query = "SELECT * FROM [User] WHERE Status = 'Manager'";
            return dataBase.ExecuteQuery(query);
        }

        public bool IsManagerIdValid(int managerId)
        {
            string query = "SELECT COUNT(*) FROM [User] WHERE Status = 'Manager' AND User_Id = @ManagerId";
            SqlParameter[] parameters = { new SqlParameter("@ManagerId", managerId) };

            DataTable resultTable = dataBase.ExecuteQuery(query, parameters);

            if (resultTable.Rows.Count > 0)
            {
                int count = Convert.ToInt32(resultTable.Rows[0][0]);
                return count > 0;
            }

            return false;
        }

        public decimal GetMotorcycleCost(int motorcycleId)
        {
            string query = "SELECT Mo.Price FROM Motorcycle M INNER JOIN Model Mo ON M.Model_Id = Mo.Model_Id WHERE M.Motorcycle_Id = @MotorcycleId";
            SqlParameter[] parameters = { new SqlParameter("@MotorcycleId", motorcycleId) };

            DataTable resultTable = dataBase.ExecuteQuery(query, parameters);

            if (resultTable.Rows.Count > 0)
            {
                return Convert.ToDecimal(resultTable.Rows[0]["Price"]);
            }

            return 0;
        }


        public void AddClient(int userId)  // добавление клиента
        {          
            string query = "INSERT INTO [Client] VALUES (@UserId)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId),
            };

            dataBase.ExecuteNonQuery(query, parameters);
        }
    }
}
