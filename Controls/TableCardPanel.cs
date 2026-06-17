using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace AlJamal.Controls;

internal sealed class TableCardPanel : Panel
{
    private bool _hover;
    private readonly Color _freeColor;
    private readonly Color _busyColor;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TableId { get; init; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string TableName { get; set; } = "";

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string RateText { get; set; } = "";

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string StatusText { get; set; } = "";

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsBusy { get; set; }

    public TableCardPanel(Color freeColor, Color busyColor)
    {
        _freeColor = freeColor;
        _busyColor = busyColor;

        Cursor = Cursors.Hand;
        Margin = new Padding(6);
        MinimumSize = new Size(120, 108);
        Size = new Size(140, 112);

        SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true);

        MouseEnter += (_, _) => { _hover = true; Invalidate(); };
        MouseLeave += (_, _) => { _hover = false; Invalidate(); };
    }

    public event EventHandler? CardClicked;

    protected override void OnClick(EventArgs e)
    {
        base.OnClick(e);
        CardClicked?.Invoke(this, EventArgs.Empty);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        var cardRect = new Rectangle(4, 4, Width - 9, Height - 9);
        var baseColor = IsBusy ? _busyColor : _freeColor;
        if (_hover)
            baseColor = ControlPaint.Light(baseColor, 0.1f);

        using (var shadowPath = CreateRoundedRect(new Rectangle(cardRect.X + 2, cardRect.Y + 3, cardRect.Width, cardRect.Height), 14))
        using (var shadowBrush = new SolidBrush(Color.FromArgb(28, 0, 0, 0)))
            g.FillPath(shadowBrush, shadowPath);

        using (var cardPath = CreateRoundedRect(cardRect, 14))
        using (var cardBrush = new LinearGradientBrush(
                   cardRect,
                   ControlPaint.Light(baseColor, 0.12f),
                   baseColor,
                   LinearGradientMode.Vertical))
            g.FillPath(cardBrush, cardPath);

        using (var borderPen = new Pen(Color.FromArgb(60, Color.White), 1.5f))
            g.DrawPath(borderPen, CreateRoundedRect(cardRect, 14));

        using var titleFont = new Font("Segoe UI", 11F, FontStyle.Bold);
        using var rateFont = new Font("Segoe UI", 9F, FontStyle.Regular);
        using var whiteBrush = new SolidBrush(Color.White);
        using var softBrush = new SolidBrush(Color.FromArgb(220, 255, 255, 255));

        var titleRect = new RectangleF(cardRect.X + 10, cardRect.Y + 12, cardRect.Width - 20, 26);
        var rateRect = new RectangleF(cardRect.X + 10, cardRect.Y + 38, cardRect.Width - 20, 22);

        var sf = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            Trimming = StringTrimming.EllipsisCharacter,
            FormatFlags = StringFormatFlags.NoWrap
        };

        g.DrawString(TableName, titleFont, whiteBrush, titleRect, sf);
        g.DrawString(RateText, rateFont, softBrush, rateRect, sf);

        DrawStatusBadge(g, cardRect);
    }

    private void DrawStatusBadge(Graphics g, Rectangle cardRect)
    {
        var badgeText = StatusText;
        using var badgeFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        var textSize = g.MeasureString(badgeText, badgeFont);
        var badgeWidth = Math.Max(72, textSize.Width + 18);
        var badgeHeight = 22;
        var badgeRect = new Rectangle(
            cardRect.X + (cardRect.Width - (int)badgeWidth) / 2,
            cardRect.Bottom - badgeHeight - 10,
            (int)badgeWidth,
            badgeHeight);

        var badgeColor = IsBusy
            ? Color.FromArgb(210, 255, 255, 255)
            : Color.FromArgb(180, 255, 255, 255);

        using var badgePath = CreateRoundedRect(badgeRect, 11);
        using var badgeBrush = new SolidBrush(badgeColor);
        g.FillPath(badgeBrush, badgePath);

        using var textBrush = new SolidBrush(IsBusy ? _busyColor : _freeColor);
        var sf = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        g.DrawString(badgeText, badgeFont, textBrush, badgeRect, sf);
    }

    private static GraphicsPath CreateRoundedRect(Rectangle bounds, int radius)
    {
        var path = new GraphicsPath();
        var d = radius * 2;
        if (d > bounds.Width) d = bounds.Width;
        if (d > bounds.Height) d = bounds.Height;

        path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
        path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
        path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }
}
