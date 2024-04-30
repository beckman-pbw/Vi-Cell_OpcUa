using AutoMapper;
using GrpcService;
using Ninject.Extensions.Logging;
using Opc.Ua;
using System;
using ViCellBlu;
using ViCellBluOpcUaModelDesign.Interfaces;
using VcbResult = GrpcService.VcbResult;
using VcbResultCreateCellType = ViCellBlu.VcbResultCreateCellType;
using VcbResultCreateQualityControl = ViCellBlu.VcbResult;
using VcbResultDeleteCellType = ViCellBlu.VcbResultDeleteCellType;
using VcbResultEjectStage = ViCellBlu.VcbResultEjectStage;
using VcbResultGetCellTypes = GrpcService.VcbResultGetCellTypes;
using VcbResultGetDiskSpace = ViCellBlu.VcbResultGetDiskSpace;
using VcbResultGetQualityControls = ViCellBlu.VcbResultGetQualityControls;
using VcbResultReleaseLock = ViCellBlu.VcbResultReleaseLock;
using VcbResultRequestLock = ViCellBlu.VcbResultRequestLock;
using VcbResultGetSampleResults = ViCellBlu.VcbResultGetSampleResults;
using VcbResultStartExport = GrpcService.VcbResultStartExport;
using VcbResultReagentVolume = ViCellBlu.VcbResultReagentVolume;

namespace ViCellBluOpcUaModelDesign.Services
{
    // TODO: Refactor this

    public class ResultResponseService : IResultResponseService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ResultResponseService(IMapper mapper, ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, 
            ref ViCellBlu.VcbResult methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResult
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error
            };
            
            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex,
            ref ViCellBlu.VcbResultGetCellTypes methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResultGetCellTypes
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error,
                CellTypes = null
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex,
            ref VcbResultCreateCellType methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResultCreateCellType
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, 
            ref VcbResultRequestLock methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResultRequestLock
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, 
            ref VcbResultReleaseLock methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResultReleaseLock
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex,
            ref VcbResultDeleteCellType methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResultDeleteCellType
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex,
            ref VcbResultGetQualityControls methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResultGetQualityControls
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error,
                QualityControls = null
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex,
            ref VcbResultGetDiskSpace methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResultGetDiskSpace
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, 
            ref VcbResultEjectStage methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResultEjectStage
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex,
            ref ViCellBlu.VcbResultStartExport methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResultStartExport
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error,
                BulkDataId = ""
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex,
            ref ViCellBlu.VcbResultRetrieveExportBlock methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResultRetrieveExportBlock
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error,
                BlockData = null
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex,
                ref VcbResultGetSampleResults methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResultGetSampleResults
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error,
                SampleResults = null
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex,
            ref ViCellBlu.VcbResultExportConfig methodResult)
        {
            var formattedResponse = LogException(methodName, ex);

            methodResult = new ViCellBlu.VcbResultExportConfig
            {
                ResponseDescription = formattedResponse,
                MethodResult = ViCellBlu.MethodResultEnum.Failure,
                ErrorLevel = ViCellBlu.ErrorLevelEnum.Error,
                FileData = null
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        public ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultReagentVolume methodResult)
        {
	        var formattedResponse = LogException(methodName, ex);

	        methodResult = new ViCellBlu.VcbResultReagentVolume
			{
		        ResponseDescription = formattedResponse,
		        MethodResult = ViCellBlu.MethodResultEnum.Failure,
		        ErrorLevel = ViCellBlu.ErrorLevelEnum.Error,
		        Volume = 0
            };

            return ServiceResult.Good; // Always "good" for the attempt (ACK)
        }

        private string LogException(string methodName, Exception exception)
        {
            var msg = $"Error executing method '{methodName}'";
            var response = $"{msg}::{exception.GetType().Name}::{exception.Message}";
            _logger.Error(exception, msg);

            return response;
        }

        public ServiceResult CreateViCellBluResponse(VcbResult gRpcResponse,
            ref ViCellBlu.VcbResult methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResult>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluGetCellTypesResponse(
            VcbResultGetCellTypes gRpcResponse,
            ref ViCellBlu.VcbResultGetCellTypes methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResultGetCellTypes>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluCreateCellTypeResponse(GrpcService.VcbResultCreateCellType gRpcResponse,
            ref VcbResultCreateCellType methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResultCreateCellType>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluRequestLockResponse(GrpcService.VcbResultRequestLock gRpcResponse,
            ref VcbResultRequestLock methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResultRequestLock>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluReleaseLockResponse(GrpcService.VcbResultReleaseLock gRpcResponse,
            ref VcbResultReleaseLock methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResultReleaseLock>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluDeleteCellTypeResponse(GrpcService.VcbResultDeleteCellType gRpcResponse,
            ref VcbResultDeleteCellType methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResultDeleteCellType>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluCreateQualityControlResponse(GrpcService.VcbResult gRpcResponse,
            ref VcbResultCreateQualityControl methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResult>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluGetQualityControlsResponse(GrpcService.VcbResultGetQualityControls gRpcResponse,
            ref VcbResultGetQualityControls methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResultGetQualityControls>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluExportConfigResponse(GrpcService.VcbResultExportConfig gRpcResponse,
            ref ViCellBlu.VcbResultExportConfig methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResultExportConfig>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluGetDiskSpaceResponse(GrpcService.VcbResultGetDiskSpace gRpcResponse,
            ref VcbResultGetDiskSpace methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResultGetDiskSpace>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluEjectStageResponse(GrpcService.VcbResultEjectStage gRpcResponse, ref VcbResultEjectStage methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResultEjectStage>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluStartExportResponse(VcbResultStartExport gRpcResponse,
            ref ViCellBlu.VcbResultStartExport methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResultStartExport>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluRetrieveExportBlockResponse(VcbResultRetrieveBulkDataBlock gRpcResponse,
            ref ViCellBlu.VcbResultRetrieveExportBlock methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResultRetrieveExportBlock> (gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluGetSampleResultsResponse(GrpcService.VcbResultGetSampleResults gRpcResponse, ref VcbResultGetSampleResults methodResult)
        {
            methodResult = _mapper.Map<ViCellBlu.VcbResultGetSampleResults>(gRpcResponse);
            return ServiceResult.Good; // always "good" for the attempt (ACK)
        }

        public ServiceResult CreateViCellBluGetGetReagentVolumeResponse(GrpcService.VcbResultReagentVolume gRpcResponse, ref VcbResultReagentVolume methodResult)
        {
	        methodResult = _mapper.Map<ViCellBlu.VcbResultReagentVolume>(gRpcResponse);
	        return ServiceResult.Good; // always "good" for the attempt (ACK)
        }
    }
}