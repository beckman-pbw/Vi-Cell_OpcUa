using ViCellBluOpcUaModelDesign.Enums;

namespace ViCellBluOpcUaModelDesign.Events
{
    public class OpcServerStatusChangedEventArgs : System.EventArgs
    {
        public ServerStatus ServerStatus { get; set; }

        public OpcServerStatusChangedEventArgs(ServerStatus status)
        {
            ServerStatus = status;
        }
    }
}