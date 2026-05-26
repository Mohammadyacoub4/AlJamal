using AlJamal.Database;
using AlJamal.Models;

namespace AlJamal.Forms;

public partial class PlayerOrdersForm : Form
{
    private readonly int _sessionId;
    private List<ProductInfo> _products = [];

    public PlayerOrdersForm(int sessionId, string playerLabel)
    {
        _sessionId = sessionId;
        InitializeComponent();
        lblPlayer.Text = $"طلبات: {playerLabel}";
    }

    private void PlayerOrdersForm_Load(object? sender, EventArgs e)
    {
        _products = BilliardRepository.GetProducts();
        BuildProductButtons();
        LoadOrders();
    }

    private void BuildProductButtons()
    {
        flpProducts.SuspendLayout();
        flpProducts.Controls.Clear();

        foreach (var product in _products)
        {
            var btn = new Button
            {
                Tag = product.ProductId,
                Text = $"{product.ProductName}\n{product.UnitPrice:N2} د.أ",
                Width = 100,
                Height = 52,
                Margin = new Padding(4),
                BackColor = Color.FromArgb(55, 71, 79),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += ProductButton_Click;
            flpProducts.Controls.Add(btn);
        }

        flpProducts.ResumeLayout();
    }

    private void ProductButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not int productId)
            return;

        try
        {
            BilliardRepository.AddOrderItem(_sessionId, productId, (int)numQty.Value);
            LoadOrders();
            numQty.Value = 1;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnClose_Click(object? sender, EventArgs e) => Close();

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (dgvOrders.CurrentRow?.Cells["colOrderItemId"].Value is not int orderItemId)
        {
            MessageBox.Show("اختر منتجاً من القائمة لحذفه.", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirm = MessageBox.Show("حذف هذا المنتج من الطلب؟", "تأكيد",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirm != DialogResult.Yes)
            return;

        try
        {
            BilliardRepository.DeleteOrderItem(orderItemId, _sessionId);
            LoadOrders();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadOrders()
    {
        var items = BilliardRepository.GetOrderItems(_sessionId);
        dgvOrders.Rows.Clear();
        foreach (var item in items)
        {
            dgvOrders.Rows.Add(
                item.ProductName,
                item.Quantity,
                item.UnitPrice.ToString("N2"),
                item.LineTotal.ToString("N2"),
                item.OrderItemId);
        }
        var total = Math.Round(items.Sum(i => i.LineTotal), 2);
        lblOrdersTotal.Text = $"مجموع الطلبات: {total:N2} د.أ";
    }
}
