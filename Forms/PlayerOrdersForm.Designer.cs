namespace AlJamal.Forms;

partial class PlayerOrdersForm
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
        lblPlayer = new Label();
        lblQty = new Label();
        numQty = new NumericUpDown();
        flpProducts = new FlowLayoutPanel();
        lblPickHint = new Label();
        dgvOrders = new DataGridView();
        colProduct = new DataGridViewTextBoxColumn();
        colQty = new DataGridViewTextBoxColumn();
        colPrice = new DataGridViewTextBoxColumn();
        colTotal = new DataGridViewTextBoxColumn();
        colOrderItemId = new DataGridViewTextBoxColumn();
        lblOrdersTotal = new Label();
        btnDelete = new Button();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)numQty).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
        SuspendLayout();
        // 
        // lblPlayer
        // 
        lblPlayer.AutoSize = true;
        lblPlayer.Font = new Font(lblPlayer.Font.FontFamily, 11F, FontStyle.Bold);
        lblPlayer.Location = new Point(12, 12);
        lblPlayer.Text = "طلبات";
        // 
        // lblPickHint
        // 
        lblPickHint.AutoSize = true;
        lblPickHint.ForeColor = Color.Gray;
        lblPickHint.Location = new Point(12, 38);
        lblPickHint.Text = "اضغط على المنتج لإضافته للطلب";
        // 
        // lblQty
        // 
        lblQty.AutoSize = true;
        lblQty.Location = new Point(12, 62);
        lblQty.Text = "الكمية:";
        // 
        // numQty
        // 
        numQty.Location = new Point(70, 60);
        numQty.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numQty.Size = new Size(55, 23);
        numQty.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // flpProducts
        // 
        flpProducts.AutoScroll = true;
        flpProducts.BorderStyle = BorderStyle.FixedSingle;
        flpProducts.Location = new Point(12, 92);
        flpProducts.Size = new Size(450, 130);
        flpProducts.WrapContents = true;
        // 
        // dgvOrders
        // 
        dgvOrders.AllowUserToAddRows = false;
        dgvOrders.AllowUserToDeleteRows = false;
        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvOrders.Columns.AddRange(new DataGridViewColumn[] { colProduct, colQty, colPrice, colTotal, colOrderItemId });
        dgvOrders.Location = new Point(12, 232);
        dgvOrders.MultiSelect = false;
        dgvOrders.ReadOnly = true;
        dgvOrders.RowHeadersVisible = false;
        dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvOrders.Size = new Size(450, 145);
        // 
        // colProduct
        // 
        colProduct.HeaderText = "المنتج";
        colProduct.Name = "colProduct";
        colProduct.ReadOnly = true;
        // 
        // colQty
        // 
        colQty.HeaderText = "الكمية";
        colQty.Name = "colQty";
        colQty.ReadOnly = true;
        // 
        // colPrice
        // 
        colPrice.HeaderText = "السعر";
        colPrice.Name = "colPrice";
        colPrice.ReadOnly = true;
        // 
        // colTotal
        // 
        colTotal.HeaderText = "المجموع";
        colTotal.Name = "colTotal";
        colTotal.ReadOnly = true;
        // 
        // colOrderItemId
        // 
        colOrderItemId.HeaderText = "Id";
        colOrderItemId.Name = "colOrderItemId";
        colOrderItemId.ReadOnly = true;
        colOrderItemId.Visible = false;
        // 
        // lblOrdersTotal
        // 
        lblOrdersTotal.AutoSize = true;
        lblOrdersTotal.Font = new Font(lblOrdersTotal.Font.FontFamily, 11F, FontStyle.Bold);
        lblOrdersTotal.Location = new Point(12, 418);
        // 
        // btnDelete
        // 
        btnDelete.BackColor = Color.FromArgb(198, 40, 40);
        btnDelete.ForeColor = Color.White;
        btnDelete.Location = new Point(12, 385);
        btnDelete.Size = new Size(110, 28);
        btnDelete.Text = "حذف المنتج";
        btnDelete.UseVisualStyleBackColor = false;
        btnDelete.Click += BtnDelete_Click;
        // 
        // btnClose
        // 
        btnClose.Location = new Point(362, 412);
        btnClose.Size = new Size(100, 30);
        btnClose.Text = "إغلاق";
        btnClose.Click += BtnClose_Click;
        // 
        // PlayerOrdersForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(474, 456);
        Controls.Add(btnDelete);
        Controls.Add(btnClose);
        Controls.Add(lblOrdersTotal);
        Controls.Add(dgvOrders);
        Controls.Add(flpProducts);
        Controls.Add(numQty);
        Controls.Add(lblQty);
        Controls.Add(lblPickHint);
        Controls.Add(lblPlayer);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterParent;
        Text = "طلبات الزبون";
        Load += PlayerOrdersForm_Load;
        ((System.ComponentModel.ISupportInitialize)numQty).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblPlayer;
    private Label lblPickHint;
    private Label lblQty;
    private NumericUpDown numQty;
    private FlowLayoutPanel flpProducts;
    private DataGridView dgvOrders;
    private DataGridViewTextBoxColumn colProduct;
    private DataGridViewTextBoxColumn colQty;
    private DataGridViewTextBoxColumn colPrice;
    private DataGridViewTextBoxColumn colTotal;
    private DataGridViewTextBoxColumn colOrderItemId;
    private Label lblOrdersTotal;
    private Button btnDelete;
    private Button btnClose;
}
