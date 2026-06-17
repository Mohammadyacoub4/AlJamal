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
        lblHint = new Label();
        lblSnooker = new Label();
        lblSnookerFirst = new Label();
        numSnookerFirst = new NumericUpDown();
        lblSnookerAdditional = new Label();
        numSnookerAdditional = new NumericUpDown();
        lblSnooker7 = new Label();
        lblSnooker7First = new Label();
        numSnooker7First = new NumericUpDown();
        lblSnooker7Additional = new Label();
        numSnooker7Additional = new NumericUpDown();
        lblBlack = new Label();
        lblBlackFirst = new Label();
        numBlackFirst = new NumericUpDown();
        lblBlackAdditional = new Label();
        numBlackAdditional = new NumericUpDown();
        btnSave = new Button();
        btnCancel = new Button();

        ((System.ComponentModel.ISupportInitialize)numSnookerFirst).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numSnookerAdditional).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numSnooker7First).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numSnooker7Additional).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numBlackFirst).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numBlackAdditional).BeginInit();

        SuspendLayout();

        // lblHint
        lblHint.ForeColor = Color.Gray;
        lblHint.Location = new Point(12, 12);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(360, 40);
        lblHint.Text = "السعر الأول للساعة الأولى. السعر الإضافي للساعات التالية.";

        // lblSnooker
        lblSnooker.AutoSize = true;
        lblSnooker.Font = new Font(lblSnooker.Font, FontStyle.Bold);
        lblSnooker.Location = new Point(12, 60);
        lblSnooker.Text = "سنوكر (1-6):";

        // lblSnookerFirst
        lblSnookerFirst.AutoSize = true;
        lblSnookerFirst.Location = new Point(12, 85);
        lblSnookerFirst.Text = "السعر الأول (د.أ):";

        // numSnookerFirst
        numSnookerFirst.DecimalPlaces = 2;
        numSnookerFirst.Location = new Point(200, 83);
        numSnookerFirst.Maximum = 999;
        numSnookerFirst.Minimum = 1;
        numSnookerFirst.Size = new Size(120, 23);
        numSnookerFirst.TabIndex = 1;

        // lblSnookerAdditional
        lblSnookerAdditional.AutoSize = true;
        lblSnookerAdditional.Location = new Point(12, 115);
        lblSnookerAdditional.Text = "السعر الإضافي (د.أ):";

        // numSnookerAdditional
        numSnookerAdditional.DecimalPlaces = 2;
        numSnookerAdditional.Location = new Point(200, 113);
        numSnookerAdditional.Maximum = 999;
        numSnookerAdditional.Minimum = 1;
        numSnookerAdditional.Size = new Size(120, 23);
        numSnookerAdditional.TabIndex = 2;

        // lblSnooker7
        lblSnooker7.AutoSize = true;
        lblSnooker7.Font = new Font(lblSnooker7.Font, FontStyle.Bold);
        lblSnooker7.Location = new Point(12, 150);
        lblSnooker7.Text = "سنوكر 7:";

        // lblSnooker7First
        lblSnooker7First.AutoSize = true;
        lblSnooker7First.Location = new Point(12, 175);
        lblSnooker7First.Text = "السعر الأول (د.أ):";

        // numSnooker7First
        numSnooker7First.DecimalPlaces = 2;
        numSnooker7First.Location = new Point(200, 173);
        numSnooker7First.Maximum = 999;
        numSnooker7First.Minimum = 1;
        numSnooker7First.Size = new Size(120, 23);
        numSnooker7First.TabIndex = 3;

        // lblSnooker7Additional
        lblSnooker7Additional.AutoSize = true;
        lblSnooker7Additional.Location = new Point(12, 205);
        lblSnooker7Additional.Text = "السعر الإضافي (د.أ):";

        // numSnooker7Additional
        numSnooker7Additional.DecimalPlaces = 2;
        numSnooker7Additional.Location = new Point(200, 203);
        numSnooker7Additional.Maximum = 999;
        numSnooker7Additional.Minimum = 1;
        numSnooker7Additional.Size = new Size(120, 23);
        numSnooker7Additional.TabIndex = 4;

        // lblBlack
        lblBlack.AutoSize = true;
        lblBlack.Font = new Font(lblBlack.Font, FontStyle.Bold);
        lblBlack.Location = new Point(12, 240);
        lblBlack.Text = "بلاك:";

        // lblBlackFirst
        lblBlackFirst.AutoSize = true;
        lblBlackFirst.Location = new Point(12, 265);
        lblBlackFirst.Text = "السعر الأول (د.أ):";

        // numBlackFirst
        numBlackFirst.DecimalPlaces = 2;
        numBlackFirst.Location = new Point(200, 263);
        numBlackFirst.Maximum = 999;
        numBlackFirst.Minimum = 1;
        numBlackFirst.Size = new Size(120, 23);
        numBlackFirst.TabIndex = 5;

        // lblBlackAdditional
        lblBlackAdditional.AutoSize = true;
        lblBlackAdditional.Location = new Point(12, 295);
        lblBlackAdditional.Text = "السعر الإضافي (د.أ):";

        // numBlackAdditional
        numBlackAdditional.DecimalPlaces = 2;
        numBlackAdditional.Location = new Point(200, 293);
        numBlackAdditional.Maximum = 999;
        numBlackAdditional.Minimum = 1;
        numBlackAdditional.Size = new Size(120, 23);
        numBlackAdditional.TabIndex = 6;

        // btnSave
        btnSave.BackColor = Color.FromArgb(46, 125, 50);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(120, 335);
        btnSave.Size = new Size(100, 34);
        btnSave.Text = "حفظ";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;

        // btnCancel
        btnCancel.Location = new Point(230, 335);
        btnCancel.Size = new Size(90, 34);
        btnCancel.Text = "إلغاء";
        btnCancel.Click += BtnCancel_Click;

        // Form
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(384, 386);

        Controls.AddRange(new Control[]
        {
            lblHint, lblSnooker, lblSnookerFirst, numSnookerFirst,
            lblSnookerAdditional, numSnookerAdditional,
            lblSnooker7, lblSnooker7First, numSnooker7First,
            lblSnooker7Additional, numSnooker7Additional,
            lblBlack, lblBlackFirst, numBlackFirst,
            lblBlackAdditional, numBlackAdditional,
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
        ((System.ComponentModel.ISupportInitialize)numSnooker7First).EndInit();
        ((System.ComponentModel.ISupportInitialize)numSnooker7Additional).EndInit();
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
    private Label lblSnooker7;
    private Label lblSnooker7First;
    private NumericUpDown numSnooker7First;
    private Label lblSnooker7Additional;
    private NumericUpDown numSnooker7Additional;
    private Label lblBlack;
    private Label lblBlackFirst;
    private NumericUpDown numBlackFirst;
    private Label lblBlackAdditional;
    private NumericUpDown numBlackAdditional;
    private Button btnSave;
    private Button btnCancel;
}
