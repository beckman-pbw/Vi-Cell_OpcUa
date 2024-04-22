using Opc.Ua;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    public interface IAddBehaviorToPredefinedNodeService
    {
        NodeState GetActiveNode(ISystemContext context, NodeState predefinedNode);
    }
}