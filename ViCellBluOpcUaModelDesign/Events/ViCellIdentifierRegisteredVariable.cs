using AutoMapper;
using GrpcClient.Interfaces;
using GrpcService;
using Ninject.Extensions.Logging;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign.Events
{
    public class ViCellIdentifierRegisteredVariable : OpcRegisteredEvent<ViCellIdentifierChangedEvent>
    {
        public ViCellIdentifierRegisteredVariable(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client, nodeService, nodeState)
        {
        }

        public override void Register()
        {
            StreamingCall = Client.ServicesClient.SubscribeViCellIdentifier(MakeRequest(TopicTypeEnum.ViCellIdentifierChangedType), MakeCallOptions());
            Client.AddRegisteredEvent(this);
        }

        protected override void OnMessage(ViCellIdentifierChangedEvent msg)
        {
            NodeService.UpdateVariable(NodeState, msg.ViCellIdentifier);
        }
	}
}