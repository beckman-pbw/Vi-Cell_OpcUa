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
    public class SampleStatusChangedRegisteredEvent : OpcRegisteredEvent<SampleStatusChangedEvent>
    {
        private readonly ILogger _logger;

        public SampleStatusChangedRegisteredEvent(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client,
            nodeService, nodeState)
        {
            _logger = logger;
        }

        public override void Register()
        {
            StreamingCall = Client.ServicesClient.SubscribeSampleStatus(
                MakeRequest(TopicTypeEnum.SampleStatusChangedType), MakeCallOptions());
            Client.AddRegisteredEvent(this);
        }

        protected override void OnMessage(SampleStatusChangedEvent msg)
        {
            try
            {
                var eventState = new SampleStatusChangedEventState(NodeService.RootFolderState);

                var eventDesc = $"Sample Status Changed to '{msg.SampleStatusData.SampleStatus}' for '{msg.SampleStatusData.SampleId}'";
                NodeService.InitEventState(eventState, NodeState, nameof(SampleStatusChangedEvent), eventDesc, (uint)EventSeverity.Medium);

                var map = Mapper.Map<ViCellBlu.SampleStatusData>(msg.SampleStatusData);
                eventState.SampleStatusData = new PropertyState<ViCellBlu.SampleStatusData>(eventState)
                {
                    Value = map
                };

                NodeService.ReportEvent(eventState);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error OnMessage(SampleStatusChangedEvent)");
            }
        }
    }
}