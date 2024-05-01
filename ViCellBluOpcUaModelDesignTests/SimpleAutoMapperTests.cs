using NUnit.Framework;

namespace ViCellBluOpcUaModelDesignTests
{
    [TestFixture]
    public class SimpleAutoMapperTests : BaseAutoMapperTests
    {
        [TestCase(GrpcService.LockStateEnum.Locked, ExpectedResult = ViCellBlu.LockStateEnum.Locked)]
        [TestCase(GrpcService.LockStateEnum.Unlocked, ExpectedResult = ViCellBlu.LockStateEnum.Unlocked)]
        public ViCellBlu.LockStateEnum LockStateEnumTests(GrpcService.LockStateEnum lockState)
        {
            return Mapper.Map<GrpcService.LockStateEnum, ViCellBlu.LockStateEnum>(lockState);
        }
        
        [TestCase(ViCellBlu.LockStateEnum.Locked, ExpectedResult = GrpcService.LockStateEnum.Locked)]
        [TestCase(ViCellBlu.LockStateEnum.Unlocked, ExpectedResult = GrpcService.LockStateEnum.Unlocked)]
        public GrpcService.LockStateEnum LockStateEnumTests2(ViCellBlu.LockStateEnum lockState)
        {
            return Mapper.Map<ViCellBlu.LockStateEnum, GrpcService.LockStateEnum>(lockState);
        }

        [TestCase(GrpcService.WorkflowTypeEnum.LowCellDensityWorkflowType, ExpectedResult = ViCellBlu.WorkflowTypeEnum.LowCellDensityWorkflow)]
        [TestCase(GrpcService.WorkflowTypeEnum.NormalWorkflowType, ExpectedResult = ViCellBlu.WorkflowTypeEnum.NormalWorkflow)]
        public ViCellBlu.WorkflowTypeEnum WorkflowTypeEnumTests(GrpcService.WorkflowTypeEnum lockState)
        {
            return Mapper.Map<GrpcService.WorkflowTypeEnum, ViCellBlu.WorkflowTypeEnum>(lockState);
        }

        [TestCase(ViCellBlu.WorkflowTypeEnum.LowCellDensityWorkflow, ExpectedResult = GrpcService.WorkflowTypeEnum.LowCellDensityWorkflowType)]
        [TestCase(ViCellBlu.WorkflowTypeEnum.NormalWorkflow, ExpectedResult = GrpcService.WorkflowTypeEnum.NormalWorkflowType)]
        public GrpcService.WorkflowTypeEnum WorkflowTypeEnumTests2(ViCellBlu.WorkflowTypeEnum lockState)
        {
            return Mapper.Map<ViCellBlu.WorkflowTypeEnum, GrpcService.WorkflowTypeEnum>(lockState);
        }

        [TestCase(GrpcService.PrecessionEnum.ColumnMajor, ExpectedResult = ViCellBlu.PlatePrecessionEnum.ColumnMajor)]
        [TestCase(GrpcService.PrecessionEnum.RowMajor, ExpectedResult = ViCellBlu.PlatePrecessionEnum.RowMajor)]
        public ViCellBlu.PlatePrecessionEnum PlatePrecessionEnumTests(GrpcService.PrecessionEnum lockState)
        {
            return Mapper.Map<GrpcService.PrecessionEnum, ViCellBlu.PlatePrecessionEnum>(lockState);
        }

        [TestCase(ViCellBlu.PlatePrecessionEnum.ColumnMajor, ExpectedResult = GrpcService.PrecessionEnum.ColumnMajor)]
        [TestCase(ViCellBlu.PlatePrecessionEnum.RowMajor, ExpectedResult = GrpcService.PrecessionEnum.RowMajor)]
        public GrpcService.PrecessionEnum PlatePrecessionEnumTests2(ViCellBlu.PlatePrecessionEnum lockState)
        {
            return Mapper.Map<ViCellBlu.PlatePrecessionEnum, GrpcService.PrecessionEnum>(lockState);
        }


        [TestCase(GrpcService.SampleStatusEnum.AcquisitionComplete, ExpectedResult = ViCellBlu.SampleStatusEnum.AcquisitionComplete)]
        [TestCase(GrpcService.SampleStatusEnum.Completed, ExpectedResult = ViCellBlu.SampleStatusEnum.Completed)]
        [TestCase(GrpcService.SampleStatusEnum.InProcessAspirating, ExpectedResult = ViCellBlu.SampleStatusEnum.InProcessAspirating)]
        [TestCase(GrpcService.SampleStatusEnum.InProcessCleaning, ExpectedResult = ViCellBlu.SampleStatusEnum.InProcessCleaning)]
        [TestCase(GrpcService.SampleStatusEnum.InProcessImageAcquisition, ExpectedResult = ViCellBlu.SampleStatusEnum.InProcessImageAcquisition)]
        [TestCase(GrpcService.SampleStatusEnum.InProcessMixing, ExpectedResult = ViCellBlu.SampleStatusEnum.InProcessMixing)]
        [TestCase(GrpcService.SampleStatusEnum.NotProcessed, ExpectedResult = ViCellBlu.SampleStatusEnum.NotProcessed)]
        [TestCase(GrpcService.SampleStatusEnum.SkipError, ExpectedResult = ViCellBlu.SampleStatusEnum.SkipError)]
        [TestCase(GrpcService.SampleStatusEnum.SkipManual, ExpectedResult = ViCellBlu.SampleStatusEnum.SkipManual)]
        public ViCellBlu.SampleStatusEnum SampleStatusEnumTests(GrpcService.SampleStatusEnum lockState)
        {
            return Mapper.Map<GrpcService.SampleStatusEnum, ViCellBlu.SampleStatusEnum>(lockState);
        }

        [TestCase(ViCellBlu.SampleStatusEnum.AcquisitionComplete, ExpectedResult = GrpcService.SampleStatusEnum.AcquisitionComplete)]
        [TestCase(ViCellBlu.SampleStatusEnum.Completed, ExpectedResult = GrpcService.SampleStatusEnum.Completed)]
        [TestCase(ViCellBlu.SampleStatusEnum.InProcessAspirating, ExpectedResult = GrpcService.SampleStatusEnum.InProcessAspirating)]
        [TestCase(ViCellBlu.SampleStatusEnum.InProcessCleaning, ExpectedResult = GrpcService.SampleStatusEnum.InProcessCleaning)]
        [TestCase(ViCellBlu.SampleStatusEnum.InProcessImageAcquisition, ExpectedResult = GrpcService.SampleStatusEnum.InProcessImageAcquisition)]
        [TestCase(ViCellBlu.SampleStatusEnum.InProcessMixing, ExpectedResult = GrpcService.SampleStatusEnum.InProcessMixing)]
        [TestCase(ViCellBlu.SampleStatusEnum.NotProcessed, ExpectedResult = GrpcService.SampleStatusEnum.NotProcessed)]
        [TestCase(ViCellBlu.SampleStatusEnum.SkipError, ExpectedResult = GrpcService.SampleStatusEnum.SkipError)]
        [TestCase(ViCellBlu.SampleStatusEnum.SkipManual, ExpectedResult = GrpcService.SampleStatusEnum.SkipManual)]
        public GrpcService.SampleStatusEnum SampleStatusEnumTests2(ViCellBlu.SampleStatusEnum lockState)
        {
            return Mapper.Map<ViCellBlu.SampleStatusEnum, GrpcService.SampleStatusEnum>(lockState);
        }

        [TestCase(GrpcService.MethodResultEnum.Failure, ExpectedResult = ViCellBlu.MethodResultEnum.Failure)]
        [TestCase(GrpcService.MethodResultEnum.Success, ExpectedResult = ViCellBlu.MethodResultEnum.Success)]
        public ViCellBlu.MethodResultEnum MethodResultEnumTests(GrpcService.MethodResultEnum lockState)
        {
            return Mapper.Map<GrpcService.MethodResultEnum, ViCellBlu.MethodResultEnum>(lockState);
        }

        [TestCase(ViCellBlu.MethodResultEnum.Failure, ExpectedResult = GrpcService.MethodResultEnum.Failure)]
        [TestCase(ViCellBlu.MethodResultEnum.Success, ExpectedResult = GrpcService.MethodResultEnum.Success)]
        public GrpcService.MethodResultEnum MethodResultEnumTests2(ViCellBlu.MethodResultEnum lockState)
        {
            return Mapper.Map<ViCellBlu.MethodResultEnum, GrpcService.MethodResultEnum>(lockState);
        }

        [TestCase(GrpcService.ErrorLevelEnum.NoError, ExpectedResult = ViCellBlu.ErrorLevelEnum.NoError)]
        [TestCase(GrpcService.ErrorLevelEnum.Error, ExpectedResult = ViCellBlu.ErrorLevelEnum.Error)]
        [TestCase(GrpcService.ErrorLevelEnum.RequiresUserInteraction, ExpectedResult = ViCellBlu.ErrorLevelEnum.RequiresUserInteraction)]
        [TestCase(GrpcService.ErrorLevelEnum.Warning, ExpectedResult = ViCellBlu.ErrorLevelEnum.Warning)]
        public ViCellBlu.ErrorLevelEnum ErrorLevelEnumTests(GrpcService.ErrorLevelEnum lockState)
        {
            return Mapper.Map<GrpcService.ErrorLevelEnum, ViCellBlu.ErrorLevelEnum>(lockState);
        }

        [TestCase(ViCellBlu.ErrorLevelEnum.NoError, ExpectedResult = GrpcService.ErrorLevelEnum.NoError)]
        [TestCase(ViCellBlu.ErrorLevelEnum.Error, ExpectedResult = GrpcService.ErrorLevelEnum.Error)]
        [TestCase(ViCellBlu.ErrorLevelEnum.RequiresUserInteraction, ExpectedResult = GrpcService.ErrorLevelEnum.RequiresUserInteraction)]
        [TestCase(ViCellBlu.ErrorLevelEnum.Warning, ExpectedResult = GrpcService.ErrorLevelEnum.Warning)]
        public GrpcService.ErrorLevelEnum ErrorLevelEnumTests2(ViCellBlu.ErrorLevelEnum lockState)
        {
            return Mapper.Map<ViCellBlu.ErrorLevelEnum, GrpcService.ErrorLevelEnum>(lockState);
        }
    }
}