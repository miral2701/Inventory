// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

class Program
{
    static string connectionString = @"Server=(localdb)\mssqllocaldb;Database=testdb;Trusted_Connection=True;TrustServerCertificate=True";



    static void Main()
    {
        string objectName = "New Computer";
        int objectQuantity = 4;
        string objectStatus = "available";

        int objectId=AddInventoryObject(connectionString,objectName, objectQuantity,objectStatus);
        Console.WriteLine(objectId);
    }
    public static int AddInventoryObject(string connectionString,string objectName,int objectQuantity,string objectStatus)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("AddInventoryObject",connection)) 
            { 
                command.CommandType=System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@objectName", objectName);
                command.Parameters.AddWithValue("@objectQuantity", objectQuantity);
                command.Parameters.AddWithValue("@objectStatus", objectStatus);

                SqlParameter outputParam = new SqlParameter("@ObjectID", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                command.Parameters.Add(outputParam);    
                command.ExecuteNonQuery();

                int objectId=Convert.ToInt32(outputParam.Value);
                return objectId;

            }
        }
    }
    
}