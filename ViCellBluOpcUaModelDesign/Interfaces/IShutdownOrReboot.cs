using Opc.Ua;
using ViCellBlu;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    public interface IShutdownOrReboot
    {
        ServiceResult HandleRequestShutdownOrReboot(NodeId sessionId, ShutdownOrRebootEnum operation, ref VcbResult methodResult);
    }
}
