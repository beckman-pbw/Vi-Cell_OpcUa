using System;
using GrpcClient;
using GrpcClient.Interfaces;
using GrpcService;
using Ninject.Extensions.Logging;
using ViCellBluOpcUaModelDesign.Events;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;

namespace ViCellBluOpcUaModelDesign.Services
{
    /// <summary>
    /// The OpcEventManager creates a default BecOpcUaUser with the bci_service user credentials.
    /// It registers to receive the necessary events (derived classes of OpcRegisteredEvent).
    /// Upon receiving an event, it generates an OPC event to be received by any clients.
    ///
    /// There is another EventManager class.
    /// </summary>
    public class OpcEventManager : IDisposable
    {
        private readonly ILogger _logger;
        private readonly BecNodeManager _nodeManager;
        private readonly IRegisteredEventFactory _registeredEventFactory;

        private WorkListCompleteRegisteredEvent _myWorkListCompleteRegisteredEvent = null;
        private SampleStatusChangedRegisteredEvent _mySampleStatusChangedRegisteredEvent = null;
        private SampleCompleteRegisteredEvent _mySampleCompleteRegisteredEvent = null;
        private DeleteSampleResultsRegisteredEvent _myDeleteSampleResultsRegisteredEvent = null;
        private ExportStatusRegisteredEvent _myExportStatusRegisteredEvent = null;
        private CleanFluidicsStatusRegisteredEvent _myCleanFluidicsStatusRegisteredEvent = null;
        private PrimeReagentsStatusRegisteredEvent _myPrimeReagentsStatusRegisteredEvent = null;
        private PurgeReagentsStatusRegisteredEvent _myPurgeReagentsStatusRegisteredEvent = null;
        private DecontaminateStatusRegisteredEvent _myDecontaminateStatusRegisteredEvent = null;
        private ErrorStatusRegisteredEvent _myErrorStatusRegisteredEvent = null;

		public OpcEventManager(ILogger logger, IOpcUaFactory opcUaFactory, IRegisteredEventFactory registeredEventFactory, BecNodeManager nodeManager)
        {
            _logger = logger;
            _nodeManager = nodeManager;
            _registeredEventFactory = registeredEventFactory;
        }

        /// <summary>
        /// Setup the registration and handling of all Scout events to generate OPC events.
        /// Initially clean up any existing service user and its registered events and
        /// resource. This allows this method to be called repeatedly in the event of
        /// lost connections. Create a new service user and register for all the
        /// events and alarms that this OPC/UA server supports. This is implemented
        /// with classes derived from OpcRegisteredEvent, and overriding the OnMessage()
        /// handler. The OnMessage() handler (currently UpdateValue()) calls the
        /// ReportEvent helper method.
        /// </summary>
        /// <returns></returns>
        public bool RegisterForScoutEvents(OpcUaGrpcClient client)
        {

            // The NodeId for the events are their object type ids. They are only used as a unique identifier.
            // The actual created events pass a null into the OPC API and are not used otherwise.

            // Add system events here.

            _myWorkListCompleteRegisteredEvent?.Dispose();
            _myWorkListCompleteRegisteredEvent = _registeredEventFactory.CreateWorkListCompletedRegisteredEvent
                (client, _nodeManager.FindNode(ViCellBlu.ObjectTypes.WorkListCompleteEvent));
            AddRegisteredEvent(_myWorkListCompleteRegisteredEvent, nameof(WorkListCompleteEvent), client);

            _mySampleStatusChangedRegisteredEvent?.Dispose();
            _mySampleStatusChangedRegisteredEvent = _registeredEventFactory.CreateSampleStatusChangedRegisteredEvent
                (client, _nodeManager.FindNode(ViCellBlu.ObjectTypes.SampleStatusChangedEvent));
            AddRegisteredEvent(_mySampleStatusChangedRegisteredEvent, nameof(SampleStatusChangedEvent), client);

            _mySampleCompleteRegisteredEvent?.Dispose();
            _mySampleCompleteRegisteredEvent = _registeredEventFactory.CreateSampleCompleteRegisteredEvent
                (client, _nodeManager.FindNode(ViCellBlu.ObjectTypes.SampleCompleteEvent));
            AddRegisteredEvent(_mySampleCompleteRegisteredEvent, nameof(SampleCompleteEvent), client);

            _myDeleteSampleResultsRegisteredEvent?.Dispose();
            _myDeleteSampleResultsRegisteredEvent = _registeredEventFactory.CreateDeleteSampleResultsCompletedRegisteredEvent
                    (client, _nodeManager.FindNode(ViCellBlu.ObjectTypes.DeleteSampleResultsProgressEvent));
            AddRegisteredEvent(_myDeleteSampleResultsRegisteredEvent, nameof(DeleteSampleResultsProgressEvent), client);

            _myExportStatusRegisteredEvent?.Dispose();
            _myExportStatusRegisteredEvent = _registeredEventFactory.CreateExportStatusRegisteredEvent
                (client, _nodeManager.FindNode(ViCellBlu.ObjectTypes.ExportStatusEvent));
            AddRegisteredEvent(_myExportStatusRegisteredEvent, nameof(ExportStatusEvent), client);

            _myCleanFluidicsStatusRegisteredEvent?.Dispose();
            _myCleanFluidicsStatusRegisteredEvent = _registeredEventFactory.CreateCleanFluidicsStatusRegisteredEvent
				(client, _nodeManager.FindNode(ViCellBlu.ObjectTypes.CleanFluidicsStatusEvent));
            AddRegisteredEvent(_myCleanFluidicsStatusRegisteredEvent, nameof(CleanFluidicsStatusEvent), client);

            _myPrimeReagentsStatusRegisteredEvent?.Dispose();
            _myPrimeReagentsStatusRegisteredEvent = _registeredEventFactory.CreatePrimeReagentsStatusRegisteredEvent
				(client, _nodeManager.FindNode(ViCellBlu.ObjectTypes.PrimeReagentsStatusEvent));
            AddRegisteredEvent(_myPrimeReagentsStatusRegisteredEvent, nameof(PrimeReagentsStatusEvent), client);

            _myPurgeReagentsStatusRegisteredEvent?.Dispose();
            _myPurgeReagentsStatusRegisteredEvent = _registeredEventFactory.CreatePurgeReagentsStatusRegisteredEvent
				(client, _nodeManager.FindNode(ViCellBlu.ObjectTypes.PurgeReagentsStatusEvent));
            AddRegisteredEvent(_myPurgeReagentsStatusRegisteredEvent, nameof(PurgeReagentsStatusEvent), client);

			_myDecontaminateStatusRegisteredEvent?.Dispose();
			_myDecontaminateStatusRegisteredEvent = _registeredEventFactory.CreateDecontaminateStatusRegisteredEvent
				(client, _nodeManager.FindNode(ViCellBlu.ObjectTypes.DecontaminateStatusEvent));
            AddRegisteredEvent(_myDecontaminateStatusRegisteredEvent, nameof(DecontaminateStatusEvent), client);

			_myErrorStatusRegisteredEvent?.Dispose();
			_myErrorStatusRegisteredEvent = _registeredEventFactory.CreateErrorStatusRegisteredEvent
				(client, _nodeManager.FindNode(ViCellBlu.ObjectTypes.ErrorStatusEvent));
			AddRegisteredEvent(_myErrorStatusRegisteredEvent, nameof(ErrorStatusEvent), client);

			return true;
        }

        private void AddRegisteredEvent(IRegisteredEvent registeredEvent, string eventName, OpcUaGrpcClient client)
        {
            if (null != registeredEvent)
            {
                client.AddRegisteredEvent(registeredEvent);
                registeredEvent.Register();
            }
            else
            {
                _logger.Error($"Failed to register for '{eventName}' Event.");
            }
        }

        public void Dispose()
        {
            _logger.Debug($"OpcEventManager dispose");
            _myWorkListCompleteRegisteredEvent?.Dispose();
            _myWorkListCompleteRegisteredEvent = null;
            _mySampleStatusChangedRegisteredEvent?.Dispose();
            _mySampleStatusChangedRegisteredEvent = null;
            _mySampleCompleteRegisteredEvent?.Dispose();
            _mySampleCompleteRegisteredEvent = null;
            _myDeleteSampleResultsRegisteredEvent?.Dispose();
            _myDeleteSampleResultsRegisteredEvent = null;
            _myExportStatusRegisteredEvent?.Dispose();
            _myExportStatusRegisteredEvent = null;

            _myCleanFluidicsStatusRegisteredEvent?.Dispose();
            _myCleanFluidicsStatusRegisteredEvent = null;
            _myPrimeReagentsStatusRegisteredEvent?.Dispose();
            _myPrimeReagentsStatusRegisteredEvent = null;
            _myPurgeReagentsStatusRegisteredEvent?.Dispose();
            _myPurgeReagentsStatusRegisteredEvent = null;
            _myDecontaminateStatusRegisteredEvent?.Dispose();
			_myDecontaminateStatusRegisteredEvent = null;
			_myErrorStatusRegisteredEvent?.Dispose();
			_myErrorStatusRegisteredEvent = null;
        }
	}
}