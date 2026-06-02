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

        // حساب وقت اللعب للطاولة ككل (يُطبع مرة واحدة)
        lines.Add("⏰ أجرة وقت الطاولة:");
        lines.Add($"   مدة اللعب: {draft.PlayHours:N2} ساعة");
        lines.Add($"   سعر الساعة: {draft.HourlyRate:N2} د.أ/ساعة");
        lines.Add($"   حساب اللعب: {draft.PlayAmount:N2} د.أ");
        lines.Add("══════════════════════════════════════");

        // تفاصيل الزبائن والطلبات
        lines.Add("👤 تفاصيل الزبائن والطلبات:");
        foreach (var player in draft.Players)
        {
            lines.Add($"");
            lines.Add($"👤 الزبون: {player.PlayerLabel}");
            lines.Add($"   وقت الدخول: {player.PlayerStartTime:HH:mm}");

            if (player.Orders.Count > 0)
            {
                lines.Add("   الطلبات:");
                foreach (var g in player.Orders.GroupBy(o => new { o.ProductId, o.ProductName, o.UnitPrice }))
                {
                    var qty = g.Sum(x => x.Quantity);
                    lines.Add($"      {g.Key.ProductName}");
                    lines.Add($"      {qty} × {g.Key.UnitPrice:N2} د.أ = {qty * g.Key.UnitPrice:N2} د.أ");
                }
                lines.Add($"   مجموع الطلبات للزبون: {player.OrdersAmount:N2} د.أ");
            }
            else
            {
                lines.Add("   الطلبات: لا يوجد طلبات");
            }
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
