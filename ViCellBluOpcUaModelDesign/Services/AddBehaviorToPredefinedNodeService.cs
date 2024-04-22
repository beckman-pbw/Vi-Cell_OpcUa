using ViCellBlu;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Interfaces;
using ObjectTypes = ViCellBlu.ObjectTypes;

namespace ViCellBluOpcUaModelDesign.Services
{
    public class AddBehaviorToPredefinedNodeService : IAddBehaviorToPredefinedNodeService
    {
        private readonly IOpcUaFactory _opcUaFactory;

        public AddBehaviorToPredefinedNodeService(IOpcUaFactory opcUaFactory)
        {
            _opcUaFactory = opcUaFactory;
        }

        public NodeState GetActiveNode(ISystemContext context, NodeState predefinedNode)
        {
            if (!(predefinedNode is BaseObjectState passiveNode))
            {
                return predefinedNode;
            }

            var typeId = passiveNode.TypeDefinitionId;

            switch ((uint)typeId.Identifier)
            {
                case ObjectTypes.MethodFolderType:
                    if (passiveNode is MethodFolderState) break;

                    // we need to recreate the Method Folder to trigger MethodFolderState.OnAfterCreate
                    var activeMethodNode = _opcUaFactory.CreateMethodFolderState(passiveNode.Parent);
                    activeMethodNode.Create(context, passiveNode);
                    passiveNode.Parent?.ReplaceChild(context, activeMethodNode);
                    return activeMethodNode;
                case ObjectTypes.PlayControlFolderType:
                    if (passiveNode is MethodFolderState) break;

                    // we need to recreate the Method Folder to trigger MethodFolderState.OnAfterCreate
                    var activePlayControlNode = _opcUaFactory.CreatePlayControlFolderState(passiveNode.Parent);
                    activePlayControlNode.Create(context, passiveNode);
                    passiveNode.Parent?.ReplaceChild(context, activePlayControlNode);
                    return activePlayControlNode;
            }

            return predefinedNode;
        }
    }
}