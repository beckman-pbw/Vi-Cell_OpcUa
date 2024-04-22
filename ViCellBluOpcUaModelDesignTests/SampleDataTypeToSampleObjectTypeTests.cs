using AutoMapper;
using Ninject;
using NUnit.Framework;
using Opc.Ua;
using System;
using ViCellBluOpcUaModelDesign;
using SampleConfig = GrpcService.SampleConfig;
using SampleStatusEnum = ViCellBlu.SampleStatusEnum;

namespace ViCellBluOpcUaModelDesignTests
{
    [TestFixture]
    public class SampleToSampleConfigTests
    {
        private IKernel _kernel;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _kernel = new StandardKernel(new BecOpcUaModule());
            _mapper = _kernel.Get<IMapper>();
            Assert.IsNotNull(_mapper);
        }

        [Test]
        public void SampleToSampleConfigTests_AverageViableDiameter()
        {
            var sample = new ViCellBlu.SampleConfig();
            var map = new SampleConfig();

            sample.SampleName = "my sample name";
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.SampleName, map.SampleName);

            sample = new ViCellBlu.SampleConfig();
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(string.Empty, map.SampleName);
        }

        [Test]
        public void SampleToSampleConfigTests_SampleUuid()
        {
            var sample = new ViCellBlu.SampleConfig();
            var map = new SampleConfig();

            sample.SampleUuid = new Uuid(Guid.NewGuid());
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.SampleUuid.GuidString.ToUpper(), map.SampleUuid.ToUpper());

            sample = new ViCellBlu.SampleConfig();
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(Uuid.Empty.GuidString.ToUpper(), map.SampleUuid.ToUpper());
        }

        [Test]
        public void SampleToSampleConfigTests_CellType()
        {
            var sample = new ViCellBlu.SampleConfig();
            var map = new SampleConfig();

            sample.CellType = new ViCellBlu.CellType
            {
                ConcentrationAdjustmentFactor = 123.456f,
            };
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.CellType.ConcentrationAdjustmentFactor, map.CellType.ConcentrationAdjustmentFactor);

            sample = new ViCellBlu.SampleConfig();
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(float), map.CellType.ConcentrationAdjustmentFactor);
        }

        [Test]
        public void SampleToSampleConfigTests_QualityControl()
        {
            var sample = new ViCellBlu.SampleConfig();
            var map = new SampleConfig();

            sample.QualityControl = new ViCellBlu.QualityControl
            {
                QualityControlName = "qc name here",
                AcceptanceLimits = 54
            };
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.QualityControl.QualityControlName, map.QualityControl.QualityControlName);
            Assert.AreEqual(sample.QualityControl.AcceptanceLimits, map.QualityControl.AcceptanceLimits);

            sample = new ViCellBlu.SampleConfig();
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.IsTrue(string.IsNullOrEmpty(map.QualityControl.QualityControlName));
            Assert.AreEqual(default(uint), map.QualityControl.AcceptanceLimits);
        }

        [Test]
        public void SampleToSampleConfigTests_Dilution()
        {
            var sample = new ViCellBlu.SampleConfig();
            var map = new SampleConfig();

            sample.Dilution = 21;
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.Dilution, map.Dilution);

            sample = new ViCellBlu.SampleConfig();
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.Dilution);
        }

        [Test]
        public void SampleToSampleConfigTests_WashType()
        {
            var sample = new ViCellBlu.SampleConfig();
            var map = new SampleConfig();

            sample.WashType = ViCellBlu.WashTypeEnum.FastWash;
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual((uint)sample.WashType, (uint)map.WashType);

            sample.WashType = ViCellBlu.WashTypeEnum.NormalWash;
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual((uint)sample.WashType, (uint)map.WashType);

            sample = new ViCellBlu.SampleConfig();
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), (uint)map.WashType);
        }

        [Test]
        public void SampleToSampleConfigTests_Tag()
        {
            var sample = new ViCellBlu.SampleConfig();
            var map = new SampleConfig();

            sample.Tag = "my cool tag";
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.Tag, map.Tag);

            sample = new ViCellBlu.SampleConfig();
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(string.Empty, map.Tag);
        }

        [Test]
        public void SampleToSampleConfigTests_SamplePosition()
        {
            var sample = new ViCellBlu.SampleConfig();
            var map = new SampleConfig();

            sample.SamplePosition = new ViCellBlu.SamplePosition
            {
                Column = 1,
                Row = "Y"
            };
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.SamplePosition.Column, map.SamplePosition.Column);
            Assert.AreEqual(sample.SamplePosition.Row, map.SamplePosition.Row);

            sample = new ViCellBlu.SampleConfig();
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.SamplePosition.Column);
            Assert.IsTrue(string.IsNullOrEmpty(map.SamplePosition.Row));
        }

        [Test]
        public void SampleToSampleConfigTests_SaveEveryNthImage()
        {
            var sample = new ViCellBlu.SampleConfig();
            var map = new SampleConfig();

            sample.SaveEveryNthImage = 17;
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.SaveEveryNthImage, map.SaveEveryNthImage);

            sample = new ViCellBlu.SampleConfig();
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.SaveEveryNthImage);
        }

        [Test]
        public void SampleToSampleConfigTests_SampleStatus()
        {
            var sample = new ViCellBlu.SampleConfig();
            var map = new SampleConfig();

            sample.SampleStatus = SampleStatusEnum.InProcessMixing;
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual((uint)sample.SampleStatus, (uint)map.SampleStatus);

            sample = new ViCellBlu.SampleConfig();
            map = _mapper.Map<SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual((uint) default(ViCellBlu.SampleStatusEnum), (uint) map.SampleStatus);
        }
    }
}