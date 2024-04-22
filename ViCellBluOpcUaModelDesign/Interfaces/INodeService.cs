using ViCellBlu;
using Opc.Ua;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    /// <summary>
    /// Provides support methods for setting OPC node values and creating OPC events.
    /// The implementation is a wrapper around BecNodeManager. This is injected into the
    /// OpcRegisteredEvent. Its methods are called inside the OnMessage() handler.
    /// </summary>
    public interface INodeService
    {
        /// <summary>
        /// Initializes base event. The calling method is responsible for populating any additional
        /// properties extracted from the gRPC event message, prior to call the ReportEvent() helper method.
        /// </summary>
        /// <typeparam name="TE"></typeparam>
        /// <param name="eventState">An object of a class than extends BecEventState</param>
        /// <param name="nodeState"></param>
        /// <param name="eventName"></param>
        /// <param name="eventMessage"></param>
        /// <param name="severity"></param>
        void InitEventState(BecEventState eventState, NodeState nodeState, string eventName, string eventMessage, uint severity);

        /// <summary>
        /// Helper method for sending out the OPC event using the OnMessage method for this RegisteredEvent.
        /// </summary>
        /// <param name="eventState">The OPC event type to generate the OPC event..</param>
        void ReportEvent(BecEventState eventState);

        /// <summary>
        /// Helper method for setting the value of a node using the NodeManager UpdateVariable method on the node associated with this RegisteredEvent.
        /// </summary>
        /// <param name="nodeState">Identifies the node to be updated.</param>
        /// <param name="newValue">New value to be assigned to the OPC node associated with this instance.</param>
        void UpdateVariable(NodeState nodeState, object newValue);

        NodeState RootFolderState { get; }
    }
}