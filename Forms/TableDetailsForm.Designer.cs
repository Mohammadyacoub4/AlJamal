namespace AlJamal.Forms;

partial class TableDetailsForm
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
        components = new System.ComponentModel.Container();
        lblTitle = new Label();
        lstPlayers = new ListView();
        colName = new ColumnHeader();
        colStart = new ColumnHeader();
        colElapsed = new ColumnHeader();
        btnAddPlayer = new Button();
        btnOrders = new Button();
        btnFinish = new Button();
        btnClose = new Button();
        tmrTick = new System.Windows.Forms.Timer(components);
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font(lblTitle.Font.FontFamily, 14F, FontStyle.Bold);
        lblTitle.Location = new Point(12, 15);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(0, 25);
        lblTitle.TabIndex = 0;
        // 
        // lstPlayers
        // 
        lstPlayers.Columns.AddRange(new ColumnHeader[]
{
    colName,
    colStart,
    colElapsed
});
        lstPlayers.FullRowSelect = true;
        lstPlayers.GridLines = true;
        lstPlayers.Location = new Point(12, 55);
        lstPlayers.Name = "lstPlayers";
        lstPlayers.Size = new Size(460, 280);
        lstPlayers.TabIndex = 1;
        lstPlayers.UseCompatibleStateImageBehavior = false;
        lstPlayers.View = View.Details;
        // 
        // colName
        // 
        colName.Text = "الزبون";
        colName.Width = 140;
        // 
        // colStart
        // 
        colStart.Text = "بدء";
        colStart.Width = 80;
        // 
        // colElapsed
        // 
        colElapsed.Text = "الوقت";
        colElapsed.Width = 100;
        // 
        // btnAddPlayer
        // 
        btnAddPlayer.Location = new Point(12, 350);
        btnAddPlayer.Name = "btnAddPlayer";
        btnAddPlayer.Size = new Size(110, 35);
        btnAddPlayer.TabIndex = 2;
        btnAddPlayer.Text = "إضافة لاعب";
        btnAddPlayer.UseVisualStyleBackColor = true;
        btnAddPlayer.Click += BtnAddPlayer_Click;
        // 
        // btnOrders
        // 
        btnOrders.Location = new Point(132, 350);
        btnOrders.Name = "btnOrders";
        btnOrders.Size = new Size(110, 35);
        btnOrders.TabIndex = 3;
        btnOrders.Text = "طلبات";
        btnOrders.UseVisualStyleBackColor = true;
        btnOrders.Click += BtnOrders_Click;
        // 
        // btnFinish
        // 
        btnFinish.BackColor = Color.FromArgb(255, 152, 0);
        btnFinish.ForeColor = Color.White;
        btnFinish.Location = new Point(252, 350);
        btnFinish.Name = "btnFinish";
        btnFinish.Size = new Size(110, 35);
        btnFinish.TabIndex = 4;
        btnFinish.Text = "إنهاء وفاتورة";
        btnFinish.UseVisualStyleBackColor = false;
        btnFinish.Click += BtnFinish_Click;
        // 
        // btnClose
        // 
        btnClose.Location = new Point(372, 350);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(100, 35);
        btnClose.TabIndex = 5;
        btnClose.Text = "إغلاق";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += BtnClose_Click;
        // 
        // tmrTick
        // 
        tmrTick.Interval = 1000;
        tmrTick.Tick += TmrTick_Tick;
        // 
        // TableDetailsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(484, 401);
        Controls.Add(btnClose);
        Controls.Add(btnFinish);
        Controls.Add(btnOrders);
        Controls.Add(btnAddPlayer);
        Controls.Add(lstPlayers);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "TableDetailsForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterParent;
        Text = "تفاصيل الطاولة — الجمل";
        FormClosed += TableDetailsForm_FormClosed;
        Load += TableDetailsForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblTitle;
    private ListView lstPlayers;
    private ColumnHeader colName;
    private ColumnHeader colStart;
    private ColumnHeader colElapsed;
    private Button btnAddPlayer;
    private Button btnOrders;
    private Button btnFinish;
    private Button btnClose;
    private System.Windows.Forms.Timer tmrTick;
}
