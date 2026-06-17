namespace AlJamal.Forms;

partial class TableInvoiceForm
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
        txtReceipt = new TextBox();
        btnPrint = new Button();
        btnClose = new Button();
        SuspendLayout();
        // 
        // txtReceipt
        // 
        txtReceipt.Font = new Font("Consolas", 10F);
        txtReceipt.Location = new Point(12, 12);
        txtReceipt.Multiline = true;
        txtReceipt.Name = "txtReceipt";
        txtReceipt.ReadOnly = true;
        txtReceipt.ScrollBars = ScrollBars.Vertical;
        txtReceipt.Size = new Size(360, 380);
        txtReceipt.TabIndex = 0;
        // 
        // btnPrint
        // 
        btnPrint.BackColor = Color.FromArgb(33, 150, 243);
        btnPrint.ForeColor = Color.White;
        btnPrint.Location = new Point(12, 405);
        btnPrint.Name = "btnPrint";
        btnPrint.Size = new Size(120, 32);
        btnPrint.TabIndex = 1;
        btnPrint.Text = "طباعة";
        btnPrint.UseVisualStyleBackColor = false;
        btnPrint.Click += BtnPrint_Click;
        // 
        // btnClose
        // 
        btnClose.Location = new Point(272, 405);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(100, 32);
        btnClose.TabIndex = 2;
        btnClose.Text = "إغلاق";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += BtnClose_Click;
        // 
        // TableInvoiceForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(384, 449);
        Controls.Add(btnClose);
        Controls.Add(btnPrint);
        Controls.Add(txtReceipt);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "TableInvoiceForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterParent;
        Text = "فاتورة الطاولة";
        Load += TableInvoiceForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    private TextBox txtReceipt;
    private Button btnPrint;
    private Button btnClose;
}
