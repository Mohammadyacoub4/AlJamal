using AlJamal.Models;

namespace AlJamal.Services;

internal static class InvoicePrintHelper
{
    public static string FormatReceipt(InvoiceDraft draft, int? invoiceId = null)
    {
        var lines = new List<string>
        {
            "══════════════════════════",
            $"      {AppSettings.ShopName} — بلياردو",
            "══════════════════════════",
            invoiceId.HasValue ? $"فاتورة رقم: {invoiceId}" : "فاتورة",
            $"التاريخ: {DateTime.Now:yyyy-MM-dd HH:mm}",
            "──────────────────────────",
            $"الطاولة: {draft.TableName}",
            $"الزبون: {draft.PlayerLabel}",
            $"من: {draft.StartTime:HH:mm}  إلى: {draft.EndTime:HH:mm}",
            "──────────────────────────"
        };

        var timeLines = draft.Lines.Where(l => l.LineType == "Time").ToList();
        var productLines = draft.Lines.Where(l => l.LineType == "Product").ToList();

        if (timeLines.Count > 0)
        {
            lines.Add("─── وقت اللعب ───");
            foreach (var line in timeLines)
            {
                lines.Add(line.Description);
                lines.Add($"  {line.Quantity:N2} ساعة × {line.UnitPrice:N2} د.أ/ساعة = {line.LineTotal:N2} د.أ");
            }
        }

        if (productLines.Count > 0)
        {
            lines.Add("─── الطلبات ───");
            foreach (var line in productLines)
            {
                lines.Add(line.Description);
                lines.Add($"  {line.Quantity:N0} × {line.UnitPrice:N2} د.أ = {line.LineTotal:N2} د.أ");
            }
        }

        lines.Add("──────────────────────────");
        lines.Add($"وقت اللعب:     {draft.PlayAmount,8:N2} د.أ");
        lines.Add($"الطلبات:       {draft.OrdersAmount,8:N2} د.أ");
        lines.Add($"المجموع:       {draft.TotalAmount,8:N2} د.أ");
        lines.Add("══════════════════════════");
        lines.Add("        شكراً لزيارتكم");
        return string.Join(Environment.NewLine, lines);
    }
}
