#nullable enable
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "BilliardDB.db");
var dir = Path.GetDirectoryName(dbPath);
if (dir != null && !Directory.Exists(dir))
{
    Directory.CreateDirectory(dir);
}

var cs = $"Data Source={dbPath}";

// 1. Run migrations if tables are missing
try
{
    using var c = new SqliteConnection(cs);
    c.Open();

    // Check if tables are missing
    using var chkCmd = new SqliteCommand("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='BilliardTables'", c);
    var hasTables = Convert.ToInt32(chkCmd.ExecuteScalar()) > 0;

    if (!hasTables)
    {
        Console.WriteLine("Database is empty. Initializing schema...");
        
        // Traverse up to find the project root containing Database folder
        var currentDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        string? sqlPath = null;
        while (currentDir != null)
        {
            var dbDir = Path.Combine(currentDir.FullName, "Database");
            var file = Path.Combine(dbDir, "MigrateSchema.sql");
            if (File.Exists(file))
            {
                sqlPath = file;
                break;
            }
            currentDir = currentDir.Parent;
        }

        if (sqlPath == null)
        {
            throw new FileNotFoundException("Could not find MigrateSchema.sql in any parent directories.");
        }

        Console.WriteLine($"Found schema script at: {sqlPath}");
        var sql = File.ReadAllText(sqlPath);
        
        // Split SQL script into individual commands by parsing semicolons or using GO
        var batches = sql.Replace("\r\n", "\n").Split(new[] { "\nGO\n", "\ngo\n", "\nGo\n", "\ngO\n" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var batch in batches)
        {
            var cleaned = batch.Trim();
            if (string.IsNullOrWhiteSpace(cleaned)) continue;
            using var cmd = new SqliteCommand(cleaned, c);
            cmd.ExecuteNonQuery();
        }
        Console.WriteLine("Database schema initialized successfully.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Initialization failed: {ex.Message}");
}

// 2. Verify schema and print tables
try
{
    using var c = new SqliteConnection(cs);
    c.Open();
    Console.WriteLine("Checking tables:");
    foreach (var t in new[] { "TableTypes", "BilliardTables", "Products", "PlayerSessions", "OrderItems", "Invoices", "InvoiceLines" })
    {
        using var chk = new SqliteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='{t}'", c);
        Console.WriteLine($"  {t}: {(chk.ExecuteScalar() is null ? "MISSING" : "OK")}");
    }
    
    Console.WriteLine("Configured billiard tables:");
    using var cmd = new SqliteCommand(
        """
        SELECT t.DisplayName, tt.TypeName,
               (SELECT COUNT(*) FROM PlayerSessions ps WHERE ps.TableId=t.TableId AND ps.IsActive=1)
        FROM BilliardTables t JOIN TableTypes tt ON t.TableTypeId=tt.TableTypeId ORDER BY t.TableNumber
        """, c);
    using var r = cmd.ExecuteReader();
    while (r.Read()) 
    {
        Console.WriteLine($"  {r.GetString(0)} ({r.GetString(1)}) - Active Players: {r.GetInt32(2)}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"FAIL: {ex.Message}");
}
