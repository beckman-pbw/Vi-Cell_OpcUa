using System;
using ViCellBlu;
using Opc.Ua;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    public interface ISampleResultsManager
    {
        ServiceResult HandleRequestStartExport(NodeId sessionId, 
	        string[] uuids,
	        ExportTypeEnum exportType,
	        ExportImagesEnum exportImages,
	        uint nthImageToExport,
	        bool isAutomationExport,
	        ref VcbResultStartExport methodResult);

        ServiceResult HandleRequestRetrieveExportBlock(NodeId sessionId, 
	        string bulkDataId, UInt32 blockIndex, 
	        ref VcbResultRetrieveExportBlock methodResult);

        ServiceResult HandleRequestGetSampleResult(NodeId sessionId, 
	        string username, DateTime fromDate, DateTime toDate,
            FilterOnEnum filterType, string cellTypeQualityControlName, 
	        string searchNameString, string searchTagString,
            ref VcbResultGetSampleResults methodResult);

        ServiceResult HandleRequestDeleteSampleResults(NodeId sessionId, 
	        Uuid[] uuids, bool retainResultsAndFirstImage, 
	        ref VcbResult methodResult);

        ServiceResult HandleRequestDeleteCampaignData(NodeId sessionId,
	        ref VcbResult methodResult);

        ServiceResult HandleRequestStartLogDataExport(NodeId sessionId, 
	        string filename, DateTime fromDate, DateTime toDate,
	        ref ViCellBlu.VcbResultStartExport methodResult);
    }
}
