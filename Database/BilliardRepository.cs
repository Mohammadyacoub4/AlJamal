using AlJamal.Models;
using Microsoft.Data.SqlClient;

namespace AlJamal.Database;

internal static class BilliardRepository
{
    public static List<BilliardTableInfo> GetTables()
    {
        const string sql = """
            SELECT t.TableId, t.TableTypeId, t.TableNumber, t.DisplayName, tt.TypeName, tt.HourlyRate,
                   tt.FirstHourRate, tt.AdditionalHourRate,
                   (SELECT COUNT(*) FROM PlayerSessions ps
                    WHERE ps.TableId = t.TableId AND ps.IsActive = 1) AS ActivePlayers
            FROM BilliardTables t
            INNER JOIN TableTypes tt ON t.TableTypeId = tt.TableTypeId
            WHERE t.IsActive = 1
            ORDER BY t.TableNumber
            """;

        var list = new List<BilliardTableInfo>();
        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(sql, con);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            list.Add(new BilliardTableInfo
            {
                TableId = r.GetInt32(0),
                TableTypeId = r.GetInt32(1),
                TableNumber = r.GetInt32(2),
                DisplayName = r.GetString(3),
                TypeName = r.GetString(4),
                HourlyRate = r.GetDecimal(5),
                FirstHourRate = r.GetDecimal(6),
                AdditionalHourRate = r.GetDecimal(7),
                ActivePlayers = r.GetInt32(8)
            });
        }
        return list;
    }

    public static BilliardTableInfo? GetTable(int tableId)
    {
        return GetTables().FirstOrDefault(t => t.TableId == tableId);
    }

    public static List<PlayerSessionInfo> GetActivePlayers(int tableId)
    {
        const string sql = """
            SELECT ps.PlayerSessionId, ps.TableId, ps.PlayerName, ps.StartTime, ps.EndTime,
                   ps.HourlyRate, ps.IsActive, ps.IsInvoiced, bt.DisplayName,
                   tt.FirstHourRate, tt.AdditionalHourRate
            FROM PlayerSessions ps
            INNER JOIN BilliardTables bt ON ps.TableId = bt.TableId
            INNER JOIN TableTypes tt ON bt.TableTypeId = tt.TableTypeId
            WHERE ps.TableId = @TableId AND ps.IsActive = 1
            ORDER BY ps.StartTime
            """;

        return ReadPlayerSessions(sql, new SqlParameter("@TableId", tableId));
    }

    public static PlayerSessionInfo? GetPlayerSession(int playerSessionId)
    {
        const string sql = """
            SELECT ps.PlayerSessionId, ps.TableId, ps.PlayerName, ps.StartTime, ps.EndTime,
                   ps.HourlyRate, ps.IsActive, ps.IsInvoiced, bt.DisplayName,
                   tt.FirstHourRate, tt.AdditionalHourRate
            FROM PlayerSessions ps
            INNER JOIN BilliardTables bt ON ps.TableId = bt.TableId
            INNER JOIN TableTypes tt ON bt.TableTypeId = tt.TableTypeId
            WHERE ps.PlayerSessionId = @Id
            """;

        var list = ReadPlayerSessions(sql, new SqlParameter("@Id", playerSessionId));
        return list.FirstOrDefault();
    }

    public static int AddPlayer(int tableId, string? playerName)
    {
        using var con = DatabaseHelper.OpenConnection();
        using var rateCmd = new SqlCommand(
            """
            SELECT tt.HourlyRate FROM BilliardTables t
            INNER JOIN TableTypes tt ON t.TableTypeId = tt.TableTypeId
            WHERE t.TableId = @TableId
            """, con);
        rateCmd.Parameters.AddWithValue("@TableId", tableId);
        var rate = (decimal)rateCmd.ExecuteScalar()!;

        using var cmd = new SqlCommand(
            """
            INSERT INTO PlayerSessions (TableId, PlayerName, StartTime, HourlyRate, IsActive, IsInvoiced)
            OUTPUT INSERTED.PlayerSessionId
            VALUES (@TableId, @PlayerName, SYSDATETIME(), @HourlyRate, 1, 0)
            """, con);
        cmd.Parameters.AddWithValue("@TableId", tableId);
        cmd.Parameters.AddWithValue("@PlayerName", (object?)playerName ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@HourlyRate", rate);
        return (int)cmd.ExecuteScalar()!;
    }

    public static List<ProductInfo> GetProducts()
    {
        const string sql = """
            SELECT ProductId, ProductName, Category, UnitPrice, IsActive
            FROM Products WHERE IsActive = 1 ORDER BY Category, ProductName
            """;

        var list = new List<ProductInfo>();
        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(sql, con);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            list.Add(new ProductInfo
            {
                ProductId = r.GetInt32(0),
                ProductName = r.GetString(1),
                Category = r.IsDBNull(2) ? null : r.GetString(2),
                UnitPrice = r.GetDecimal(3),
                IsActive = r.GetBoolean(4)
            });
        }
        return list;
    }

    public static void AddOrderItem(int playerSessionId, int productId, int quantity)
    {
        using var con = DatabaseHelper.OpenConnection();
        using var priceCmd = new SqlCommand(
            "SELECT UnitPrice FROM Products WHERE ProductId = @ProductId", con);
        priceCmd.Parameters.AddWithValue("@ProductId", productId);
        var unitPrice = (decimal)priceCmd.ExecuteScalar()!;

        using var cmd = new SqlCommand(
            """
            INSERT INTO OrderItems (PlayerSessionId, ProductId, Quantity, UnitPrice, AddedAt)
            VALUES (@SessionId, @ProductId, @Qty, @UnitPrice, SYSDATETIME())
            """, con);
        cmd.Parameters.AddWithValue("@SessionId", playerSessionId);
        cmd.Parameters.AddWithValue("@ProductId", productId);
        cmd.Parameters.AddWithValue("@Qty", quantity);
        cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
        cmd.ExecuteNonQuery();
    }

    public static List<OrderItemInfo> GetOrderItems(int playerSessionId)
    {
        const string sql = """
            SELECT oi.OrderItemId, oi.ProductId, p.ProductName, oi.Quantity, oi.UnitPrice, oi.AddedAt
            FROM OrderItems oi
            INNER JOIN Products p ON oi.ProductId = p.ProductId
            WHERE oi.PlayerSessionId = @SessionId
            ORDER BY oi.AddedAt
            """;

        var list = new List<OrderItemInfo>();
        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@SessionId", playerSessionId);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            list.Add(new OrderItemInfo
            {
                OrderItemId = r.GetInt32(0),
                ProductId = r.GetInt32(1),
                ProductName = r.GetString(2),
                Quantity = r.GetInt32(3),
                UnitPrice = r.GetDecimal(4),
                AddedAt = r.GetDateTime(5)
            });
        }
        return list;
    }

    public static decimal GetOrdersTotal(int playerSessionId)
    {
        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(
            """
            SELECT ISNULL(SUM(Quantity * UnitPrice), 0)
            FROM OrderItems WHERE PlayerSessionId = @SessionId
            """, con);
        cmd.Parameters.AddWithValue("@SessionId", playerSessionId);
        return (decimal)cmd.ExecuteScalar()!;
    }

    public static InvoiceDraft BuildInvoiceDraft(int playerSessionId)
    {
        var session = GetPlayerSession(playerSessionId)
            ?? throw new InvalidOperationException("جلسة اللاعب غير موجودة.");

        if (session.IsInvoiced)
            throw new InvalidOperationException("تم إصدار فاتورة لهذا الزبون مسبقاً.");

        var endTime = DateTime.Now;
        var elapsed = endTime - session.StartTime;
        if (elapsed < TimeSpan.Zero)
            elapsed = TimeSpan.Zero;

        var (playMinutes, playHours, playAmount) = CalculatePlayCharge(elapsed, session.FirstHourRate, session.AdditionalHourRate);
        var orders = GetOrderItems(playerSessionId);
        var ordersAmount = Math.Round(orders.Sum(o => o.LineTotal), 2);

        var lines = new List<InvoiceLineDraft>
        {
            new()
            {
                LineType = "Time",
                Description = $"وقت اللعب ({playMinutes} دقيقة)",
                Quantity = playHours,
                UnitPrice = session.HourlyRate,
                LineTotal = playAmount
            }
        };

        foreach (var g in orders.GroupBy(o => new { o.ProductId, o.ProductName, o.UnitPrice }))
        {
            var qty = g.Sum(x => x.Quantity);
            lines.Add(new InvoiceLineDraft
            {
                LineType = "Product",
                Description = g.Key.ProductName,
                Quantity = qty,
                UnitPrice = g.Key.UnitPrice,
                LineTotal = qty * g.Key.UnitPrice
            });
        }

        return new InvoiceDraft
        {
            PlayerSessionId = playerSessionId,
            PlayerLabel = session.DisplayLabel,
            TableName = session.TableDisplayName,
            StartTime = session.StartTime,
            EndTime = endTime,
            PlayMinutes = playMinutes,
            PlayHours = playHours,
            HourlyRate = session.HourlyRate,
            FirstHourRate = session.FirstHourRate,
            AdditionalHourRate = session.AdditionalHourRate,
            PlayAmount = playAmount,
            OrdersAmount = ordersAmount,
            Lines = lines
        };
    }

    public static int SaveInvoice(InvoiceDraft draft)
    {
        using var con = DatabaseHelper.OpenConnection();
        using var tx = con.BeginTransaction();

        try
        {
            using (var endCmd = new SqlCommand(
                """
                UPDATE PlayerSessions
                SET EndTime = @EndTime, IsActive = 0, IsInvoiced = 1
                WHERE PlayerSessionId = @Id AND IsInvoiced = 0
                """, con, tx))
            {
                endCmd.Parameters.AddWithValue("@EndTime", draft.EndTime);
                endCmd.Parameters.AddWithValue("@Id", draft.PlayerSessionId);
                if (endCmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException("تعذر إغلاق الجلسة.");
            }

            int invoiceId;
            using (var invCmd = new SqlCommand(
                """
                INSERT INTO Invoices (PlayerSessionId, PlayMinutes, PlayAmount, OrdersAmount, TotalAmount, CreatedAt)
                OUTPUT INSERTED.InvoiceId
                VALUES (@SessionId, @PlayMin, @PlayAmt, @OrdAmt, @Total, SYSDATETIME())
                """, con, tx))
            {
                invCmd.Parameters.AddWithValue("@SessionId", draft.PlayerSessionId);
                invCmd.Parameters.AddWithValue("@PlayMin", draft.PlayMinutes);
                invCmd.Parameters.AddWithValue("@PlayAmt", draft.PlayAmount);
                invCmd.Parameters.AddWithValue("@OrdAmt", draft.OrdersAmount);
                invCmd.Parameters.AddWithValue("@Total", draft.TotalAmount);
                invoiceId = (int)invCmd.ExecuteScalar()!;
            }

            foreach (var line in draft.Lines)
            {
                using var lineCmd = new SqlCommand(
                    """
                    INSERT INTO InvoiceLines (InvoiceId, LineType, Description, Quantity, UnitPrice, LineTotal)
                    VALUES (@InvoiceId, @LineType, @Desc, @Qty, @UnitPrice, @LineTotal)
                    """, con, tx);
                lineCmd.Parameters.AddWithValue("@InvoiceId", invoiceId);
                lineCmd.Parameters.AddWithValue("@LineType", line.LineType);
                lineCmd.Parameters.AddWithValue("@Desc", line.Description);
                lineCmd.Parameters.AddWithValue("@Qty", line.Quantity);
                lineCmd.Parameters.AddWithValue("@UnitPrice", line.UnitPrice);
                lineCmd.Parameters.AddWithValue("@LineTotal", line.LineTotal);
                lineCmd.ExecuteNonQuery();
            }

            tx.Commit();
            return invoiceId;
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public static void MarkInvoicePrinted(int invoiceId)
    {
        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(
            "UPDATE Invoices SET PrintedAt = SYSDATETIME() WHERE InvoiceId = @Id", con);
        cmd.Parameters.AddWithValue("@Id", invoiceId);
        cmd.ExecuteNonQuery();
    }

    public static List<InvoiceSummary> SearchInvoices(DateTime from, DateTime to)
    {
        const string sql = """
            SELECT i.InvoiceId, i.PlayerSessionId, ps.PlayerName, bt.DisplayName,
                   i.PlayMinutes, i.PlayAmount, i.OrdersAmount, i.TotalAmount,
                   i.CreatedAt, i.PrintedAt
            FROM Invoices i
            INNER JOIN PlayerSessions ps ON i.PlayerSessionId = ps.PlayerSessionId
            INNER JOIN BilliardTables bt ON ps.TableId = bt.TableId
            WHERE i.CreatedAt >= @From AND i.CreatedAt < @ToEnd
            ORDER BY i.CreatedAt DESC
            """;

        var list = new List<InvoiceSummary>();
        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@From", from.Date);
        cmd.Parameters.AddWithValue("@ToEnd", to.Date.AddDays(1));
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            list.Add(new InvoiceSummary
            {
                InvoiceId = r.GetInt32(0),
                PlayerSessionId = r.GetInt32(1),
                PlayerName = r.IsDBNull(2) ? null : r.GetString(2),
                TableName = r.GetString(3),
                PlayMinutes = r.GetInt32(4),
                PlayAmount = r.GetDecimal(5),
                OrdersAmount = r.GetDecimal(6),
                TotalAmount = r.GetDecimal(7),
                CreatedAt = r.GetDateTime(8),
                PrintedAt = r.IsDBNull(9) ? null : r.GetDateTime(9)
            });
        }
        return list;
    }

    public static InvoiceDraft LoadInvoiceForReprint(int invoiceId)
    {
        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(
            """
            SELECT i.PlayerSessionId, ps.PlayerName, bt.DisplayName,
                   i.PlayMinutes, i.PlayAmount, i.OrdersAmount,
                   ps.StartTime, ISNULL(ps.EndTime, i.CreatedAt)
            FROM Invoices i
            INNER JOIN PlayerSessions ps ON i.PlayerSessionId = ps.PlayerSessionId
            INNER JOIN BilliardTables bt ON ps.TableId = bt.TableId
            WHERE i.InvoiceId = @Id
            """, con);
        cmd.Parameters.AddWithValue("@Id", invoiceId);
        using var r = cmd.ExecuteReader();
        if (!r.Read())
            throw new InvalidOperationException("الفاتورة غير موجودة.");

        var sessionId = r.GetInt32(0);
        var draft = new InvoiceDraft
        {
            PlayerSessionId = sessionId,
            PlayerLabel = r.IsDBNull(1) ? $"زبون #{sessionId}" : r.GetString(1)!,
            TableName = r.GetString(2),
            PlayMinutes = r.GetInt32(3),
            PlayAmount = r.GetDecimal(4),
            OrdersAmount = r.GetDecimal(5),
            StartTime = r.GetDateTime(6),
            EndTime = r.GetDateTime(7)
        };
        r.Close();

        var lines = new List<InvoiceLineDraft>();
        using var lineCmd = new SqlCommand(
            """
            SELECT LineType, Description, Quantity, UnitPrice, LineTotal
            FROM InvoiceLines WHERE InvoiceId = @Id ORDER BY InvoiceLineId
            """, con);
        lineCmd.Parameters.AddWithValue("@Id", invoiceId);
        using var lr = lineCmd.ExecuteReader();
        while (lr.Read())
        {
            lines.Add(new InvoiceLineDraft
            {
                LineType = lr.GetString(0),
                Description = lr.GetString(1),
                Quantity = lr.GetDecimal(2),
                UnitPrice = lr.GetDecimal(3),
                LineTotal = lr.GetDecimal(4)
            });
        }
        draft.Lines.AddRange(lines);
        return draft;
    }

    private static List<PlayerSessionInfo> ReadPlayerSessions(string sql, params SqlParameter[] parameters)
    {
        var list = new List<PlayerSessionInfo>();
        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(sql, con);
        cmd.Parameters.AddRange(parameters);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            list.Add(new PlayerSessionInfo
            {
                PlayerSessionId = r.GetInt32(0),
                TableId = r.GetInt32(1),
                PlayerName = r.IsDBNull(2) ? null : r.GetString(2),
                StartTime = r.GetDateTime(3),
                EndTime = r.IsDBNull(4) ? null : r.GetDateTime(4),
                HourlyRate = r.GetDecimal(5),
                IsActive = r.GetBoolean(6),
                IsInvoiced = r.GetBoolean(7),
                TableDisplayName = r.GetString(8),
                FirstHourRate = r.GetDecimal(9),
                AdditionalHourRate = r.GetDecimal(10)
            });
        }
        return list;
    }

    /// <summary>
    /// حساب رسوم اللعب مع نموذج التسعير الديناميكي:
    /// الساعة الأولى: FirstHourRate
    /// الساعات الإضافية: AdditionalHourRate
    /// </summary>
    public static (int PlayMinutes, decimal PlayHours, decimal PlayAmount) CalculatePlayCharge(
        TimeSpan elapsed, decimal firstHourRate, decimal additionalHourRate)
    {
        if (elapsed < TimeSpan.Zero)
            elapsed = TimeSpan.Zero;

        var playMinutes = Math.Max(1, (int)Math.Ceiling(elapsed.TotalMinutes));
        var playHours = Math.Round((decimal)elapsed.TotalHours, 4);
        if (playHours <= 0)
            playHours = playMinutes / 60m;

        decimal playAmount;
        if (playHours <= 1)
        {
            playAmount = Math.Round(playHours * firstHourRate, 2);
        }
        else
        {
            var firstHourCharge = firstHourRate;
            var additionalHours = playHours - 1;
            var additionalCharge = Math.Round(additionalHours * additionalHourRate, 2);
            playAmount = Math.Round(firstHourCharge + additionalCharge, 2);
        }

        return (playMinutes, playHours, playAmount);
    }

    public static void DeleteOrderItem(int orderItemId, int playerSessionId)
    {
        using var con = DatabaseHelper.OpenConnection();
        using var check = new SqlCommand(
            """
            SELECT IsInvoiced FROM PlayerSessions
            WHERE PlayerSessionId = @SessionId AND IsActive = 1
            """, con);
        check.Parameters.AddWithValue("@SessionId", playerSessionId);
        var invoiced = check.ExecuteScalar();
        if (invoiced is null)
            throw new InvalidOperationException("جلسة اللاعب غير موجودة أو منتهية.");
        if ((bool)invoiced)
            throw new InvalidOperationException("لا يمكن حذف طلبات بعد إصدار الفاتورة.");

        using var cmd = new SqlCommand(
            """
            DELETE FROM OrderItems
            WHERE OrderItemId = @OrderItemId AND PlayerSessionId = @SessionId
            """, con);
        cmd.Parameters.AddWithValue("@OrderItemId", orderItemId);
        cmd.Parameters.AddWithValue("@SessionId", playerSessionId);
        if (cmd.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("تعذر حذف الطلب.");
    }

    public static List<TableTypeInfo> GetTableTypes()
    {
        const string sql = """
            SELECT TableTypeId, TypeName, HourlyRate, FirstHourRate, AdditionalHourRate FROM TableTypes ORDER BY TableTypeId
            """;

        var list = new List<TableTypeInfo>();
        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(sql, con);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            var typeName = r.GetString(1);
            list.Add(new TableTypeInfo
            {
                TableTypeId = r.GetInt32(0),
                TypeName = typeName,
                DisplayNameAr = typeName.Equals("Snooker", StringComparison.OrdinalIgnoreCase) ? "سنوكر" : "بلاك",
                HourlyRate = r.GetDecimal(2),
                FirstHourRate = r.GetDecimal(3),
                AdditionalHourRate = r.GetDecimal(4)
            });
        }
        return list;
    }

    public static void UpdateHourlyRate(int tableTypeId, decimal hourlyRate)
    {
        if (hourlyRate <= 0)
            throw new ArgumentException("سعر الساعة يجب أن يكون أكبر من صفر.");

        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(
            "UPDATE TableTypes SET HourlyRate = @Rate WHERE TableTypeId = @Id", con);
        cmd.Parameters.AddWithValue("@Rate", hourlyRate);
        cmd.Parameters.AddWithValue("@Id", tableTypeId);
        if (cmd.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("نوع الطاولة غير موجود.");
    }

    public static void UpdatePricingRates(int tableTypeId, decimal firstHourRate, decimal additionalHourRate)
    {
        if (firstHourRate <= 0 || additionalHourRate <= 0)
            throw new ArgumentException("الأسعار يجب أن تكون أكبر من صفر.");

        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(
            """
            UPDATE TableTypes 
            SET FirstHourRate = @FirstHour, AdditionalHourRate = @AddHour, HourlyRate = @FirstHour
            WHERE TableTypeId = @Id
            """, con);
        cmd.Parameters.AddWithValue("@FirstHour", firstHourRate);
        cmd.Parameters.AddWithValue("@AddHour", additionalHourRate);
        cmd.Parameters.AddWithValue("@Id", tableTypeId);
        if (cmd.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("نوع الطاولة غير موجود.");
    }

    public static List<ProductInfo> GetAllProducts()
    {
        const string sql = """
            SELECT ProductId, ProductName, Category, UnitPrice, IsActive
            FROM Products ORDER BY IsActive DESC, Category, ProductName
            """;

        var list = new List<ProductInfo>();
        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(sql, con);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            list.Add(new ProductInfo
            {
                ProductId = r.GetInt32(0),
                ProductName = r.GetString(1),
                Category = r.IsDBNull(2) ? null : r.GetString(2),
                UnitPrice = r.GetDecimal(3),
                IsActive = r.GetBoolean(4)
            });
        }
        return list;
    }

    public static int AddProduct(string name, string? category, decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("اسم المنتج مطلوب.");
        if (unitPrice < 0)
            throw new ArgumentException("السعر لا يمكن أن يكون سالباً.");

        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(
            """
            INSERT INTO Products (ProductName, Category, UnitPrice, IsActive)
            OUTPUT INSERTED.ProductId
            VALUES (@Name, @Category, @Price, 1)
            """, con);
        cmd.Parameters.AddWithValue("@Name", name.Trim());
        cmd.Parameters.AddWithValue("@Category", (object?)category?.Trim() ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Price", unitPrice);
        return (int)cmd.ExecuteScalar()!;
    }

    public static void UpdateProduct(int productId, string name, string? category, decimal unitPrice, bool isActive)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("اسم المنتج مطلوب.");

        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(
            """
            UPDATE Products
            SET ProductName = @Name, Category = @Category, UnitPrice = @Price, IsActive = @Active
            WHERE ProductId = @Id
            """, con);
        cmd.Parameters.AddWithValue("@Name", name.Trim());
        cmd.Parameters.AddWithValue("@Category", (object?)category?.Trim() ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Price", unitPrice);
        cmd.Parameters.AddWithValue("@Active", isActive);
        cmd.Parameters.AddWithValue("@Id", productId);
        if (cmd.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("المنتج غير موجود.");
    }

    public static void DeleteProduct(int productId)
    {
        using var con = DatabaseHelper.OpenConnection();
        using var cmd = new SqlCommand(
            "DELETE FROM Products WHERE ProductId = @Id", con);
        cmd.Parameters.AddWithValue("@Id", productId);
        if (cmd.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("المنتج غير موجود.");
    }

    public static void ClearAllProducts()
    {
        using var con = DatabaseHelper.OpenConnection();
        using var tx = con.BeginTransaction();

        try
        {
            using (var cmd = new SqlCommand("DELETE FROM OrderItems", con, tx))
                cmd.ExecuteNonQuery();

            using (var cmd = new SqlCommand("DELETE FROM Products", con, tx))
                cmd.ExecuteNonQuery();

            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw new InvalidOperationException("تعذر تفريغ قائمة المنتجات. تأكد من عدم وجود فواتير معلقة.");
        }
    }

    public static TableInvoiceDraft BuildTableInvoiceDraft(int tableId)
    {
        var table = GetTable(tableId) ?? throw new InvalidOperationException("الطاولة غير موجودة.");
        var activePlayers = GetActivePlayers(tableId);

        if (activePlayers.Count == 0)
            throw new InvalidOperationException("لا توجد لاعبين نشطين على هذه الطاولة.");

        var endTime = DateTime.Now;
        var tableStartTime = activePlayers.Min(p => p.StartTime);
        
        // حساب وقت اللعب للطاولة ككل
        var tableDuration = endTime - tableStartTime;
        if (tableDuration < TimeSpan.Zero)
            tableDuration = TimeSpan.Zero;

        var firstPlayer = activePlayers.First();
        var (tablePlayMinutes, tablePlayHours, tablePlayAmount) = CalculatePlayCharge(
            tableDuration, firstPlayer.FirstHourRate, firstPlayer.AdditionalHourRate);

        // توزيع قيمة اللعب الكلية على اللاعبين بالتساوي لتسجيلها في قاعدة البيانات بشكل سليم
        decimal playAmountPerPlayer = Math.Round(tablePlayAmount / activePlayers.Count, 2);
        decimal playAmountSum = 0;

        var playerDetails = new List<PlayerInvoiceDetail>();
        decimal totalAmount = 0;

        for (int i = 0; i < activePlayers.Count; i++)
        {
            var player = activePlayers[i];
            var playerEndTime = endTime;
            var elapsed = playerEndTime - player.StartTime;
            if (elapsed < TimeSpan.Zero)
                elapsed = TimeSpan.Zero;

            // تحديد حصة اللاعب الحالي
            decimal playAmountShare = (i == activePlayers.Count - 1)
                ? (tablePlayAmount - playAmountSum)
                : playAmountPerPlayer;
            playAmountSum += playAmountShare;

            var (playMinutes, playHours, _) = CalculatePlayCharge(elapsed, player.FirstHourRate, player.AdditionalHourRate);
            var orders = GetOrderItems(player.PlayerSessionId);
            var ordersAmount = Math.Round(orders.Sum(o => o.LineTotal), 2);
            var playerTotal = playAmountShare + ordersAmount;

            playerDetails.Add(new PlayerInvoiceDetail
            {
                PlayerSessionId = player.PlayerSessionId,
                PlayerLabel = player.DisplayLabel,
                PlayerStartTime = player.StartTime,
                PlayerEndTime = playerEndTime,
                PlayMinutes = playMinutes,
                PlayHours = playHours,
                HourlyRate = player.HourlyRate,
                FirstHourRate = player.FirstHourRate,
                AdditionalHourRate = player.AdditionalHourRate,
                PlayAmount = playAmountShare,
                Orders = orders,
                OrdersAmount = ordersAmount
            });

            totalAmount += playerTotal;
        }

        return new TableInvoiceDraft
        {
            TableId = tableId,
            TableName = table.DisplayName,
            StartTime = tableStartTime,
            EndTime = endTime,
            PlayMinutes = tablePlayMinutes,
            PlayHours = tablePlayHours,
            HourlyRate = firstPlayer.HourlyRate,
            FirstHourRate = firstPlayer.FirstHourRate,
            AdditionalHourRate = firstPlayer.AdditionalHourRate,
            PlayAmount = tablePlayAmount,
            TotalAmount = Math.Round(totalAmount, 2),
            Players = playerDetails
        };
    }

    public static void SaveTableInvoice(TableInvoiceDraft draft)
    {
        using var con = DatabaseHelper.OpenConnection();
        using var tx = con.BeginTransaction();

        try
        {
            foreach (var playerDetail in draft.Players)
            {
                using (var endCmd = new SqlCommand(
                    """
                    UPDATE PlayerSessions
                    SET EndTime = @EndTime, IsActive = 0, IsInvoiced = 1
                    WHERE PlayerSessionId = @Id AND IsInvoiced = 0
                    """, con, tx))
                {
                    endCmd.Parameters.AddWithValue("@EndTime", draft.EndTime);
                    endCmd.Parameters.AddWithValue("@Id", playerDetail.PlayerSessionId);
                    if (endCmd.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException($"تعذر إغلاق جلسة {playerDetail.PlayerLabel}.");
                }

                using (var invCmd = new SqlCommand(
                    """
                    INSERT INTO Invoices (PlayerSessionId, PlayMinutes, PlayAmount, OrdersAmount, TotalAmount, CreatedAt)
                    OUTPUT INSERTED.InvoiceId
                    VALUES (@SessionId, @PlayMin, @PlayAmt, @OrdAmt, @Total, SYSDATETIME())
                    """, con, tx))
                {
                    invCmd.Parameters.AddWithValue("@SessionId", playerDetail.PlayerSessionId);
                    invCmd.Parameters.AddWithValue("@PlayMin", playerDetail.PlayMinutes);
                    invCmd.Parameters.AddWithValue("@PlayAmt", playerDetail.PlayAmount);
                    invCmd.Parameters.AddWithValue("@OrdAmt", playerDetail.OrdersAmount);
                    invCmd.Parameters.AddWithValue("@Total", playerDetail.PlayerTotal);
                    var invoiceId = (int)invCmd.ExecuteScalar()!;

                    using (var timeCmd = new SqlCommand(
                        """
                        INSERT INTO InvoiceLines (InvoiceId, LineType, Description, Quantity, UnitPrice, LineTotal)
                        VALUES (@InvoiceId, @LineType, @Desc, @Qty, @UnitPrice, @LineTotal)
                        """, con, tx))
                    {
                        timeCmd.Parameters.AddWithValue("@InvoiceId", invoiceId);
                        timeCmd.Parameters.AddWithValue("@LineType", "Time");
                        timeCmd.Parameters.AddWithValue("@Desc", $"وقت اللعب ({playerDetail.PlayMinutes} دقيقة)");
                        timeCmd.Parameters.AddWithValue("@Qty", playerDetail.PlayHours);
                        timeCmd.Parameters.AddWithValue("@UnitPrice", playerDetail.HourlyRate);
                        timeCmd.Parameters.AddWithValue("@LineTotal", playerDetail.PlayAmount);
                        timeCmd.ExecuteNonQuery();
                    }

                    foreach (var g in playerDetail.Orders.GroupBy(o => new { o.ProductId, o.ProductName, o.UnitPrice }))
                    {
                        var qty = g.Sum(x => x.Quantity);
                        using (var prodCmd = new SqlCommand(
                            """
                            INSERT INTO InvoiceLines (InvoiceId, LineType, Description, Quantity, UnitPrice, LineTotal)
                            VALUES (@InvoiceId, @LineType, @Desc, @Qty, @UnitPrice, @LineTotal)
                            """, con, tx))
                        {
                            prodCmd.Parameters.AddWithValue("@InvoiceId", invoiceId);
                            prodCmd.Parameters.AddWithValue("@LineType", "Product");
                            prodCmd.Parameters.AddWithValue("@Desc", g.Key.ProductName);
                            prodCmd.Parameters.AddWithValue("@Qty", qty);
                            prodCmd.Parameters.AddWithValue("@UnitPrice", g.Key.UnitPrice);
                            prodCmd.Parameters.AddWithValue("@LineTotal", qty * g.Key.UnitPrice);
                            prodCmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }
}
