using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ASPCoreWebApplication.Model;

namespace ASPCoreWebApplication.Services
{
    public class ProductService
    {
        private static string databaseSource = "azdevserver.database.windows.net";
        private static string username = "azdevadmin";
        private static string password = "azdevPassword@123";
        private static string database = "AzDevLearning";

        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = databaseSource;
            builder.InitialCatalog = database;
            builder.UserID = username;
            builder.Password = password;

            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            List<Product> productlst = new List<Product>();
            string statement = "SELECT ProductID,ProductName,Quantity from Products";
            SqlConnection connection = GetConnection();

            connection.Open();

            SqlCommand sqlcommand = new SqlCommand(statement, connection);

            using (SqlDataReader reader = sqlcommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    productlst.Add(product);
                }
            }
            connection.Close();
            return productlst;
        }
    } 
}
