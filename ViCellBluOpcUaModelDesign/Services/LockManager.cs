using GrpcService;
using Opc.Ua;
using System;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;

namespace ViCellBluOpcUaModelDesign.Services
{
    public class LockManager : ILockManager
    {
        private readonly BecOpcServer _opcServer;
        private readonly IResultResponseService _resultResponseService;

        public LockManager(BecOpcServer opcServer, IResultResponseService resultResponseService)
        {
            _opcServer = opcServer;
            _resultResponseService = resultResponseService;
        }

        public ServiceResult HandleRequestLockRequest(NodeId sessionId,
            ref ViCellBlu.VcbResultRequestLock methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var lockRequest = new RequestRequestLock();
                var result = opcUser.GrpcClient.SendRequestRequestLock(lockRequest);

                return _resultResponseService.CreateViCellBluRequestLockResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleRequestLockRequest), e, ref methodResult);
            }
        }

        public ServiceResult HandleReleaseLockRequest(NodeId sessionId, ref ViCellBlu.VcbResultReleaseLock methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var unlockRequest = new RequestReleaseLock();
                var result = opcUser.GrpcClient.SendRequestReleaseLock(unlockRequest);

                return _resultResponseService.CreateViCellBluReleaseLockResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleReleaseLockRequest), e, ref methodResult);
            }
        }
    }
}