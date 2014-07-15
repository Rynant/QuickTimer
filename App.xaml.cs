using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Runtime.InteropServices;

namespace QuickTimer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static double CommandLineTime = 0;

        public void WriteToConsole(string message)
        {
            AttachConsole(-1);
            Console.WriteLine(message);
        }

        [DllImport("Kernel32.dll")]
        public static extern bool AttachConsole(int processId);

        void App_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 0) return;

            try
            {
                CommandLineTime = double.Parse(e.Args[0]);
            }
            catch
            {
                WriteToConsole("\nEnter a number as an argument to start the timer with the specified\nnumber of minutes.");
                WriteToConsole("\nExample: Start the timer with two and a half minutes on the clock.");
                WriteToConsole("\n\tC:> QuickTimer 2.5\n");
                Application.Current.Shutdown();
            }
        }
    }
}
