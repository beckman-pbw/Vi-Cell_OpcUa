using AutoMapper;
using ViCellBlu;
using GrpcService;
using Opc.Ua;
using System;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;
using SampleConfig = GrpcService.SampleConfig;

namespace ViCellBluOpcUaModelDesign.Services
{
    public class SampleProcessingManager : ISampleProcessingManager
    {
        private readonly BecOpcServer _opcServer;
        private readonly IMapper _mapper;
        private readonly IResultResponseService _resultResponseService;

        public SampleProcessingManager(BecOpcServer opcServer, IMapper mapper,
            IResultResponseService resultResponseService)
        {
            _opcServer = opcServer;
            _mapper = mapper;
            _resultResponseService = resultResponseService;
        }

        public ServiceResult HandleEjectStageRequest(NodeId sessionId, ref ViCellBlu.VcbResultEjectStage methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var ejectStageRequest = new RequestEjectStage();
                var result = opcUser.GrpcClient.SendRequestEjectStage(ejectStageRequest);

                // set the output args
                return _resultResponseService.CreateViCellBluEjectStageResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleEjectStageRequest), e, ref methodResult);
            }
        }

        public ServiceResult HandlePauseRequest(NodeId sessionId, ref ViCellBlu.VcbResult methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var pauseRequest = new RequestPause();
                var result = opcUser.GrpcClient.SendRequestPause(pauseRequest);

                // set the output args
                return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandlePauseRequest), e, ref methodResult);
            }
        }

        public ServiceResult HandleResumeRequest(NodeId sessionId, ref ViCellBlu.VcbResult methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var resumeRequest = new RequestResume();
                var result = opcUser.GrpcClient.SendRequestResume(resumeRequest);

                // set the output args
                return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleResumeRequest), e, ref methodResult);
            }
        }

        public ServiceResult HandleStartRequest(NodeId sessionId, ref ViCellBlu.VcbResult methodResult, ref ViCellBlu.SampleConfig sampleToStart)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var startRequest = new RequestStartSample
                {
                    SampleConfig = _mapper.Map<SampleConfig>(sampleToStart)
                };
                var result = opcUser.GrpcClient.SendRequestStartSample(startRequest);

                // set the output args
                return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleStartRequest), e, ref methodResult);
            }
        }

        public ServiceResult HandleStartSetRequest(NodeId sessionId, ref ViCellBlu.VcbResult methodResult, ref SampleSet sampleSetToStart)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var startSetRequest = new RequestStartSampleSet()
                {
                    SampleSetConfig = _mapper.Map<SampleSetConfig>(sampleSetToStart)
                };
                var result = opcUser.GrpcClient.SendRequestStartSampleSet(startSetRequest);

                // set the output args
                return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleStartSetRequest), e, ref methodResult);
            }
        }

        public ServiceResult HandleStopRequest(NodeId sessionId, ref ViCellBlu.VcbResult methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var stopRequest = new RequestStop();
                var result = opcUser.GrpcClient.SendRequestStop(stopRequest);

                // set the output args
                return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleStopRequest), e, ref methodResult);
            }
        }
    }
}