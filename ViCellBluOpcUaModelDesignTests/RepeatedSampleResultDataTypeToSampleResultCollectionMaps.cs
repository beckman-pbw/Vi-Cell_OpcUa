using System;
using ViCellBlu;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using GrpcService;
using NUnit.Framework;
using SampleResult = GrpcService.SampleResult;

namespace ViCellBluOpcUaModelDesignTests
{
    [TestFixture]
    public class RepeatedSampleResultToSampleResultCollectionMaps : BaseAutoMapperTests
    {
        [Test]
        public void Test1()
        {
            var repeated = new RepeatedField<GrpcService.SampleResult>();
            repeated.Add(GetSampleResult());
            repeated.Add(GetSampleResult());
            repeated.Add(GetSampleResult());

            var map = Mapper.Map<SampleResultCollection>(repeated);

            Assert.IsNotNull(map);
            Assert.AreEqual(repeated.Count, map.Count);
            AssertIndexElementsAreEqual(repeated, map, 0);
            AssertIndexElementsAreEqual(repeated, map, 1);
            AssertIndexElementsAreEqual(repeated, map, 2);
        }

        [Test]
        public void Test2()
        {
            var repeated = new RepeatedField<GrpcService.SampleResult>();
            repeated.Add(new SampleResult());
            repeated.Add(new SampleResult());
            repeated.Add(new SampleResult());

            var map = Mapper.Map<SampleResultCollection>(repeated);

            Assert.IsNotNull(map);
            Assert.AreEqual(repeated.Count, map.Count);
        }

        private static void AssertIndexElementsAreEqual(RepeatedField<SampleResult> repeated, SampleResultCollection map, int index)
        {
            Assert.AreEqual(repeated[index].AverageBackgroundIntensity, map[index].AverageBackgroundIntensity);
            Assert.AreEqual(repeated[index].AverageCellsPerImage, map[index].AverageCellsPerImage);
            Assert.AreEqual(repeated[index].AverageCircularity, map[index].AverageCircularity);
            Assert.AreEqual(repeated[index].AverageDiameter, map[index].AverageDiameter);
            Assert.AreEqual(repeated[index].AverageViableDiameter, map[index].AverageViableDiameter);
            Assert.AreEqual(repeated[index].BubbleCount, map[index].BubbleCount);
            Assert.AreEqual(repeated[index].CellCount, map[index].CellCount);
            Assert.AreEqual(repeated[index].ClusterCount, map[index].ClusterCount);
            Assert.AreEqual(repeated[index].AnalysisDateTime.ToDateTime(), map[index].AnalysisDateTime);
            Assert.AreEqual(repeated[index].CellType, map[index].CellType);
            Assert.AreEqual(repeated[index].Dilution, map[index].Dilution);
            Assert.AreEqual(repeated[index].SampleId, map[index].SampleId);
        }

        private SampleResult GetSampleResult()
        {
            var rand = new Random(DateTime.Now.Millisecond * 68);
            return new SampleResult
            {
                AverageBackgroundIntensity = (uint) rand.Next(0, 9999),
                AverageCellsPerImage = rand.NextDouble(),
                AverageCircularity = rand.NextDouble(),
                AverageDiameter = rand.NextDouble(),
                AverageViableDiameter = rand.NextDouble(),
                BubbleCount = (uint)rand.Next(0, 9999),
                CellCount = (uint)rand.Next(0, 9999),
                ClusterCount = (uint)rand.Next(0, 9999),
                AnalysisDateTime = Timestamp.FromDateTime(DateTime.UtcNow),
                CellType = "Insect",
                Dilution = 1,
                SampleId = "name",    
                AnalysisBy = "miah",
            };
        }
    }
}