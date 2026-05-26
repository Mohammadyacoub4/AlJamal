using Microsoft.Data.SqlClient;

namespace AlJamal.Database;

internal static class DatabaseHelper
{
    public const string ConnectionString =
        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BilliardDB;Integrated Security=True;TrustServerCertificate=True";

    public static SqlConnection OpenConnection()
    {
        var con = new SqlConnection(ConnectionString);
        con.Open();
        return con;
    }
}
