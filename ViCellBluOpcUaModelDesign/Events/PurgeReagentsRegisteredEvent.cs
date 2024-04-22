using System;
using AutoMapper;
using ViCellBlu;
using GrpcClient.Interfaces;
using GrpcService;
using Ninject.Extensions.Logging;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Events;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign.Events
{
    public class PurgeReagentsStatusRegisteredEvent : OpcRegisteredEvent<PurgeReagentsStatusEvent>
    {
        private readonly ILogger _logger;

        public PurgeReagentsStatusRegisteredEvent(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client,
            nodeService, nodeState)
        {
            _logger = logger;
        }

        protected override void OnMessage(PurgeReagentsStatusEvent msg)
        {
            try
            {
	            _logger.Debug($"{nameof(PurgeReagentsStatusEvent)} status: {msg.Status}");

				var message = msg.Status.ToString();

                var eventState = new PurgeReagentsStatusEventState(NodeService.RootFolderState);
                NodeService.InitEventState(eventState, NodeState, nameof(PurgeReagentsStatusEvent), message, (uint)EventSeverity.Low);

                var reqStat = new PurgeReagentsStatus()
                {
					Status = (ViCellBlu.PurgeReagentsStatusEnum)msg.Status,
				};

                eventState.PurgeReagentsStatus = new PropertyState<PurgeReagentsStatus>(NodeService.RootFolderState);
                eventState.PurgeReagentsStatus.Value = reqStat;
                NodeService.ReportEvent(eventState);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error OnMessage({nameof(PurgeReagentsStatusEvent)})");
            }
        }

        public override void Register()
        {
			StreamingCall = Client.ServicesClient.SubscribePurgeReagentsStatus(
				MakeRequest(TopicTypeEnum.PrimeReagentsStatusType), MakeCallOptions());
			Client.AddRegisteredEvent(this);
		}
    }

}
