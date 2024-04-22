using AutoMapper;
using GrpcClient.Interfaces;
using GrpcService;
using Ninject.Extensions.Logging;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign.Events
{
    public class ReagentUseRemainingRegisteredVariable : OpcRegisteredEvent<ReagentUsesRemainingChangedEvent>
    {
        public ReagentUseRemainingRegisteredVariable(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client, nodeService, nodeState)
        {
        }

        public override void Register()
        {
            StreamingCall = Client.ServicesClient.SubscribeReagentUsesRemaining(MakeRequest(TopicTypeEnum.ReagentUsesRemainingChangedType), MakeCallOptions());
            Client.AddRegisteredEvent(this);
        }

        protected override void OnMessage(ReagentUsesRemainingChangedEvent msg)
        {
            NodeService.UpdateVariable(NodeState, msg.ReagentUsesRemaining);
        }
    }
}