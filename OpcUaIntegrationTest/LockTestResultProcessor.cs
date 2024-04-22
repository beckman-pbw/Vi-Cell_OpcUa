using System;
using AutoMapper;
using Grpc.Core;
using GrpcService;
using Ninject.Extensions.Logging;
using OpcUaIntegrationTest;
// ReSharper disable InconsistentNaming

namespace GrpcServer
{
    /// <summary>
    /// A LockTestResultProcessor exists for each gRPC/OPC client and is associated with its GrpcClient instance.
    /// </summary>
    public class LockTestResultProcessor : EventProcessor<LockStateChangedEvent>
    {
        protected ITestLockManager _lockManager;

        public LockTestResultProcessor(ILogger logger, IMapper mapper, ITestLockManager lockManager) : base(logger, mapper)
        {
            _lockManager = lockManager;
        }

        /// <summary>
        /// Duplicate subscriptions will be ignored.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="responseStream"></param>
        public override void Subscribe(ServerCallContext context, IServerStreamWriter<LockStateChangedEvent> responseStream)
        {
            _subscription?.Dispose();
            _responseStream = responseStream;
            _subscription = _lockManager.SubscribeStateChanges().Subscribe(SetLockStatus);
            base.Subscribe(context, responseStream);
        }

        private void SetLockStatus(LockTestEnum res)
        {
            var message = new LockStateChangedEvent
            {
                LockState = _mapper.Map<LockStateEnum>(res)
            };

            QueueMessage(message);
        }
    }
}