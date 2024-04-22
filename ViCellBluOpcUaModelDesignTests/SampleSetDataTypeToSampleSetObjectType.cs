using System;
using ViCellBlu;
using GrpcService;
using NUnit.Framework;
using Opc.Ua;

namespace ViCellBluOpcUaModelDesignTests
{
    public class SampleSetToSampleSetConfig : BaseAutoMapperTests
    {
        [Test]
        public void SampleSetToSampleSetConfig_Name()
        {
            var source = new SampleSet();
            var map = new SampleSetConfig();

            source = new SampleSet
            {
                SampleSetName = "my set name"
            };
            map = Mapper.Map<SampleSetConfig>(source);

            Assert.IsNotNull(map);
            Assert.AreEqual(source.SampleSetName, map.SampleSetName);

            source = new SampleSet
            {
                SampleSetName = null
            };
            map = Mapper.Map<SampleSetConfig>(source);

            Assert.IsNotNull(map);
            Assert.AreEqual(string.Empty, map.SampleSetName);
        }

        [Test]
        public void SampleSetToSampleSetConfig_Uuid()
        {
            var source = new SampleSet();
            var map = new SampleSetConfig();

            source = new SampleSet
            {
                SampleSetUuid = new Uuid(Guid.NewGuid())
            };
            map = Mapper.Map<SampleSetConfig>(source);

            Assert.IsNotNull(map);
            Assert.AreEqual(source.SampleSetUuid.GuidString.ToUpper(), map.SampleSetUuid.ToUpper());

            source = new SampleSet
            {
                SampleSetName = null
            };
            map = Mapper.Map<SampleSetConfig>(source);

            Assert.IsNotNull(map);
            Assert.AreEqual(Uuid.Empty.GuidString.ToUpper(), map.SampleSetUuid.ToUpper());
        }

        [Test]
        public void SampleSetToSampleSetConfig_Samples()
        {
            var source = new SampleSet();
            var map = new SampleSetConfig();
            var samples = new SampleConfigCollection();

            samples.Add(new ViCellBlu.SampleConfig
            {
                SampleName = "sample 1",
                CellType = new ViCellBlu.CellType { CellTypeName = "Yeast"},
                Dilution = 2,
                SamplePosition = new ViCellBlu.SamplePosition { Row = "A", Column = 4},
                SaveEveryNthImage = 2,
                Tag = "tag 1"
            });
            samples.Add(new ViCellBlu.SampleConfig
            {
                SampleName = "sample 2",
                CellType = new ViCellBlu.CellType { CellTypeName = "Insect"},
                Dilution = 4,
                SamplePosition = new ViCellBlu.SamplePosition { Row = "D", Column = 3},
                SaveEveryNthImage = 4,
                Tag = "tag 2"
            });

            source = new SampleSet
            {
                Samples = samples
            };
            map = Mapper.Map<SampleSetConfig>(source);

            Assert.IsNotNull(map);
            Assert.AreEqual(2, map.Samples.Count);

            Assert.AreEqual(source.Samples[0].SampleName, map.Samples[0].SampleName);
            Assert.AreEqual(source.Samples[0].CellType.CellTypeName, map.Samples[0].CellType.CellTypeName);
            Assert.AreEqual(source.Samples[0].Dilution, map.Samples[0].Dilution);
            Assert.AreEqual(source.Samples[0].SamplePosition.Row, map.Samples[0].SamplePosition.Row);
            Assert.AreEqual(source.Samples[0].SamplePosition.Column, map.Samples[0].SamplePosition.Column);
            Assert.AreEqual(source.Samples[0].SaveEveryNthImage, map.Samples[0].SaveEveryNthImage);
            Assert.AreEqual(source.Samples[0].Tag, map.Samples[0].Tag);

            Assert.AreEqual(source.Samples[1].SampleName, map.Samples[1].SampleName);
            Assert.AreEqual(source.Samples[1].CellType.CellTypeName, map.Samples[1].CellType.CellTypeName);
            Assert.AreEqual(source.Samples[1].Dilution, map.Samples[1].Dilution);
            Assert.AreEqual(source.Samples[1].SamplePosition.Row, map.Samples[1].SamplePosition.Row);
            Assert.AreEqual(source.Samples[1].SamplePosition.Column, map.Samples[1].SamplePosition.Column);
            Assert.AreEqual(source.Samples[1].SaveEveryNthImage, map.Samples[1].SaveEveryNthImage);
            Assert.AreEqual(source.Samples[1].Tag, map.Samples[1].Tag);

            source = new SampleSet();
            map = Mapper.Map<SampleSetConfig>(source);
            Assert.IsNotNull(map);
        }
    }
}