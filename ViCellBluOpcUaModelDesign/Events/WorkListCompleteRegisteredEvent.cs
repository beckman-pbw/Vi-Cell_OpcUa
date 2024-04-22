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
    public class WorkListCompleteRegisteredEvent : OpcRegisteredEvent<WorkListCompleteEvent>
    {
        private readonly ILogger _logger;

        public WorkListCompleteRegisteredEvent(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client,
            nodeService, nodeState)
        {
            _logger = logger;
        }

        public override void Register()
        {
            StreamingCall = Client.ServicesClient.SubscribeWorkListComplete(MakeRequest(TopicTypeEnum.WorkListCompleteType), MakeCallOptions());
            Client.AddRegisteredEvent(this);
        }

        protected override void OnMessage(WorkListCompleteEvent msg)
        {
            try
            {
                var eventState = new WorkListCompleteEventState(NodeService.RootFolderState);

                var message = $"WorkList Complete: '{msg.SampleDataUuidList}'";
                NodeService.InitEventState(eventState, NodeState, nameof(WorkListCompleteEvent),
                    message, 501);

                var map = Mapper.Map<Guid[]>(msg.SampleDataUuidList);
                eventState.SampleDataUuidList = new PropertyState<Guid[]>(eventState)
                {
                    Value = map
                };

                NodeService.ReportEvent(eventState);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error OnMessage(WorkListCompleteEvent)");
            }
        }
    }
}