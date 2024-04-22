using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Ninject;
using Ninject.Extensions.Logging;
using ViCellBluOpcUaModelDesign;

namespace ViCellOpcUaServer
{
    public class Program
    {
        private static ILogger _logger;
        private static Service _service;

        public static void Main(string[] args)
        {
            SetupProcessEvents();

            var kernel = new StandardKernel(new BecOpcUaModule());
            kernel.Bind<Service>().ToSelf().InSingletonScope();

            SetupLog4Net();
            _logger = kernel.Get<ILoggerFactory>().GetCurrentClassLogger();
            _service = kernel.Get<Service>();
            var serviceAwaiter = _service.Run().ConfigureAwait(false);
            serviceAwaiter.GetAwaiter().GetResult();
            _service.Dispose();
        }

        private static void SetupProcessEvents()
        {
            var currentProcess = Process.GetCurrentProcess();
            currentProcess.Exited += CurrentDomain_ProcessExit;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_ProcessExit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            _logger.Fatal("Unhandled Exception", ex);
            Console.WriteLine($"Unhandled Exception:{Environment.NewLine}{ex?.Message}");
            Environment.Exit(1);
        }
        internal static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("Received a Close");
            _service?.Stop();
        }

        private static void SetupLog4Net()
        {
            const string file = "log4net.config";

            if (!File.Exists(file))
            {
                Console.WriteLine($"Unable to find log4net.config");
                return;
            }

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo(file));
        }
    }
}
