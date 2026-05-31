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
        lblSnookerFirst = new Label();
        numSnookerFirst = new NumericUpDown();
        lblSnookerAdditional = new Label();
        numSnookerAdditional = new NumericUpDown();
        lblBlack = new Label();
        lblBlackFirst = new Label();
        numBlackFirst = new NumericUpDown();
        lblBlackAdditional = new Label();
        numBlackAdditional = new NumericUpDown();
        btnSave = new Button();
        btnCancel = new Button();
        lblHint = new Label();
        ((System.ComponentModel.ISupportInitialize)numSnookerFirst).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numSnookerAdditional).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numBlackFirst).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numBlackAdditional).BeginInit();
        SuspendLayout();
        // 
        // lblHint
        // 
        lblHint.ForeColor = Color.Gray;
        lblHint.Location = new Point(12, 12);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(360, 40);
        lblHint.Text = "السعر الأول للساعة الأولى. السعر الإضافي للساعات التالية.";
        // 
        // lblSnooker
        // 
        lblSnooker.AutoSize = true;
        lblSnooker.Font = new Font(lblSnooker.Font, FontStyle.Bold);
        lblSnooker.Location = new Point(12, 60);
        lblSnooker.Text = "سنوكر:";
        // 
        // lblSnookerFirst
        // 
        lblSnookerFirst.AutoSize = true;
        lblSnookerFirst.Location = new Point(12, 85);
        lblSnookerFirst.Text = "السعر الأول (د.أ):";
        // 
        // numSnookerFirst
        // 
        numSnookerFirst.DecimalPlaces = 2;
        numSnookerFirst.Location = new Point(200, 83);
        numSnookerFirst.Maximum = 999;
        numSnookerFirst.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
        numSnookerFirst.Size = new Size(120, 23);
        numSnookerFirst.TabIndex = 1;
        // 
        // lblSnookerAdditional
        // 
        lblSnookerAdditional.AutoSize = true;
        lblSnookerAdditional.Location = new Point(12, 115);
        lblSnookerAdditional.Text = "السعر الإضافي (د.أ):";
        // 
        // numSnookerAdditional
        // 
        numSnookerAdditional.DecimalPlaces = 2;
        numSnookerAdditional.Location = new Point(200, 113);
        numSnookerAdditional.Maximum = 999;
        numSnookerAdditional.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
        numSnookerAdditional.Size = new Size(120, 23);
        numSnookerAdditional.TabIndex = 2;
        // 
        // lblBlack
        // 
        lblBlack.AutoSize = true;
        lblBlack.Font = new Font(lblBlack.Font, FontStyle.Bold);
        lblBlack.Location = new Point(12, 150);
        lblBlack.Text = "بلاك:";
        // 
        // lblBlackFirst
        // 
        lblBlackFirst.AutoSize = true;
        lblBlackFirst.Location = new Point(12, 175);
        lblBlackFirst.Text = "السعر الأول (د.أ):";
        // 
        // numBlackFirst
        // 
        numBlackFirst.DecimalPlaces = 2;
        numBlackFirst.Location = new Point(200, 173);
        numBlackFirst.Maximum = 999;
        numBlackFirst.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
        numBlackFirst.Size = new Size(120, 23);
        numBlackFirst.TabIndex = 3;
        // 
        // lblBlackAdditional
        // 
        lblBlackAdditional.AutoSize = true;
        lblBlackAdditional.Location = new Point(12, 205);
        lblBlackAdditional.Text = "السعر الإضافي (د.أ):";
        // 
        // numBlackAdditional
        // 
        numBlackAdditional.DecimalPlaces = 2;
        numBlackAdditional.Location = new Point(200, 203);
        numBlackAdditional.Maximum = 999;
        numBlackAdditional.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
        numBlackAdditional.Size = new Size(120, 23);
        numBlackAdditional.TabIndex = 4;
        // 
        // btnSave
        // 
        btnSave.BackColor = Color.FromArgb(46, 125, 50);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(120, 245);
        btnSave.Size = new Size(100, 34);
        btnSave.Text = "حفظ";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;
        // 
        // btnCancel
        // 
        btnCancel.Location = new Point(230, 245);
        btnCancel.Size = new Size(90, 34);
        btnCancel.Text = "إلغاء";
        btnCancel.Click += (_, _) => Close();
        // 
        // HourlyRatesForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(384, 296);
        Controls.AddRange(new Control[] { 
            lblHint, lblSnooker, lblSnookerFirst, numSnookerFirst, lblSnookerAdditional, numSnookerAdditional,
            lblBlack, lblBlackFirst, numBlackFirst, lblBlackAdditional, numBlackAdditional,
            btnSave, btnCancel 
        });
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterParent;
        Text = "أسعار الساعة";
        Load += HourlyRatesForm_Load;
        ((System.ComponentModel.ISupportInitialize)numSnookerFirst).EndInit();
        ((System.ComponentModel.ISupportInitialize)numSnookerAdditional).EndInit();
        ((System.ComponentModel.ISupportInitialize)numBlackFirst).EndInit();
        ((System.ComponentModel.ISupportInitialize)numBlackAdditional).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblHint;
    private Label lblSnooker;
    private Label lblSnookerFirst;
    private NumericUpDown numSnookerFirst;
    private Label lblSnookerAdditional;
    private NumericUpDown numSnookerAdditional;
    private Label lblBlack;
    private Label lblBlackFirst;
    private NumericUpDown numBlackFirst;
    private Label lblBlackAdditional;
    private NumericUpDown numBlackAdditional;
    private Button btnSave;
    private Button btnCancel;
}
