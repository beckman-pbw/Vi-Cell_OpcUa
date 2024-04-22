using AutoMapper;
using ViCellBlu;
using GrpcClient;
using GrpcClient.Services;
using GrpcServer;
using Microsoft.Extensions.Configuration;
using Moq;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Logging;
using Ninject.Extensions.Logging.Log4net;
using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;
using ViCellBluOpcUaModelDesign;
using ViCellBluOpcUaModelDesign.Events;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;
using ViCellBluOpcUaModelDesign.Services;
using ViCellBluOpcUaModelDesign.ViCellBluManagement;
using LockStateEnum = ViCellBlu.LockStateEnum;

namespace OpcUaIntegrationTest
{
    public class OpcUaBaseTestClass
    {
        protected IKernel Kernel;
        protected ILogger Logger;
        protected readonly Mock<INodeService> MockNodeService = new Mock<INodeService>();

        // OPC/UA server
        private EmbeddedService _service;
        private ConfiguredTaskAwaitable _serviceAwaiter;

        // gRPC server (pretend Scout)
        private LockTestResultProcessor _currentResultProcessor;
        private EmbeddedGrpcServer _grpcServer;

        protected OpcUaTestingClient OpcUaClient { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            ConfigNinject();
            StartOpcUaServer();
            StartGrpcServer();
        }

        protected virtual void ConfigNinject()
        {
            var settings = new NinjectSettings()
            {
                LoadExtensions = false
            };
            
            Kernel = new StandardKernel(settings, new Log4NetModule(), new FuncModule());

            // Setup IConfiguration implementation
            var configBuilder = new ConfigurationBuilder();
            configBuilder.Sources.Add(new BeckmanNamespaceSource());
            var config = configBuilder.Build();

            Kernel.Bind<Microsoft.Extensions.Configuration.IConfiguration>().ToConstant(config);
            Kernel.Bind<BecOpcConfig>().ToSelf().InSingletonScope();
            Kernel.Bind<IBecServerController>().To<BecServerController>().InSingletonScope();
            Kernel.Bind<OpcEventManager>().ToSelf().InSingletonScope();
            Kernel.Bind<BecOpcServer>().ToSelf().InSingletonScope();
            Kernel.Bind<BecNodeManager>().ToSelf().InSingletonScope();
            Kernel.Bind<Grpc.Core.Logging.ILogger, OpcGrpcLogger>().To<OpcGrpcLogger>().InTransientScope();
            Kernel.Bind<OpcUaGrpcClient>().ToSelf().InTransientScope();
            Kernel.Bind<INodeService>().ToConstant(MockNodeService.Object);
            Kernel.Bind<ILockManager>().To<LockManager>().InSingletonScope();
            Kernel.Bind<ISampleResultsManager>().To<SampleResultsManager>().InSingletonScope();
            Kernel.Bind<MethodFolderState>().ToSelf().InTransientScope();
            Kernel.Bind<ISampleProcessingManager>().To<SampleProcessingManager>().InSingletonScope();
            Kernel.Bind<PlayControlFolderState>().ToSelf().InTransientScope();
            Kernel.Bind<IOpcUaFactory>().ToFactory();
            Kernel.Bind<IAddBehaviorToPredefinedNodeService>().To<AddBehaviorToPredefinedNodeService>()
                .InSingletonScope();

            // Events
            Kernel.Bind<IRegisteredEventFactory>().ToFactory();
            Kernel.Bind<NodeEventFactory>().ToSelf().InSingletonScope();

            // Add new events here
            Kernel.Bind<LockStateRegisteredVariable>().ToSelf().InTransientScope();

            // For AutoMapper
            var mapperConfiguration = CreateConfiguration();
            Kernel.Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            // This teaches Ninject how to create AutoMapper instances say if for instance
            // MyResolver has a constructor with a parameter that needs to be injected
            Kernel.Bind<IMapper>().ToMethod(ctx =>
                new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));

            // Bindings for Embedded gRPC server pretending to be Scout.
            Kernel.Bind<EmbeddedService>().ToSelf().InSingletonScope();
            Kernel.Bind<ITestLockManager, TestLockManager>().To<TestLockManager>().InSingletonScope();
            Kernel.Bind<LockTestResultProcessor>().ToSelf().InTransientScope();

            Logger = Kernel.Get<ILoggerFactory>().GetCurrentClassLogger();
        }

        private MapperConfiguration CreateConfiguration()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                // Add all profiles for each gRPC event
                cfg.CreateMap<LockStateEnum, GrpcService.LockStateEnum>();
                cfg.CreateMap<LockTestEnum, GrpcService.LockStateEnum>();
            });

            return mapperConfig;
        }

        [TearDown]
        public virtual void Cleanup()
        {
            Assert.True(StopOpcUaServer());
            //Assert.AreEqual(ExitCode.Ok,(int)OpcUaTestingClient.ExitCode);
        }

        /// <summary>
        /// Run the OpcUaServer inside this test instance, not as a separate process.
        /// </summary>
        /// <returns></returns>
        protected void StartOpcUaServer()
        {
            _service = Kernel.Get<EmbeddedService>();
            _serviceAwaiter = _service.Run().ConfigureAwait(false);
        }

        /// <summary>
        /// Run the GrpcServer inside this test instance, not as a separate process.
        /// </summary>
        /// <returns></returns>
        protected void StartGrpcServer()
        {
            try
            {
                _grpcServer = new EmbeddedGrpcServer(this);
                Console.WriteLine("OPC/UA gRPC server listening on port 22222");
                Console.WriteLine("Press any key to stop the server...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OPC/UA gRPC server exception during startup: {ex.Message}");
            }
        }

        protected void StopGrpcServer()
        {
            _grpcServer.Stop();
        }

        /// <summary>
        /// Stop the ScoutX OPC/UA server
        /// </summary>
        /// <returns>true if stopped cleanly, false if kill was used.</returns>
        protected bool StopOpcUaServer()
        {
            _service.Stop();
            _serviceAwaiter.GetAwaiter().GetResult();
            _service.Dispose();
            return true;
        }

        public LockTestResultProcessor CreateResultProcessor()
        {
            _currentResultProcessor = Kernel.Get<LockTestResultProcessor>();
            return _currentResultProcessor;
        }
    }
}