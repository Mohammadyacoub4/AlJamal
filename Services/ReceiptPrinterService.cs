using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace AlJamal.Services;

internal static class ReceiptPrinterService
{
    private static readonly PaperSize ReceiptPaper = new("Receipt80mm", 315, 1000);

    public static void PrintReceipt(string receiptText)
    {
        using var doc = new PrintDocument();
        doc.DocumentName = $"فاتورة {AppSettings.ShopName}";
        doc.PrinterSettings = new PrinterSettings();
        doc.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);
        doc.DefaultPageSettings.PaperSize = ReceiptPaper;

        var lines = receiptText.Split(Environment.NewLine);
        var lineIndex = 0;

        doc.PrintPage += (_, ev) =>
        {
            using var font = new Font("Consolas", 9);
            float y = ev.MarginBounds.Top;
            var lineHeight = font.GetHeight(ev.Graphics!) + 2;
            var x = ev.MarginBounds.Left;

            while (lineIndex < lines.Length && y + lineHeight < ev.MarginBounds.Bottom)
            {
                ev.Graphics!.DrawString(lines[lineIndex], font, Brushes.Black, x, y);
                y += lineHeight;
                lineIndex++;
            }

            ev.HasMorePages = lineIndex < lines.Length;
        };

        doc.Print();
    }

    public static void OpenCashDrawer()
    {
        // ESC/POS: فتح درج النقود (pin 2)
        ReadOnlySpan<byte> openDrawer = [0x1B, 0x70, 0x00, 0x19, 0xFA];
        RawPrinterHelper.SendBytesToDefaultPrinter(openDrawer);
    }

    public static void PrintReceiptAndOpenDrawer(string receiptText)
    {
        PrintReceipt(receiptText);
        OpenCashDrawer();
    }

    private static class RawPrinterHelper
    {
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool StartDocPrinter(IntPtr hPrinter, int level, ref DOC_INFO_1 pDocInfo);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool WritePrinter(IntPtr hPrinter, byte[] pBytes, int dwCount, out int dwWritten);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct DOC_INFO_1
        {
            public string pDocName;
            public string pOutputFile;
            public string pDatatype;
        }

        public static void SendBytesToDefaultPrinter(ReadOnlySpan<byte> bytes)
        {
            var printerName = new PrinterSettings().PrinterName;
            if (string.IsNullOrWhiteSpace(printerName))
                return;

            if (!OpenPrinter(printerName, out var hPrinter, IntPtr.Zero))
                return;

            try
            {
                var docInfo = new DOC_INFO_1
                {
                    pDocName = "CashDrawer",
                    pOutputFile = null!,
                    pDatatype = "RAW"
                };

                if (!StartDocPrinter(hPrinter, 1, ref docInfo))
                    return;

                try
                {
                    if (!StartPagePrinter(hPrinter))
                        return;

                    try
                    {
                        var buffer = bytes.ToArray();
                        WritePrinter(hPrinter, buffer, buffer.Length, out _);
                    }
                    finally
                    {
                        EndPagePrinter(hPrinter);
                    }
                }
                finally
                {
                    EndDocPrinter(hPrinter);
                }
            }
            finally
            {
                ClosePrinter(hPrinter);
            }
        }
    }
}
