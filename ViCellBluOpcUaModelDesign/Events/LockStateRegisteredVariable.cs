using AutoMapper;
using GrpcClient.Interfaces;
using GrpcService;
using Ninject.Extensions.Logging;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign.Events
{
    public class LockStateRegisteredVariable : OpcRegisteredEvent<LockStateChangedEvent>
    {
        public LockStateRegisteredVariable(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client, nodeService, nodeState)
        {
        }

        public override void Register()
        {
            StreamingCall = Client.ServicesClient.SubscribeLockState(MakeRequest(TopicTypeEnum.LockStateChangedType), MakeCallOptions());
            Client.AddRegisteredEvent(this);
        }

        protected override void OnMessage(LockStateChangedEvent msg)
        {
            NodeService.UpdateVariable(NodeState, Mapper.Map<ViCellBlu.LockStateEnum>(msg.LockState));
        }
    }
}