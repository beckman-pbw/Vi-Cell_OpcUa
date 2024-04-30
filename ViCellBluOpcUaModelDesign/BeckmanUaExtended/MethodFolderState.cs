using Opc.Ua;
using System;
using System.Linq;
using GrpcService;
using ViCellBluOpcUaModelDesign.Interfaces;

// ReSharper disable IdentifierTypo

namespace ViCellBlu
{
    public partial class MethodFolderState
    {
        private readonly ILockManager _lockManager;
        private readonly ISampleResultsManager _sampleResultsManager;
        private readonly ICellTypeManager _cellTypeManager;
        private readonly IConfigurationManager _configurationManager;
        private readonly IReagentsManager _reagentsManager;
        private readonly IShutdownOrReboot _shutdownOrReboot;

        public MethodFolderState(ILockManager lockManager, ISampleResultsManager sampleResultsManager, 
            ICellTypeManager cellTypeManager, IConfigurationManager configurationManager, IReagentsManager reagentsManager,
			IShutdownOrReboot shutdownOrReboot,
            NodeState parent) : base(parent)
        {
            _lockManager = lockManager;
            _sampleResultsManager = sampleResultsManager;
            _cellTypeManager = cellTypeManager;
            _configurationManager = configurationManager;
            _reagentsManager = reagentsManager;
            _shutdownOrReboot = shutdownOrReboot;
        }

        /// <summary>
        /// Initializes the object as a collection of counters which change value on read.
        /// </summary>
        protected override void OnAfterCreate(ISystemContext context, NodeState node)
        {
            base.OnAfterCreate(context, node);

            // Shouldn't we get the initial value from ScoutX via gRPC?

            // Add the method handlers for incoming OPC UA calls
            RequestLock.OnCall = OnRequestLockCall;
            ReleaseLock.OnCall = OnReleaseLockCall;
            CreateCellType.OnCall = CreateCellMethodStateMethodCallHandler;
            DeleteCellType.OnCall = DeleteCellMethodStateMethodCallHandler;
            CreateQualityControl.OnCall = CreateQualityControlMethodStateMethodCallHandler;
            GetSampleResults.OnCall = OnRequestGetSampleResultsCall;
            StartExport.OnCall = OnStartExportCall;
            RetrieveExportDataBlock.OnCall = OnRetrieveExportBlockCall;
            ExportConfig.OnCall = OnExportConfigFilesCall;
            ImportConfig.OnCall = OnImportConfigFilesCall;
            DeleteSampleResults.OnCall = OnDeleteSampleResultsCall;
            GetCellTypes.OnCall = OnGetCellTypesCall;
            GetQualityControls.OnCall = OnGetQualityControlsCall;
            GetAvailableDiskSpace.OnCall = OnGetAvailableDiskSpaceCall;
            CleanFluidics.OnCall = OnCleanFluidicsCall;
            GetReagentVolume.OnCall = OnGetReagentVolumeCall;
            SetReagentVolume.OnCall = OnSetReagentVolumeCall;
            AddReagentVolume.OnCall = OnAddReagentVolumeCall;
            ShutdownOrReboot.OnCall = OnShutdownOrRebootCall;
            DeleteCampaignData.OnCall = OnDeleteCampaignDataCall;
            StartLogDataExport.OnCall = OnStartLogDataExportCall;
            PrimeReagents.OnCall = OnPrimeReagentsCall;
            CancelPrimeReagents.OnCall = OnCancelPrimeReagentsCall;
            PurgeReagents.OnCall = OnPurgeReagentsCall;
            CancelPurgeReagents.OnCall = OnCancelPurgeReagentsCall;
            Decontaminate.OnCall = OnDecontaminateCall;
			CancelDecontaminate.OnCall = OnCancelDecontaminateCall;
        }

        private ServiceResult OnRequestGetSampleResultsCall(
            ISystemContext context,
            MethodState method,
            NodeId objectid,
            string username,
            DateTime fromdate,
            DateTime todate,
            FilterOnEnum filtertype,
            string celltypequalitycontrolname,
            string searchnamestring,
            string searchtagstring,
            ref VcbResultGetSampleResults methodresult)
        {
            var serviceResult = _sampleResultsManager.HandleRequestGetSampleResult(context.SessionId, username,
                fromdate, todate, filtertype, celltypequalitycontrolname,
                searchnamestring, searchtagstring, ref methodresult);

            return serviceResult;
        }

        private ServiceResult OnRequestLockCall(ISystemContext context, MethodState method, NodeId objectid,
            ref VcbResultRequestLock methodresult)
        {
            var serviceResult = _lockManager.HandleRequestLockRequest(context.SessionId, ref methodresult);

            if (StatusCode.IsGood(serviceResult.StatusCode) && 
                methodresult.MethodResult == MethodResultEnum.Success)
            {
                // ToDo: Shouldn't the OPC node's value be updated? Otherwise, the value in the tree can become stale. A read or subscription will cause a fresh value to be pulled from ScoutX, but still...
                // objectId
            }

            return serviceResult;
        }

        private ServiceResult OnReleaseLockCall(ISystemContext context, MethodState method, NodeId objectid,
            ref VcbResultReleaseLock methodresult)
        {
            var serviceResult = _lockManager.HandleReleaseLockRequest(context.SessionId, ref methodresult);

            if (StatusCode.IsGood(serviceResult.StatusCode) &&
                methodresult.MethodResult == MethodResultEnum.Success)
            {
                // ToDo: Shouldn't the OPC node's value be updated? Otherwise, the value in the tree can become stale. A read or subscription will cause a fresh value to be pulled from ScoutX, but still...
            }

            return serviceResult;
        }

        private ServiceResult OnStartExportCall(ISystemContext context, MethodState method, NodeId objectid, 
            Uuid[] uuids, 
            ExportTypeEnum exportType, 
            ExportImagesEnum exportImages, 
            uint nthImageToExport,
            bool automationExport, 
            ref VcbResultStartExport methodResult)
        {
            var uuidArray = uuids.Select(id => id.ToString()).ToArray();
            var serviceResult = _sampleResultsManager.HandleRequestStartExport(context.SessionId, 
	            uuidArray, exportType, exportImages, nthImageToExport, automationExport, ref methodResult);
            return serviceResult;
        }

        private ServiceResult OnRetrieveExportBlockCall(ISystemContext context, MethodState method, NodeId objectid,
            string bulkDataId, UInt32 blockIndex, ref VcbResultRetrieveExportBlock methodResult)
        {
            var serviceResult = _sampleResultsManager.HandleRequestRetrieveExportBlock(context.SessionId, bulkDataId, blockIndex, ref methodResult);
            return serviceResult;
        }

        private ServiceResult CreateCellMethodStateMethodCallHandler(
            ISystemContext context,
            MethodState method,
            NodeId objectId,
            CellType cellTypeObject,
            ref VcbResultCreateCellType methodResult)
        {
            return _cellTypeManager.HandleCreateCellTypeRequest(context.SessionId, ref methodResult, ref cellTypeObject);
        }

        private ServiceResult DeleteCellMethodStateMethodCallHandler(
            ISystemContext context,
            MethodState method,
            NodeId objectId,
            string cellTypeName,
            ref VcbResultDeleteCellType methodResult)
        {
            return _cellTypeManager.HandleDeleteCellTypeRequest(context.SessionId, ref methodResult, ref cellTypeName);
        }

        public ServiceResult CreateQualityControlMethodStateMethodCallHandler(
            ISystemContext context,
            MethodState method,
            NodeId objectId,
            QualityControl qualityControlObject,
            ref VcbResult methodResult)
        {
            return _cellTypeManager.HandleCreateQualityControlRequest(context.SessionId, ref methodResult, ref qualityControlObject);
        }

        private ServiceResult OnExportConfigFilesCall(
            ISystemContext context, 
            MethodState method, 
            NodeId objectId,
            ref VcbResultExportConfig methodResult)
        {
            return _configurationManager.HandleRequestExportConfig(context.SessionId, ref methodResult);
        }

        private ServiceResult OnImportConfigFilesCall(
            ISystemContext context, 
            MethodState method, 
            NodeId id,
            byte[] fileData, 
            ref VcbResult methodResult)
        {
            return _configurationManager.HandleRequestImportConfig(context.SessionId, ref methodResult, fileData);
        }

        private ServiceResult OnDeleteSampleResultsCall(
            ISystemContext context, 
            MethodState method, 
            NodeId objectid, 
            Uuid[] sampleresultuuids, 
            bool retainresultsandfirstimage, 
            ref VcbResult methodresult)
        {
            return _sampleResultsManager.HandleRequestDeleteSampleResults(context.SessionId,
                sampleresultuuids, retainresultsandfirstimage, ref methodresult);
        }

        private ServiceResult OnGetCellTypesCall(
            ISystemContext context, 
            MethodState method, 
            NodeId objectid, 
            ref VcbResultGetCellTypes methodresult)
        {
            return _cellTypeManager.HandleRequestGetCellTypes(context.SessionId, ref methodresult);
        }

        private ServiceResult OnGetQualityControlsCall(
            ISystemContext context, 
            MethodState method, 
            NodeId objectid, 
            ref VcbResultGetQualityControls methodResult)
        {
            return _cellTypeManager.HandleRequestGetQualityControls(context.SessionId, ref methodResult);
        }

        private ServiceResult OnGetAvailableDiskSpaceCall(
            ISystemContext context, 
            MethodState method, 
            NodeId objectid, 
            ref VcbResultGetDiskSpace methodResult)
        {
            return _configurationManager.HandleRequestGetAvailableDiskSpace(context.SessionId, ref methodResult);
        }

        private ServiceResult OnCleanFluidicsCall(
	        ISystemContext context,
	        MethodState method,
	        NodeId objectid,
	        ref VcbResult methodResult)
        {
	        return _reagentsManager.HandleRequestCleanFluidics(context.SessionId, ref methodResult);
        }

        private ServiceResult OnGetReagentVolumeCall(
	        ISystemContext context,
	        MethodState method,
	        NodeId objectid,
	        CellHealthFluidTypeEnum type,
			ref VcbResultReagentVolume methodResult)
        {
	        return _reagentsManager.HandleRequestGetReagentVolume(context.SessionId, type, ref methodResult);
        }

        private ServiceResult OnSetReagentVolumeCall(
	        ISystemContext context,
	        MethodState method,
	        NodeId objectid,
	        CellHealthFluidTypeEnum type,
	        Int32 volume,
	        ref VcbResult methodResult)
        {
	        return _reagentsManager.HandleRequestSetReagentVolume(context.SessionId, type, volume, ref methodResult);
        }

		private ServiceResult OnAddReagentVolumeCall( 
	        ISystemContext context,
	        MethodState method,
	        NodeId objectid,
	        CellHealthFluidTypeEnum type,
	        Int32 volume,
	        ref VcbResult methodResult)
        {
	        return _reagentsManager.HandleRequestAddReagentVolume(context.SessionId, type, volume, ref methodResult);
        }

		private ServiceResult OnShutdownOrRebootCall(
			ISystemContext context,
			MethodState method,
			NodeId objectid,
			ShutdownOrRebootEnum operation,
			ref VcbResult methodResult)
		{
			return _shutdownOrReboot.HandleRequestShutdownOrReboot(context.SessionId, operation, ref methodResult);
		}

		private ServiceResult OnDeleteCampaignDataCall(
			ISystemContext context,
			MethodState method,
			NodeId objectid,
			ref VcbResult methodResult)
		{
			return _sampleResultsManager.HandleRequestDeleteCampaignData(context.SessionId, ref methodResult);
		}

		private ServiceResult OnStartLogDataExportCall(
			ISystemContext context,
			MethodState method,
			NodeId objectid,
			string filename,
			DateTime fromDate,
			DateTime toDate,
			ref VcbResultStartExport methodResult)
		{
			return _sampleResultsManager.HandleRequestStartLogDataExport(context.SessionId,
				filename,
				fromDate,
				toDate,
				ref methodResult);
		}

		private ServiceResult OnPrimeReagentsCall(
	        ISystemContext context,
	        MethodState method,
	        NodeId objectid,
	        ref VcbResult methodResult)
        {
	        return _reagentsManager.HandleRequestPrimeReagents(context.SessionId, ref methodResult);
        }

        private ServiceResult OnCancelPrimeReagentsCall(
	        ISystemContext context,
	        MethodState method,
	        NodeId objectid,
	        ref VcbResult methodResult)
        {
	        return _reagentsManager.HandleRequestCancelPrimeReagents(context.SessionId, ref methodResult);
        }

        private ServiceResult OnPurgeReagentsCall(
	        ISystemContext context,
	        MethodState method,
	        NodeId objectid,
	        ref VcbResult methodResult)
        {
	        return _reagentsManager.HandleRequestPurgeReagents(context.SessionId, ref methodResult);
        }

        private ServiceResult OnCancelPurgeReagentsCall(
	        ISystemContext context,
	        MethodState method,
	        NodeId objectid,
	        ref VcbResult methodResult)
        {
	        return _reagentsManager.HandleRequestCancelPurgeReagents(context.SessionId, ref methodResult);
        }

        private ServiceResult OnDecontaminateCall(
	        ISystemContext context,
	        MethodState method,
	        NodeId objectid,
	        ref VcbResult methodResult)
        {
	        return _reagentsManager.HandleRequestDecontaminate(context.SessionId, ref methodResult);
        }

        private ServiceResult OnCancelDecontaminateCall(
	        ISystemContext context,
	        MethodState method,
	        NodeId objectid,
	        ref VcbResult methodResult)
        {
	        return _reagentsManager.HandleRequestCancelDecontaminate(context.SessionId, ref methodResult);
        }
	}
}
