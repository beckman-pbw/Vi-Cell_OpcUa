# Implementing OPC/UA Events in the Server {#implmenting_opc_ua_events_in_the_server}

The event framework supports OPC/UA events and variables. In both cases, the OPC/UA server must
request that an event be sent from the Scout process via gRPC and then make the appropriate OPC/UA
calls when receiving that event. If the OPC/UA client disconnects, the framework ensures that the
connections and resources are correctly closed and reclaimed.

## Design

The [BecOpcUaUser](@ref ViCellBluOpcUaModelDesign.ViCellBluManagement.BecOpcUaUser) maintains a list of IRegisteredEvents,
a base interface that allows the registration and removal (via Dispose()) of the request for events
from the gRPC server in the ScoutX process.

![Add the Hawkeye OPC/UA Server](Images/RegisteredEventManagement.png)

Derived classes of the EventProcessor implement the connection to Reactive subjects on the gRPC server.
You can read more about how [OPC/UA events are implemented on the server](@ref opcua_events). When the
GrpcClient on the OpcUaServer process makes the gRPC call to the ScoutX process an output stream is
kept open from the ScoutX process back to the OpcUaServer process. When a Reactive event occurs on the
server, it adds a message to a queue, which is then written to the stream. A derived class of
[OpcRegisteredEvent](@ref ViCellBluOpcUaModelDesign.Events.OpcRegisteredEvent), such as
[LockStateRegisteredEvent](@ref ViCellBluOpcUaModelDesign.Events.LockStateRegisteredEvent) has its
OnMessage() method called when a message is received.

```csharp
public class LockStateRegisteredEvent : OpcRegisteredEvent<LockStateChangedEvent>
{

    public LockStateRegisteredEvent(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client, nodeService, nodeState)
    {
    }

    public override void Register()
    {
        StreamingCall = Client.ServicesClient.SubscribeLockState(MakeRequest(TopicType.LockStateChanged), MakeCallOptions());
        Client.AddRegisteredEvent(this);
    }

    protected override void OnMessage(LockStateChangedEvent msg)
    {
        var eventState = new LockStateChangedEventState(NodeService.RootFolderState);

        // ToDo: Use language resources
        var lockedDesc = "locked";
        var unlockedDesc = "unlocked";
        var lockStateDesc = msg.LockState == GrpcService.LockStateEnum.Locked ? lockedDesc : unlockedDesc;
        NodeService.InitEventState(eventState, NodeState, "AutomationLockStateChanged", $"The automation lock has been set to {lockStateDesc}", 501);

        // Assign lock state to event
        eventState.LockState = new PropertyState<LockStateEnum>(eventState)
        {
            Value = Mapper.Map<ViCellBlu.LockStateEnum>(msg.LockState)
        };

        NodeService.ReportEvent(eventState);
    }
}
```
The OnMessage() is responsible for constructing an Event message based on any data that was part of the message.
It initializes the OPC event with the InitEventState() method and then submits the event for broadcast with
the ReportEvent() method.

These methods are provided by the INodeService interface, whose implementation is
a wrapper around the BecNodeManager class. The INodeService provides the ability to mock the OPC server
infrastructure in a test.

The Register() method in the above class makes the desired gRPC interface call to set up the stream. The base
RegisteredEvent class creates a thread to read from the stream and calls the OnMessage() method when it recieves
one. Implementing a new event for either an OPC event or variable update is as simple as implementing these two
methods.


