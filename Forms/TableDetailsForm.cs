using AlJamal.Database;
using AlJamal.Models;
using AlJamal.Services;
using System.Drawing.Printing;

namespace AlJamal.Forms;

public partial class TableDetailsForm : Form
{
    private readonly int _tableId;
    private List<PlayerSessionInfo> _players = [];
    private DateTime _tableStartTime = DateTime.Now;

    public TableDetailsForm(int tableId)
    {
        _tableId = tableId;
        InitializeComponent();
    }

    private void TableDetailsForm_Load(object sender, EventArgs e)
    {
        var table = BilliardRepository.GetTable(_tableId);
        lblTitle.Text = table != null
            ? $"{table.DisplayName} — {table.TypeName} — {table.HourlyRate:N2} د.أ/ساعة"
            : "طاولة";
        RefreshPlayers();
        tmrTick.Start();
    }

    private void TableDetailsForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        tmrTick.Stop();
    }

    private void TmrTick_Tick(object sender, EventArgs e) => UpdateElapsedColumn();

    private void BtnAddPlayer_Click(object sender, EventArgs e)
    {
        var name = InputPrompt.Show(this, "اسم اللاعب (اختياري):", "إضافة لاعب");
        if (name == null)
            return;

        try
        {
            BilliardRepository.AddPlayer(_tableId, string.IsNullOrWhiteSpace(name) ? null : name.Trim());
            RefreshPlayers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnOrders_Click(object sender, EventArgs e)
    {
        var session = GetSelectedSession();
        if (session == null)
        {
            MessageBox.Show("اختر لاعباً من القائمة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var f = new PlayerOrdersForm(session.PlayerSessionId, session.DisplayLabel);
        f.ShowDialog(this);
    }

    private void BtnFinish_Click(object sender, EventArgs e)
    {
        if (_players.Count == 0)
        {
            MessageBox.Show("لا توجد لاعبين على الطاولة.", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show(
            $"هل تريد إنهاء الجلسة للاعبين {_players.Count} على الطاولة وطباعة فاتورة موحدة؟",
            "تأكيد الإنهاء",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result != DialogResult.Yes)
            return;

        try
        {
            var draft = BilliardRepository.BuildTableInvoiceDraft(_tableId);
            BilliardRepository.SaveTableInvoice(draft);

            var receiptText = TableInvoicePrintHelper.FormatReceipt(draft);
            PrintReceipt(receiptText);
            RefreshPlayers();
            MessageBox.Show("تم إنهاء جلسة الطاولة بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PrintReceipt(string receiptText)
    {
        using var doc = new PrintDocument();
        doc.DocumentName = $"فاتورة {AppSettings.ShopName}";
        var lines = receiptText.Split(Environment.NewLine);
        var lineIndex = 0;

        doc.PrintPage += (_, ev) =>
        {
            var font = new Font("Arial", 9);
            float y = ev.MarginBounds.Top;
            var lineHeight = font.GetHeight(ev.Graphics!) + 2;

            while (lineIndex < lines.Length && y + lineHeight < ev.MarginBounds.Bottom)
            {
                ev.Graphics!.DrawString(lines[lineIndex], font, Brushes.Black, ev.MarginBounds.Left, y);
                y += lineHeight;
                lineIndex++;
            }

            ev.HasMorePages = lineIndex < lines.Length;
        };

        using var preview = new PrintPreviewDialog
        {
            Document = doc,
            Width = 500,
            Height = 700
        };
        preview.ShowDialog(this);
    }

    private void BtnClose_Click(object sender, EventArgs e) => Close();

    private PlayerSessionInfo? GetSelectedSession()
    {
        if (lstPlayers.SelectedItems.Count == 0)
            return null;
        if (lstPlayers.SelectedItems[0].Tag is not int id)
            return null;
        return _players.FirstOrDefault(p => p.PlayerSessionId == id);
    }

    private void RefreshPlayers()
    {
        _players = BilliardRepository.GetActivePlayers(_tableId);
        lstPlayers.Items.Clear();

        if (_players.Count > 0)
            _tableStartTime = _players.Min(p => p.StartTime);

        var tableElapsed = GetTableElapsedText();
        foreach (var p in _players)
        {
            var item = new ListViewItem(p.DisplayLabel) { Tag = p.PlayerSessionId };
            item.SubItems.Add(p.StartTime.ToString("HH:mm"));
            item.SubItems.Add(tableElapsed);
            lstPlayers.Items.Add(item);
        }
        if (lstPlayers.Items.Count > 0 && lstPlayers.SelectedIndices.Count == 0)
            lstPlayers.Items[0].Selected = true;
    }

    private void UpdateElapsedColumn()
    {
        _players = BilliardRepository.GetActivePlayers(_tableId);
        var tableElapsed = GetTableElapsedText();
        for (var i = 0; i < lstPlayers.Items.Count && i < _players.Count; i++)
            lstPlayers.Items[i].SubItems[2].Text = tableElapsed;
    }

    private string GetTableElapsedText()
    {
        var elapsed = DateTime.Now - _tableStartTime;
        if (elapsed < TimeSpan.Zero)
            elapsed = TimeSpan.Zero;
        return $"{(int)elapsed.TotalHours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";
    }
}
