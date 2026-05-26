namespace AlJamal.Forms;

internal static class InputPrompt
{
    public static string? Show(IWin32Window owner, string prompt, string title, string defaultValue = "")
    {
        using var form = new Form
        {
            Text = title,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            ClientSize = new Size(360, 120),
            MaximizeBox = false,
            MinimizeBox = false,
            RightToLeft = RightToLeft.Yes,
            RightToLeftLayout = true
        };

        var lbl = new Label { Text = prompt, Location = new Point(12, 12), AutoSize = true };
        var txt = new TextBox { Text = defaultValue, Location = new Point(12, 40), Width = 336 };
        var ok = new Button { Text = "موافق", DialogResult = DialogResult.OK, Location = new Point(188, 75), Width = 75 };
        var cancel = new Button { Text = "إلغاء", DialogResult = DialogResult.Cancel, Location = new Point(273, 75), Width = 75 };
        form.Controls.AddRange(new Control[] { lbl, txt, ok, cancel });
        form.AcceptButton = ok;
        form.CancelButton = cancel;

        return form.ShowDialog(owner) == DialogResult.OK ? txt.Text : null;
    }
}
