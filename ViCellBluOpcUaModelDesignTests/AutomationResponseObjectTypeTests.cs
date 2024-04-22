using GrpcService;
using NUnit.Framework;

namespace ViCellBluOpcUaModelDesignTests
{
    public class AutomationResponseObjectTypeTests : BaseAutoMapperTests
    {
        [TestCase("description test", ExpectedResult = "description test")]
        [TestCase("dsadsadsadsdsa", ExpectedResult = "dsadsadsadsdsa")]
        [TestCase("", ExpectedResult = "")]
        public string TestAutomationResponseObjectType1(string description)
        {
            var source = new VcbResult { Description = description };
            return Mapper.Map<ViCellBlu.VcbResult>(source).ResponseDescription;
        }

        [TestCase(ErrorLevelEnum.NoError, ExpectedResult = ViCellBlu.ErrorLevelEnum.NoError)]
        [TestCase(ErrorLevelEnum.Error, ExpectedResult = ViCellBlu.ErrorLevelEnum.Error)]
        [TestCase(ErrorLevelEnum.RequiresUserInteraction, ExpectedResult = ViCellBlu.ErrorLevelEnum.RequiresUserInteraction)]
        [TestCase(ErrorLevelEnum.Warning, ExpectedResult = ViCellBlu.ErrorLevelEnum.Warning)]
        public ViCellBlu.ErrorLevelEnum TestAutomationResponseObjectType2(ErrorLevelEnum errorLevel)
        {
            var source = new VcbResult { ErrorLevel = errorLevel };
            return Mapper.Map<ViCellBlu.VcbResult>(source).ErrorLevel;
        }
        
        [TestCase(MethodResultEnum.Failure, ExpectedResult = ViCellBlu.MethodResultEnum.Failure)]
        [TestCase(MethodResultEnum.Success, ExpectedResult = ViCellBlu.MethodResultEnum.Success)]
        public ViCellBlu.MethodResultEnum TestAutomationResponseObjectType4(MethodResultEnum methodResult)
        {
            var source = new VcbResult { MethodResult = methodResult };
            return Mapper.Map<ViCellBlu.VcbResult>(source).MethodResult;
        }

        // -------------------------

        [TestCase("description test", ExpectedResult = "description test")]
        [TestCase("dsadsadsadsdsa", ExpectedResult = "dsadsadsadsdsa")]
        [TestCase("", ExpectedResult = "")]
        public string TestAutomationResponseObjectType5(string description)
        {
            var source = new ViCellBlu.VcbResult { ResponseDescription = description };
            return Mapper.Map<VcbResult>(source).Description;
        }

        [TestCase(ViCellBlu.ErrorLevelEnum.NoError, ExpectedResult = ErrorLevelEnum.NoError)]
        [TestCase(ViCellBlu.ErrorLevelEnum.Error, ExpectedResult = ErrorLevelEnum.Error)]
        [TestCase(ViCellBlu.ErrorLevelEnum.RequiresUserInteraction, ExpectedResult = ErrorLevelEnum.RequiresUserInteraction)]
        [TestCase(ViCellBlu.ErrorLevelEnum.Warning, ExpectedResult = ErrorLevelEnum.Warning)]
        public ErrorLevelEnum TestAutomationResponseObjectType6(ViCellBlu.ErrorLevelEnum errorLevel)
        {
            var source = new ViCellBlu.VcbResult { ErrorLevel = errorLevel };
            return Mapper.Map<VcbResult>(source).ErrorLevel;
        }
        
        [TestCase(ViCellBlu.MethodResultEnum.Failure, ExpectedResult = MethodResultEnum.Failure)]
        [TestCase(ViCellBlu.MethodResultEnum.Success, ExpectedResult = MethodResultEnum.Success)]
        public MethodResultEnum TestAutomationResponseObjectType8(ViCellBlu.MethodResultEnum methodResult)
        {
            var source = new ViCellBlu.VcbResult { MethodResult = methodResult };
            return Mapper.Map<VcbResult>(source).MethodResult;
        }
    }
}