using Microsoft.Extensions.DependencyInjection;
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
            var service = new ServiceCollection();
            service.AddScoped<Form1>();

            ServiceProvider serviceProvider = service.BuildServiceProvider();                

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(serviceProvider.GetService<Form1>());
        }
    }
}