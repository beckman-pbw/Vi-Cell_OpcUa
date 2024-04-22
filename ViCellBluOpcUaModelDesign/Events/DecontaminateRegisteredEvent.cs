using System;
using AutoMapper;
using ViCellBlu;
using GrpcClient.Interfaces;
using GrpcService;
using Ninject.Extensions.Logging;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign.Events
{
    public class DecontaminateStatusRegisteredEvent : OpcRegisteredEvent<DecontaminateStatusEvent>
    {
        private readonly ILogger _logger;

        public DecontaminateStatusRegisteredEvent(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client,
            nodeService, nodeState)
        {
            _logger = logger;
        }

        protected override void OnMessage(DecontaminateStatusEvent msg)
        {
            try
            {
	            _logger.Debug($"{nameof(DecontaminateStatusEvent)} status: {msg.Status}");

				var message = msg.Status.ToString();

                var eventState = new DecontaminateStatusEventState(NodeService.RootFolderState);
                NodeService.InitEventState(eventState, NodeState, nameof(DecontaminateStatusEvent), message, (uint)EventSeverity.Low);

                var reqStat = new DecontaminateStatus()
                {
					Status = (ViCellBlu.DecontaminateStatusEnum)msg.Status,
				};

                eventState.DecontaminateStatus = new PropertyState<DecontaminateStatus>(NodeService.RootFolderState);
                eventState.DecontaminateStatus.Value = reqStat;
                NodeService.ReportEvent(eventState);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error OnMessage({nameof(DecontaminateStatusEvent)})");
            }
        }

        public override void Register()
        {
			StreamingCall = Client.ServicesClient.SubscribeDecontaminateStatus(
				MakeRequest(TopicTypeEnum.PrimeReagentsStatusType), MakeCallOptions());
			Client.AddRegisteredEvent(this);
		}
    }

}
