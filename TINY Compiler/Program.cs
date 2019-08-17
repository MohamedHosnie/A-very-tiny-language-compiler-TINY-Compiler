using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINY_Compiler
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static Compiler compiler;
        public static MainForm mainForm;
        public static ScanForm scanForm;
        public static ParserForm parserForm;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Program.compiler = new Compiler();
            mainForm = new MainForm();
            Application.Run(mainForm);
        }
    }
}
