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
        lblName = new Label();
        txtName = new TextBox();
        lblCategory = new Label();
        txtCategory = new TextBox();
        lblPrice = new Label();
        numPrice = new NumericUpDown();
        btnAdd = new Button();
        btnSaveEdit = new Button();
        btnDelete = new Button();
        btnClose = new Button();
        btnClearAll = new Button();
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
        grpAdd.Controls.Add(btnAdd);
        grpAdd.Controls.Add(btnSaveEdit);
        grpAdd.Controls.Add(btnDelete);
        grpAdd.Location = new Point(12, 262);
        grpAdd.Name = "grpAdd";
        grpAdd.Size = new Size(560, 110);
        grpAdd.TabIndex = 1;
        grpAdd.TabStop = false;
        grpAdd.Text = "منتج جديد / تعديل";
        // 
        // lblName
        // 
        lblName.AutoSize = true;
        lblName.Location = new Point(517, 25);
        lblName.Name = "lblName";
        lblName.Size = new Size(37, 15);
        lblName.TabIndex = 0;
        lblName.Text = "الاسم:";
        // 
        // txtName
        // 
        txtName.Location = new Point(421, 22);
        txtName.Name = "txtName";
        txtName.Size = new Size(90, 23);
        txtName.TabIndex = 1;
        // 
        // lblCategory
        // 
        lblCategory.AutoSize = true;
        lblCategory.Location = new Point(360, 25);
        lblCategory.Name = "lblCategory";
        lblCategory.Size = new Size(53, 15);
        lblCategory.TabIndex = 2;
        lblCategory.Text = "التصنيف:";
        // 
        // txtCategory
        // 
        txtCategory.Location = new Point(244, 22);
        txtCategory.Name = "txtCategory";
        txtCategory.Size = new Size(110, 23);
        txtCategory.TabIndex = 3;
        // 
        // lblPrice
        // 
        lblPrice.AutoSize = true;
        lblPrice.Location = new Point(170, 25);
        lblPrice.Name = "lblPrice";
        lblPrice.Size = new Size(39, 15);
        lblPrice.TabIndex = 4;
        lblPrice.Text = "السعر:";
        lblPrice.Click += lblPrice_Click;
        // 
        // numPrice
        // 
        numPrice.DecimalPlaces = 2;
        numPrice.Location = new Point(63, 22);
        numPrice.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
        numPrice.Name = "numPrice";
        numPrice.Size = new Size(90, 23);
        numPrice.TabIndex = 5;
        // 
        // btnAdd
        // 
        btnAdd.BackColor = Color.FromArgb(46, 125, 50);
        btnAdd.ForeColor = Color.White;
        btnAdd.Location = new Point(360, 58);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(90, 32);
        btnAdd.TabIndex = 6;
        btnAdd.Text = "إضافة";
        btnAdd.UseVisualStyleBackColor = false;
        btnAdd.Click += BtnAdd_Click;
        // 
        // btnSaveEdit
        // 
        btnSaveEdit.BackColor = Color.FromArgb(25, 118, 210);
        btnSaveEdit.ForeColor = Color.White;
        btnSaveEdit.Location = new Point(260, 58);
        btnSaveEdit.Name = "btnSaveEdit";
        btnSaveEdit.Size = new Size(90, 32);
        btnSaveEdit.TabIndex = 7;
        btnSaveEdit.Text = "حفظ التعديل";
        btnSaveEdit.UseVisualStyleBackColor = false;
        btnSaveEdit.Click += BtnSaveEdit_Click;
        // 
        // btnDelete
        // 
        btnDelete.BackColor = Color.FromArgb(211, 47, 47);
        btnDelete.ForeColor = Color.White;
        btnDelete.Location = new Point(140, 58);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(110, 32);
        btnDelete.TabIndex = 8;
        btnDelete.Text = "حذف المنتج";
        btnDelete.UseVisualStyleBackColor = false;
        btnDelete.Click += BtnDelete_Click;
        // 
        // btnClose
        // 
        btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnClose.Location = new Point(12, 395);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(90, 32);
        btnClose.TabIndex = 0;
        btnClose.Text = "إغلاق";
        btnClose.Click += btnClose_Click;
        // 
        // btnClearAll
        // 
        btnClearAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnClearAll.BackColor = Color.FromArgb(229, 57, 53);
        btnClearAll.ForeColor = Color.White;
        btnClearAll.Location = new Point(112, 395);
        btnClearAll.Name = "btnClearAll";
        btnClearAll.Size = new Size(150, 32);
        btnClearAll.TabIndex = 1;
        btnClearAll.Text = "تفريغ كل المنتجات";
        btnClearAll.UseVisualStyleBackColor = false;
        btnClearAll.Click += BtnClearAll_Click;
        // 
        // ProductsManagementForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(584, 439);
        Controls.Add(btnClose);
        Controls.Add(btnClearAll);
        Controls.Add(grpAdd);
        Controls.Add(dgvProducts);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ProductsManagementForm";
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
    private Button btnDelete;
    private Button btnClose;
    private Button btnClearAll;
}
