using AutoMapper;
using GrpcClient.Interfaces;
using GrpcService;
using Ninject.Extensions.Logging;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign.Events
{
    public class ViCellStatusRegisteredVariable : OpcRegisteredEvent<ViCellStatusChangedEvent>
    {
        public ViCellStatusRegisteredVariable(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client, nodeService, nodeState)
        {
        }

        public override void Register()
        {
            StreamingCall = Client.ServicesClient.SubscribeViCellStatus(MakeRequest(TopicTypeEnum.ViCellStatusChangedType), MakeCallOptions());
            Client.AddRegisteredEvent(this);
        }

        protected override void OnMessage(ViCellStatusChangedEvent msg)
        {
            NodeService.UpdateVariable(NodeState, Mapper.Map<ViCellBlu.ViCellStatusEnum>(msg.ViCellStatus));
        }
    }
}