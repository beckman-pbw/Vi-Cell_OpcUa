using AutoMapper;
using GrpcService;
using Ninject;
using NUnit.Framework;
using System;
using Google.Protobuf.WellKnownTypes;
using ViCellBluOpcUaModelDesign;

namespace ViCellBluOpcUaModelDesignTests
{
    [TestFixture]
    public class QualityControlToQualityControl
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
        public void QualityControlToQualityControl_AverageViableDiameter()
        {
            var qc = new GrpcService.QualityControl();
            var map = new ViCellBlu.QualityControl();

            qc.QualityControlName = "my qc name";
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.AreEqual(qc.QualityControlName, map.QualityControlName);

            qc = new QualityControl();
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.IsTrue(string.IsNullOrEmpty(map.QualityControlName));
        }

        [Test]
        public void QualityControlToQualityControl_Comments()
        {
            var qc = new GrpcService.QualityControl();
            var map = new ViCellBlu.QualityControl();

            qc.Comments = "my qc name";
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.AreEqual(qc.Comments, map.Comments);

            qc = new QualityControl();
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.IsTrue(string.IsNullOrEmpty(map.Comments));
        }

        [Test]
        public void QualityControlToQualityControl_CellTypeName()
        {
            var qc = new GrpcService.QualityControl();
            var map = new ViCellBlu.QualityControl();

            qc.CellTypeName = "Fake";
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.AreEqual(qc.CellTypeName, map.CellTypeName);

            qc = new QualityControl();
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.IsTrue(string.IsNullOrEmpty(map.CellTypeName));
        }

        [Test]
        public void QualityControlToQualityControl_AssayParameter()
        {
            var qc = new GrpcService.QualityControl();
            var map = new ViCellBlu.QualityControl();

            qc.AssayParameter = AssayParameterEnum.PopulationPercentage;
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.AreEqual((uint) qc.AssayParameter, (uint) map.AssayParameter);

            qc = new QualityControl();
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), (uint) map.AssayParameter);
        }

        [Test]
        public void QualityControlToQualityControl_AssayValue()
        {
            var qc = new GrpcService.QualityControl();
            var map = new ViCellBlu.QualityControl();

            qc.AssayValue = 654.321;
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.AreEqual((uint) qc.AssayValue, (uint) map.AssayValue);

            qc = new QualityControl();
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), (uint) map.AssayValue);
        }

        [Test]
        public void QualityControlToQualityControl_AcceptanceLimits()
        {
            var qc = new GrpcService.QualityControl();
            var map = new ViCellBlu.QualityControl();

            qc.AcceptanceLimits = 98;
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.AreEqual(qc.AcceptanceLimits, map.AcceptanceLimits);

            qc = new QualityControl();
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.AcceptanceLimits);
        }

        [Test]
        public void QualityControlToQualityControl_ExpirationDate()
        {
            var qc = new GrpcService.QualityControl();
            var map = new ViCellBlu.QualityControl();

            qc.ExpirationDate = Timestamp.FromDateTime(DateTime.UtcNow);
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.AreEqual(qc.ExpirationDate.ToDateTime(), map.ExpirationDate);

            qc = new QualityControl();
            map = _mapper.Map<ViCellBlu.QualityControl>(qc);
            Assert.IsNotNull(map);
            Assert.AreEqual(DateTime.MinValue, map.ExpirationDate);
        }
    }
}