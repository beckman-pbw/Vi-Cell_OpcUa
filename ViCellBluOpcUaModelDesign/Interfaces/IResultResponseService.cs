using GrpcService;
using Opc.Ua;
using System;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    public interface IResultResponseService
    {
        // TODO: Refactor this

        // gRPC Result Responses
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResult methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultGetCellTypes methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultCreateCellType methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultRequestLock methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultReleaseLock methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultDeleteCellType methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultGetQualityControls methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultGetDiskSpace methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultEjectStage methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultStartExport methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultRetrieveExportBlock methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultGetSampleResults methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultExportConfig methodResult);
        ServiceResult CreateResponseForGrpcCallException(string methodName, Exception ex, ref ViCellBlu.VcbResultReagentVolume methodResult);

        // ViCellBlu Responses
        ServiceResult CreateViCellBluResponse(VcbResult gRpcResponse, ref ViCellBlu.VcbResult methodResult);
        ServiceResult CreateViCellBluGetCellTypesResponse(VcbResultGetCellTypes gRpcResponse, ref ViCellBlu.VcbResultGetCellTypes methodResult);
        ServiceResult CreateViCellBluCreateCellTypeResponse(VcbResultCreateCellType gRpcResponse, ref ViCellBlu.VcbResultCreateCellType methodResult);
        ServiceResult CreateViCellBluRequestLockResponse(VcbResultRequestLock gRpcResponse, ref ViCellBlu.VcbResultRequestLock methodResult);
        ServiceResult CreateViCellBluReleaseLockResponse(VcbResultReleaseLock gRpcResponse, ref ViCellBlu.VcbResultReleaseLock methodResult);
        ServiceResult CreateViCellBluDeleteCellTypeResponse(VcbResultDeleteCellType gRpcResponse, ref ViCellBlu.VcbResultDeleteCellType methodResult);
        ServiceResult CreateViCellBluCreateQualityControlResponse(VcbResult gRpcResponse, ref ViCellBlu.VcbResult methodResult);
        ServiceResult CreateViCellBluGetQualityControlsResponse(VcbResultGetQualityControls gRpcResponse, ref ViCellBlu.VcbResultGetQualityControls methodResult);
        ServiceResult CreateViCellBluGetDiskSpaceResponse(VcbResultGetDiskSpace gRpcResponse, ref ViCellBlu.VcbResultGetDiskSpace methodResult);
        ServiceResult CreateViCellBluEjectStageResponse(VcbResultEjectStage gRpcResponse, ref ViCellBlu.VcbResultEjectStage methodResult);
        ServiceResult CreateViCellBluStartExportResponse(VcbResultStartExport gRpcResponse, ref ViCellBlu.VcbResultStartExport methodResult);
        ServiceResult CreateViCellBluRetrieveExportBlockResponse(VcbResultRetrieveBulkDataBlock gRpcResponse, ref ViCellBlu.VcbResultRetrieveExportBlock methodResult);
        ServiceResult CreateViCellBluGetSampleResultsResponse(VcbResultGetSampleResults gRpcResponse, ref ViCellBlu.VcbResultGetSampleResults methodResult);
        ServiceResult CreateViCellBluExportConfigResponse(VcbResultExportConfig gRpcResponse, ref ViCellBlu.VcbResultExportConfig methodResult);
        ServiceResult CreateViCellBluGetGetReagentVolumeResponse(VcbResultReagentVolume gRpcResponse, ref ViCellBlu.VcbResultReagentVolume methodResult);
    }
}
