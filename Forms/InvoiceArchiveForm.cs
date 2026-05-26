using AlJamal.Database;
using AlJamal.Models;

namespace AlJamal.Forms;

public partial class InvoiceArchiveForm : Form
{
    private List<InvoiceSummary> _invoices = [];

    public InvoiceArchiveForm()
    {
        InitializeComponent();
    }

    private void InvoiceArchiveForm_Load(object? sender, EventArgs e)
    {
        dtpFrom.Value = DateTime.Today;
        dtpTo.Value = DateTime.Today;
        SetupGrid();
        Search();
    }

    private void SetupGrid()
    {
        dgvInvoices.AutoGenerateColumns = false;
        dgvInvoices.Columns.Clear();
        dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "رقم", DataPropertyName = "InvoiceId", FillWeight = 50 });
        dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Table", HeaderText = "الطاولة", DataPropertyName = "TableName" });
        dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Player", HeaderText = "الزبون", DataPropertyName = "PlayerDisplay" });
        dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Minutes", HeaderText = "دقائق", DataPropertyName = "PlayMinutes", FillWeight = 50 });
        dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "المجموع", DataPropertyName = "TotalDisplay" });
        dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Created", HeaderText = "التاريخ", DataPropertyName = "CreatedDisplay" });
        dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn { Name = "Printed", HeaderText = "طبعت", DataPropertyName = "PrintedDisplay" });
    }

    private void BtnSearch_Click(object? sender, EventArgs e) => Search();

    private void Search()
    {
        try
        {
            _invoices = BilliardRepository.SearchInvoices(dtpFrom.Value, dtpTo.Value);
            var view = _invoices.Select(i => new
            {
                i.InvoiceId,
                i.TableName,
                PlayerDisplay = string.IsNullOrWhiteSpace(i.PlayerName) ? $"#{i.PlayerSessionId}" : i.PlayerName,
                i.PlayMinutes,
                TotalDisplay = $"{i.TotalAmount:N2} د.أ",
                CreatedDisplay = i.CreatedAt.ToString("yyyy-MM-dd HH:mm"),
                PrintedDisplay = i.PrintedAt?.ToString("yyyy-MM-dd HH:mm") ?? "—"
            }).ToList();

            dgvInvoices.DataSource = view;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnReprint_Click(object? sender, EventArgs e)
    {
        var idx = dgvInvoices.CurrentRow?.Index ?? -1;
        if (idx < 0 || idx >= _invoices.Count)
        {
            MessageBox.Show("اختر فاتورة من القائمة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var invoiceId = _invoices[idx].InvoiceId;

        try
        {
            var draft = BilliardRepository.LoadInvoiceForReprint(invoiceId);
            using var f = new InvoiceForm(draft, isNew: false, existingInvoiceId: invoiceId);
            f.ShowDialog(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnClose_Click(object? sender, EventArgs e) => Close();
}
