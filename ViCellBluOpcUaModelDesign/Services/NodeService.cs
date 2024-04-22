using ViCellBlu;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;

namespace ViCellBluOpcUaModelDesign.Services
{
    public class NodeService : INodeService
    {
        private readonly BecNodeManager _nodeManager;

        public NodeService(BecNodeManager nodeManager)
        {
            _nodeManager = nodeManager;
        }

        public void InitEventState(BecEventState eventState, NodeState nodeState, string eventName, string eventMessage, uint severity)
        {
            eventState.Initialize(_nodeManager.SystemContext, nodeState, (EventSeverity)severity, eventMessage);
            eventState.SetChildValue(_nodeManager.SystemContext, Opc.Ua.BrowseNames.SourceName, eventName, false);
            eventState.SetChildValue(_nodeManager.SystemContext, Opc.Ua.BrowseNames.SourceNode, Opc.Ua.ObjectIds.Server, false);
            eventState.SetChildValue(_nodeManager.SystemContext, Opc.Ua.BrowseNames.BrowsePath, nodeState.BrowseName + Constants.EventNodeBrowseSuffix, false);
        }

        public void ReportEvent(BecEventState eventState)
        {
            _nodeManager?.Server?.ReportEvent(eventState);
        }

        public void UpdateVariable(NodeState nodeState, object newValue)
        {
            _nodeManager.UpdateVariable(nodeState, newValue);
        }

        public NodeState RootFolderState => _nodeManager.RootFolderState;
    }
}