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
        grpSnooker = new GroupBox();
        tlpSnooker = new TableLayoutPanel();
        grpBlack = new GroupBox();
        tlpBlack = new TableLayoutPanel();
        tmrRefresh = new System.Windows.Forms.Timer(components);
        menuStrip.SuspendLayout();
        panelHeader.SuspendLayout();
        panelContent.SuspendLayout();
        grpSnooker.SuspendLayout();
        grpBlack.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip
        // 
        menuStrip.Items.AddRange(new ToolStripItem[] { mnuSettings, mnuArchive, mnuRefresh });
        menuStrip.Location = new Point(0, 0);
        menuStrip.Size = new Size(1000, 24);
        // 
        // mnuSettings
        // 
        mnuSettings.DropDownItems.AddRange(new ToolStripItem[] { mnuHourlyRates, mnuProducts, mnuSep1 });
        mnuSettings.Text = "إعدادات";
        // 
        // mnuHourlyRates
        // 
        mnuHourlyRates.Text = "أسعار الساعة";
        mnuHourlyRates.Click += MnuHourlyRates_Click;
        // 
        // mnuProducts
        // 
        mnuProducts.Text = "المنتجات";
        mnuProducts.Click += MnuProducts_Click;
        // 
        // mnuArchive
        // 
        mnuArchive.Text = "أرشيف الفواتير";
        mnuArchive.Click += MnuArchive_Click;
        // 
        // mnuRefresh
        // 
        mnuRefresh.Text = "تحديث";
        mnuRefresh.Click += MnuRefresh_Click;
        // 
        // panelHeader
        // 
        panelHeader.BackColor = Color.FromArgb(33, 33, 33);
        panelHeader.Controls.Add(lblTitle);
        panelHeader.Controls.Add(lblSubtitle);
        panelHeader.Dock = DockStyle.Top;
        panelHeader.Size = new Size(1000, 68);
        // 
        // lblTitle
        // 
        lblTitle.Dock = DockStyle.Top;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.Height = 36;
        lblTitle.Text = "الجمل — بلياردو";
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblSubtitle
        // 
        lblSubtitle.Dock = DockStyle.Top;
        lblSubtitle.ForeColor = Color.FromArgb(200, 200, 200);
        lblSubtitle.Height = 28;
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
        panelContent.Padding = new Padding(12);
        // 
        // grpSnooker
        // 
        grpSnooker.Controls.Add(tlpSnooker);
        grpSnooker.Dock = DockStyle.Top;
        grpSnooker.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        grpSnooker.ForeColor = Color.FromArgb(27, 94, 32);
        grpSnooker.Padding = new Padding(10, 4, 10, 10);
        grpSnooker.Size = new Size(960, 250);
        grpSnooker.Text = "سنوكر (7)";
        // 
        // tlpSnooker
        // 
        tlpSnooker.ColumnCount = 4;
        tlpSnooker.Dock = DockStyle.Top;
        tlpSnooker.Location = new Point(10, 23);
        tlpSnooker.RowCount = 2;
        tlpSnooker.Size = new Size(940, 210);
        // 
        // grpBlack
        // 
        grpBlack.Controls.Add(tlpBlack);
        grpBlack.Dock = DockStyle.Top;
        grpBlack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        grpBlack.ForeColor = Color.FromArgb(21, 101, 192);
        grpBlack.Padding = new Padding(10, 4, 10, 10);
        grpBlack.Size = new Size(960, 140);
        grpBlack.Text = "بلاك (3)";
        // 
        // tlpBlack
        // 
        tlpBlack.ColumnCount = 3;
        tlpBlack.Dock = DockStyle.Top;
        tlpBlack.Location = new Point(10, 23);
        tlpBlack.RowCount = 1;
        tlpBlack.Size = new Size(940, 100);
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
        grpSnooker.ResumeLayout(false);
        grpBlack.ResumeLayout(false);
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
