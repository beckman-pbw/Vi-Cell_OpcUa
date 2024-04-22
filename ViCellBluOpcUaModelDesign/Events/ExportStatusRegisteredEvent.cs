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
    public class ExportStatusRegisteredEvent : OpcRegisteredEvent<ExportStatusEvent>
    {
        private readonly ILogger _logger;

        public ExportStatusRegisteredEvent(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client,
            nodeService, nodeState)
        {
            _logger = logger;
        }

        protected override void OnMessage(ExportStatusEvent msg)
        {
            try
            {
                _logger.Debug("ExportStatusEvent msg" + msg.StatusInfo.Status);                            

                var eventDesc = msg.StatusInfo.Status.ToString();
                eventDesc += "," + msg.StatusInfo.Percent;
                eventDesc += "," + msg.StatusInfo.BulkDataId;

                var eventState = new ExportStatusEventState(NodeService.RootFolderState);
                NodeService.InitEventState(eventState, NodeState, nameof(ExportStatusEvent), eventDesc, (uint)EventSeverity.Low);

                var reqStat = new ViCellBlu.ExportStatusData()
                {
                    BulkDataId = msg.StatusInfo.BulkDataId,
                    ExportStatus = (ViCellBlu.ExportStatusEnum)msg.StatusInfo.Status,
                    ExportPercent = msg.StatusInfo.Percent
                };
                eventState.ExportStatusData = new PropertyState<ViCellBlu.ExportStatusData>(NodeService.RootFolderState);
                eventState.ExportStatusData.Value = reqStat;

                NodeService.ReportEvent(eventState);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error OnMessage({nameof(ExportStatusEventState)})");
            }
        }

        public override void Register()
        {
            StreamingCall = Client.ServicesClient.SubscribeExportStatus(
                MakeRequest(TopicTypeEnum.ExportStatusProgressType), MakeCallOptions());
            Client.AddRegisteredEvent(this);
        }
    }

}
