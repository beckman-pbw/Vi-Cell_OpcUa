using AutoMapper;
using ViCellBlu;
using GrpcService;
using Opc.Ua;
using System;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;

namespace ViCellBluOpcUaModelDesign.Services
{
    class CellTypeManager : ICellTypeManager
    {
        private readonly BecOpcServer _opcServer;
        private readonly IMapper _mapper;
        private readonly IResultResponseService _resultResponseService;

        public CellTypeManager(BecOpcServer opcServer, IMapper mapper, IResultResponseService resultResponseService)
        {
            _opcServer = opcServer;
            _mapper = mapper;
            _resultResponseService = resultResponseService;
        }
        public ServiceResult HandleCreateCellTypeRequest(NodeId sessionId, ref ViCellBlu.VcbResultCreateCellType methodResult, ref ViCellBlu.CellType cellTypeData)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var startRequest = new RequestCreateCellType
                {
                    Cell = _mapper.Map<GrpcService.CellType>(cellTypeData),
                };

                var result = opcUser.GrpcClient.SendRequestCreateCellType(startRequest);
                return _resultResponseService.CreateViCellBluCreateCellTypeResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleCreateCellTypeRequest), e, ref methodResult);
            }
        }

        public ServiceResult HandleDeleteCellTypeRequest(NodeId sessionId, ref ViCellBlu.VcbResultDeleteCellType methodResult, ref string cellTypeName)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var startRequest = new RequestDeleteCellType
                {
                    CellTypeName = cellTypeName,
                };

                var result = opcUser.GrpcClient.SendRequestDeleteCellType(startRequest);
                return _resultResponseService.CreateViCellBluDeleteCellTypeResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleDeleteCellTypeRequest), e, ref methodResult);
            }
        }

        public ServiceResult HandleCreateQualityControlRequest(NodeId sessionId, ref ViCellBlu.VcbResult methodResult, ref ViCellBlu.QualityControl qc)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var createQcRequest = new RequestCreateQualityControl
                {
                    QualityControl = _mapper.Map<GrpcService.QualityControl>(qc)
                };

                var result = opcUser.GrpcClient.SendRequestCreateQualityControl(createQcRequest);
                return _resultResponseService.CreateViCellBluCreateQualityControlResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleCreateQualityControlRequest), e, ref methodResult);
            }
        }

        public ServiceResult HandleRequestGetCellTypes(NodeId sessionId, ref ViCellBlu.VcbResultGetCellTypes methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var request = new RequestGetCellTypes();
                var result = opcUser.GrpcClient.SendRequestGetCellTypes(request);
                return _resultResponseService.CreateViCellBluGetCellTypesResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleRequestGetCellTypes), e, ref methodResult);
            }
        }

        public ServiceResult HandleRequestGetQualityControls(NodeId sessionId, ref ViCellBlu.VcbResultGetQualityControls methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var request = new RequestGetQualityControls();
                var result = opcUser.GrpcClient.SendRequestGetQualityControls(request);
                return _resultResponseService.CreateViCellBluGetQualityControlsResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleRequestGetQualityControls), e, ref methodResult);
            }
        }
    }
}
