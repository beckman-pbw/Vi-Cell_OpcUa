using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace ViCellBLU_dotNET_Test
{

    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] args = Environment.GetCommandLineArgs();
            if (args.Count() < 2)
            {
                // No comamnd line - Show the main form
                Application.Run(new frmMain());
                return 0;
            }

            if (args.Count() >= 3)
            {

                //string cmdline = args[1];
                // Command line given, display console
                if (!AttachConsole(-1))
                { // Attach to an parent process console
                    AllocConsole(); // Alloc a new console
                }
                Console.WriteLine("");
                Console.WriteLine("****************************");
                bool waitComplete = false;
                if ((args.Count() >= 4) && !string.IsNullOrEmpty(args[3]))                    
                {
                    waitComplete = args[3].ToUpper().Contains("--WAIT");
                }

                if (args[1].EndsWith(".ccf") && File.Exists(args[1]))
                {
                    if (args[2].EndsWith(".bsc") && File.Exists(args[2]))
                    {
                        // Run a sample
                        Console.WriteLine("Run single sample, config file: " + args[1]);
                        if (frmMain.RunSample(args[1], args[2], false, waitComplete))
                        {
                            Console.WriteLine("Run single sample, OK");
                            return 0;
                        }
                        Console.WriteLine("Run single sample, Error");
                    }
                    else if (args[2].EndsWith(".stc") && File.Exists(args[2]))
                    {
                        // Run a sample
                        Console.WriteLine("Run sample set, config file: " + args[1]);
                        if (frmMain.RunSample(args[1], args[2], true, waitComplete))
                        {
                            Console.WriteLine("Run sample set, OK");
                            return 0;
                        }
                        Console.WriteLine("Run sample set, Error");
                    }
                }
            }

            // Command line args but not an xml file - run the API tests
            if (frmMain.RunAPITests(args[1]))
            {
                return 0;
            }
            return -1;

        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int pid);
    }
}