using System;
using AutoMapper;
using GrpcService;
using Ninject;
using NUnit.Framework;
using Opc.Ua;
using ViCellBluOpcUaModelDesign;

namespace ViCellBluOpcUaModelDesignTests
{
    public class SampleConfigToSample
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
        public void SampleConfigToSample_AverageViableDiameter()
        {
            var sample = new GrpcService.SampleConfig();
            var map = new ViCellBlu.SampleConfig();

            sample.SampleName = "my sample name";
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.SampleName, map.SampleName);

            sample = new SampleConfig();
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(string.Empty, map.SampleName);
        }

        [Test]
        public void SampleConfigToSample_SampleUuid()
        {
            var sample = new GrpcService.SampleConfig();
            var map = new ViCellBlu.SampleConfig();

            sample.SampleUuid = Guid.NewGuid().ToString();
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.SampleUuid.ToUpper(), map.SampleUuid.GuidString.ToUpper());

            sample = new SampleConfig();
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(Uuid.Empty.GuidString.ToUpper(), map.SampleUuid.ToString().ToUpper());
        }

        [Test]
        public void SampleConfigToSample_CellType()
        {
            var sample = new GrpcService.SampleConfig();
            var map = new ViCellBlu.SampleConfig();

            sample.CellType = new CellType
            {
                ConcentrationAdjustmentFactor = 123.456f,
            };
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.CellType.ConcentrationAdjustmentFactor, map.CellType.ConcentrationAdjustmentFactor);

            sample = new SampleConfig();
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(float), map.CellType.ConcentrationAdjustmentFactor);
        }

        [Test]
        public void SampleConfigToSample_QualityControl()
        {
            var sample = new GrpcService.SampleConfig();
            var map = new ViCellBlu.SampleConfig();

            sample.QualityControl = new QualityControl
            {
                QualityControlName = "qc name here",
                AcceptanceLimits = 54
            };
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.QualityControl.QualityControlName, map.QualityControl.QualityControlName);
            Assert.AreEqual(sample.QualityControl.AcceptanceLimits, map.QualityControl.AcceptanceLimits);

            sample = new SampleConfig();
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.IsTrue(string.IsNullOrEmpty(map.QualityControl.QualityControlName));
            Assert.AreEqual(default(uint), map.QualityControl.AcceptanceLimits);
        }

        [Test]
        public void SampleConfigToSample_Dilution()
        {
            var sample = new GrpcService.SampleConfig();
            var map = new ViCellBlu.SampleConfig();

            sample.Dilution = 21;
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.Dilution, map.Dilution);

            sample = new SampleConfig();
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.Dilution);
        }

        [Test]
        public void SampleConfigToSample_WashType()
        {
            var sample = new GrpcService.SampleConfig();
            var map = new ViCellBlu.SampleConfig();

            sample.WashType = WashTypeEnum.FastWashType;
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual((uint) sample.WashType, (uint) map.WashType);

            sample.WashType = WashTypeEnum.NormalWashType;
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual((uint) sample.WashType, (uint) map.WashType);

            sample = new SampleConfig();
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), (uint) map.WashType);
        }

        [Test]
        public void SampleConfigToSample_Tag()
        {
            var sample = new GrpcService.SampleConfig();
            var map = new ViCellBlu.SampleConfig();

            sample.Tag = "my cool tag";
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.Tag, map.Tag);

            sample = new SampleConfig();
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(string.Empty, map.Tag);
        }

        [Test]
        public void SampleConfigToSample_SamplePosition()
        {
            var sample = new GrpcService.SampleConfig();
            var map = new ViCellBlu.SampleConfig();

            sample.SamplePosition = new SamplePosition
            {
                Column = 1,
                Row = "Y"
            };
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.SamplePosition.Column, map.SamplePosition.Column);
            Assert.AreEqual(sample.SamplePosition.Row, map.SamplePosition.Row);

            sample = new SampleConfig();
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.SamplePosition.Column);
            Assert.IsTrue(string.IsNullOrEmpty(map.SamplePosition.Row));
        }

        [Test]
        public void SampleConfigToSample_SaveEveryNthImage()
        {
            var sample = new GrpcService.SampleConfig();
            var map = new ViCellBlu.SampleConfig();

            sample.SaveEveryNthImage = 17;
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(sample.SaveEveryNthImage, map.SaveEveryNthImage);

            sample = new SampleConfig();
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.SaveEveryNthImage);
        }

        [Test]
        public void SampleConfigToSample_SampleStatus()
        {
            var sample = new GrpcService.SampleConfig();
            var map = new ViCellBlu.SampleConfig();

            sample.SampleStatus = SampleStatusEnum.InProcessMixing;
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual((uint) sample.SampleStatus, (uint)map.SampleStatus);

            sample = new SampleConfig();
            map = _mapper.Map<ViCellBlu.SampleConfig>(sample);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(ViCellBlu.SampleStatusEnum), map.SampleStatus);
        }
    }
}