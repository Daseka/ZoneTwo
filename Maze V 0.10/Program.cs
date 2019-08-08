using System;
using System.Windows.Forms;

namespace MazeV
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var startUp = new StartUp();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(startUp.GetService<Form1>());
        }
    }
}