using AutoMapper;
using GrpcService;
using Ninject;
using NUnit.Framework;
using ViCellBluOpcUaModelDesign;

namespace ViCellBluOpcUaModelDesignTests
{
    [TestFixture]
    public class CellTypeToCellType
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
        public void CellTypeToCellType_CellTypeName()
        {
            var cellType = new GrpcService.CellType();
            var map = new ViCellBlu.CellType();

            cellType.CellTypeName = "my qc name";
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(cellType.CellTypeName, map.CellTypeName);

            cellType = new CellType();
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.IsTrue(string.IsNullOrEmpty(map.CellTypeName));
        }

        [Test]
        public void CellTypeToCellType_MinDiameter()
        {
            var cellType = new GrpcService.CellType();
            var map = new ViCellBlu.CellType();

            cellType.MinDiameter = 32.45;
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(cellType.MinDiameter, map.MinDiameter);

            cellType = new CellType();
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.MinDiameter);
        }

        [Test]
        public void CellTypeToCellType_MaxDiameter()
        {
            var cellType = new GrpcService.CellType();
            var map = new ViCellBlu.CellType();

            cellType.MaxDiameter = 32.45;
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(cellType.MaxDiameter, map.MaxDiameter);

            cellType = new CellType();
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.MaxDiameter);
        }

        [Test]
        public void CellTypeToCellType_NumImages()
        {
            var cellType = new GrpcService.CellType();
            var map = new ViCellBlu.CellType();

            cellType.NumImages = 44;
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(cellType.NumImages, map.NumImages);

            cellType = new CellType();
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.NumImages);
        }

        [Test]
        public void CellTypeToCellType_CellSharpness()
        {
            var cellType = new GrpcService.CellType();
            var map = new ViCellBlu.CellType();

            cellType.CellSharpness = 44.7894f;
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(cellType.CellSharpness, map.CellSharpness);

            cellType = new CellType();
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.CellSharpness);
        }

        [Test]
        public void CellTypeToCellType_MinCircularity()
        {
            var cellType = new GrpcService.CellType();
            var map = new ViCellBlu.CellType();

            cellType.MinCircularity = 44.7894;
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(cellType.MinCircularity, map.MinCircularity);

            cellType = new CellType();
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.MinCircularity);
        }

        [Test]
        public void CellTypeToCellType_DeclusterDegree()
        {
            var cellType = new GrpcService.CellType();
            var map = new ViCellBlu.CellType();

            cellType.DeclusterDegree = DeclusterDegreeEnum.Medium;
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual((uint) cellType.DeclusterDegree, (uint)map.DeclusterDegree);

            cellType = new CellType();
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), (uint) map.DeclusterDegree);
        }

        [Test]
        public void CellTypeToCellType_NumAspirationCycles()
        {
            var cellType = new GrpcService.CellType();
            var map = new ViCellBlu.CellType();

            cellType.NumAspirationCycles = 44;
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(cellType.NumAspirationCycles, map.NumAspirationCycles);

            cellType = new CellType();
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.NumAspirationCycles);
        }

        [Test]
        public void CellTypeToCellType_ViableSpotBrightness()
        {
            var cellType = new GrpcService.CellType();
            var map = new ViCellBlu.CellType();

            cellType.ViableSpotBrightness = 44.7894f;
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(cellType.ViableSpotBrightness, map.ViableSpotBrightness);

            cellType = new CellType();
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.ViableSpotBrightness);
        }

        [Test]
        public void CellTypeToCellType_ViableSpotArea()
        {
            var cellType = new GrpcService.CellType();
            var map = new ViCellBlu.CellType();

            cellType.ViableSpotArea = 44.7894f;
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(cellType.ViableSpotArea, map.ViableSpotArea);

            cellType = new CellType();
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.ViableSpotArea);
        }

        [Test]
        public void CellTypeToCellType_NumMixingCycles()
        {
            var cellType = new GrpcService.CellType();
            var map = new ViCellBlu.CellType();

            cellType.NumMixingCycles = 894;
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(cellType.NumMixingCycles, map.NumMixingCycles);

            cellType = new CellType();
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.NumMixingCycles);
        }

        [Test]
        public void CellTypeToCellType_ConcentrationAdjustmentFactor()
        {
            var cellType = new GrpcService.CellType();
            var map = new ViCellBlu.CellType();

            cellType.ConcentrationAdjustmentFactor = 44.7894f;
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(cellType.ConcentrationAdjustmentFactor, map.ConcentrationAdjustmentFactor);

            cellType = new CellType();
            map = _mapper.Map<ViCellBlu.CellType>(cellType);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(float), map.ConcentrationAdjustmentFactor);
        }
    }
}