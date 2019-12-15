using System;
using System.Windows.Forms;

namespace App_FOR_DB
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            TooSharp.Connection.connectionInstance = new TooSharp.Connection("kursach");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AutorizationForm());
        }
    }
}
