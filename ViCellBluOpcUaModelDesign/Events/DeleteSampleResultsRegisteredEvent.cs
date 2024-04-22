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
    public class DeleteSampleResultsRegisteredEvent : OpcRegisteredEvent<DeleteSampleResultsProgressEvent>
    {
        private readonly ILogger _logger;

        public DeleteSampleResultsRegisteredEvent(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState)
	        : base(logger, mapper, client, nodeService, nodeState)
        {
            _logger = logger;
        }

        protected override void OnMessage(DeleteSampleResultsProgressEvent msg)
        {
            try
            {
                var eventState = new ViCellBlu.DeleteSampleResultsProgressEventState(NodeService.RootFolderState);
                var message = $"Delete Sample Results Complete: Status: '{msg.DeleteSampleResultsArgs.DeleteStatus}', Percent Complete: '{msg.DeleteSampleResultsArgs.PercentComplete}'";
                NodeService.InitEventState(eventState, NodeState, nameof(DeleteSampleResultsProgressEvent),
                    message, (uint)EventSeverity.Low);


                var reqStat = new DeleteSampleStatus()
                {
                    DeleteStatus = (ViCellBlu.DeleteStatusEnum)msg.DeleteSampleResultsArgs.DeleteStatus,
                    DeletePercent= (uint)msg.DeleteSampleResultsArgs.PercentComplete
                };
                eventState.DeleteStatusInfo = new PropertyState<DeleteSampleStatus>(NodeService.RootFolderState);
                eventState.DeleteStatusInfo.Value = reqStat;

                NodeService.ReportEvent(eventState);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error OnMessage({nameof(DeleteSampleResultsProgressEvent)})");
            }
        }

        public override void Register()
        {
            StreamingCall = Client.ServicesClient.SubscribeDeleteSampleResultsProgress(
                MakeRequest(TopicTypeEnum.DeleteSampleResultsProgressType), MakeCallOptions());
            Client.AddRegisteredEvent(this);
        }
    }
}