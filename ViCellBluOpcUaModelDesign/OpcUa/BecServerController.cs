using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViCellBluOpcUaModelDesign.Enums;
using ViCellBluOpcUaModelDesign.Events;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;

namespace ViCellBluOpcUaModelDesign.ViCellBluManagement
{
    public class BecServerController : IBecServerController
    {
        #region Constructor

        public BecServerController(BecOpcServer becServer)
        {
            _becServer = becServer;
            _becServer.ServerStatusChanged += OnServerStatusChanged;
        }

        public void Dispose()
        {
            if (ServerStatus != ServerStatus.NotStarted)
            {
                StopServer();
            }

            _becServer.Dispose();
        }

        #endregion

        #region Properties & Fields

        private readonly BecOpcServer _becServer;
        public ServerStatus ServerStatus => _becServer?.ServerStatus ?? ServerStatus.Unknown;
        public EventHandler<OpcServerStatusChangedEventArgs> ServerStatusChanged { get; set; }
        public IEnumerable<Uri> EndpointAddresses => _becServer?.CurrentInstance.EndpointAddresses;

        #endregion

        #region Public Methods

        public async Task StartServerAsync()
        {
            if (_becServer != null) await _becServer.StartServerAsync();
        }

        public void StopServer()
        {
            _becServer?.StopServer();
        }

        #endregion

        #region Private Methods

        private void OnServerStatusChanged(object sender, OpcServerStatusChangedEventArgs e)
        {
            ServerStatusChanged?.Invoke(sender, e);
        }

        #endregion
    }
}