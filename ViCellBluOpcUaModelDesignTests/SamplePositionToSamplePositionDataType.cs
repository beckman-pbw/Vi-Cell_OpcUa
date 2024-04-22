using AutoMapper;
using GrpcService;
using Ninject;
using NUnit.Framework;
using ViCellBluOpcUaModelDesign;

namespace ViCellBluOpcUaModelDesignTests
{
    [TestFixture]
    public class SamplePositionToSamplePositionDataType
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
        public void SamplePositionToSamplePositionDataType_Row()
        {
            var samplePosition = new GrpcService.SamplePosition();
            var map = new ViCellBlu.SamplePosition();

            samplePosition.Row = "Y";
            map = _mapper.Map<ViCellBlu.SamplePosition>(samplePosition);
            Assert.IsNotNull(map);
            Assert.AreEqual(samplePosition.Row, map.Row);

            samplePosition.Row = "Z";
            map = _mapper.Map<ViCellBlu.SamplePosition>(samplePosition);
            Assert.IsNotNull(map);
            Assert.AreEqual(samplePosition.Row, map.Row);

            samplePosition.Row = "B";
            map = _mapper.Map<ViCellBlu.SamplePosition>(samplePosition);
            Assert.IsNotNull(map);
            Assert.AreEqual(samplePosition.Row, map.Row);

            samplePosition = new SamplePosition();
            map = _mapper.Map<ViCellBlu.SamplePosition>(samplePosition);
            Assert.IsNotNull(map);
            Assert.IsTrue(string.IsNullOrEmpty(map.Row));
        }

        [Test]
        public void SamplePositionToSamplePositionDataType_Column()
        {
            var samplePosition = new GrpcService.SamplePosition();
            var map = new ViCellBlu.SamplePosition();

            samplePosition.Column = 1;
            map = _mapper.Map<ViCellBlu.SamplePosition>(samplePosition);
            Assert.IsNotNull(map);
            Assert.AreEqual(samplePosition.Column, map.Column);

            samplePosition.Column = 5;
            map = _mapper.Map<ViCellBlu.SamplePosition>(samplePosition);
            Assert.IsNotNull(map);
            Assert.AreEqual(samplePosition.Column, map.Column);

            samplePosition = new SamplePosition();
            map = _mapper.Map<ViCellBlu.SamplePosition>(samplePosition);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.Column);
        }
    }
}