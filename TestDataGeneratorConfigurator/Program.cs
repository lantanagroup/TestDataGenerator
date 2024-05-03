using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace LantanaGroup.TestDataGenerator.Configurator
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

            /** To figure out the public key used for InternalsVisibleTo() 
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            Console.WriteLine("{0}, PublicKey={1}",
                assemblyName.Name,
            string.Join("", assemblyName.GetPublicKey().Select(m => string.Format("{0:x2}", m))));
            **/

            Application.Run(new MainForm());
        }
    }
}
