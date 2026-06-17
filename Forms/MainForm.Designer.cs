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
        headerLayout = new TableLayoutPanel();
        picLogo = new PictureBox();
        titlePanel = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        panelContent = new Panel();
        panelFooter = new Panel();
        lblLegend = new Label();
        panelBlackSection = new Panel();
        blackHeader = new Panel();
        lblBlackTitle = new Label();
        lblBlackCount = new Label();
        tlpBlack = new TableLayoutPanel();
        panelSnookerSection = new Panel();
        snookerHeader = new Panel();
        lblSnookerTitle = new Label();
        lblSnookerCount = new Label();
        tlpSnooker = new TableLayoutPanel();
        tmrRefresh = new System.Windows.Forms.Timer(components);
        menuStrip.SuspendLayout();
        panelHeader.SuspendLayout();
        headerLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
        titlePanel.SuspendLayout();
        panelContent.SuspendLayout();
        panelFooter.SuspendLayout();
        panelBlackSection.SuspendLayout();
        blackHeader.SuspendLayout();
        panelSnookerSection.SuspendLayout();
        snookerHeader.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip
        // 
        menuStrip.BackColor = Color.FromArgb(27, 38, 59);
        menuStrip.ForeColor = Color.White;
        menuStrip.Items.AddRange(new ToolStripItem[] { mnuSettings, mnuArchive, mnuRefresh });
        menuStrip.Location = new Point(0, 0);
        menuStrip.Name = "menuStrip";
        menuStrip.Padding = new Padding(8, 2, 8, 2);
        menuStrip.Size = new Size(1080, 28);
        menuStrip.TabIndex = 2;
        // 
        // mnuSettings
        // 
        mnuSettings.ForeColor = Color.White;
        mnuSettings.Name = "mnuSettings";
        mnuSettings.Size = new Size(58, 24);
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
        mnuArchive.ForeColor = Color.White;
        mnuArchive.Name = "mnuArchive";
        mnuArchive.Size = new Size(90, 24);
        mnuArchive.Text = "أرشيف الفواتير";
        mnuArchive.Click += MnuArchive_Click;
        // 
        // mnuRefresh
        // 
        mnuRefresh.ForeColor = Color.White;
        mnuRefresh.Name = "mnuRefresh";
        mnuRefresh.Size = new Size(51, 24);
        mnuRefresh.Text = "تحديث";
        mnuRefresh.Click += MnuRefresh_Click;
        // 
        // panelHeader
        // 
        panelHeader.BackColor = Color.FromArgb(27, 38, 59);
        panelHeader.Controls.Add(headerLayout);
        panelHeader.Dock = DockStyle.Top;
        panelHeader.Location = new Point(0, 28);
        panelHeader.Name = "panelHeader";
        panelHeader.Size = new Size(1080, 96);
        panelHeader.TabIndex = 1;
        // 
        // headerLayout
        // 
        headerLayout.ColumnCount = 2;
        headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F));
        headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        headerLayout.Controls.Add(picLogo, 0, 0);
        headerLayout.Controls.Add(titlePanel, 1, 0);
        headerLayout.Dock = DockStyle.Fill;
        headerLayout.Location = new Point(0, 0);
        headerLayout.Name = "headerLayout";
        headerLayout.Padding = new Padding(16, 10, 16, 10);
        headerLayout.RowCount = 1;
        headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        headerLayout.Size = new Size(1080, 96);
        headerLayout.TabIndex = 0;
        // 
        // picLogo
        // 
        picLogo.Dock = DockStyle.Fill;
        picLogo.Location = new Point(19, 13);
        picLogo.Margin = new Padding(3, 3, 12, 3);
        picLogo.Name = "picLogo";
        picLogo.Size = new Size(73, 70);
        picLogo.SizeMode = PictureBoxSizeMode.Zoom;
        picLogo.TabIndex = 0;
        picLogo.TabStop = false;
        // 
        // titlePanel
        // 
        titlePanel.Controls.Add(lblTitle);
        titlePanel.Controls.Add(lblSubtitle);
        titlePanel.Dock = DockStyle.Fill;
        titlePanel.Location = new Point(107, 13);
        titlePanel.Name = "titlePanel";
        titlePanel.Size = new Size(957, 70);
        titlePanel.TabIndex = 1;
        // 
        // lblTitle
        // 
        lblTitle.Dock = DockStyle.Top;
        lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(0, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(957, 42);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "الجمل";
        lblTitle.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblSubtitle
        // 
        lblSubtitle.Dock = DockStyle.Fill;
        lblSubtitle.Font = new Font("Segoe UI", 10F);
        lblSubtitle.ForeColor = Color.FromArgb(180, 198, 218);
        lblSubtitle.Location = new Point(0, 42);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(957, 28);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "إدارة الطاولات واللاعبين";
        lblSubtitle.TextAlign = ContentAlignment.TopRight;
        // 
        // panelContent
        // 
        panelContent.AutoScroll = true;
        panelContent.BackColor = Color.FromArgb(238, 242, 247);
        panelContent.Controls.Add(panelBlackSection);
        panelContent.Controls.Add(panelSnookerSection);
        panelContent.Controls.Add(panelFooter);
        panelContent.Dock = DockStyle.Fill;
        panelContent.Location = new Point(0, 124);
        panelContent.Name = "panelContent";
        panelContent.Padding = new Padding(20, 16, 20, 12);
        panelContent.Size = new Size(1080, 596);
        panelContent.TabIndex = 0;
        // 
        // panelFooter
        // 
        panelFooter.Controls.Add(lblLegend);
        panelFooter.Dock = DockStyle.Bottom;
        panelFooter.Location = new Point(20, 548);
        panelFooter.Name = "panelFooter";
        panelFooter.Size = new Size(1040, 36);
        panelFooter.TabIndex = 2;
        // 
        // lblLegend
        // 
        lblLegend.Dock = DockStyle.Fill;
        lblLegend.Font = new Font("Segoe UI", 9F);
        lblLegend.ForeColor = Color.FromArgb(90, 106, 122);
        lblLegend.Location = new Point(0, 0);
        lblLegend.Name = "lblLegend";
        lblLegend.Size = new Size(1040, 36);
        lblLegend.TabIndex = 0;
        lblLegend.Text = "● فاضية   ● مشغولة   — اضغط على الطاولة لإدارة اللاعبين والطلبات";
        lblLegend.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // panelBlackSection
        // 
        panelBlackSection.BackColor = Color.White;
        panelBlackSection.Controls.Add(tlpBlack);
        panelBlackSection.Controls.Add(blackHeader);
        panelBlackSection.Dock = DockStyle.Top;
        panelBlackSection.Location = new Point(20, 296);
        panelBlackSection.Margin = new Padding(0, 0, 0, 16);
        panelBlackSection.Name = "panelBlackSection";
        panelBlackSection.Padding = new Padding(16, 12, 16, 16);
        panelBlackSection.Size = new Size(1040, 252);
        panelBlackSection.TabIndex = 0;
        // 
        // blackHeader
        // 
        blackHeader.Controls.Add(lblBlackTitle);
        blackHeader.Controls.Add(lblBlackCount);
        blackHeader.Dock = DockStyle.Top;
        blackHeader.Location = new Point(16, 12);
        blackHeader.Name = "blackHeader";
        blackHeader.Size = new Size(1008, 36);
        blackHeader.TabIndex = 0;
        // 
        // lblBlackTitle
        // 
        lblBlackTitle.AutoSize = true;
        lblBlackTitle.Dock = DockStyle.Right;
        lblBlackTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblBlackTitle.ForeColor = Color.FromArgb(29, 78, 137);
        lblBlackTitle.Location = new Point(948, 0);
        lblBlackTitle.Name = "lblBlackTitle";
        lblBlackTitle.Padding = new Padding(0, 4, 0, 0);
        lblBlackTitle.Size = new Size(60, 29);
        lblBlackTitle.TabIndex = 0;
        lblBlackTitle.Text = "بلاك";
        // 
        // lblBlackCount
        // 
        lblBlackCount.BackColor = Color.FromArgb(232, 240, 248);
        lblBlackCount.Dock = DockStyle.Left;
        lblBlackCount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblBlackCount.ForeColor = Color.FromArgb(29, 78, 137);
        lblBlackCount.Location = new Point(0, 0);
        lblBlackCount.Name = "lblBlackCount";
        lblBlackCount.Padding = new Padding(8, 6, 8, 0);
        lblBlackCount.Size = new Size(40, 36);
        lblBlackCount.TabIndex = 1;
        lblBlackCount.Text = "3";
        lblBlackCount.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // tlpBlack
        // 
        tlpBlack.ColumnCount = 3;
        tlpBlack.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        tlpBlack.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        tlpBlack.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        tlpBlack.Dock = DockStyle.Top;
        tlpBlack.Location = new Point(16, 48);
        tlpBlack.Name = "tlpBlack";
        tlpBlack.Padding = new Padding(0, 4, 0, 0);
        tlpBlack.RowCount = 1;
        tlpBlack.RowStyles.Add(new RowStyle(SizeType.Absolute, 124F));
        tlpBlack.Size = new Size(1008, 128);
        tlpBlack.TabIndex = 1;
        // 
        // panelSnookerSection
        // 
        panelSnookerSection.BackColor = Color.White;
        panelSnookerSection.Controls.Add(tlpSnooker);
        panelSnookerSection.Controls.Add(snookerHeader);
        panelSnookerSection.Dock = DockStyle.Top;
        panelSnookerSection.Location = new Point(20, 16);
        panelSnookerSection.Margin = new Padding(0, 0, 0, 16);
        panelSnookerSection.Name = "panelSnookerSection";
        panelSnookerSection.Padding = new Padding(16, 12, 16, 16);
        panelSnookerSection.Size = new Size(1040, 280);
        panelSnookerSection.TabIndex = 1;
        // 
        // snookerHeader
        // 
        snookerHeader.Controls.Add(lblSnookerTitle);
        snookerHeader.Controls.Add(lblSnookerCount);
        snookerHeader.Dock = DockStyle.Top;
        snookerHeader.Location = new Point(16, 12);
        snookerHeader.Name = "snookerHeader";
        snookerHeader.Size = new Size(1008, 36);
        snookerHeader.TabIndex = 0;
        // 
        // lblSnookerTitle
        // 
        lblSnookerTitle.AutoSize = true;
        lblSnookerTitle.Dock = DockStyle.Right;
        lblSnookerTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblSnookerTitle.ForeColor = Color.FromArgb(45, 106, 79);
        lblSnookerTitle.Location = new Point(928, 0);
        lblSnookerTitle.Name = "lblSnookerTitle";
        lblSnookerTitle.Padding = new Padding(0, 4, 0, 0);
        lblSnookerTitle.Size = new Size(80, 29);
        lblSnookerTitle.TabIndex = 0;
        lblSnookerTitle.Text = "سنوكر";
        // 
        // lblSnookerCount
        // 
        lblSnookerCount.BackColor = Color.FromArgb(232, 245, 237);
        lblSnookerCount.Dock = DockStyle.Left;
        lblSnookerCount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSnookerCount.ForeColor = Color.FromArgb(45, 106, 79);
        lblSnookerCount.Location = new Point(0, 0);
        lblSnookerCount.Name = "lblSnookerCount";
        lblSnookerCount.Padding = new Padding(8, 6, 8, 0);
        lblSnookerCount.Size = new Size(40, 36);
        lblSnookerCount.TabIndex = 1;
        lblSnookerCount.Text = "7";
        lblSnookerCount.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // tlpSnooker
        // 
        tlpSnooker.ColumnCount = 4;
        tlpSnooker.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpSnooker.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpSnooker.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpSnooker.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpSnooker.Dock = DockStyle.Top;
        tlpSnooker.Location = new Point(16, 48);
        tlpSnooker.Name = "tlpSnooker";
        tlpSnooker.Padding = new Padding(0, 4, 0, 0);
        tlpSnooker.RowCount = 2;
        tlpSnooker.RowStyles.Add(new RowStyle(SizeType.Absolute, 124F));
        tlpSnooker.RowStyles.Add(new RowStyle(SizeType.Absolute, 124F));
        tlpSnooker.Size = new Size(1008, 252);
        tlpSnooker.TabIndex = 1;
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
        BackColor = Color.White;
        ClientSize = new Size(1080, 720);
        Controls.Add(panelContent);
        Controls.Add(panelHeader);
        Controls.Add(menuStrip);
        Font = new Font("Segoe UI", 9F);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MainMenuStrip = menuStrip;
        MinimumSize = new Size(900, 620);
        Name = "MainForm";
        RightToLeft = RightToLeft.Yes;
        RightToLeftLayout = true;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "الجمل — إدارة البلياردو";
        Load += MainForm_Load;
        mnuSettings.DropDownItems.AddRange(new ToolStripItem[] { mnuHourlyRates, mnuProducts, mnuSep1 });
        menuStrip.ResumeLayout(false);
        menuStrip.PerformLayout();
        panelHeader.ResumeLayout(false);
        headerLayout.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
        titlePanel.ResumeLayout(false);
        panelContent.ResumeLayout(false);
        panelFooter.ResumeLayout(false);
        panelBlackSection.ResumeLayout(false);
        blackHeader.ResumeLayout(false);
        blackHeader.PerformLayout();
        panelSnookerSection.ResumeLayout(false);
        snookerHeader.ResumeLayout(false);
        snookerHeader.PerformLayout();
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
    private TableLayoutPanel headerLayout;
    private PictureBox picLogo;
    private Panel titlePanel;
    private Label lblTitle;
    private Label lblSubtitle;
    private Panel panelContent;
    private Panel panelSnookerSection;
    private Panel snookerHeader;
    private Label lblSnookerTitle;
    private Label lblSnookerCount;
    private TableLayoutPanel tlpSnooker;
    private Panel panelBlackSection;
    private Panel blackHeader;
    private Label lblBlackTitle;
    private Label lblBlackCount;
    private TableLayoutPanel tlpBlack;
    private Panel panelFooter;
    private Label lblLegend;
    private System.Windows.Forms.Timer tmrRefresh;
}
