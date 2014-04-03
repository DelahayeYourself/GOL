using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ScreenSaver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); 
            ShowScreenSaver();
            Application.Run();
        }

        /// <summary>
        /// Display the form on each of the computer's monitors.
        /// </summary>
        static void ShowScreenSaver()
        { 
            ScreenSaverForm screensaver = new ScreenSaverForm(Screen.PrimaryScreen.Bounds);
            screensaver.Show();
        }
    }
}
