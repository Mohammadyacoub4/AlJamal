using AlJamal.Database;
using AlJamal.Models;

namespace AlJamal.Forms;

public partial class TableDetailsForm : Form
{
    private readonly int _tableId;
    private List<PlayerSessionInfo> _players = [];

    public TableDetailsForm(int tableId)
    {
        _tableId = tableId;
        InitializeComponent();
    }

    private void TableDetailsForm_Load(object? sender, EventArgs e)
    {
        var table = BilliardRepository.GetTable(_tableId);
        lblTitle.Text = table != null
            ? $"{table.DisplayName} — {table.TypeName} — {table.HourlyRate:N2} د.أ/ساعة"
            : "طاولة";
        RefreshPlayers();
        tmrTick.Start();
    }

    private void TableDetailsForm_FormClosed(object? sender, FormClosedEventArgs e)
    {
        tmrTick.Stop();
    }

    private void TmrTick_Tick(object? sender, EventArgs e) => UpdateElapsedColumn();

    private void BtnAddPlayer_Click(object? sender, EventArgs e)
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

    private void BtnOrders_Click(object? sender, EventArgs e)
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

    private void BtnFinish_Click(object? sender, EventArgs e)
    {
        var session = GetSelectedSession();
        if (session == null)
        {
            MessageBox.Show("اختر لاعباً لإنهاء جلسته وإصدار الفاتورة.", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var draft = BilliardRepository.BuildInvoiceDraft(session.PlayerSessionId);
            using var invForm = new InvoiceForm(draft, isNew: true);
            if (invForm.ShowDialog(this) == DialogResult.OK)
                RefreshPlayers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnClose_Click(object? sender, EventArgs e) => Close();

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
        foreach (var p in _players)
        {
            var item = new ListViewItem(p.DisplayLabel) { Tag = p.PlayerSessionId };
            item.SubItems.Add(p.StartTime.ToString("HH:mm"));
            item.SubItems.Add(p.ElapsedText);
            lstPlayers.Items.Add(item);
        }
        if (lstPlayers.Items.Count > 0 && lstPlayers.SelectedIndices.Count == 0)
            lstPlayers.Items[0].Selected = true;
    }

    private void UpdateElapsedColumn()
    {
        _players = BilliardRepository.GetActivePlayers(_tableId);
        for (var i = 0; i < lstPlayers.Items.Count && i < _players.Count; i++)
            lstPlayers.Items[i].SubItems[2].Text = _players[i].ElapsedText;
    }
}
