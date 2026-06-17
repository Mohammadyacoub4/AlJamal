using AlJamal.Models;
using AlJamal.Services;

namespace AlJamal.Forms;

public partial class TableInvoiceForm : Form
{
    private readonly TableInvoiceDraft _draft;
    private string _receiptText = "";

    public TableInvoiceForm(TableInvoiceDraft draft)
    {
        _draft = draft;
        InitializeComponent();
    }

    private void TableInvoiceForm_Load(object sender, EventArgs e)
    {
        _receiptText = TableInvoicePrintHelper.FormatReceipt(_draft);
        txtReceipt.Text = _receiptText;
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            ReceiptPrinterService.PrintReceiptAndOpenDrawer(_receiptText);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"تعذرت الطباعة.\n\n{ex.Message}", "خطأ",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }
}
