using Microsoft.Data.SqlClient;

namespace AlJamal.Database;

internal static class DatabaseBootstrap
{
    private const string MasterConnection =
        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=15";

    public static string? LastError { get; private set; }

    public static bool TryInitialize(out string? errorMessage)
    {
        LastError = null;
        try
        {
            EnsureLocalDbStarted();
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

    private static void EnsureLocalDbStarted()
    {
        try
        {
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "sqllocaldb",
                Arguments = "start MSSQLLocalDB",
                CreateNoWindow = true,
                UseShellExecute = false
            };
            using var p = System.Diagnostics.Process.Start(psi);
            p?.WaitForExit(5000);
        }
        catch
        {
            // sqllocaldb غير موجود — نفترض أن الخادم يعمل
        }
    }

    private static void EnsureDatabaseExists()
    {
        using var con = new SqlConnection(MasterConnection);
        con.Open();
        using var cmd = new SqlCommand(
            """
            IF DB_ID(N'BilliardDB') IS NULL
                CREATE DATABASE BilliardDB;
            """, con);
        cmd.ExecuteNonQuery();
    }

    private static bool SchemaIsValid()
    {
        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(
            """
            SELECT CASE WHEN
                COL_LENGTH('dbo.BilliardTables', 'DisplayName') IS NOT NULL
                AND OBJECT_ID('dbo.PlayerSessions') IS NOT NULL
                AND OBJECT_ID('dbo.Products') IS NOT NULL
                AND OBJECT_ID('dbo.OrderItems') IS NOT NULL
                AND COL_LENGTH('dbo.Invoices', 'PlayerSessionId') IS NOT NULL
            THEN 1 ELSE 0 END
            """, con);
        return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
    }

    private static void RunScript(string fileName)
    {
        var path = FindScript(fileName);
        if (path == null)
            throw new FileNotFoundException($"لم يُعثر على {fileName} بجانب التطبيق.");

        var script = File.ReadAllText(path);
        var batches = SplitBatches(script);

        using var con = new SqlConnection(MasterConnection);
        con.Open();

        foreach (var batch in batches)
        {
            if (string.IsNullOrWhiteSpace(batch))
                continue;

            using var cmd = new SqlCommand(batch, con) { CommandTimeout = 120 };
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
