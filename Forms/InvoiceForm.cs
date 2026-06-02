using System.Drawing;
using System.Drawing.Printing;
using AlJamal.Database;
using AlJamal.Models;
using AlJamal.Services;

namespace AlJamal.Forms;

public partial class InvoiceForm : Form
{
    private readonly InvoiceDraft _draft;
    private readonly bool _isNew;
    private int? _invoiceId;
    private string _receiptText = "";

    public InvoiceForm(InvoiceDraft draft, bool isNew, int? existingInvoiceId = null)
    {
        _draft = draft;
        _isNew = isNew;
        _invoiceId = existingInvoiceId;
        InitializeComponent();

        if (!isNew)
        {
            btnSave.Visible = false;
            btnCancel.Text = "إغلاق";
            Text = "إعادة طباعة فاتورة";
        }
    }

    private void InvoiceForm_Load(object sender, EventArgs e)
    {
        _receiptText = InvoicePrintHelper.FormatReceipt(_draft, _invoiceId);
        txtReceipt.Text = _receiptText;
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        if (_isNew && !_invoiceId.HasValue)
        {
            MessageBox.Show("احفظ الفاتورة أولاً قبل الطباعة.", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        PrintReceipt();

        if (_invoiceId.HasValue)
            BilliardRepository.MarkInvoicePrinted(_invoiceId.Value);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!_isNew)
            return;

        try
        {
            _invoiceId = BilliardRepository.SaveInvoice(_draft);

            _receiptText = InvoicePrintHelper.FormatReceipt(_draft, _invoiceId);
            txtReceipt.Text = _receiptText;

            var print = MessageBox.Show(
                "تم حفظ الفاتورة. هل تريد الطباعة الآن؟",
                "نجاح",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (print == DialogResult.Yes)
            {
                PrintReceipt();
                BilliardRepository.MarkInvoicePrinted(_invoiceId.Value);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = _isNew ? DialogResult.Cancel : DialogResult.OK;
        Close();
    }

    // =========================
    // 🖨️ PRINT (تلقائي على الطابعة الافتراضية)
    // =========================
    private void PrintReceipt()
    {
        using var doc = new PrintDocument();

        doc.DocumentName = "Invoice";

        // ✅ يختار الطابعة الافتراضية تلقائياً (المشبّكة غالباً)
        doc.PrinterSettings = new PrinterSettings();

        // 🔥 إعداد ورق حراري (80mm)
        doc.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);
        doc.DefaultPageSettings.PaperSize = new PaperSize("Receipt", 315, 1000);

        var lines = _receiptText.Split(Environment.NewLine);
        int lineIndex = 0;

        doc.PrintPage += (sender, ev) =>
        {
            Font font = new Font("Consolas", 9);
            float y = ev.MarginBounds.Top;
            float lineHeight = font.GetHeight(ev.Graphics) + 2;

            float x = ev.MarginBounds.Left;

            while (lineIndex < lines.Length && y + lineHeight < ev.MarginBounds.Bottom)
            {
                ev.Graphics.DrawString(lines[lineIndex], font, Brushes.Black, x, y);
                y += lineHeight;
                lineIndex++;
            }

            ev.HasMorePages = lineIndex < lines.Length;
        };

        // 🚀 طباعة مباشرة
        doc.Print();
    }
}