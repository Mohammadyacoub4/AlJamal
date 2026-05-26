namespace AlJamal.Forms;

partial class HourlyRatesForm
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
        lblSnooker = new Label();
        numSnooker = new NumericUpDown();
        lblBlack = new Label();
        numBlack = new NumericUpDown();
        btnSave = new Button();
        btnCancel = new Button();
        lblHint = new Label();
        ((System.ComponentModel.ISupportInitialize)numSnooker).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numBlack).BeginInit();
        SuspendLayout();
        // 
        // lblHint
        // 
        lblHint.ForeColor = Color.Gray;
        lblHint.Location = new Point(12, 12);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(360, 40);
        lblHint.Text = "السعر يُطبَّق على الجلسات الجديدة. الجلسات النشطة تحتفظ بالسعر وقت البدء.";
        // 
        // lblSnooker
        // 
        lblSnooker.AutoSize = true;
        lblSnooker.Font = new Font(lblSnooker.Font, FontStyle.Bold);
        lblSnooker.Location = new Point(12, 65);
        lblSnooker.Text = "سنوكر (د.أ / ساعة):";
        // 
        // numSnooker
        // 
        numSnooker.DecimalPlaces = 2;
        numSnooker.Location = new Point(200, 63);
        numSnooker.Maximum = 999;
        numSnooker.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
        numSnooker.Size = new Size(120, 23);
        numSnooker.TabIndex = 1;
        // 
        // lblBlack
        // 
        lblBlack.AutoSize = true;
        lblBlack.Font = new Font(lblBlack.Font, FontStyle.Bold);
        lblBlack.Location = new Point(12, 105);
        lblBlack.Text = "بلاك (د.أ / ساعة):";
        // 
        // numBlack
        // 
        numBlack.DecimalPlaces = 2;
        numBlack.Location = new Point(200, 103);
        numBlack.Maximum = 999;
        numBlack.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
        numBlack.Size = new Size(120, 23);
        numBlack.TabIndex = 2;
        // 
        // btnSave
        // 
        btnSave.BackColor = Color.FromArgb(46, 125, 50);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(120, 155);
        btnSave.Size = new Size(100, 34);
        btnSave.Text = "حفظ";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;
        // 
        // btnCancel
        // 
        btnCancel.Location = new Point(230, 155);
        btnCancel.Size = new Size(90, 34);
        btnCancel.Text = "إلغاء";
        btnCancel.Click += (_, _) => Close();
        // 
        // HourlyRatesForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(384, 206);
        Controls.AddRange(new Control[] { lblHint, lblSnooker, numSnooker, lblBlack, numBlack, btnSave, btnCancel });
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterParent;
        Text = "أسعار الساعة";
        Load += HourlyRatesForm_Load;
        ((System.ComponentModel.ISupportInitialize)numSnooker).EndInit();
        ((System.ComponentModel.ISupportInitialize)numBlack).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblHint;
    private Label lblSnooker;
    private NumericUpDown numSnooker;
    private Label lblBlack;
    private NumericUpDown numBlack;
    private Button btnSave;
    private Button btnCancel;
}
