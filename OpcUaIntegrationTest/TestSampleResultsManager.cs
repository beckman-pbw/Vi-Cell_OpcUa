using System;
using System.Reactive.Subjects;
using ViCellBlu;
using GrpcService;

namespace OpcUaIntegrationTest
{
    public class TestSampleResultsManager : ITestSampleResultsManager
    {
        private readonly Subject<SampleResultCollection> _sampleResultChangeSubject;

        public TestSampleResultsManager()
        {
            _sampleResultChangeSubject = new Subject<SampleResultCollection>();
        }

        public IObservable<SampleResultCollection> SubscribeGetSampleResults()
        {
            return _sampleResultChangeSubject;
        }

        public void GetSampleResults(RequestGetSampleResults request)
        {
            
        }

        public void PublishSamples(SampleResultCollection sampleData)
        {
            _sampleResultChangeSubject.OnNext(sampleData);
        }
    }
}
