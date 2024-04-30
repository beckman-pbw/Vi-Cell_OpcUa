using Opc.Ua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViCellBlu;

namespace ViCellBLU_dotNET
{
    class DecodeHelper
    {
        // ******************************************************************
        // ******************************************************************
        // @todo move to private functions region
        public static ViCellBlu.VcbResult DecodeRaw(Object rawResult, ServiceMessageContext messageContext)
        {
            var callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Warning,
	            MethodResult = MethodResultEnum.Failure
            };

            callResult.ResponseDescription = "Decoding raw result ...";
            
            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                callResult.ErrorLevel = ErrorLevelEnum.RequiresUserInteraction;
                callResult.MethodResult = MethodResultEnum.Failure;
                callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
            }
            return callResult;
        }

        public static ViCellBlu.VcbResultStartExport DecodeRawStartExport(Object rawResult, ServiceMessageContext messageContext)
        {
            ViCellBlu.VcbResultStartExport callResult = new ViCellBlu.VcbResultStartExport {
				ErrorLevel = ErrorLevelEnum.Warning, 
				MethodResult = MethodResultEnum.Failure,
				BulkDataId = "",
            };

            callResult.ResponseDescription = "DecodeRawStartExport";
            
            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                callResult.ErrorLevel = ErrorLevelEnum.RequiresUserInteraction;
                callResult.MethodResult = MethodResultEnum.Failure;
                callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
            }
            return callResult;
        }

        public static ViCellBlu.ExportStatusData DecodeExportStatus(Object rawResult, ServiceMessageContext messageContext)
        {
            ViCellBlu.ExportStatusData outResult = new ExportStatusData();
            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                outResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception)
            {
                outResult.BulkDataId = "";
            }
            return outResult;
        }

        public static ViCellBlu.DeleteSampleStatus DecodeDeleteSampleStatus(Object rawResult, ServiceMessageContext messageContext)
        {
            ViCellBlu.DeleteSampleStatus outResult = new DeleteSampleStatus();
            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                outResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception)
            {
                outResult.DeletePercent = 0;
            }
            return outResult;
        }

        public static ViCellBlu.VcbResultRetrieveExportBlock DecodeRawRetrieveExportBlock(Object rawResult, ServiceMessageContext messageContext)
        {
            ViCellBlu.VcbResultRetrieveExportBlock callResult = new ViCellBlu.VcbResultRetrieveExportBlock
            {
	            ErrorLevel = ErrorLevelEnum.Warning, 
	            MethodResult = MethodResultEnum.Failure
            };

            callResult.ResponseDescription = "DecodeRawRetrieveExportBlock";
            
            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                callResult.ErrorLevel = ErrorLevelEnum.RequiresUserInteraction;
                callResult.MethodResult = MethodResultEnum.Failure;
                callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
            }
            return callResult;
        }

        public static ViCellBlu.VcbResultExportConfig DecodeRawExportConfig(Object rawResult, ServiceMessageContext messageContext)
        {
            ViCellBlu.VcbResultExportConfig callResult = new ViCellBlu.VcbResultExportConfig
            {
	            ErrorLevel = ErrorLevelEnum.Warning, 
	            MethodResult = MethodResultEnum.Failure
            };

            callResult.ResponseDescription = "DecodeRawExportConfig";

            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                callResult.ErrorLevel = ErrorLevelEnum.RequiresUserInteraction;
                callResult.MethodResult = MethodResultEnum.Failure;
                callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
            }
            return callResult;
        }

        public static ViCellBlu.VcbResultGetSampleResults DecodeRawSampleResults(Object rawResult, ServiceMessageContext messageContext)
        {
            ViCellBlu.VcbResultGetSampleResults callResult = new ViCellBlu.VcbResultGetSampleResults()
            {
	            ErrorLevel = ErrorLevelEnum.Warning,
	            MethodResult = MethodResultEnum.Failure
            };

            callResult.ResponseDescription = "DecodeRawSampleResults";
            
            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                callResult.ErrorLevel = ErrorLevelEnum.RequiresUserInteraction;
                callResult.MethodResult = MethodResultEnum.Failure;
                callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
            }
            return callResult;
        }

        public static ViCellBlu.SampleResult DecodeRawSampleComplete(Object rawResult, ServiceMessageContext messageContext)
        {
            ViCellBlu.SampleResult outResult = new SampleResult();
            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                outResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception)
            {
                outResult.Status = SampleStatusEnum.NotProcessed;
            }
            return outResult;

        }

        public static SampleStatusData DecodeRawSampleStatusData(Object rawResult, ServiceMessageContext messageContext)
        {
            var sampleStatusData = new SampleStatusData();
            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                sampleStatusData.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return sampleStatusData;
        }

        public static VcbResultGetDiskSpace DecodeRawDiskSpaceData(Object rawResult, ServiceMessageContext messageContext)
        {
            VcbResultGetDiskSpace callResult = new VcbResultGetDiskSpace();
            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return callResult;
        }

        public static VcbResultGetCellTypes DecodeRawCellTypesData(Object rawResult, ServiceMessageContext messageContext)
        {
            VcbResultGetCellTypes callResult = new VcbResultGetCellTypes()
            {
	            ErrorLevel = ErrorLevelEnum.Warning, 
	            MethodResult = MethodResultEnum.Failure
            };

            callResult.ResponseDescription = "DecodeRawCellTypesData";
            
            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                callResult.ErrorLevel = ErrorLevelEnum.RequiresUserInteraction;
                callResult.MethodResult = MethodResultEnum.Failure;
                callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
            }
            return callResult;
        }

        public static VcbResultGetQualityControls DecodeRawQcData(Object rawResult, ServiceMessageContext messageContext)
        {
            VcbResultGetQualityControls callResult = new VcbResultGetQualityControls()
            {
	            ErrorLevel = ErrorLevelEnum.Warning,
	            MethodResult = MethodResultEnum.Failure
            };

            callResult.ResponseDescription = "DecodeRawQcData";
            
            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                callResult.ErrorLevel = ErrorLevelEnum.RequiresUserInteraction;
                callResult.MethodResult = MethodResultEnum.Failure;
                callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
            }
            return callResult;
        }

        public static ViCellBlu.VcbResultReagentVolume DecodeGetReagentVolumeRaw(Object rawResult, ServiceMessageContext messageContext)
        {
	        var callResult = new ViCellBlu.VcbResultReagentVolume()
	        {
		        ErrorLevel = ErrorLevelEnum.NoError,
		        MethodResult = MethodResultEnum.Success
	        };

	        callResult.ResponseDescription = "Decoding raw result ...";

	        try
	        {
		        byte[] myData;
		        var val = (Opc.Ua.ExtensionObject)rawResult;
		        myData = (byte[])val.Body;
		        callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
	        }
	        catch (Exception ex)
	        {
		        callResult.ErrorLevel = ErrorLevelEnum.RequiresUserInteraction;
		        callResult.MethodResult = MethodResultEnum.Failure;
		        callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
	        }
	        return callResult;
        }

        public static ViCellBlu.CleanFluidicsStatus DecodeCleanFluidicsStatus(Object rawResult, ServiceMessageContext messageContext)
        {
	        ViCellBlu.CleanFluidicsStatus outResult = new CleanFluidicsStatus();
	        try
	        {
		        byte[] myData;
		        var val = (Opc.Ua.ExtensionObject)rawResult;
		        myData = (byte[])val.Body;
		        outResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
	        }
	        catch
	        {
		        //
	        }
	        return outResult;
        }

        public static ViCellBlu.PrimeReagentsStatus DecodePrimeReagentsStatus(Object rawResult, ServiceMessageContext messageContext)
        {
	        ViCellBlu.PrimeReagentsStatus outResult = new PrimeReagentsStatus();
	        try
	        {
		        byte[] myData;
		        var val = (Opc.Ua.ExtensionObject)rawResult;
		        myData = (byte[])val.Body;
		        outResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
	        }
	        catch
	        {
		        //
	        }
	        return outResult;
        }

        public static ViCellBlu.PurgeReagentsStatus DecodePurgeReagentsStatus(Object rawResult, ServiceMessageContext messageContext)
        {
	        ViCellBlu.PurgeReagentsStatus outResult = new PurgeReagentsStatus();
	        try
	        {
		        byte[] myData;
		        var val = (Opc.Ua.ExtensionObject)rawResult;
		        myData = (byte[])val.Body;
		        outResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
	        }
	        catch
	        {
		        //
	        }
	        return outResult;
        }

        public static ViCellBlu.DecontaminateStatus DecodeDecontaminateStatus(Object rawResult, ServiceMessageContext messageContext)
        {
	        ViCellBlu.DecontaminateStatus outResult = new DecontaminateStatus();
	        try
	        {
		        byte[] myData;
		        var val = (Opc.Ua.ExtensionObject)rawResult;
		        myData = (byte[])val.Body;
		        outResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
	        }
	        catch
	        {
		        //
	        }
	        return outResult;
        }

        public static ViCellBlu.ErrorStatusType DecodeErrorStatus(Object rawResult, ServiceMessageContext messageContext)
        {
	        ViCellBlu.ErrorStatusType outResult = new ErrorStatusType();
	        try
	        {
				byte[] myData;
				var val = (Opc.Ua.ExtensionObject)rawResult;
				myData = (byte[])val.Body;
				outResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
	        }
			catch (Exception)
	        {
		        // 
	        }
	        return outResult;
        }
    }
}
