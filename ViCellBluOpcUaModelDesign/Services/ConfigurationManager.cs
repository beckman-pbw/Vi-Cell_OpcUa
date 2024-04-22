using ViCellBlu;
using GrpcService;
using Google.Protobuf;
using Opc.Ua;
using System;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;
using VcbResult = ViCellBlu.VcbResult;
using VcbResultGetDiskSpace = ViCellBlu.VcbResultGetDiskSpace;

namespace ViCellBluOpcUaModelDesign.Services
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly BecOpcServer _opcServer;
        private readonly IResultResponseService _resultResponseService;

        public ConfigurationManager(BecOpcServer opcServer, IResultResponseService resultResponseService)
        {
            _opcServer = opcServer;
            _resultResponseService = resultResponseService;
        }

        public ServiceResult HandleRequestExportConfig(NodeId sessionId, ref ViCellBlu.VcbResultExportConfig methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var configRequest = new RequestExportConfig();
                var result = opcUser.GrpcClient.SendRequestExportConfig(configRequest);
                return _resultResponseService.CreateViCellBluExportConfigResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleRequestExportConfig), e, ref methodResult);
            }
        }

        public ServiceResult HandleRequestImportConfig(NodeId sessionId, ref ViCellBlu.VcbResult methodResult,
            byte[] fileData)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var configRequest = new RequestImportConfig
                {
                    FileData = ByteString.CopyFrom(fileData)
                };

                var result = opcUser.GrpcClient.SendRequestImportConfig(configRequest);
                return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleRequestImportConfig), e, ref methodResult);
            }
        }

        public ServiceResult HandleRequestGetAvailableDiskSpace(NodeId sessionId, ref VcbResultGetDiskSpace methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var request = new RequestGetAvailableDiskSpace();
                var result = opcUser.GrpcClient.SendRequestGetAvailableDiskSpace(request);
                return _resultResponseService.CreateViCellBluGetDiskSpaceResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleRequestGetAvailableDiskSpace), e, ref methodResult);
            }
        }
	}
}
