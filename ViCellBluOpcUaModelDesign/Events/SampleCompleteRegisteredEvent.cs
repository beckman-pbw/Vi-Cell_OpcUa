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
	public class SampleCompleteRegisteredEvent : OpcRegisteredEvent<SampleCompleteEvent>
    {
        private readonly ILogger _logger;

        public SampleCompleteRegisteredEvent(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, mapper, client,
            nodeService, nodeState)
        {
            _logger = logger;
        }

        public override void Register()
        {
            StreamingCall = Client.ServicesClient.SubscribeSampleComplete(
                MakeRequest(TopicTypeEnum.SampleCompleteType), MakeCallOptions());
            Client.AddRegisteredEvent(this);
        }

        protected override void OnMessage(SampleCompleteEvent msg)
        {
            try
            {
                // Only send to the same user that started the sample
                if (!Client.ClientCredentialHelper.Username.Equals(msg.SampleResultData.AnalysisBy))
                    return;

                var message = $"Sample Complete '{msg.SampleResultData.Status}' for '{msg.SampleResultData.SampleId}'";

                var eventState = new SampleCompleteEventState(NodeService.RootFolderState);
                NodeService.InitEventState(eventState, NodeState, nameof(SampleCompleteEvent), message, (uint)EventSeverity.Low);

                var map = Mapper.Map<ViCellBlu.SampleResult>(msg.SampleResultData);
                eventState.SampleResult = new PropertyState<ViCellBlu.SampleResult>(eventState)
                {
                    Value = map
                };

                NodeService.ReportEvent(eventState);             
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error OnMessage(SampleCompleteEvent)");
            }
        }
    }

}
