using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlJamal.Database;

internal static class DatabaseBootstrap
{
    public static string? LastError { get; private set; }

    public static bool TryInitialize(out string? errorMessage)
    {
        LastError = null;
        try
        {
            EnsureDatabaseExists();
            RunScript("MigrateSchema.sql");
            if (!SchemaIsValid())
            {
                errorMessage = "المخطط غير مكتمل بعد الترقية. راجع Database\\MigrateSchema.sql";
                LastError = errorMessage;
                return false;
            }

            errorMessage = null;
            return true;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            LastError = ex.ToString();
            return false;
        }
    }

    private static void EnsureDatabaseExists()
    {
        var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "BilliardDB.db");
        var dir = Path.GetDirectoryName(dbPath);
        if (dir != null && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        
        // SQLite will automatically create the database file when we open the connection,
        // but we can make sure the helper connection works.
        using var con = DatabaseHelper.OpenConnection();
    }

    private static bool SchemaIsValid()
    {
        try
        {
            using var con = DatabaseHelper.OpenConnection();
            using var cmd = new SqliteCommand(
                """
                SELECT COUNT(*) FROM sqlite_master 
                WHERE type='table' 
                AND name IN ('TableTypes', 'BilliardTables', 'Products', 'PlayerSessions', 'OrderItems', 'Invoices', 'InvoiceLines')
                """, con);
            return Convert.ToInt32(cmd.ExecuteScalar()) == 7;
        }
        catch
        {
            return false;
        }
    }

    private static void RunScript(string fileName)
    {
        var path = FindScript(fileName);
        if (path == null)
            throw new FileNotFoundException($"لم يُعثر على {fileName} بجانب التطبيق.");

        var script = File.ReadAllText(path);
        var batches = SplitBatches(script);

        using var con = DatabaseHelper.OpenConnection();

        foreach (var batch in batches)
        {
            if (string.IsNullOrWhiteSpace(batch))
                continue;

            using var cmd = new SqliteCommand(batch, con);
            cmd.ExecuteNonQuery();
        }
    }

    private static string? FindScript(string fileName)
    {
        var candidates = new[]
        {
            Path.Combine(AppContext.BaseDirectory, "Database", fileName),
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Database", fileName))
        };

        return candidates.FirstOrDefault(File.Exists);
    }

    private static IEnumerable<string> SplitBatches(string script)
    {
        var lines = script.Replace("\r\n", "\n").Split('\n');
        var batch = new List<string>();

        foreach (var line in lines)
        {
            if (line.Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
            {
                if (batch.Count > 0)
                    yield return string.Join(Environment.NewLine, batch);
                batch.Clear();
            }
            else
            {
                batch.Add(line);
            }
        }

        if (batch.Count > 0)
            yield return string.Join(Environment.NewLine, batch);
    }
}
