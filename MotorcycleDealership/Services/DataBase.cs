using MotorcycleDealership.Entities;
using System.Data;
using System.Data.SqlClient;

namespace MotorcycleDealership.Services
{
    public class DataBase
    {

        private readonly string connectionString; // строка подключения

        public DataBase(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void ExecuteNonQuery(string query, SqlParameter[] parameters = null) // (например, запросы INSERT, UPDATE, DELETE и другие, не связанные с извлечением данных)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null) // для получения данных от сервера 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
    }
}