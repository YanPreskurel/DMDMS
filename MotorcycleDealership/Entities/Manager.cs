using MotorcycleDealership.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleDealership.Entities
{
    public class ManagerManagement
    {
        private readonly DataBase dataBase;

        public ManagerManagement(DataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        public int AddSupplier(string name, string address, string phone) // добавление поставщика
        {
            string query = "INSERT INTO Supplier (Name, Address, Phone) " +
                           "OUTPUT INSERTED.Supplier_Id " +
                           "VALUES (@Name, @Address, @Phone)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Address", address),
                new SqlParameter("@Phone", phone)
            };

            DataTable result = dataBase.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0 && result.Rows[0]["Supplier_Id"] != DBNull.Value)
            {
                return Convert.ToInt32(result.Rows[0]["Supplier_Id"]);
            }

            return 0;
        }

        public void DeleteSupplier(int supplierId) // удаление поставщика
        {
            string query = "DELETE FROM Supplier WHERE Supplier_Id = @SupplierId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@SupplierId", supplierId)
            };

            dataBase.ExecuteNonQuery(query, parameters);
        }

        public int AddOrganization(string name, string description, string phone, int supplierId) // добавление организации
        {
            string query = "INSERT INTO Organization (Name, Description, Phone, Supplier_Id) " +
                           "OUTPUT INSERTED.Organization_Id " +
                           "VALUES (@Name, @Description, @Phone, @SupplierId)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Description", description),
                new SqlParameter("@Phone", supplierId),
                new SqlParameter("@SupplierId", supplierId)
            };

            DataTable result = dataBase.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0 && result.Rows[0]["Organization_Id"] != DBNull.Value)
            {
                return Convert.ToInt32(result.Rows[0]["Organization_Id"]);
            }

            return 0;
        }

        public void DeleteOrganization(int organizationId) // удаление организации
        {
            string query = "DELETE FROM Organization WHERE Organization_Id = @OrganizationId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@OrganizationId", organizationId)
            };

            dataBase.ExecuteNonQuery(query, parameters);
        }

        public int AddCertificate(string name, DateTime dateOfIssue, DateTime dateOfExpiration, int organizationId) // добавление сертификата
        {
            string query = "INSERT INTO Certificate (Name, Date_of_issue, Date_of_expiration, Organization_Id) " +
                           "OUTPUT INSERTED.Certificate_Id " +
                           "VALUES (@Name, @DateOfIssue, @DateOfExpiration, @OrganizationId)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@DateOfIssue", dateOfIssue),
                new SqlParameter("@DateOfExpiration", dateOfExpiration),
                new SqlParameter("@OrganizationId", organizationId)
            };

            DataTable result = dataBase.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0 && result.Rows[0]["Certificate_Id"] != DBNull.Value)
            {
                return Convert.ToInt32(result.Rows[0]["Certificate_Id"]);
            }

            return 0;
        }

        public void DeleteCertificate(int сertificateId) // удаление сертификата
        {
            string query = "DELETE FROM Certificate WHERE Certificate_Id = @CertificateId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@CertificateId", сertificateId)
            };

            dataBase.ExecuteNonQuery(query, parameters);
        }

        public int AddModel(string name, decimal price, string characteristics) // добавление модели
        {
            string query = "INSERT INTO Model (Name, Price, Characteristics) " +
                           "OUTPUT INSERTED.Model_Id " +
                           "VALUES (@Name, @Price, @Characteristics)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Price", price),
                new SqlParameter("@Characteristics", characteristics)
            };

            DataTable result = dataBase.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0 && result.Rows[0]["Model_Id"] != DBNull.Value)
            {
                return Convert.ToInt32(result.Rows[0]["Model_Id"]);
            }

            return 0;
        }

        public void DeleteModel(int modelId) // удаление модели
        {
            string query = "DELETE FROM Model WHERE Model_Id = @ModelId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@ModelId", modelId)
            };

            dataBase.ExecuteNonQuery(query, parameters);
        }

        public void AddMotorcycle(int modelId, string color, int yearOfManufacture, int certificateId) // добавление мотоцикла
        {
            string query = "INSERT INTO Motorcycle (Model_Id, Color, Year_of_manufacture, Certificate_Id) " +
                           "VALUES (@ModelId, @Color, @YearOfManufacture, @CertificateId)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@ModelId", modelId),
                new SqlParameter("@Color", color),
                new SqlParameter("@YearOfManufacture", yearOfManufacture),
                new SqlParameter("@CertificateId", certificateId)
            };

            dataBase.ExecuteNonQuery(query, parameters);
        }

        public int AddPart(string name, decimal price) // добавление запчастей
        {
            string query = "INSERT INTO Part (Name, Price) " +
                           "OUTPUT INSERTED.Part_Id " +
                           "VALUES (@Name, @Price)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Price", price)
            };

            DataTable result = dataBase.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0 && result.Rows[0]["Part_Id"] != DBNull.Value)
            {
                return Convert.ToInt32(result.Rows[0]["Part_Id"]);
            }

            return 0;
        }


        public void DeleteMotorcycle(int motorcycleId) // удаление мотоцикла 
        {
            string query = "DELETE FROM Motorcycle WHERE Motorcycle_Id = @MotorcycleId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MotorcycleId", motorcycleId)
            };

            dataBase.ExecuteNonQuery(query, parameters);
        }

        public void AddMotorcyclePart(int motorcycleId, int partId)
        {
            string query = "INSERT INTO Motorcycle_Part (Motorcycle_Id, Part_Id) " +
                           "VALUES (@MotorcycleId, @PartId)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MotorcycleId", motorcycleId),
                new SqlParameter("@PartId", partId)
            };

            dataBase.ExecuteNonQuery(query, parameters);
        }

        public void DeleteMotorcyclePart(int motorcycleId, int partId)
        {
            string query = "DELETE FROM Motorcycle_Part WHERE Motorcycle_Id = @MotorcycleId AND Part_Id = @PartId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MotorcycleId", motorcycleId),
                new SqlParameter("@PartId", partId)
            };

            dataBase.ExecuteNonQuery(query, parameters);
        }

        public DataTable ViewMotorcycleList()
        {
            string query = "SELECT * FROM Motorcycle";

            return dataBase.ExecuteQuery(query);
        }

        public DataTable ViewMotorcyclePartList(int motorcycleId)
        {
            string query = @"
                             SELECT P.* 
                             FROM Part P
                             INNER JOIN Motorcycle_Part MP ON P.Part_Id = MP.Part_Id
                             WHERE MP.Motorcycle_Id = @MotorcycleId";

            SqlParameter[] parameters = { new SqlParameter("@MotorcycleId", motorcycleId) };
            return dataBase.ExecuteQuery(query, parameters);
        }

        public DataTable ViewModelList()
        {
            string query = "SELECT * FROM Model";
            return dataBase.ExecuteQuery(query);
        }

        public DataTable ViewOrganizationList()
        {
            string query = "SELECT * FROM Organization";
            return dataBase.ExecuteQuery(query);
        }

        public DataTable ViewSupplierList()
        {
            string query = "SELECT * FROM Supplier";
            return dataBase.ExecuteQuery(query);
        }

        public DataTable ViewOrderList(int managerId)
        {
            string query = "SELECT * FROM [Order] WHERE Manager_Id = @ManagerId";
            SqlParameter[] parameters = { new SqlParameter("@ManagerId", managerId) };
            return dataBase.ExecuteQuery(query, parameters);
        }

        public DataTable ViewClientList()
        {
            string query = "SELECT * FROM [User] WHERE Status = 'Client'";
            return dataBase.ExecuteQuery(query);
        }


        public int AddСlient(User user)
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

        public void DeleteСlient(int userId) // удаление клиента
        {
            string query = "DELETE FROM [User] WHERE User_Id = @UserId AND Status = 'Client'";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId),
            };

            dataBase.ExecuteNonQuery(query, parameters);
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
        public int GetMotorcycleIdFromUser()
        {
            while (true)
            {
                Console.Write("Введите Id мотоцикла: ");
                if (int.TryParse(Console.ReadLine(), out int motorcycleId))
                {
                    // Проверяем существование мотоцикла с введенным Id
                    if (IsMotorcycleIdValid(motorcycleId))
                    {
                        return motorcycleId;
                    }
                    else
                    {
                        Console.WriteLine("Мотоцикла с таким Id не существует. Попробуйте еще раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                }
            }
        }

        // Проверка существования мотоцикла с указанным Id
        public bool IsMotorcycleIdValid(int motorcycleId)
        {
            string query = "SELECT COUNT(*) FROM Motorcycle WHERE Motorcycle_Id = @MotorcycleId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MotorcycleId", motorcycleId),
            };

            DataTable resultTable = dataBase.ExecuteQuery(query, parameters);

            if (resultTable.Rows.Count > 0)
            {
                int count = Convert.ToInt32(resultTable.Rows[0][0]);
                return count > 0;
            }

            return false;
        }


        public int GetManagerIdFromUser(int userId)
        {
            string query = "SELECT Manager_Id FROM Manager WHERE User_Id = @UserId";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId", userId),
            };

            DataTable result = dataBase.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0 && result.Rows[0]["Manager_Id"] != DBNull.Value)
            {
                return Convert.ToInt32(result.Rows[0]["Manager_Id"]);
            }

            // Если не найдено, вернуть 0 или другое значение по умолчанию
            return 0;
        }
        public void AddManager(int userId) // добавление менеджера
        {
            string query = "INSERT INTO [Manager] (User_Id) VALUES (@UserId)";

            SqlParameter[] parameters =
            {
                    new SqlParameter("@UserId", userId),
                };

            dataBase.ExecuteNonQuery(query, parameters);
        }
    }
}
