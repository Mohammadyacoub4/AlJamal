using AlJamal.Models;

namespace AlJamal.Services;

internal static class TableInvoicePrintHelper
{
    public static string FormatReceipt(TableInvoiceDraft draft)
    {
        var lines = new List<string>
        {
            "══════════════════════════════════════",
            $"      {AppSettings.ShopName} — بلياردو",
            "══════════════════════════════════════",
            "فاتورة موحدة للطاولة",
            $"التاريخ: {DateTime.Now:yyyy-MM-dd HH:mm}",
            "──────────────────────────────────────"
        };

        lines.Add($"الطاولة: {draft.TableName}");
        lines.Add($"من: {draft.StartTime:HH:mm}  إلى: {draft.EndTime:HH:mm}");
        lines.Add("══════════════════════════════════════");

        foreach (var player in draft.Players)
        {
            lines.Add($"");
            lines.Add($"👤 الزبون: {player.PlayerLabel}");
            lines.Add($"   الوقت: {player.PlayerStartTime:HH:mm} - {player.PlayerEndTime:HH:mm}");
            lines.Add("──────────────────────────────────────");

            lines.Add("   وقت اللعب:");
            lines.Add($"      {player.PlayHours:N2} ساعة × {player.HourlyRate:N2} د.أ/ساعة = {player.PlayAmount:N2} د.أ");

            if (player.Orders.Count > 0)
            {
                lines.Add("   الطلبات:");
                foreach (var g in player.Orders.GroupBy(o => new { o.ProductId, o.ProductName, o.UnitPrice }))
                {
                    var qty = g.Sum(x => x.Quantity);
                    lines.Add($"      {g.Key.ProductName}");
                    lines.Add($"      {qty} × {g.Key.UnitPrice:N2} د.أ = {qty * g.Key.UnitPrice:N2} د.أ");
                }
            }

            lines.Add($"   المجموع للزبون: {player.PlayerTotal:N2} د.أ");
            lines.Add("──────────────────────────────────────");
        }

        lines.Add("");
        lines.Add("══════════════════════════════════════");
        lines.Add($"المجموع الكلي:  {draft.TotalAmount:N2} د.أ".PadLeft(38));
        lines.Add("══════════════════════════════════════");
        lines.Add("        شكراً لزيارتكم");
        return string.Join(Environment.NewLine, lines);
    }
}
