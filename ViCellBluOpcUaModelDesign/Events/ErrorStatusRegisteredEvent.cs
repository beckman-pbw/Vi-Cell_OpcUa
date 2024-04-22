using System;
using AutoMapper;
using GrpcClient.Interfaces;
using GrpcService;
using Ninject.Extensions.Logging;
using Opc.Ua;
using ViCellBlu;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign.Events
{
	public class ErrorStatusRegisteredEvent : OpcRegisteredEvent<ErrorStatusEvent>
	{
		private readonly ILogger _logger;

		public ErrorStatusRegisteredEvent(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client, nodeService, nodeState)
		{
			_logger = logger;
		}

		public override void Register()
		{
			StreamingCall = Client.ServicesClient.SubscribeErrorStatus(MakeRequest(TopicTypeEnum.ErrorStatusChangedType), MakeCallOptions());
			Client.AddRegisteredEvent(this);
		}

		protected override void OnMessage(ErrorStatusEvent msg)
		{
			try
			{
				var eventDesc = $"Status: '{msg.Status}'";

				var eventState = new ErrorStatusEventState(NodeService.RootFolderState);
				NodeService.InitEventState(eventState, NodeState, nameof(ErrorStatusEvent), eventDesc, (uint)EventSeverity.MediumHigh);

				var data = Mapper.Map<ViCellBlu.ErrorStatusType>(msg.Status);
				eventState.Status = new PropertyState<ViCellBlu.ErrorStatusType>(eventState)
				{
					Value = data
				};

				NodeService.ReportEvent(eventState);
			}
			catch (Exception e)
			{
				_logger.Error(e, $"Error OnMessage(ErrorStatusEvent)");
			}
		}
	}
}
