namespace AlJamal.Forms;

partial class MainForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        menuStrip = new MenuStrip();
        mnuSettings = new ToolStripMenuItem();
        mnuHourlyRates = new ToolStripMenuItem();
        mnuProducts = new ToolStripMenuItem();
        mnuSep1 = new ToolStripSeparator();
        mnuArchive = new ToolStripMenuItem();
        mnuRefresh = new ToolStripMenuItem();
        panelHeader = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        panelContent = new Panel();
        grpBlack = new GroupBox();
        tlpBlack = new TableLayoutPanel();
        grpSnooker = new GroupBox();
        tlpSnooker = new TableLayoutPanel();
        tmrRefresh = new System.Windows.Forms.Timer(components);
        menuStrip.SuspendLayout();
        panelHeader.SuspendLayout();
        panelContent.SuspendLayout();
        grpBlack.SuspendLayout();
        grpSnooker.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip
        // 
        menuStrip.Items.AddRange(new ToolStripItem[] { mnuSettings, mnuArchive, mnuRefresh });
        menuStrip.Location = new Point(0, 0);
        menuStrip.Name = "menuStrip";
        menuStrip.Size = new Size(1000, 24);
        menuStrip.TabIndex = 2;
        // 
        // mnuSettings
        // 
        mnuSettings.DropDownItems.AddRange(new ToolStripItem[] { mnuHourlyRates, mnuProducts, mnuSep1 });
        mnuSettings.Name = "mnuSettings";
        mnuSettings.Size = new Size(58, 20);
        mnuSettings.Text = "إعدادات";
        // 
        // mnuHourlyRates
        // 
        mnuHourlyRates.Name = "mnuHourlyRates";
        mnuHourlyRates.Size = new Size(138, 22);
        mnuHourlyRates.Text = "أسعار الساعة";
        mnuHourlyRates.Click += MnuHourlyRates_Click;
        // 
        // mnuProducts
        // 
        mnuProducts.Name = "mnuProducts";
        mnuProducts.Size = new Size(138, 22);
        mnuProducts.Text = "المنتجات";
        mnuProducts.Click += MnuProducts_Click;
        // 
        // mnuSep1
        // 
        mnuSep1.Name = "mnuSep1";
        mnuSep1.Size = new Size(135, 6);
        // 
        // mnuArchive
        // 
        mnuArchive.Name = "mnuArchive";
        mnuArchive.Size = new Size(90, 20);
        mnuArchive.Text = "أرشيف الفواتير";
        mnuArchive.Click += MnuArchive_Click;
        // 
        // mnuRefresh
        // 
        mnuRefresh.Name = "mnuRefresh";
        mnuRefresh.Size = new Size(51, 20);
        mnuRefresh.Text = "تحديث";
        mnuRefresh.Click += MnuRefresh_Click;
        // 
        // panelHeader
        // 
        panelHeader.BackColor = Color.FromArgb(33, 33, 33);
        panelHeader.Controls.Add(lblTitle);
        panelHeader.Controls.Add(lblSubtitle);
        panelHeader.Dock = DockStyle.Top;
        panelHeader.Location = new Point(0, 24);
        panelHeader.Name = "panelHeader";
        panelHeader.Size = new Size(1000, 68);
        panelHeader.TabIndex = 1;
        // 
        // lblTitle
        // 
        lblTitle.Dock = DockStyle.Top;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(0, 28);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(1000, 36);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "الجمل — بلياردو";
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblSubtitle
        // 
        lblSubtitle.Dock = DockStyle.Top;
        lblSubtitle.ForeColor = Color.FromArgb(200, 200, 200);
        lblSubtitle.Location = new Point(0, 0);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(1000, 28);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "اضغط على الطاولة لإدارة اللاعبين والطلبات";
        lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // panelContent
        // 
        panelContent.AutoScroll = true;
        panelContent.BackColor = Color.FromArgb(245, 245, 245);
        panelContent.Controls.Add(grpBlack);
        panelContent.Controls.Add(grpSnooker);
        panelContent.Dock = DockStyle.Fill;
        panelContent.Location = new Point(0, 92);
        panelContent.Name = "panelContent";
        panelContent.Padding = new Padding(12);
        panelContent.Size = new Size(1000, 468);
        panelContent.TabIndex = 0;
        // 
        // grpBlack
        // 
        grpBlack.Controls.Add(tlpBlack);
        grpBlack.Dock = DockStyle.Top;
        grpBlack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        grpBlack.ForeColor = Color.FromArgb(21, 101, 192);
        grpBlack.Location = new Point(12, 262);
        grpBlack.Name = "grpBlack";
        grpBlack.Padding = new Padding(10, 4, 10, 10);
        grpBlack.Size = new Size(976, 140);
        grpBlack.TabIndex = 0;
        grpBlack.TabStop = false;
        grpBlack.Text = "بلاك (3)";
        // 
        // tlpBlack
        // 
        tlpBlack.ColumnCount = 3;
        tlpBlack.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tlpBlack.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tlpBlack.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tlpBlack.Dock = DockStyle.Top;
        tlpBlack.Location = new Point(10, 22);
        tlpBlack.Name = "tlpBlack";
        tlpBlack.RowCount = 1;
        tlpBlack.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tlpBlack.Size = new Size(956, 100);
        tlpBlack.TabIndex = 0;
        // 
        // grpSnooker
        // 
        grpSnooker.Controls.Add(tlpSnooker);
        grpSnooker.Dock = DockStyle.Top;
        grpSnooker.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        grpSnooker.ForeColor = Color.FromArgb(27, 94, 32);
        grpSnooker.Location = new Point(12, 12);
        grpSnooker.Name = "grpSnooker";
        grpSnooker.Padding = new Padding(10, 4, 10, 10);
        grpSnooker.Size = new Size(976, 250);
        grpSnooker.TabIndex = 1;
        grpSnooker.TabStop = false;
        grpSnooker.Text = "سنوكر (7)";
        // 
        // tlpSnooker
        // 
        tlpSnooker.ColumnCount = 4;
        tlpSnooker.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tlpSnooker.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tlpSnooker.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tlpSnooker.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tlpSnooker.Dock = DockStyle.Top;
        tlpSnooker.Location = new Point(10, 22);
        tlpSnooker.Name = "tlpSnooker";
        tlpSnooker.RowCount = 2;
        tlpSnooker.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tlpSnooker.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tlpSnooker.Size = new Size(956, 210);
        tlpSnooker.TabIndex = 0;
        // 
        // tmrRefresh
        // 
        tmrRefresh.Interval = 5000;
        tmrRefresh.Tick += TmrRefresh_Tick;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(1000, 560);
        Controls.Add(panelContent);
        Controls.Add(panelHeader);
        Controls.Add(menuStrip);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MainMenuStrip = menuStrip;
        MinimumSize = new Size(820, 500);
        Name = "MainForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "الجمل — إدارة البلياردو";
        Load += MainForm_Load;
        menuStrip.ResumeLayout(false);
        menuStrip.PerformLayout();
        panelHeader.ResumeLayout(false);
        panelContent.ResumeLayout(false);
        grpBlack.ResumeLayout(false);
        grpSnooker.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private MenuStrip menuStrip;
    private ToolStripMenuItem mnuSettings;
    private ToolStripMenuItem mnuHourlyRates;
    private ToolStripMenuItem mnuProducts;
    private ToolStripSeparator mnuSep1;
    private ToolStripMenuItem mnuArchive;
    private ToolStripMenuItem mnuRefresh;
    private Panel panelHeader;
    private Label lblTitle;
    private Label lblSubtitle;
    private Panel panelContent;
    private GroupBox grpSnooker;
    private TableLayoutPanel tlpSnooker;
    private GroupBox grpBlack;
    private TableLayoutPanel tlpBlack;
    private System.Windows.Forms.Timer tmrRefresh;
}
