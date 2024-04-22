using AutoMapper;
using GrpcClient.Interfaces;
using GrpcService;
using Ninject.Extensions.Logging;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Events;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign.Events
{
    public class WasteTubeCapacityRegisteredVariable : OpcRegisteredEvent<WasteTubeCapacityChangedEvent>
    {
        public WasteTubeCapacityRegisteredVariable(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client, nodeService, nodeState)
        {
        }

        public override void Register()
        {
            StreamingCall = Client.ServicesClient.SubscribeWasteTubeCapacity(MakeRequest(TopicTypeEnum.WasteTubeCapacityChangedType), MakeCallOptions());
            Client.AddRegisteredEvent(this);
        }

        protected override void OnMessage(WasteTubeCapacityChangedEvent msg)
        {
            NodeService.UpdateVariable(NodeState, msg.WasteTubeRemainingCapacity);
        }
    }
}