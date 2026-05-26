namespace AlJamal.Forms;

partial class InvoiceArchiveForm
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
        lblFrom = new Label();
        dtpFrom = new DateTimePicker();
        lblTo = new Label();
        dtpTo = new DateTimePicker();
        btnSearch = new Button();
        dgvInvoices = new DataGridView();
        btnReprint = new Button();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvInvoices).BeginInit();
        SuspendLayout();
        // 
        // lblFrom
        // 
        lblFrom.AutoSize = true;
        lblFrom.Location = new Point(12, 18);
        lblFrom.Name = "lblFrom";
        lblFrom.Size = new Size(24, 15);
        lblFrom.TabIndex = 0;
        lblFrom.Text = "من:";
        // 
        // dtpFrom
        // 
        dtpFrom.Format = DateTimePickerFormat.Short;
        dtpFrom.Location = new Point(50, 14);
        dtpFrom.Name = "dtpFrom";
        dtpFrom.Size = new Size(120, 23);
        dtpFrom.TabIndex = 1;
        // 
        // lblTo
        // 
        lblTo.AutoSize = true;
        lblTo.Location = new Point(190, 18);
        lblTo.Name = "lblTo";
        lblTo.Size = new Size(26, 15);
        lblTo.TabIndex = 2;
        lblTo.Text = "إلى:";
        // 
        // dtpTo
        // 
        dtpTo.Format = DateTimePickerFormat.Short;
        dtpTo.Location = new Point(228, 14);
        dtpTo.Name = "dtpTo";
        dtpTo.Size = new Size(120, 23);
        dtpTo.TabIndex = 3;
        // 
        // btnSearch
        // 
        btnSearch.Location = new Point(365, 13);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(75, 25);
        btnSearch.TabIndex = 4;
        btnSearch.Text = "بحث";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += BtnSearch_Click;
        // 
        // dgvInvoices
        // 
        dgvInvoices.AllowUserToAddRows = false;
        dgvInvoices.AllowUserToDeleteRows = false;
        dgvInvoices.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvInvoices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvInvoices.Location = new Point(12, 50);
        dgvInvoices.MultiSelect = false;
        dgvInvoices.Name = "dgvInvoices";
        dgvInvoices.ReadOnly = true;
        dgvInvoices.RowHeadersVisible = false;
        dgvInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvInvoices.Size = new Size(760, 380);
        dgvInvoices.TabIndex = 5;
        // 
        // btnReprint
        // 
        btnReprint.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnReprint.Location = new Point(572, 442);
        btnReprint.Name = "btnReprint";
        btnReprint.Size = new Size(100, 32);
        btnReprint.TabIndex = 6;
        btnReprint.Text = "إعادة طباعة";
        btnReprint.UseVisualStyleBackColor = true;
        btnReprint.Click += BtnReprint_Click;
        // 
        // btnClose
        // 
        btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnClose.Location = new Point(682, 442);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(90, 32);
        btnClose.TabIndex = 7;
        btnClose.Text = "إغلاق";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += BtnClose_Click;
        // 
        // InvoiceArchiveForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(784, 486);
        Controls.Add(btnClose);
        Controls.Add(btnReprint);
        Controls.Add(dgvInvoices);
        Controls.Add(btnSearch);
        Controls.Add(dtpTo);
        Controls.Add(lblTo);
        Controls.Add(dtpFrom);
        Controls.Add(lblFrom);
        Name = "InvoiceArchiveForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterParent;
        Text = "أرشيف الفواتير";
        Load += InvoiceArchiveForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvInvoices).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblFrom;
    private DateTimePicker dtpFrom;
    private Label lblTo;
    private DateTimePicker dtpTo;
    private Button btnSearch;
    private DataGridView dgvInvoices;
    private Button btnReprint;
    private Button btnClose;
}
