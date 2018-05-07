using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TLD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Start st = new Start();
            st.ShowDialog();
            Application.Run(new Main());
            
        }
    }
}