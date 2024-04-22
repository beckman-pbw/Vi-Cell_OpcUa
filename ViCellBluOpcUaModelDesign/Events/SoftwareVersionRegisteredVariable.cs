using AutoMapper;
using GrpcClient.Interfaces;
using GrpcService;
using Ninject.Extensions.Logging;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign.Events
{
	public class SoftwareVersionRegisteredVariable : OpcRegisteredEvent<SoftwareVersionChangedEvent>
	{
		public SoftwareVersionRegisteredVariable(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client, nodeService, nodeState)
		{
		}

		public override void Register()
		{
			StreamingCall = Client.ServicesClient.SubscribeSoftwareVersion(MakeRequest(TopicTypeEnum.SoftwareVersionChangedType), MakeCallOptions());
			Client.AddRegisteredEvent(this);
		}

		protected override void OnMessage(SoftwareVersionChangedEvent msg)
		{
			NodeService.UpdateVariable(NodeState, msg.Version);
		}
	}
}

