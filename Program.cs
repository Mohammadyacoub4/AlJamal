namespace AlJamal
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (!Database.DatabaseBootstrap.TryInitialize(out var dbError))
            {
                MessageBox.Show(
                    $"تعذر تهيئة قاعدة البيانات.\n\n{dbError}\n\n" +
                    "تأكد أن SQL Server LocalDB مثبت، ثم أعد تشغيل البرنامج.\n" +
                    "أو نفّذ يدوياً: Database\\MigrateSchema.sql",
                    "خطأ قاعدة البيانات",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            Application.Run(new Forms.MainForm());
        }
    }
}