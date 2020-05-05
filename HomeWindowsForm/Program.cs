using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWindowsForm
{
    //static class Program
    //{
    //    /// <summary>
    //    /// The main entry point for the application.
    //    /// </summary>
    //    [STAThread]
    //    static void Main()
    //    {
    //        Application.EnableVisualStyles();
    //        Application.SetCompatibleTextRenderingDefault(false);
    //        Application.Run(new Form1());
    //    }
    //}

    class Program
    {
        // [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            //Application.Run(new CredentialWindowsForm()); // or whatever
            Application.Run(new HomeWindowsForm());
            // Application.Run(new GridViewButton());
        }
    }
}
