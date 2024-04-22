using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using GrpcService;
using Opc.Ua;
using System;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.OpcUa;

namespace ViCellBluOpcUaModelDesign.Services
{
    public class SampleResultsManager : ISampleResultsManager
    {
        private readonly IMapper _mapper;
        private readonly BecOpcServer _opcServer;
        private readonly IResultResponseService _resultResponseService;

        public SampleResultsManager(IMapper mapper, BecOpcServer opcServer,
            IResultResponseService resultResponseService)
        {
            _mapper = mapper;
            _opcServer = opcServer;
            _resultResponseService = resultResponseService;
        }

        public ServiceResult HandleRequestGetSampleResult(NodeId sessionId, string username, DateTime fromDate, 
            DateTime toDate, ViCellBlu.FilterOnEnum filterType, string cellTypeQualityControlName, 
            string searchNameString, string searchTagString, 
            ref ViCellBlu.VcbResultGetSampleResults methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var sampleResultsRequest = new RequestGetSampleResults
                {
                    FromDate = Timestamp.FromDateTime(fromDate.ToUniversalTime()),
                    ToDate = Timestamp.FromDateTime(toDate.ToUniversalTime()),
                    Username = username,
                    FilterType = filterType == ViCellBlu.FilterOnEnum.Sample
                        ? GrpcService.FilterOnEnum.FilterSample
                        : GrpcService.FilterOnEnum.FilterSampleSet,
                    SearchNameString = searchNameString,
                    SearchTagString = searchTagString,
                    CellTypeQualityControlName = cellTypeQualityControlName
                };
                var result = opcUser.GrpcClient.SendRequestGetSampleResults(sampleResultsRequest);
                return _resultResponseService.CreateViCellBluGetSampleResultsResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleRequestGetSampleResult), e, ref methodResult);
            }
        }

        public ServiceResult HandleRequestStartExport(NodeId sessionId, 
	        string[] uuids, 
	        ViCellBlu.ExportTypeEnum exportType, 
	        ViCellBlu.ExportImagesEnum exportImages, 
	        uint nthImageToExport,
	        bool isAutomationExport, 
	        ref ViCellBlu.VcbResultStartExport methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var exportRequest = new RequestStartExport
                {
	                SampleListUuid = { uuids }, 
	                ExportType = ConvertExportType(exportType), 
	                ImageExport = ConvertExportImages(exportImages), 
	                NthImageToExport = nthImageToExport, 
	                IsAutomationExport = isAutomationExport,
				};
                var result = opcUser.GrpcClient.SendStartExport(exportRequest);
                return _resultResponseService.CreateViCellBluStartExportResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleRequestStartExport), e, ref methodResult);
            }
        }

        public ServiceResult HandleRequestRetrieveExportBlock(NodeId sessionId, string bulkDataId, UInt32 blockIndex, ref ViCellBlu.VcbResultRetrieveExportBlock methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var exportDataRequest = new RequestRetrieveBulkDataBlock
                {
                    BlockIndex = blockIndex,
                    BulkDataId = bulkDataId
				};
                var result = opcUser.GrpcClient.SendRetrieveBulkDataBlock(exportDataRequest);
                return _resultResponseService.CreateViCellBluRetrieveExportBlockResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleRequestRetrieveExportBlock), e, ref methodResult);
            }
        }

        public ServiceResult HandleRequestDeleteSampleResults(NodeId sessionId, Uuid[] uuids,
            bool retainResultsAndFirstImage, ref ViCellBlu.VcbResult methodResult)
        {
            try
            {
                var opcUser = _opcServer.LookupUserBySession(sessionId);
                var sampleRetrieveRequest = GetDeleteSampleResultsRequest(uuids, retainResultsAndFirstImage);
                var result = opcUser.GrpcClient.SendRequestDeleteSampleResults(sampleRetrieveRequest);
                return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
            }
            catch (Exception e)
            {
                return _resultResponseService.CreateResponseForGrpcCallException(
                    nameof(HandleRequestDeleteSampleResults), e, ref methodResult);
            }
        }

        public ServiceResult HandleRequestDeleteCampaignData(NodeId sessionId, ref ViCellBlu.VcbResult methodResult)
        {
	        try
	        {
		        var opcUser = _opcServer.LookupUserBySession(sessionId);
		        var request = new RequestDeleteCampaignData();
		        var result = opcUser.GrpcClient.SendRequestDeleteCampaignData(request);
		        return _resultResponseService.CreateViCellBluResponse(result, ref methodResult);
	        }
	        catch (Exception e)
	        {
		        return _resultResponseService.CreateResponseForGrpcCallException(
			        nameof(HandleRequestDeleteCampaignData), e, ref methodResult);
	        }
        }

        public ServiceResult HandleRequestStartLogDataExport(NodeId sessionId, 
	        string filename, DateTime fromDate, DateTime toDate, ref ViCellBlu.VcbResultStartExport methodResult)
        {
	        try
	        {
		        var opcUser = _opcServer.LookupUserBySession(sessionId);
		        var request = new RequestStartLogDataExport
				{
					Filename = filename,
			        FromDate = Timestamp.FromDateTime(fromDate.ToUniversalTime()),
			        ToDate = Timestamp.FromDateTime(toDate.ToUniversalTime()),
		        };
		        var result = opcUser.GrpcClient.SendRequestStartLogDataExport(request);
		        return _resultResponseService.CreateViCellBluStartExportResponse(result, ref methodResult);
	        }
	        catch (Exception e)
	        {
		        return _resultResponseService.CreateResponseForGrpcCallException(
			        nameof(HandleRequestStartLogDataExport), e, ref methodResult);
	        }
        }

		#region Helper Methods

		private RequestDeleteSampleResults GetDeleteSampleResultsRequest(Uuid[] uuids, bool retainResultsAndFirstImage)
        {
            var sampleRetrieveRequest = new RequestDeleteSampleResults
            {
                RetainResultsAndFirstImage = retainResultsAndFirstImage
            };

            // sampleRetrieveRequest.Uuids is readonly but the list is mutable, so you have to add the 
            // objects in a foreach loop
            foreach (var i in uuids)
            {
                var map = _mapper.Map<string>(i);
                sampleRetrieveRequest.Uuids.Add(map);
            }

            return sampleRetrieveRequest;
        }

		private GrpcService.ExportTypeEnum ConvertExportType(ViCellBlu.ExportTypeEnum type)
		{
			GrpcService.ExportTypeEnum grpcValue = GrpcService.ExportTypeEnum.Csv;

			if (type == ViCellBlu.ExportTypeEnum.Offline)
			{
				grpcValue = GrpcService.ExportTypeEnum.Offline;
			}

			return grpcValue;
		}

		private GrpcService.ExportImagesEnum ConvertExportImages(ViCellBlu.ExportImagesEnum type)
		{
			GrpcService.ExportImagesEnum grpcValue = GrpcService.ExportImagesEnum.All;

			switch (type)
			{
				case ViCellBlu.ExportImagesEnum.FirstAndLastOnly:
					grpcValue = GrpcService.ExportImagesEnum.FirstAndLastOnly;
					break;
				case ViCellBlu.ExportImagesEnum.NthImage:
					grpcValue = GrpcService.ExportImagesEnum.NthImage;
					break;
			}

			return grpcValue;
		}

	#endregion

    }
}
