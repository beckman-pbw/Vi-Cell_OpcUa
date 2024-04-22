using System;
using System.Linq;
using System.Threading.Tasks;
using ViCellBluOpcUaModelDesign.Enums;
using ViCellBluOpcUaModelDesign.Events;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace OpcUaIntegrationTest
{
    public class EmbeddedService : IDisposable
    {
        #region Constructor

        public EmbeddedService(IBecServerController becServerController)
        {
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
        private bool _running;

        #endregion

        #region Event Handlers

        private void ServerStatusChanged(object sender, OpcServerStatusChangedEventArgs args)
        {
            if (args.ServerStatus == ServerStatus.Started)
            {
                var address = _becController.EndpointAddresses.First(a => a.ToString().StartsWith("opc.tcp"));
            }
        }

        #endregion

        public void Stop()
        {
            if (_running)
            {
                _becController.StopServer();
                _running = false;
            }
        }

        public async Task Run()
        {
            await _becController.StartServerAsync();
        }
    }
}