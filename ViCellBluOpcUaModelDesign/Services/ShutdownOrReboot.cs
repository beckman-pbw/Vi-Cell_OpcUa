using ViCellBlu;
using GrpcService;
using Google.Protobuf;
using Opc.Ua;
using System;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;
using ShutdownOrRebootEnum = ViCellBlu.ShutdownOrRebootEnum;
using VcbResult = ViCellBlu.VcbResult;
using VcbResultGetDiskSpace = ViCellBlu.VcbResultGetDiskSpace;

namespace ViCellBluOpcUaModelDesign.Services
{
    public class ShutdownOrReboot : IShutdownOrReboot
	{
        private readonly BecOpcServer _opcServer;
        private readonly IResultResponseService _resultResponseService;

        public ShutdownOrReboot(BecOpcServer opcServer, IResultResponseService resultResponseService)
        {
            _opcServer = opcServer;
            _resultResponseService = resultResponseService;
        }

        private GrpcService.ShutdownOrRebootEnum ConvertType(ViCellBlu.ShutdownOrRebootEnum type)
        {
	        GrpcService.ShutdownOrRebootEnum grpcValue = GrpcService.ShutdownOrRebootEnum.Shutdown;

			switch (type)
	        {
		        case ViCellBlu.ShutdownOrRebootEnum.Shutdown:
			        grpcValue = GrpcService.ShutdownOrRebootEnum.Shutdown;
			        break;
		        case ViCellBlu.ShutdownOrRebootEnum.Reboot:
			        grpcValue = GrpcService.ShutdownOrRebootEnum.Reboot;
			        break;
	        }

	        return grpcValue;
        }

		public ServiceResult HandleRequestShutdownOrReboot(NodeId sessionId, ShutdownOrRebootEnum operation,  ref VcbResult methodResult)
        {
	        try
	        {
		        var opcUser = _opcServer.LookupUserBySession(sessionId);
		        var request = new RequestShutdownOrReboot()
		        {
			        Operation = ConvertType(operation),
				};
		        var result = opcUser.GrpcClient.SendRequestShutdownOrReboot(request);
		        return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
	        }
	        catch (Exception e)
	        {
		        return _resultResponseService.CreateResponseForGrpcCallException(
			        nameof(HandleRequestShutdownOrReboot), e, ref methodResult);
	        }
        }

    }
}
