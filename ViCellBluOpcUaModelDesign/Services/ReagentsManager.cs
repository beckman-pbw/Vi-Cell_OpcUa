using ViCellBlu;
using GrpcService;
using Google.Protobuf;
using Opc.Ua;
using System;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;
using VcbResult = ViCellBlu.VcbResult;

namespace ViCellBluOpcUaModelDesign.Services
{
    public class ReagentsManager : IReagentsManager
	{
        private readonly BecOpcServer _opcServer;
        private readonly IResultResponseService _resultResponseService;

        public ReagentsManager(BecOpcServer opcServer, IResultResponseService resultResponseService)
        {
            _opcServer = opcServer;
            _resultResponseService = resultResponseService;
        }

        public ServiceResult HandleRequestCleanFluidics(NodeId sessionId, ref VcbResult methodResult)
        {
	        try
	        {
		        var opcUser = _opcServer.LookupUserBySession(sessionId);
		        var request = new RequestCleanFluidics();
		        var result = opcUser.GrpcClient.SendRequestCleanFluidics(request);
		        return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
	        }
	        catch (Exception e)
	        {
		        return _resultResponseService.CreateResponseForGrpcCallException(
			        nameof(HandleRequestCleanFluidics), e, ref methodResult);
	        }
        }

        private GrpcService.CellHealthFluidTypeEnum ConvertFluidType (ViCellBlu.CellHealthFluidTypeEnum type)
        {
	        GrpcService.CellHealthFluidTypeEnum grpcValue = GrpcService.CellHealthFluidTypeEnum.Unknown;

	        switch (type)
	        {
		        case ViCellBlu.CellHealthFluidTypeEnum.TrypanBlue:
			        grpcValue = GrpcService.CellHealthFluidTypeEnum.TrypanBlue;
			        break;
		        case ViCellBlu.CellHealthFluidTypeEnum.Cleaner:
			        grpcValue = GrpcService.CellHealthFluidTypeEnum.Cleaner;
			        break;
		        case ViCellBlu.CellHealthFluidTypeEnum.ConditioningSolution:
			        grpcValue = GrpcService.CellHealthFluidTypeEnum.ConditioningSolution;
			        break;
		        case ViCellBlu.CellHealthFluidTypeEnum.Buffer:
			        grpcValue = GrpcService.CellHealthFluidTypeEnum.Buffer;
			        break;
		        case ViCellBlu.CellHealthFluidTypeEnum.Diluent:
			        grpcValue = GrpcService.CellHealthFluidTypeEnum.Diluent;
			        break;
		        case ViCellBlu.CellHealthFluidTypeEnum.Unknown:
			        break;
	        }

	        return grpcValue;
        }

		public ServiceResult HandleRequestGetReagentVolume (NodeId sessionId, ViCellBlu.CellHealthFluidTypeEnum type, ref ViCellBlu.VcbResultReagentVolume methodResult)
        {
	        try
	        {
		        var opcUser = _opcServer.LookupUserBySession(sessionId);
				var request = new RequestGetReagentVolume()
		        {
					Type = ConvertFluidType(type),
				};
				var result = opcUser.GrpcClient.SendRequestGetReagentVolume(request);
		        return _resultResponseService.CreateViCellBluGetGetReagentVolumeResponse(result, ref methodResult);
	        }
	        catch (Exception e)
	        {
		        return _resultResponseService.CreateResponseForGrpcCallException(
			        nameof(HandleRequestGetReagentVolume), e, ref methodResult);
	        }
        }

        public ServiceResult HandleRequestSetReagentVolume(NodeId sessionId, ViCellBlu.CellHealthFluidTypeEnum type, Int32 volume, ref VcbResult methodResult)
        {
	        try
	        {
		        var opcUser = _opcServer.LookupUserBySession(sessionId);
		        var request = new RequestSetReagentVolume()
		        {
			        Type = ConvertFluidType(type),
			        Volume = volume
		        };
		        var result = opcUser.GrpcClient.SendRequestSetReagentVolume(request);
		        return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
	        }
	        catch (Exception e)
	        {
		        return _resultResponseService.CreateResponseForGrpcCallException(
			        nameof(HandleRequestSetReagentVolume), e, ref methodResult);
	        }
        }

        public ServiceResult HandleRequestAddReagentVolume(NodeId sessionId, ViCellBlu.CellHealthFluidTypeEnum type, Int32 volume, ref VcbResult methodResult)
        {
	        try
	        {
		        var opcUser = _opcServer.LookupUserBySession(sessionId);
		        var request = new RequestAddReagentVolume()
		        {
			        Type = ConvertFluidType(type),
			        Volume = volume
		        };
		        var result = opcUser.GrpcClient.SendRequestAddReagentVolume(request);
		        return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
	        }
	        catch (Exception e)
	        {
		        return _resultResponseService.CreateResponseForGrpcCallException(
			        nameof(HandleRequestAddReagentVolume), e, ref methodResult);
	        }
        }

		public ServiceResult HandleRequestPrimeReagents(NodeId sessionId, ref VcbResult methodResult)
		{
			try
			{
				var opcUser = _opcServer.LookupUserBySession(sessionId);
				var request = new RequestPrimeReagents();
				var result = opcUser.GrpcClient.SendRequestPrimeReagents(request);
				return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
			}
			catch (Exception e)
			{
				return _resultResponseService.CreateResponseForGrpcCallException(
					nameof(HandleRequestPrimeReagents), e, ref methodResult);
			}
		}

		public ServiceResult HandleRequestCancelPrimeReagents(NodeId sessionId, ref VcbResult methodResult)
		{
			try
			{
				var opcUser = _opcServer.LookupUserBySession(sessionId);
				var request = new RequestCancelPrimeReagents();
				var result = opcUser.GrpcClient.SendRequestCancelPrimeReagents(request);
				return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
			}
			catch (Exception e)
			{
				return _resultResponseService.CreateResponseForGrpcCallException(
					nameof(HandleRequestCancelPrimeReagents), e, ref methodResult);
			}
		}

		public ServiceResult HandleRequestPurgeReagents(NodeId sessionId, ref VcbResult methodResult)
		{
			try
			{
				var opcUser = _opcServer.LookupUserBySession(sessionId);
				var request = new RequestPurgeReagents();
				var result = opcUser.GrpcClient.SendRequestPurgeReagents(request);
				return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
			}
			catch (Exception e)
			{
				return _resultResponseService.CreateResponseForGrpcCallException(
					nameof(HandleRequestPurgeReagents), e, ref methodResult);
			}
		}

		public ServiceResult HandleRequestCancelPurgeReagents(NodeId sessionId, ref VcbResult methodResult)
		{
			try
			{
				var opcUser = _opcServer.LookupUserBySession(sessionId);
				var request = new RequestCancelPurgeReagents();
				var result = opcUser.GrpcClient.SendRequestCancelPurgeReagents(request);
				return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
			}
			catch (Exception e)
			{
				return _resultResponseService.CreateResponseForGrpcCallException(
					nameof(HandleRequestCancelPurgeReagents), e, ref methodResult);
			}
		}

		public ServiceResult HandleRequestDecontaminate(NodeId sessionId, ref VcbResult methodResult)
		{
			try
			{
				var opcUser = _opcServer.LookupUserBySession(sessionId);
				var request = new RequestDecontaminate();
				var result = opcUser.GrpcClient.SendRequestDecontaminate(request);
				return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
			}
			catch (Exception e)
			{
				return _resultResponseService.CreateResponseForGrpcCallException(
					nameof(HandleRequestDecontaminate), e, ref methodResult);
			}
		}

		public ServiceResult HandleRequestCancelDecontaminate(NodeId sessionId, ref VcbResult methodResult)
		{
			try
			{
				var opcUser = _opcServer.LookupUserBySession(sessionId);
				var request = new RequestCancelDecontaminate();
				var result = opcUser.GrpcClient.SendRequestCancelDecontaminate(request);
				return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
			}
			catch (Exception e)
			{
				return _resultResponseService.CreateResponseForGrpcCallException(
					nameof(HandleRequestCancelDecontaminate), e, ref methodResult);
			}
		}

	}
}
