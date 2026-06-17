namespace AlJamal.Forms;

partial class InvoiceForm
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
        btnSave = new Button();
        btnCancel = new Button();
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
        btnPrint.Location = new Point(12, 405);
        btnPrint.Name = "btnPrint";
        btnPrint.Size = new Size(100, 32);
        btnPrint.TabIndex = 1;
        btnPrint.Text = "طباعة";
        btnPrint.UseVisualStyleBackColor = true;
        btnPrint.Click += BtnPrint_Click;
        // 
        // btnSave
        // 
        btnSave.BackColor = Color.FromArgb(76, 175, 80);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(136, 405);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(120, 32);
        btnSave.TabIndex = 2;
        btnSave.Text = "حفظ وإغلاق";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;
        // 
        // btnCancel
        // 
        btnCancel.Location = new Point(272, 405);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(100, 32);
        btnCancel.TabIndex = 3;
        btnCancel.Text = "إلغاء";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += BtnCancel_Click;
        // 
        // InvoiceForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(384, 449);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(btnPrint);
        Controls.Add(txtReceipt);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "InvoiceForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterParent;
        Text = "الفاتورة";
        Load += InvoiceForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    private TextBox txtReceipt;
    private Button btnPrint;
    private Button btnSave;
    private Button btnCancel;
}
