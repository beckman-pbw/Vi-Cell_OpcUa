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
    public class CleanFluidicsStatusRegisteredEvent : OpcRegisteredEvent<CleanFluidicsStatusEvent>
    {
        private readonly ILogger _logger;

        public CleanFluidicsStatusRegisteredEvent(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client,
            nodeService, nodeState)
        {
            _logger = logger;
        }

        protected override void OnMessage(CleanFluidicsStatusEvent evt)
        {
            try
            {
                _logger.Debug("CleanFluidicsStatusEvent evt: " + evt.Status);                            

                var message = evt.Status.ToString();

				var eventState = new CleanFluidicsStatusEventState(NodeService.RootFolderState);
				NodeService.InitEventState(eventState, NodeState, nameof(CleanFluidicsStatusEvent), message, (uint)EventSeverity.Low);

				var reqStat = new CleanFluidicsStatus()
                {
	                Status = (ViCellBlu.CleanFluidicsStatusEnum)evt.Status
				};

				eventState.CleanFluidicsStatus = new PropertyState<CleanFluidicsStatus>(NodeService.RootFolderState);
				eventState.CleanFluidicsStatus.Value = reqStat;
				NodeService.ReportEvent(eventState);
			}
            catch (Exception e)
            {
                _logger.Error(e, $"Error OnMessage({nameof(CleanFluidicsStatusEventState)})");
            }
        }

        public override void Register()
        {
            StreamingCall = Client.ServicesClient.SubscribeCleanFluidicsStatus(
                MakeRequest(TopicTypeEnum.CleanFluidicsStatusType), MakeCallOptions());
            Client.AddRegisteredEvent(this);
        }
    }

}
