using System;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Interfaces;

// ReSharper disable IdentifierTypo

namespace ViCellBlu
{
    public partial class PlayControlFolderState
    {
        private readonly ISampleProcessingManager _sampleProcessingManager;
        private readonly IResultResponseService _resultResponseService;

        public PlayControlFolderState(ISampleProcessingManager sampleProcessingManager,
            NodeState parent, IResultResponseService resultResponseService)
            : base(parent)
        {
            _sampleProcessingManager = sampleProcessingManager;
            _resultResponseService = resultResponseService;
        }

        /// <summary>
        /// Initializes the object as a collection of counters which change value on read.
        /// </summary>
        protected override void OnAfterCreate(ISystemContext context, NodeState node)
        {
            base.OnAfterCreate(context, node);

            // Add the method handlers for incoming OPC UA calls
            StartSample.OnCall = OnStartSampleCall;
            StartSampleSet.OnCall = OnStartSampleSetCall;
            EjectStage.OnCall = OnEjectStageCall;
            Stop.OnCall = OnStopCall;
            Pause.OnCall = OnPauseCall;
            Resume.OnCall = OnResumeCall;
        }

        private ServiceResult OnStartSampleCall(ISystemContext context, MethodState method, NodeId objectid, 
            SampleConfig sampletorun, ref VcbResult methodresult)
        {
            try
            {
                return _sampleProcessingManager.HandleStartRequest(context.SessionId, ref methodresult, 
                    ref sampletorun);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(nameof(OnStartSampleCall), e, 
                    ref methodresult);
            }
        }

        private ServiceResult OnStartSampleSetCall(ISystemContext context, MethodState method, NodeId objectid, 
            SampleSet sampletorun, ref VcbResult methodresult)
        {
            try
            {
                return _sampleProcessingManager.HandleStartSetRequest(context.SessionId, 
                    ref methodresult, ref sampletorun);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(nameof(OnStartSampleSetCall), e, 
                    ref methodresult);
            }
        }

        private ServiceResult OnEjectStageCall(ISystemContext context, MethodState method, NodeId objectid,
            ref VcbResultEjectStage methodResult)
        {
            try
            {
                return _sampleProcessingManager.HandleEjectStageRequest(context.SessionId, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(nameof(OnEjectStageCall), e, 
                    ref methodResult);
            }
        }

        private ServiceResult OnStopCall(ISystemContext context, MethodState method, NodeId objectid,
            ref VcbResult methodResult)
        {
            try
            {
                return _sampleProcessingManager.HandleStopRequest(context.SessionId, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(nameof(OnStopCall), e, 
                    ref methodResult);
            }
        }
        private ServiceResult OnPauseCall(ISystemContext context, MethodState method, NodeId objectid,
            ref VcbResult methodResult)
        {
            try
            {
                return _sampleProcessingManager.HandlePauseRequest(context.SessionId, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(nameof(OnPauseCall), e, 
                    ref methodResult);
            }
        }

        private ServiceResult OnResumeCall(ISystemContext context, MethodState method, NodeId objectid,
            ref VcbResult methodResult)
        {
            try
            {
                return _sampleProcessingManager.HandleResumeRequest(context.SessionId, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(nameof(OnResumeCall), e,
                    ref methodResult);
            }
        }
    }
}