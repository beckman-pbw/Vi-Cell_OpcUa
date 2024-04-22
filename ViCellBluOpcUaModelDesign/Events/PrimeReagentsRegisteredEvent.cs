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
    public class PrimeReagentsStatusRegisteredEvent : OpcRegisteredEvent<PrimeReagentsStatusEvent>
    {
        private readonly ILogger _logger;

        public PrimeReagentsStatusRegisteredEvent(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client,
            nodeService, nodeState)
        {
            _logger = logger;
        }

        protected override void OnMessage(PrimeReagentsStatusEvent msg)
        {
            try
            {
                _logger.Debug($"{nameof(PrimeReagentsStatusEvent)} status: {msg.Status}");

                var message = msg.Status.ToString();

                var eventState = new PrimeReagentsStatusEventState(NodeService.RootFolderState);
                NodeService.InitEventState(eventState, NodeState, nameof(PrimeReagentsStatusEvent), message, (uint)EventSeverity.Low);

                var reqStat = new PrimeReagentsStatus()
                {
					Status = (ViCellBlu.PrimeReagentsStatusEnum)msg.Status,
				};

                eventState.PrimeReagentsStatus = new PropertyState<PrimeReagentsStatus>(NodeService.RootFolderState);
                eventState.PrimeReagentsStatus.Value = reqStat;
                NodeService.ReportEvent(eventState);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error OnMessage({nameof(PrimeReagentsStatusEvent)})");
            }
        }

        public override void Register()
        {
			StreamingCall = Client.ServicesClient.SubscribePrimeReagentsStatus(
				MakeRequest(TopicTypeEnum.PrimeReagentsStatusType), MakeCallOptions());
			Client.AddRegisteredEvent(this);
		}
    }

}
