namespace AlJamal.Forms;

partial class ProductsManagementForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        dgvProducts = new DataGridView();
        grpAdd = new GroupBox();
        txtName = new TextBox();
        txtCategory = new TextBox();
        numPrice = new NumericUpDown();
        btnAdd = new Button();
        lblName = new Label();
        lblCategory = new Label();
        lblPrice = new Label();
        btnSaveEdit = new Button();
        btnClose = new Button();
        chkActive = new CheckBox();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
        grpAdd.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
        SuspendLayout();
        // 
        // dgvProducts
        // 
        dgvProducts.AllowUserToAddRows = false;
        dgvProducts.AllowUserToDeleteRows = false;
        dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvProducts.Location = new Point(12, 12);
        dgvProducts.MultiSelect = false;
        dgvProducts.Name = "dgvProducts";
        dgvProducts.ReadOnly = true;
        dgvProducts.RowHeadersVisible = false;
        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.Size = new Size(560, 240);
        dgvProducts.TabIndex = 0;
        dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;
        // 
        // grpAdd
        // 
        grpAdd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpAdd.Controls.Add(lblName);
        grpAdd.Controls.Add(txtName);
        grpAdd.Controls.Add(lblCategory);
        grpAdd.Controls.Add(txtCategory);
        grpAdd.Controls.Add(lblPrice);
        grpAdd.Controls.Add(numPrice);
        grpAdd.Controls.Add(chkActive);
        grpAdd.Controls.Add(btnAdd);
        grpAdd.Controls.Add(btnSaveEdit);
        grpAdd.Location = new Point(12, 262);
        grpAdd.Name = "grpAdd";
        grpAdd.Size = new Size(560, 120);
        grpAdd.TabIndex = 1;
        grpAdd.Text = "منتج جديد / تعديل";
        // 
        // lblName
        // 
        lblName.AutoSize = true;
        lblName.Location = new Point(480, 28);
        lblName.Text = "الاسم:";
        // 
        // txtName
        // 
        txtName.Location = new Point(280, 25);
        txtName.Size = new Size(190, 23);
        // 
        // lblCategory
        // 
        lblCategory.AutoSize = true;
        lblCategory.Location = new Point(220, 28);
        lblCategory.Text = "التصنيف:";
        // 
        // txtCategory
        // 
        txtCategory.Location = new Point(80, 25);
        txtCategory.Size = new Size(130, 23);
        // 
        // lblPrice
        // 
        lblPrice.AutoSize = true;
        lblPrice.Location = new Point(480, 65);
        lblPrice.Text = "السعر:";
        // 
        // numPrice
        // 
        numPrice.DecimalPlaces = 2;
        numPrice.Location = new Point(360, 63);
        numPrice.Maximum = 9999;
        numPrice.Size = new Size(110, 23);
        // 
        // chkActive
        // 
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Location = new Point(80, 65);
        chkActive.Text = "نشط (يظهر في الطلبات)";
        // 
        // btnAdd
        // 
        btnAdd.BackColor = Color.FromArgb(46, 125, 50);
        btnAdd.ForeColor = Color.White;
        btnAdd.Location = new Point(240, 58);
        btnAdd.Size = new Size(90, 30);
        btnAdd.Text = "إضافة";
        btnAdd.UseVisualStyleBackColor = false;
        btnAdd.Click += BtnAdd_Click;
        // 
        // btnSaveEdit
        // 
        btnSaveEdit.BackColor = Color.FromArgb(25, 118, 210);
        btnSaveEdit.ForeColor = Color.White;
        btnSaveEdit.Location = new Point(130, 58);
        btnSaveEdit.Size = new Size(100, 30);
        btnSaveEdit.Text = "حفظ التعديل";
        btnSaveEdit.UseVisualStyleBackColor = false;
        btnSaveEdit.Click += BtnSaveEdit_Click;
        // 
        // btnClose
        // 
        btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnClose.Location = new Point(12, 395);
        btnClose.Size = new Size(90, 32);
        btnClose.Text = "إغلاق";
        btnClose.Click += (_, _) => Close();
        // 
        // ProductsManagementForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(584, 439);
        Controls.Add(btnClose);
        Controls.Add(grpAdd);
        Controls.Add(dgvProducts);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterParent;
        Text = "إدارة المنتجات";
        Load += ProductsManagementForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
        grpAdd.ResumeLayout(false);
        grpAdd.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
        ResumeLayout(false);
    }

    private DataGridView dgvProducts;
    private GroupBox grpAdd;
    private TextBox txtName;
    private TextBox txtCategory;
    private NumericUpDown numPrice;
    private Button btnAdd;
    private Label lblName;
    private Label lblCategory;
    private Label lblPrice;
    private Button btnSaveEdit;
    private Button btnClose;
    private CheckBox chkActive;
}
