using ViCellBlu;
using Opc.Ua;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    public interface ILockManager
    {
        ServiceResult HandleRequestLockRequest(NodeId sessionId, ref VcbResultRequestLock methodResult);

        ServiceResult HandleReleaseLockRequest(NodeId sessionId, ref VcbResultReleaseLock methodResult);
    }
}