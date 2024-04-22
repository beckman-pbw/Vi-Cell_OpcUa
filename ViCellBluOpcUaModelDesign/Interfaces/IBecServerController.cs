using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViCellBluOpcUaModelDesign.Enums;
using ViCellBluOpcUaModelDesign.Events;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    public interface IBecServerController : IDisposable
    {
        ServerStatus ServerStatus { get; }
        EventHandler<OpcServerStatusChangedEventArgs> ServerStatusChanged { get; set; }
        IEnumerable<Uri> EndpointAddresses { get; }

        Task StartServerAsync();
        void StopServer();
    }
}