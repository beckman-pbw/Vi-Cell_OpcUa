using AutoMapper;
using GrpcClient.Interfaces;
using GrpcService;
using Ninject.Extensions.Logging;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign.Events
{
	public class FirmwareVersionRegisteredVariable : OpcRegisteredEvent<FirmwareVersionChangedEvent>
	{
		public FirmwareVersionRegisteredVariable(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client, nodeService, nodeState)
		{
		}

		public override void Register()
		{
			StreamingCall = Client.ServicesClient.SubscribeFirmwareVersion(MakeRequest(TopicTypeEnum.FirmwareVersionChangedType), MakeCallOptions());
			Client.AddRegisteredEvent(this);
		}

		protected override void OnMessage(FirmwareVersionChangedEvent msg)
		{
			NodeService.UpdateVariable(NodeState, msg.Version);
		}
	}
}

