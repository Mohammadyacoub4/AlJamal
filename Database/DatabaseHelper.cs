using Microsoft.Data.Sqlite;
using System.IO;

namespace AlJamal.Database;

internal static class DatabaseHelper
{
    public static readonly string ConnectionString =
        $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "BilliardDB.db")}";

    public static SqliteConnection OpenConnection()
    {
        var con = new SqliteConnection(ConnectionString);
        con.Open();
        return con;
    }
}
