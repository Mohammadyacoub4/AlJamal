using Microsoft.Data.SqlClient;

var cs = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BilliardDB;Integrated Security=True;TrustServerCertificate=True";
try
{
    using var c = new SqlConnection(cs);
    c.Open();
    foreach (var t in new[] { "PlayerSessions", "OrderItems", "Products", "InvoiceLines" })
    {
        using var chk = new SqlCommand($"SELECT OBJECT_ID('dbo.{t}')", c);
        Console.WriteLine($"{t}: {(chk.ExecuteScalar() is DBNull ? "MISSING" : "OK")}");
    }
    using var cmd = new SqlCommand(
        """
        SELECT t.DisplayName, tt.TypeName,
               (SELECT COUNT(*) FROM PlayerSessions ps WHERE ps.TableId=t.TableId AND ps.IsActive=1)
        FROM BilliardTables t JOIN TableTypes tt ON t.TableTypeId=tt.TableTypeId ORDER BY t.TableNumber
        """, c);
    using var r = cmd.ExecuteReader();
    while (r.Read()) Console.WriteLine($"  {r.GetString(0)} ({r.GetString(1)})");
}
catch (Exception ex)
{
    Console.WriteLine($"FAIL: {ex.Message}");
}
