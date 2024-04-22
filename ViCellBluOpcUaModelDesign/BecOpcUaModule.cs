using System;
using AutoMapper;
using Google.Protobuf;
using ViCellBlu;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core.Interceptors;
using GrpcClient;
using GrpcClient.Services;
using Microsoft.Extensions.Configuration;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Events;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;
using ViCellBluOpcUaModelDesign.Services;
using ViCellBluOpcUaModelDesign.ViCellBluManagement;
using CellType = ViCellBlu.CellType;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;
using ErrorLevelEnum = GrpcService.ErrorLevelEnum;
using LockStateEnum = ViCellBlu.LockStateEnum;
using MethodResultEnum = GrpcService.MethodResultEnum;
using QualityControl = ViCellBlu.QualityControl;
using SamplePosition = ViCellBlu.SamplePosition;
using WashTypeEnum = ViCellBlu.WashTypeEnum;

namespace ViCellBluOpcUaModelDesign
{
    public class BecOpcUaModule : NinjectModule
    {
        public override void Load()
        {
            // Setup IConfiguration implementation
            var configBuilder = new ConfigurationBuilder();
            configBuilder.Sources.Add(new BeckmanNamespaceSource());
            var config = configBuilder.Build();

            Bind<IConfiguration>().ToConstant(config);
            Bind<BecOpcConfig>().ToSelf().InSingletonScope();
            Bind<IBecServerController>().To<BecServerController>().InSingletonScope();
            Bind<OpcEventManager>().ToSelf().InSingletonScope();
            Bind<BecOpcServer>().ToSelf().InSingletonScope();
            Bind<BecNodeManager>().ToSelf().InSingletonScope();
            Bind<Grpc.Core.Logging.ILogger, OpcGrpcLogger>().To<OpcGrpcLogger>().InTransientScope();
            Bind<OpcUaGrpcClient>().ToSelf().InTransientScope();
            Bind<Interceptor>().To<OpcExceptionInterceptor>().InSingletonScope();
            Bind<INodeService>().To<NodeService>().InSingletonScope();
            Bind<ILockManager>().To<LockManager>().InSingletonScope();
            Bind<ISampleResultsManager>().To<SampleResultsManager>().InSingletonScope();
            Bind<MethodFolderState>().ToSelf().InTransientScope();
            Bind<ISampleProcessingManager>().To<SampleProcessingManager>().InSingletonScope();
            Bind<PlayControlFolderState>().ToSelf().InTransientScope();
            Bind<IOpcUaFactory>().ToFactory();
            Bind<IAddBehaviorToPredefinedNodeService>().To<AddBehaviorToPredefinedNodeService>()
                .InSingletonScope();
            Bind<ICellTypeManager>().To<CellTypeManager>().InSingletonScope();
            Bind<IConfigurationManager>().To<ConfigurationManager>().InSingletonScope();
            Bind<IResultResponseService>().To<ResultResponseService>().InTransientScope();
            Bind<IReagentsManager>().To<ReagentsManager>().InSingletonScope();
            Bind<IShutdownOrReboot>().To<ShutdownOrReboot>().InSingletonScope();

			// Events
			Bind<IRegisteredEventFactory>().ToFactory();
            Bind<NodeEventFactory>().ToSelf().InSingletonScope();

            // Add variables/events here
            // TODO... I don't think we need the following lines. Commenting out for now
            //Bind<LockStateRegisteredVariable>().ToSelf().InTransientScope();
            //Bind<ViCellStatusRegisteredVariable>().ToSelf().InTransientScope();
            //Bind<ViCellIdRegisteredVariable>().ToSelf().InTransientScope();
            //Bind<ReagentUseRemainingRegisteredVariable>().ToSelf().InTransientScope();
            //Bind<WasteTubeCapacityRegisteredVariable>().ToSelf().InTransientScope();
            //Bind<DeleteSampleResultsRegisteredEvent>().ToSelf().InTransientScope();
            //Bind<WorkListCompleteEvent>().ToSelf().InTransientScope();
            //Bind<SampleStatusChangedEvent>().ToSelf().InTransientScope();

            // For AutoMapper
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            // This teaches Ninject how to create AutoMapper instances say if for instance
            // MyResolver has a constructor with a parameter that needs to be injected
            Bind<IMapper>().ToMethod(ctx =>
                new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                // Add all profiles for each gRPC event
                cfg.CreateMap<string, string>().ConvertUsing(s => s ?? string.Empty);

                cfg.CreateMap<GrpcService.LockStateEnum, LockStateEnum>().ReverseMap();
                cfg.CreateMap<GrpcService.WashTypeEnum, WashTypeEnum>().ReverseMap();
                cfg.CreateMap<GrpcService.SampleStatusEnum, ViCellBlu.SampleStatusEnum>().ReverseMap();
                cfg.CreateMap<GrpcService.MethodResultEnum, ViCellBlu.MethodResultEnum>().ReverseMap();
                cfg.CreateMap<GrpcService.ErrorLevelEnum, ViCellBlu.ErrorLevelEnum>().ReverseMap();

                cfg.CreateMap<string, Uuid>().ConvertUsing(s =>
                    string.IsNullOrEmpty(s) ? Uuid.Empty : new Uuid(s));
                cfg.CreateMap<Uuid, string>().ConvertUsing(u =>
                    u == null || u == Uuid.Empty ? Uuid.Empty.GuidString.ToUpper() : u.GuidString.ToUpper());
                cfg.CreateMap<string, Guid>().ConvertUsing(s =>
                    string.IsNullOrEmpty(s) ? Guid.Empty : Guid.Parse(s));
                cfg.CreateMap<Guid, string>().ConvertUsing(g =>
                    g == null || g == Guid.Empty ? Guid.Empty.ToString().ToUpper() : g.ToString().ToUpper());
                cfg.CreateMap<DateTime, Timestamp>().ConvertUsing(d =>
                    d != null ? DateTime.SpecifyKind(d, DateTimeKind.Utc).ToTimestamp() : null);
                cfg.CreateMap<Timestamp, DateTime>().ConvertUsing(t =>
                    t != null ? DateTime.SpecifyKind(t.ToDateTime(), DateTimeKind.Local) : DateTime.MinValue);

                cfg.CreateMap<GrpcService.LockStateEnum, LockStateEnum>().ReverseMap();
                cfg.CreateMap<GrpcService.ViCellStatusEnum, ViCellBlu.ViCellStatusEnum>().ReverseMap();
                cfg.CreateMap<GrpcService.WashTypeEnum, WashTypeEnum>().ReverseMap();
                cfg.CreateMap<GrpcService.PrecessionEnum, PlatePrecessionEnum>().ReverseMap();
                cfg.CreateMap<GrpcService.SampleStatusEnum, ViCellBlu.SampleStatusEnum>().ReverseMap();
                cfg.CreateMap<GrpcService.SampleResult, ViCellBlu.SampleResult>();
                //cfg.CreateMap<GrpcService.SampleResult, ViCellBlu.SampleResult>().ReverseMap(); // TODO: See if we can comment this out and just use the SampleResult
                cfg.CreateMap<GrpcService.SamplePosition, SamplePosition>().ReverseMap();
                cfg.CreateMap<GrpcService.CellType, CellType>().ReverseMap();
                cfg.CreateMap<GrpcService.QualityControl, QualityControl>().ReverseMap();

                cfg.CreateMap<GrpcService.SampleConfig, ViCellBlu.SampleConfig>()
                   .ForMember(dest => dest.CellType, opt =>
                       opt.MapFrom(src => src.CellType))
                   .ForMember(dest => dest.QualityControl, opt =>
                       opt.MapFrom(src => src.QualityControl));

                cfg.CreateMap<ViCellBlu.SampleConfig, GrpcService.SampleConfig>()
                   .ForMember(dest => dest.CellType, opt =>
                       opt.MapFrom(src => src.CellType))
                   .ForMember(dest => dest.QualityControl, opt =>
                       opt.MapFrom(src => src.QualityControl));

                cfg.CreateMap<ViCellBlu.SampleSet, GrpcService.SampleSetConfig>();

                cfg.CreateMap<RepeatedField<GrpcService.SampleResult>, 
                       SampleResultCollection>()
                   .ConvertUsing(new SampleResultCollectionConverter<GrpcService.SampleResult,
                       SampleResultCollection>());

                cfg.CreateMap<SampleResultCollection, RepeatedField<GrpcService.SampleResult>>()
                   .ConvertUsing(new CollectionToRepeatedSampleResultConverter<
                       SampleResultCollection, GrpcService.SampleResult>());

                cfg.CreateMap<GrpcService.VcbResult, ViCellBlu.VcbResult>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResult, GrpcService.VcbResult>().ConvertUsing(new AutomationResponseTypeConverter());

                cfg.CreateMap<GrpcService.VcbResultRequestLock, ViCellBlu.VcbResultRequestLock>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResultRequestLock, GrpcService.VcbResultRequestLock>().ConvertUsing(new AutomationResponseRequestLockTypeConverter());

                cfg.CreateMap<GrpcService.VcbResultGetCellTypes, ViCellBlu.VcbResultGetCellTypes>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResultGetCellTypes, GrpcService.VcbResultGetCellTypes>().ConvertUsing(new AutomationResponseGetCellTypesTypeConverter());

                cfg.CreateMap<GrpcService.VcbResultCreateCellType, ViCellBlu.VcbResultCreateCellType>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResultCreateCellType, GrpcService.VcbResultCreateCellType>().ConvertUsing(new AutomationResponseCreateCellTypeConverter());

                cfg.CreateMap<GrpcService.VcbResultReleaseLock, ViCellBlu.VcbResultReleaseLock>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResultReleaseLock, GrpcService.VcbResultReleaseLock>().ConvertUsing(new AutomationResponseReleaseLockTypeConverter());

                cfg.CreateMap<GrpcService.VcbResultDeleteCellType, ViCellBlu.VcbResultDeleteCellType>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResultDeleteCellType, GrpcService.VcbResultDeleteCellType>().ConvertUsing(new AutomationResponseDeleteCellTypeTypeConverter());

                cfg.CreateMap<GrpcService.VcbResult, ViCellBlu.VcbResult>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResult, GrpcService.VcbResult>().ConvertUsing(new AutomationResponseCreateQualityControlTypeConverter());

                cfg.CreateMap<GrpcService.VcbResultGetQualityControls, ViCellBlu.VcbResultGetQualityControls>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResultGetQualityControls, GrpcService.VcbResultGetQualityControls>().ConvertUsing(new AutomationResponseGetQualityControlsTypeConverter());

                cfg.CreateMap<GrpcService.VcbResultGetDiskSpace, ViCellBlu.VcbResultGetDiskSpace>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResultGetDiskSpace, GrpcService.VcbResultGetDiskSpace>().ConvertUsing(new AutomationResponseGetDiskSpaceTypeConverter());

                cfg.CreateMap<GrpcService.VcbResultEjectStage, ViCellBlu.VcbResultEjectStage>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResultEjectStage, GrpcService.VcbResultEjectStage>().ConvertUsing(new AutomationResponseEjectStageTypeConverter());

                cfg.CreateMap<GrpcService.VcbResultStartExport, ViCellBlu.VcbResultStartExport>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResultStartExport, GrpcService.VcbResultStartExport>().ConvertUsing(new AutomationResponseStartExportTypeConverter());

                cfg.CreateMap<ViCellBlu.VcbResultRetrieveExportBlock, GrpcService.VcbResultRetrieveBulkDataBlock>().ConvertUsing(new AutomationResponseRetrieveExportBlockTypeConverter());
                cfg.CreateMap<GrpcService.VcbResultRetrieveBulkDataBlock, ViCellBlu.VcbResultRetrieveExportBlock>().ConvertUsing(new AutomationResponseRetrieveExportBlockTypeConverterRev());

                cfg.CreateMap<GrpcService.VcbResultGetSampleResults, ViCellBlu.VcbResultGetSampleResults>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResultGetSampleResults, GrpcService.VcbResultGetSampleResults>().ConvertUsing(new AutomationResponseGetSampleResultsTypeConverter());

                cfg.CreateMap<GrpcService.VcbResultExportConfig, ViCellBlu.VcbResultExportConfig>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResultExportConfig, GrpcService.VcbResultExportConfig>().ConvertUsing(new AutomationResponseExportConfigTypeConverter());

                cfg.CreateMap<ViCellBlu.SampleSet, GrpcService.SampleSetConfig>()
                   .ForMember(dest => dest.Samples, opt =>
                       opt.MapFrom(src => src.Samples));

                cfg.CreateMap<GrpcService.SampleStatusData, ViCellBlu.SampleStatusData>().ReverseMap();

                cfg.CreateMap<GrpcService.VcbResultReagentVolume, ViCellBlu.VcbResultReagentVolume>().ForMember(dest => dest.ResponseDescription, opt => opt.MapFrom(src => src.Description));
                cfg.CreateMap<ViCellBlu.VcbResultReagentVolume, GrpcService.VcbResultReagentVolume>().ConvertUsing(new AutomationResponseReagentVolumeTypeConverter());

                cfg.CreateMap<GrpcService.ErrorStatusType, ViCellBlu.ErrorStatusType>().ReverseMap();
            });

            return mapperConfig;
        }

        public class AutomationResponseTypeConverter
            : ITypeConverter<ViCellBlu.VcbResult, GrpcService.VcbResult>
        {
            public GrpcService.VcbResult Convert(ViCellBlu.VcbResult source, GrpcService.VcbResult destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResult();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                return destination;
            }
        }

        public class AutomationResponseGetCellTypesTypeConverter
            : ITypeConverter<ViCellBlu.VcbResultGetCellTypes, GrpcService.VcbResultGetCellTypes>
        {
            public GrpcService.VcbResultGetCellTypes Convert(ViCellBlu.VcbResultGetCellTypes source, GrpcService.VcbResultGetCellTypes destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResultGetCellTypes();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                foreach (var item in source.CellTypes)
                {
                    destination.CellTypes.Add(context.Mapper.Map<GrpcService.CellType>(item));
                }

                return destination;
            }
        }

        public class AutomationResponseCreateCellTypeConverter
            : ITypeConverter<ViCellBlu.VcbResultCreateCellType, GrpcService.VcbResultCreateCellType>
        {
            public GrpcService.VcbResultCreateCellType Convert(ViCellBlu.VcbResultCreateCellType source, GrpcService.VcbResultCreateCellType destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResultCreateCellType();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                return destination;
            }
        }

        public class AutomationResponseRequestLockTypeConverter
            : ITypeConverter<ViCellBlu.VcbResultRequestLock, GrpcService.VcbResultRequestLock>
        {
            public GrpcService.VcbResultRequestLock Convert(ViCellBlu.VcbResultRequestLock source, GrpcService.VcbResultRequestLock destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResultRequestLock();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                destination.LockState = context.Mapper.Map<GrpcService.LockStateEnum>(source.LockState);
                return destination;
            }
        }

        public class AutomationResponseReleaseLockTypeConverter
            : ITypeConverter<ViCellBlu.VcbResultReleaseLock, GrpcService.VcbResultReleaseLock>
        {
            public GrpcService.VcbResultReleaseLock Convert(ViCellBlu.VcbResultReleaseLock source, GrpcService.VcbResultReleaseLock destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResultReleaseLock();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                destination.LockState = context.Mapper.Map<GrpcService.LockStateEnum>(source.LockState);
                return destination;
            }
        }

        public class AutomationResponseDeleteCellTypeTypeConverter
            : ITypeConverter<ViCellBlu.VcbResultDeleteCellType, GrpcService.VcbResultDeleteCellType>
        {
            public GrpcService.VcbResultDeleteCellType Convert(ViCellBlu.VcbResultDeleteCellType source, GrpcService.VcbResultDeleteCellType destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResultDeleteCellType();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                return destination;
            }
        }

        public class AutomationResponseCreateQualityControlTypeConverter
            : ITypeConverter<ViCellBlu.VcbResult, GrpcService.VcbResult>
        {
            public GrpcService.VcbResult Convert(ViCellBlu.VcbResult source, GrpcService.VcbResult destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResult();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                return destination;
            }
        }

        public class AutomationResponseGetQualityControlsTypeConverter
            : ITypeConverter<ViCellBlu.VcbResultGetQualityControls, GrpcService.VcbResultGetQualityControls>
        {
            public GrpcService.VcbResultGetQualityControls Convert(ViCellBlu.VcbResultGetQualityControls source, GrpcService.VcbResultGetQualityControls destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResultGetQualityControls();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                foreach (var item in source.QualityControls)
                {
                    destination.QualityControls.Add(context.Mapper.Map<GrpcService.QualityControl>(item));
                }
                return destination;
            }
        }

        public class AutomationResponseGetDiskSpaceTypeConverter
            : ITypeConverter<ViCellBlu.VcbResultGetDiskSpace, GrpcService.VcbResultGetDiskSpace>
        {
            public GrpcService.VcbResultGetDiskSpace Convert(ViCellBlu.VcbResultGetDiskSpace source, GrpcService.VcbResultGetDiskSpace destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResultGetDiskSpace();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                destination.TotalSizeBytes = source.TotalSizeBytes;
                destination.TotalFreeBytes = source.TotalFreeBytes;
                destination.DiskSpaceOtherBytes = source.DiskSpaceOtherBytes;
                destination.DiskSpaceDataBytes = source.DiskSpaceOtherBytes;
                destination.DiskSpaceExportBytes = source.DiskSpaceExportBytes;
                return destination;
            }
        }

        public class AutomationResponseEjectStageTypeConverter
            : ITypeConverter<ViCellBlu.VcbResultEjectStage, GrpcService.VcbResultEjectStage>
        {
            public GrpcService.VcbResultEjectStage Convert(ViCellBlu.VcbResultEjectStage source, GrpcService.VcbResultEjectStage destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResultEjectStage();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                return destination;
            }
        }

        public class AutomationResponseStartExportTypeConverter
            : ITypeConverter<ViCellBlu.VcbResultStartExport, GrpcService.VcbResultStartExport>
        {
            public GrpcService.VcbResultStartExport Convert(ViCellBlu.VcbResultStartExport source, GrpcService.VcbResultStartExport destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResultStartExport();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                destination.BulkDataId = source.BulkDataId;
                return destination;
            }
        }

        public class AutomationResponseRetrieveExportBlockTypeConverter
            : ITypeConverter<ViCellBlu.VcbResultRetrieveExportBlock, GrpcService.VcbResultRetrieveBulkDataBlock>
        {
            public GrpcService.VcbResultRetrieveBulkDataBlock Convert(ViCellBlu.VcbResultRetrieveExportBlock source, GrpcService.VcbResultRetrieveBulkDataBlock destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResultRetrieveBulkDataBlock();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                destination.BlockIndex = source.BlockData.Index;
                destination.BulkDataId = source.BlockData.BulkDataId;
                destination.SampleResultsZipFileBytes = ByteString.CopyFrom(source.BlockData.FileData);
                return destination;
            }
        }
        public class AutomationResponseRetrieveExportBlockTypeConverterRev
            : ITypeConverter<GrpcService.VcbResultRetrieveBulkDataBlock, ViCellBlu.VcbResultRetrieveExportBlock>
        {
            public ViCellBlu.VcbResultRetrieveExportBlock Convert(GrpcService.VcbResultRetrieveBulkDataBlock source, ViCellBlu.VcbResultRetrieveExportBlock destination, ResolutionContext context)
            {
                destination ??= new ViCellBlu.VcbResultRetrieveExportBlock();
                destination.ResponseDescription = source.Description ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ViCellBlu.ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<ViCellBlu.MethodResultEnum>(source.MethodResult);
                destination.BlockData = new ExportBlockData();
                destination.BlockData.Index = source.BlockIndex;
                destination.BlockData.BulkDataId = source.BulkDataId;
                destination.BlockData.Status = context.Mapper.Map<ViCellBlu.GetBlockStateEnum>(source.Status);
                destination.BlockData.FileData = source.SampleResultsZipFileBytes.ToByteArray();
                return destination;
            }
        }


        public class AutomationResponseExportConfigTypeConverter
            : ITypeConverter<ViCellBlu.VcbResultExportConfig, GrpcService.VcbResultExportConfig>
        {
            public GrpcService.VcbResultExportConfig Convert(ViCellBlu.VcbResultExportConfig source, GrpcService.VcbResultExportConfig destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResultExportConfig();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                destination.FileData = ByteString.CopyFrom(source.FileData);
                return destination;
            }
        }

        public class SampleResultCollectionConverter<TITemSource, TSampleResultCollection>
            : ITypeConverter<RepeatedField<TITemSource>, ViCellBlu.SampleResultCollection>
        {
            public ViCellBlu.SampleResultCollection Convert(RepeatedField<TITemSource> source,
                ViCellBlu.SampleResultCollection destination, ResolutionContext context)
            {
                destination ??= new ViCellBlu.SampleResultCollection();
                foreach (var item in source)
                {
                    destination.Add(context.Mapper.Map<ViCellBlu.SampleResult>(item));
                }

                return destination;
            }
        }

        public class CollectionToRepeatedSampleResultConverter<TSampleResultCollection, TITemSource>
            : ITypeConverter<ViCellBlu.SampleResultCollection, RepeatedField<TITemSource>>
        {
            public RepeatedField<TITemSource> Convert(SampleResultCollection source, RepeatedField<TITemSource> destination, ResolutionContext context)
            {
                destination ??= new RepeatedField<TITemSource>();
                foreach (var item in source)
                {
                    destination.Add(context.Mapper.Map<TITemSource>(item));
                }

                return destination;
            }
        }

        public class AutomationResponseGetSampleResultsTypeConverter
            : ITypeConverter<ViCellBlu.VcbResultGetSampleResults, GrpcService.VcbResultGetSampleResults>
        {
            public GrpcService.VcbResultGetSampleResults Convert(ViCellBlu.VcbResultGetSampleResults source, GrpcService.VcbResultGetSampleResults destination, ResolutionContext context)
            {
                destination ??= new GrpcService.VcbResultGetSampleResults();
                destination.Description = source.ResponseDescription ?? string.Empty;
                destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
                destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
                foreach (var item in source.SampleResults)
                {
                    destination.SampleResults.Add(context.Mapper.Map<GrpcService.SampleResult>(item));
                }
                return destination;
            }
        }

        public class AutomationResponseReagentVolumeTypeConverter
	        : ITypeConverter<ViCellBlu.VcbResultReagentVolume, GrpcService.VcbResultReagentVolume>
        {
	        public GrpcService.VcbResultReagentVolume Convert(ViCellBlu.VcbResultReagentVolume source, GrpcService.VcbResultReagentVolume destination, ResolutionContext context)
	        {
		        destination ??= new GrpcService.VcbResultReagentVolume();
		        destination.Description = source.ResponseDescription ?? string.Empty;
		        destination.ErrorLevel = context.Mapper.Map<ErrorLevelEnum>(source.ErrorLevel);
		        destination.MethodResult = context.Mapper.Map<MethodResultEnum>(source.MethodResult);
				destination.Volume = context.Mapper.Map<int>(source.Volume);
		        return destination;
	        }
        }
    }
}