using ViCellBlu;
using Google.Protobuf.WellKnownTypes;
using GrpcService;
using Ninject;
using NUnit.Framework;
using ViCellBluOpcUaModelDesign.OpcUa;
using FilterOnEnum = GrpcService.FilterOnEnum;

namespace OpcUaIntegrationTest
{
    [TestFixture]
    public class SampleResultsManagerTest : OpcUaBaseTestClass
    {
        private TestSampleResultsManager _sampleResultsManager;
        private BecNodeManager _nodeManager;

        public override void Setup()
        {
            base.Setup();
            _sampleResultsManager = Kernel.Get<TestSampleResultsManager>();
            _nodeManager = Kernel.Get<BecNodeManager>();
        }

        public void SampleResultsManagerGetSamples()
        {
            // Formulate request
            var request = new RequestGetSampleResults
            {
                Username = string.Empty,
                FromDate = new Timestamp(),
                ToDate = new Timestamp(),
                FilterType = FilterOnEnum.FilterSampleSet,
                CellTypeQualityControlName = string.Empty,
                SearchNameString = string.Empty,
                SearchTagString = string.Empty
            };

            //TODO: This integration test should be further implemented when all API methods are completed.

            // Publish results
            _sampleResultsManager.PublishSamples(new SampleResultCollection());

            MockNodeService.Verify();
        }
    }
}
