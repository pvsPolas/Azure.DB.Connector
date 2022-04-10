using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDbConnector
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            ScriptHelper helper = new ScriptHelper();

           // helper.CreateDummyInsertsForSproc();

            builder.DataSource = "?.database.windows.net";
            builder.UserID = "?";
            builder.Password = "?";
            builder.InitialCatalog = "?";

            try
            {  
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    //SampleQuery(connection);                    
                    GetData(connection);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }

        static void GetData(SqlConnection connection)
        {
            Console.WriteLine("\nStore Proc From Azure");
            Console.WriteLine("=======================\n");

            String sproc = "sp_Users_GetAllUsers";

            using (SqlCommand command = new SqlCommand(sproc, connection))
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1} {2} {3}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    }
                }
            }
        }

        static void SampleQuery(SqlConnection connection)
        {
            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");

            String sql = "SELECT name, collation_name FROM sys.databases";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                    }
                }
            }
        }
    }
}
