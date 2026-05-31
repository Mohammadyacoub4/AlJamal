using AlJamal.Database;
using AlJamal.Models;

namespace AlJamal.Forms;

public partial class ProductsManagementForm : Form
{
    private List<ProductInfo> _products = [];
    private int? _editingProductId;

    public ProductsManagementForm()
    {
        InitializeComponent();
    }

    private void ProductsManagementForm_Load(object? sender, EventArgs e)
    {
        SetupGrid();
        LoadProducts();
    }

    private void SetupGrid()
    {
        dgvProducts.AutoGenerateColumns = false;
        dgvProducts.Columns.Clear();
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        { Name = "Id", DataPropertyName = "ProductId", Visible = false });
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        { HeaderText = "المنتج", DataPropertyName = "ProductName", FillWeight = 150 });
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        { HeaderText = "التصنيف", DataPropertyName = "Category", FillWeight = 100 });
        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        { HeaderText = "السعر", DataPropertyName = "PriceDisplay", FillWeight = 80 });
    }

    private void LoadProducts()
    {
        _products = BilliardRepository.GetAllProducts();
        dgvProducts.DataSource = _products.Select(p => new
        {
            p.ProductId,
            p.ProductName,
            Category = p.Category ?? "—",
            PriceDisplay = $"{p.UnitPrice:N2} د.أ"
        }).ToList();
        ClearEdit();
    }

    private void DgvProducts_SelectionChanged(object? sender, EventArgs e)
    {
        var idx = dgvProducts.CurrentRow?.Index ?? -1;
        if (idx < 0 || idx >= _products.Count)
        {
            ClearEdit();
            return;
        }

        var p = _products[idx];
        _editingProductId = p.ProductId;
        txtName.Text = p.ProductName;
        txtCategory.Text = p.Category ?? "";
        numPrice.Value = Math.Min(numPrice.Maximum, Math.Max(numPrice.Minimum, p.UnitPrice));
        grpAdd.Text = "تعديل منتج";
    }

    private void ClearEdit()
    {
        _editingProductId = null;
        txtName.Clear();
        txtCategory.Clear();
        numPrice.Value = 1;
        grpAdd.Text = "منتج جديد";
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        try
        {
            BilliardRepository.AddProduct(txtName.Text, txtCategory.Text, numPrice.Value);
            LoadProducts();
            MessageBox.Show("تمت إضافة المنتج.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnSaveEdit_Click(object? sender, EventArgs e)
    {
        if (!_editingProductId.HasValue)
        {
            MessageBox.Show("اختر منتجاً من القائمة للتعديل.", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            BilliardRepository.UpdateProduct(
                _editingProductId.Value,
                txtName.Text,
                txtCategory.Text,
                numPrice.Value,
                isActive: true);
            LoadProducts();
            MessageBox.Show("تم حفظ التعديل.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (!_editingProductId.HasValue)
        {
            MessageBox.Show("اختر منتجاً من القائمة للحذف.", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show(
            $"هل تريد حقاً حذف المنتج '{txtName.Text}'؟ هذا لا يمكن التراجع عنه.",
            "تأكيد الحذف",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (result != DialogResult.Yes)
            return;

        try
        {
            BilliardRepository.DeleteProduct(_editingProductId.Value);
            LoadProducts();
            MessageBox.Show("تم حذف المنتج بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"لم يتمكن من حذف المنتج: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnClearAll_Click(object? sender, EventArgs e)
    {
        var result = MessageBox.Show(
            "هل تريد حذف جميع المنتجات؟ هذا لا يمكن التراجع عنه!",
            "تفريغ كل المنتجات",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (result != DialogResult.Yes)
            return;

        try
        {
            BilliardRepository.ClearAllProducts();
            LoadProducts();
            MessageBox.Show("تم تفريغ قائمة المنتجات بنجاح. يمكنك الآن إضافة منتجات جديدة.", "نجاح",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"لم يتمكن من تفريغ المنتجات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lblPrice_Click(object sender, EventArgs e)
    {

    }
}
