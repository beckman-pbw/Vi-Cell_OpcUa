using System;
using ViCellBlu;
using GrpcService;

namespace OpcUaIntegrationTest
{
    public interface ITestSampleResultsManager
    {
        IObservable<SampleResultCollection> SubscribeGetSampleResults();
        void GetSampleResults(RequestGetSampleResults request);
        void PublishSamples(SampleResultCollection sampleData);
    }
}
