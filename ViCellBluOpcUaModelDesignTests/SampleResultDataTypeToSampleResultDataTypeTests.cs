using System;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using GrpcService;
using Ninject;
using NUnit.Framework;
using Opc.Ua;
using ViCellBluOpcUaModelDesign;

namespace ViCellBluOpcUaModelDesignTests
{
    public class SampleResultToSampleResultTests
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
        public void SampleResultToSampleResult_SampleDataUuid()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.SampleDataUuid = Guid.NewGuid().ToString();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.SampleDataUuid, map.SampleDataUuid.ToString());

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(Uuid), map.SampleDataUuid);
        }

        [Test]
        public void SampleResultToSampleResult_SampleId()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.SampleId = "SampleId";
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.SampleId, map.SampleId);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(string.Empty, map.SampleId);
        }

        [Test]
        public void SampleResultToSampleResult_Status()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.Status = SampleStatusEnum.AcquisitionComplete;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual((ViCellBlu.SampleStatusEnum)sampleResult.Status, map.Status);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(ViCellBlu.SampleStatusEnum), map.Status);
        }

        [Test]
        public void SampleResultToSampleResult_CellCount()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.CellCount = 24;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.CellCount, map.CellCount);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.CellCount);
        }

        [Test]
        public void SampleResultToSampleResult_ViableCells()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.ViableCells = 22;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.ViableCells, map.ViableCells);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.ViableCells);
        }

        [Test]
        public void SampleResultToSampleResult_TotalCellsPerML()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.TotalCellsPerMilliliter = 1234567;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.TotalCellsPerMilliliter, map.TotalCellsPerMilliliter);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.TotalCellsPerMilliliter);
        }

        [Test]
        public void SampleResultToSampleResult_ViableCellsPerML()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.TotalCellsPerMilliliter = 1234567;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.ViableCellsPerMilliliter, map.ViableCellsPerMilliliter);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.ViableCellsPerMilliliter);
        }

        [Test]
        public void SampleResultToSampleResult_ViabilityPercent()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.ViabilityPercent = 122.345;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.ViabilityPercent, map.ViabilityPercent);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.ViabilityPercent);
        }

        [Test]
        public void SampleResultToSampleResult_AverageDiameter()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.AverageDiameter = 222.355;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.AverageDiameter, map.AverageDiameter);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.AverageDiameter);
        }

        [Test]
        public void SampleResultToSampleResult_AverageViableDiameter()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.AverageViableDiameter = 123.456;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.AverageViableDiameter, map.AverageViableDiameter);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.AverageViableDiameter);
        }

        [Test]
        public void SampleResultToSampleResult_AverageCircularity()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.AverageCircularity = 222.355;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.AverageCircularity, map.AverageCircularity);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.AverageCircularity);
        }

        [Test]
        public void SampleResultToSampleResult_AverageViableCircularity()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.AverageViableCircularity = 222.355;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.AverageViableCircularity, map.AverageViableCircularity);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.AverageViableCircularity);
        }

        [Test]
        public void SampleResultToSampleResult_AverageCellsPerImage()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.AverageCellsPerImage = 222.355;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.AverageCellsPerImage, map.AverageCellsPerImage);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.AverageCellsPerImage);
        }

        [Test]
        public void SampleResultToSampleResult_AverageBackgroundIntensity()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.AverageBackgroundIntensity = 222;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.AverageBackgroundIntensity, map.AverageBackgroundIntensity);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(double), map.AverageBackgroundIntensity);
        }

        [Test]
        public void SampleResultToSampleResult_BubbleCount()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.BubbleCount = 125;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.BubbleCount, map.BubbleCount);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.BubbleCount);
        }

        [Test]
        public void SampleResultToSampleResult_ClusterCount()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.ClusterCount = 125;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.ClusterCount, map.ClusterCount);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.ClusterCount);
        }

        [Test]
        public void SampleResultToSampleResult_ImagesForAnalysis()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.ImagesForAnalysis = 100;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.ImagesForAnalysis, map.ImagesForAnalysis);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.ImagesForAnalysis);
        }

        [Test]
        public void SampleResultToSampleResult_QCName()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.QualityControlName = "I'm a cell type";
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.QualityControlName, map.QualityControlName);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(string.Empty, map.QualityControlName);
        }

        [Test]
        public void SampleResultToSampleResult_CellType()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.CellType = "I'm a cell type";
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.CellType, map.CellType);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(string.Empty, map.CellType);
        }

        [Test]
        public void SampleResultToSampleResult_MinimumDiameter()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.MinimumDiameter = 50;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.MinimumDiameter, map.MinimumDiameter);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.MinimumDiameter);
        }

        [Test]
        public void SampleResultToSampleResult_MaximumDiameter()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.MaximumDiameter = 50;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.MaximumDiameter, map.MaximumDiameter);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.MaximumDiameter);
        }

        [Test]
        public void SampleResultToSampleResult_Images()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.Images = 93;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.Images, map.Images);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(int), map.Images);
        }

        [Test]
        public void SampleResultToSampleResult_CellSharpness()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.CellSharpness = 93.3f;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.CellSharpness, map.CellSharpness);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(float), map.CellSharpness);
        }

        [Test]
        public void SampleResultToSampleResult_MinimumCircularity()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.MinimumCircularity = 0.9f;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.MinimumCircularity, map.MinimumCircularity);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(float), map.MinimumCircularity);
        }

        [Test]
        public void SampleResultToSampleResult_DeclusterDegree()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.DeclusterDegree = DeclusterDegreeEnum.High;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual((ViCellBlu.DeclusterDegreeEnum)sampleResult.DeclusterDegree, map.DeclusterDegree);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(ViCellBlu.DeclusterDegreeEnum), map.DeclusterDegree);
        }

        [Test]
        public void SampleResultToSampleResult_AspirationCycles()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.AspirationCycles = 3;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.AspirationCycles, map.AspirationCycles);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(int), map.AspirationCycles);
        }

        [Test]
        public void SampleResultToSampleResult_ViableSpotBrightness()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.ViableSpotBrightness = 3.5f;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.ViableSpotBrightness, map.ViableSpotBrightness);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(float), map.ViableSpotBrightness);
        }

        [Test]
        public void SampleResultToSampleResult_ViableSpotArea()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.ViableSpotArea = 3.5f;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.ViableSpotArea, map.ViableSpotArea);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(float), map.ViableSpotArea);
        }

        [Test]
        public void SampleResultToSampleResult_MixingCycles()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.MixingCycles = 3;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.MixingCycles, map.MixingCycles);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(int), map.MixingCycles);
        }

        [Test]
        public void SampleResultToSampleResult_AnalysisDateTime()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.AnalysisDateTime = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime());
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.AnalysisDateTime.ToDateTime(), map.AnalysisDateTime);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(DateTime.MinValue.ToLocalTime(), map.AnalysisDateTime);
        }

        [Test]
        public void SampleResultToSampleResult_ReanalysisDateTime()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.ReanalysisDateTime = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime());
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.ReanalysisDateTime.ToDateTime(), map.ReanalysisDateTime);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(DateTime.MinValue.ToLocalTime(), map.ReanalysisDateTime);
        }

        [Test]
        public void SampleResultToSampleResult_AnalysisBy()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.AnalysisBy = "Username";
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.AnalysisBy, map.AnalysisBy);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(string.Empty, map.AnalysisBy);
        }

        [Test]
        public void SampleResultToSampleResult_ReanalysisBy()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.ReanalysisBy = "Username";
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.ReanalysisBy, map.ReanalysisBy);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(string.Empty, map.ReanalysisBy);
        }

        [Test]
        public void SampleResultToSampleResult_Dilution()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.Dilution = 78;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.Dilution, map.Dilution);

            sampleResult = new SampleResult();
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(default(uint), map.Dilution);
        }

        [Test]
        public void SampleResultToSampleResult_WashType()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.WashType = WashTypeEnum.FastWashType;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual((ViCellBlu.WashTypeEnum)sampleResult.WashType, map.WashType);
        }

        [Test]
        public void SampleResultToSampleResult_Tag()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.Tag = "Tag Tag Tag";
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.Tag, map.Tag);
        }

        [Test]
        public void SampleResultToSampleResult_NumImages()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.Images = 100;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.Images, map.Images);
        }

        [Test]
        public void SampleResultToSampleResult_ConcentrationAdjustmentFactor()
        {
            var sampleResult = new GrpcService.SampleResult();
            var map = new ViCellBlu.SampleResult();

            sampleResult.ConcentrationAdjustmentFactor = 4.5f;
            map = _mapper.Map<ViCellBlu.SampleResult>(sampleResult);
            Assert.IsNotNull(map);
            Assert.AreEqual(sampleResult.ConcentrationAdjustmentFactor, map.ConcentrationAdjustmentFactor);
        }
    }
}