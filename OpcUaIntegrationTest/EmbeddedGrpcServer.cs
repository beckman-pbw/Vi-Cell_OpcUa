using System.Threading.Tasks;
using Grpc.Core;
using GrpcService;
using Ninject;

namespace OpcUaIntegrationTest
{
    public class EmbeddedGrpcServer: GrpcServices.GrpcServicesBase
    {
        private readonly OpcUaBaseTestClass _testClass;
        private Server _grpcServer;

        public EmbeddedGrpcServer(OpcUaBaseTestClass testClass)
        {
            _testClass = testClass;
        }

        public void Start()
        {
            _grpcServer = new Server();
            _grpcServer.Services.Add(GrpcServices.BindService(this));
            _grpcServer.Ports.Add(new ServerPort("127.0.0.1", 22222, ServerCredentials.Insecure));
            _grpcServer.Start();
        }

        public void Stop()
        {
            _grpcServer?.ShutdownAsync().Wait();
        }

        public override Task<VcbResultRequestLock> RequestLock(RequestRequestLock request, ServerCallContext context)
        {
            return base.RequestLock(request, context);
        }

        public override Task<VcbResultReleaseLock> ReleaseLock(RequestReleaseLock request, ServerCallContext context)
        {
            return base.ReleaseLock(request, context);
        }

        public override Task<VcbResultGetSampleResults> GetSampleResults(RequestGetSampleResults request, ServerCallContext context)
        {
            return base.GetSampleResults(request, context);
        }

        public override Task<VcbResultExportConfig> ExportConfig(RequestExportConfig request, ServerCallContext context)
        {
            return base.ExportConfig(request, context);
        }

        public override Task<VcbResult> ImportConfig(RequestImportConfig request, ServerCallContext context)
        {
            return base.ImportConfig(request, context);
        }

        public override Task SubscribeLockState(RegistrationRequest request, IServerStreamWriter<LockStateChangedEvent> responseStream, ServerCallContext context)
        {
            var resultProcessor = _testClass.CreateResultProcessor();
            resultProcessor.Subscribe(context, responseStream);

            return base.SubscribeLockState(request, responseStream, context);
        }
    }
}