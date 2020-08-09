using System;
using System.Windows.Forms;

namespace Web_Scraper
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new WebScrapeDisplay());
            }
            catch { }
        }
    }
}