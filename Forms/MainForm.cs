using AlJamal.Controls;
using AlJamal.Database;
using AlJamal.Models;

namespace AlJamal.Forms;

public partial class MainForm : Form
{
    private static readonly Color FreeSnooker = Color.FromArgb(45, 106, 79);
    private static readonly Color BusySnooker = Color.FromArgb(193, 18, 31);
    private static readonly Color FreeBlack = Color.FromArgb(29, 78, 137);
    private static readonly Color BusyBlack = Color.FromArgb(214, 40, 57);

    private const int SnookerColumns = 4;
    private const int BlackColumns = 3;
    private const int CardRowHeight = 124;
    private const string LogoFileName = "photo_2026-05-31_17-03-52.ico";

    public MainForm()
    {
        InitializeComponent();
        lblTitle.Text = AppSettings.ShopName;
        lblSubtitle.Text = "إدارة الطاولات واللاعبين";
        Text = AppSettings.AppTitle;
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        LoadLogo();
        panelHeader.Paint += PanelHeader_Paint;
        LoadTables();
        tmrRefresh.Start();
    }

    private void LoadLogo()
    {
        var candidates = new[]
        {
            Path.Combine(AppContext.BaseDirectory, LogoFileName),
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", LogoFileName))
        };

        foreach (var path in candidates)
        {
            if (!File.Exists(path))
                continue;

            try
            {
                using var icon = new Icon(path, 72, 72);
                picLogo.Image = icon.ToBitmap();
                return;
            }
            catch
            {
                // try next path
            }
        }

        picLogo.Visible = false;
    }

    private void PanelHeader_Paint(object? sender, PaintEventArgs e)
    {
        var rect = panelHeader.ClientRectangle;
        if (rect.Width <= 0 || rect.Height <= 0)
            return;

        using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
            rect,
            Color.FromArgb(255, 27, 38, 59),
            Color.FromArgb(255, 13, 27, 42),
            System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
        e.Graphics.FillRectangle(brush, rect);

        using var accentPen = new Pen(Color.FromArgb(120, 212, 175, 55), 3);
        e.Graphics.DrawLine(accentPen, 0, rect.Bottom - 1, rect.Width, rect.Bottom - 1);
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
            var snooker = tables.Where(t => t.TableTypeId == 1 || t.TableTypeId == 3).ToList();
            var black = tables.Where(t => t.TableTypeId == 2).ToList();

            lblSnookerCount.Text = snooker.Count.ToString();
            lblBlackCount.Text = black.Count.ToString();

            PopulateTablePanel(tlpSnooker, snooker, SnookerColumns, FreeSnooker, BusySnooker);
            PopulateTablePanel(tlpBlack, black, BlackColumns, FreeBlack, BusyBlack);

            ResizeSectionPanel(panelSnookerSection, tlpSnooker, snookerHeader);
            ResizeSectionPanel(panelBlackSection, tlpBlack, blackHeader);
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

    private void PopulateTablePanel(
        TableLayoutPanel panel,
        List<BilliardTableInfo> tables,
        int columns,
        Color freeColor,
        Color busyColor)
    {
        panel.SuspendLayout();
        panel.Controls.Clear();
        panel.ColumnStyles.Clear();
        panel.RowStyles.Clear();

        if (tables.Count == 0)
        {
            panel.Height = 0;
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
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, CardRowHeight));

        panel.Height = rows * CardRowHeight + panel.Padding.Vertical + 8;

        for (var i = 0; i < tables.Count; i++)
        {
            var card = CreateTableCard(tables[i], freeColor, busyColor);
            card.Dock = DockStyle.Fill;
            panel.Controls.Add(card, i % columns, i / columns);
        }

        panel.ResumeLayout(true);
    }

    private static void ResizeSectionPanel(Panel section, TableLayoutPanel tablePanel, Panel header)
    {
        section.Height = section.Padding.Vertical + header.Height + tablePanel.Height + 8;
    }

    private TableCardPanel CreateTableCard(BilliardTableInfo table, Color freeColor, Color busyColor)
    {
        var busy = table.IsBusy;
        var status = busy ? $"{table.ActivePlayers} لاعب" : "فاضية";

        var card = new TableCardPanel(freeColor, busyColor)
        {
            TableId = table.TableId,
            TableName = table.DisplayName,
            RateText = $"{table.HourlyRate:N0} د.أ / ساعة",
            StatusText = status,
            IsBusy = busy
        };

        card.CardClicked += (_, _) => OpenTableDetails(card.TableId);
        return card;
    }

    private void OpenTableDetails(int tableId)
    {
        using var f = new TableDetailsForm(tableId);
        f.ShowDialog(this);
        LoadTables();
    }
}
