using Ninject.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ViCellBluOpcUaModelDesign.Enums;
using ViCellBluOpcUaModelDesign.Events;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellOpcUaServer
{
    public class Service : IDisposable
    {
        #region Constructor

        public Service(IBecServerController becServerController, ILogger logger)
        {
            _logger = logger;
            _becController = becServerController;
            _becController.ServerStatusChanged += ServerStatusChanged;
        }

        public void Dispose()
        {
            _becController.ServerStatusChanged -= ServerStatusChanged;
            _becController?.Dispose();
        }

        #endregion

        #region Properties & Fields

        private readonly IBecServerController _becController;
        private readonly ILogger _logger;
        private bool _running;

        #endregion

        #region Event Handlers

        private void ServerStatusChanged(object sender, OpcServerStatusChangedEventArgs args)
        {
            _logger.Info($"OPC-UA Server Status changed to '{args.ServerStatus}'");
            if (args.ServerStatus == ServerStatus.Started)
            {
                var address = _becController.EndpointAddresses.First(a => a.ToString().StartsWith("opc.tcp"));
                _logger.Debug($"OPC-UA Server Address: {address}");
            }
        }

        #endregion

        public void Stop()
        {
            if (_running)
            {
                _becController.StopServer();
                _running = false;
                _logger.Info($"OPC-UA server stopped");
            }
		}

        public async Task Run()
        {
            _running = true;
            _logger.Debug("Starting OPC-UA server...");
            await _becController.StartServerAsync();
            
            while (_running)
            {
                //Prevents excess CPU usage when there's no console
                Thread.Sleep(125);

                Console.Write("> ");
                var line = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrEmpty(line) || line.Equals("help"))
                {
                    Console.WriteLine($"Type 'exit' to close console app.{Environment.NewLine}" +
                                      $"Type 'stop' to stop OPC UA Server.{Environment.NewLine}" +
                                      $"Type 'start' to start OPC UA Server.{Environment.NewLine}");
                    continue;
                }

                if (line.Equals("exit"))
                {
                    Stop();
                }

                if (line.Equals("stop"))
                {
                    Stop();
                }

                if (line.Equals("start"))
                {
                    await _becController.StartServerAsync();
                }
            }
        }
    }
}