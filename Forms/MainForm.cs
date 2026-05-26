using AlJamal.Database;
using AlJamal.Models;

namespace AlJamal.Forms;

public partial class MainForm : Form
{
    private static readonly Color FreeSnooker = Color.FromArgb(56, 142, 60);
    private static readonly Color BusySnooker = Color.FromArgb(183, 28, 28);
    private static readonly Color FreeBlack = Color.FromArgb(30, 136, 229);
    private static readonly Color BusyBlack = Color.FromArgb(198, 40, 40);

    private const int SnookerColumns = 4;
    private const int BlackColumns = 3;
    private const int ButtonRowHeight = 96;

    public MainForm()
    {
        InitializeComponent();
        lblTitle.Text = AppSettings.AppTitle;
        Text = AppSettings.AppTitle;
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        LoadTables();
        tmrRefresh.Start();
    }

    private void TmrRefresh_Tick(object? sender, EventArgs e) => LoadTables();

    private void MnuRefresh_Click(object? sender, EventArgs e) => LoadTables();

    private void MnuArchive_Click(object? sender, EventArgs e)
    {
        using var f = new InvoiceArchiveForm();
        f.ShowDialog(this);
    }

    private void MnuHourlyRates_Click(object? sender, EventArgs e)
    {
        using var f = new HourlyRatesForm();
        if (f.ShowDialog(this) == DialogResult.OK)
            LoadTables();
    }

    private void MnuProducts_Click(object? sender, EventArgs e)
    {
        using var f = new ProductsManagementForm();
        f.ShowDialog(this);
    }

    private void LoadTables()
    {
        try
        {
            var tables = BilliardRepository.GetTables();
            var snooker = tables.Where(t => t.TableTypeId == 1).ToList();
            var black = tables.Where(t => t.TableTypeId != 1).ToList();

            PopulateTablePanel(tlpSnooker, snooker, SnookerColumns);
            PopulateTablePanel(tlpBlack, black, BlackColumns);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"تعذر الاتصال بقاعدة البيانات.\n\n{ex.Message}\n\n" +
                "أعد تشغيل البرنامج (سيتم إصلاح المخطط تلقائياً).",
                "خطأ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void PopulateTablePanel(TableLayoutPanel panel, List<BilliardTableInfo> tables, int columns)
    {
        panel.SuspendLayout();
        panel.Controls.Clear();
        panel.ColumnStyles.Clear();
        panel.RowStyles.Clear();

        if (tables.Count == 0)
        {
            panel.ResumeLayout();
            return;
        }

        var rows = (int)Math.Ceiling(tables.Count / (double)columns);
        panel.ColumnCount = columns;
        panel.RowCount = rows;

        var colPercent = 100f / columns;
        for (var c = 0; c < columns; c++)
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colPercent));

        for (var r = 0; r < rows; r++)
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, ButtonRowHeight));

        panel.Height = rows * ButtonRowHeight + panel.Padding.Vertical + 4;

        for (var i = 0; i < tables.Count; i++)
        {
            var btn = CreateTableButton(tables[i]);
            btn.Dock = DockStyle.Fill;
            btn.Margin = new Padding(5);
            panel.Controls.Add(btn, i % columns, i / columns);
        }

        panel.ResumeLayout(true);
    }

    private Button CreateTableButton(BilliardTableInfo table)
    {
        var isSnooker = table.TableTypeId == 1;
        var busy = table.IsBusy;
        var freeColor = isSnooker ? FreeSnooker : FreeBlack;
        var busyColor = isSnooker ? BusySnooker : BusyBlack;

        var status = busy ? $"{table.ActivePlayers} لاعب" : "فاضية";
        var btn = new Button
        {
            Tag = table.TableId,
            Text = $"{table.DisplayName}\n{table.HourlyRate:N0} د.أ/س\n{status}",
            BackColor = busy ? busyColor : freeColor,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Cursor = Cursors.Hand,
            TextAlign = ContentAlignment.MiddleCenter,
            UseCompatibleTextRendering = true,
            MinimumSize = new Size(80, 80)
        };
        btn.FlatAppearance.BorderSize = 0;
        btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(busy ? busyColor : freeColor, 0.12f);
        btn.Click += TableButton_Click;
        return btn;
    }

    private void TableButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not int tableId)
            return;

        using var f = new TableDetailsForm(tableId);
        f.ShowDialog(this);
        LoadTables();
    }
}
