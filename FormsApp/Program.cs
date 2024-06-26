using DataLayer.Utilities;

namespace FormsApp
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
            try
            {
                ApplicationConfiguration.Initialize();
                if (ConfigService.HasUserSettings)
                {
                    Application.Run(new MainForm());
                }
                else
                {
                    Application.Run(new Settings());
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Application.Exit();
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}