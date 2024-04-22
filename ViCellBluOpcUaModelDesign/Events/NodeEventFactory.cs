using System.Collections.Generic;
using GrpcClient.Interfaces;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;

namespace ViCellBluOpcUaModelDesign.Events
{
    internal delegate IRegisteredEvent MakeRegisteredEvent(IGrpcClient client, NodeState nodeState);

	/// <summary>
	/// Factory for gRPC registered events indexed by their OPC/UA NodeId. Leverages Ninject to inject the logger and node manager into each registered event instance.
	/// For each supported NodeState that events can be requested, an entry for its <a href="@ref ViCellBluOpcUaModelDesign.OpcRegisteredEvent">OpcRegisteredEvent</a>
	/// needs to be added to the <a href="@ref ViCellBluOpcUaModelDesign.BecOpcUaModule">Ninject module for OPC/UA</a>
	/// </summary>
	public class NodeEventFactory
    {
        private BecNodeManager _nodeManager;
        private readonly Dictionary<NodeId, MakeRegisteredEvent> _registeredEvents;

        public NodeEventFactory(BecNodeManager nodeManager, IRegisteredEventFactory registeredEventFactory)
        {
            _nodeManager = nodeManager;
            var beckmanNamespaceIndex = nodeManager.LookupNamespaceIndex(ViCellBlu.Namespaces.ViCellBlu);
            _registeredEvents = new Dictionary<NodeId, MakeRegisteredEvent>
            {
                // Add new event types here.
                {new NodeId(ViCellBlu.Variables.ViCellBluState_LockState, beckmanNamespaceIndex ), registeredEventFactory.CreateLockStateRegisteredVariable},
                {new NodeId(ViCellBlu.Variables.ViCellBluState_ViCellStatus, beckmanNamespaceIndex ), registeredEventFactory.CreateViCellStatusRegisteredVariable},
                {new NodeId(ViCellBlu.Variables.ViCellBluState_ViCellIdentifier, beckmanNamespaceIndex ), registeredEventFactory.CreateViCellIdentifierRegisteredVariable},
                {new NodeId(ViCellBlu.Variables.ViCellBluState_ReagentUsesRemaining, beckmanNamespaceIndex ), registeredEventFactory.CreateReagentUseRemainingRegisteredVariable},
                {new NodeId(ViCellBlu.Variables.ViCellBluState_WasteTubeRemainingCapacity, beckmanNamespaceIndex ), registeredEventFactory.CreateWasteTubeCapacityRegisteredVariable},
				{new NodeId(ViCellBlu.Variables.ViCellBluState_SoftwareVersion, beckmanNamespaceIndex ), registeredEventFactory.CreateSoftwareVersionRegisteredVariable},
				{new NodeId(ViCellBlu.Variables.ViCellBluState_FirmwareVersion, beckmanNamespaceIndex ), registeredEventFactory.CreateFirmwareVersionRegisteredVariable},
			};
        }

        public IRegisteredEvent CreateRegisteredEvent(IGrpcClient client, NodeState nodeState)
        {
            return _registeredEvents.TryGetValue(nodeState.NodeId, out var registeredEventCreator) ? registeredEventCreator.Invoke(client, nodeState) : null;
        }
    }
}