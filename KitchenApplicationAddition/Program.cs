using KitchenApplicationAddition.Classes.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KitchenApplicationAddition
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MessageManager MessageInstance = MessageManager.Instance;
            Application.Run(new Form1());
            MessageManager.CloseConnection();
        }
    }
}
