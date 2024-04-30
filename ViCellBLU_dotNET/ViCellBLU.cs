using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using ViCellBlu;

namespace ViCellBLU_dotNET
{
	public class ViCellBLU
    {

	#region Enums

        public enum WashType
        {
            Normal = 0,
            Fast
        }

        public enum VcbResult
        {
            // @deprecated - this is being replaced
            Error = 0,
            NoConnection,
            NotLocked,
            Success
        }
        
	#endregion


	#region Properties

        public bool IsConnected { get { return _opcSession != null && _opcSession.Connected; } }
        public string ViCellIdentifier { get; private set; } = string.Empty;
        public ViCellStatusEnum CurrentStatus { get; private set; } = ViCellStatusEnum.Faulted;
        public LockStateEnum CurrentLockState { get; private set; } = LockStateEnum.Locked;
        public string CurrentSampleName { get; private set; } = string.Empty;
        public ViCellBlu.SamplePosition CurrentSamplePosition { get; private set; } = new ViCellBlu.SamplePosition();
        public UInt32 ReagentUsesRemaining { get; private set; } = 0;
        public UInt32 WasteTubeRemainingCapacityRemaining { get; private set; } = 0;
        public UInt64 DiskSpaceAvailable { get; private set; } = 0;
        public string CurrentSampleId { get; private set; } = string.Empty;
        public string CurrentSampleOwner { get; private set; } = string.Empty;
        public SampleStatusEnum CurrentSampleStatus { get; private set; } = SampleStatusEnum.NotProcessed;
        public ExportStatusEnum CurrentExportStatus { get; private set; } = ExportStatusEnum.Unknown;
        public DeleteStatusEnum CurrentDeleteStatus { get; private set; } = DeleteStatusEnum.Unknown;
        public uint CurrentExportPercent { get; private set; } = 0;
        public Uuid CurrentSampleDataUuid { get; private set; } = Uuid.Empty;
        public Uuid[]? WorklistUuids { get; private set; }
        public SampleResult LastSampleResult { get; private set; } = new SampleResult();
        public ApplicationConfiguration OpcAppConfig { get; private set; } = new ApplicationConfiguration();
        private Dictionary<string, int> fields = new Dictionary<string, int>();

	#endregion


	#region Delegates

        public delegate void SampleCompleteHandler(SampleResult result);
        public delegate void SampleSetCompleteHandler(bool status);

        public delegate void SampleStatusChangedHandler(SampleStatusData status);
        public delegate void WorklistCompleteHandler(Uuid[] worklist);

        public delegate void PauseCompleteHandler();
        public delegate void StopCompleteHandler();
        public delegate void ResumeCompleteHandler();

        public delegate void UpdateSystemStatusHandler(ViCellStatusEnum status);
        public delegate void UpdateLockStateHandler(LockStateEnum state);

        public delegate void DeleteSampleStatusHandler(DeleteSampleStatus status);
        public delegate void DeleteSampleResultsCompleteHandler(bool status);

        public delegate void ExportStatusHandler(ExportStatusData status);
        public delegate void ExportProgressHandler(ExportStatusData status);
        public delegate void ExportCompleteHandler(ExportStatusEnum status, string outFile);

        public delegate void UpdateReagentUsesHandler(UInt32 remaining);
        public delegate void UpdateWasteTubesHandler(UInt32 remaining);

        public delegate void UpdateViCellIdentifier(string id);

        public delegate void ReconnectHandler();
        public delegate void DisconnectHandler(StatusCode code);

        public delegate void UpdateCleanFluidicsStatusHandler(CleanFluidicsStatus status);
        public delegate void UpdatePrimeReagentsStatusHandler(PrimeReagentsStatus status);
        public delegate void UpdatePurgeReagentsStatusHandler(PurgeReagentsStatus status);
        public delegate void UpdateDecontaminateStatusHandler(DecontaminateStatus status);
        public delegate void UpdateSoftwareVersionHandler(string version);
        public delegate void UpdateFirmwareVersionHandler(string version);
        public delegate void UpdateErrorStatusHandler(ErrorStatusType errorStatus);
	#endregion
        
	#region Callbacks
		public DisconnectHandler? OnDisconnect { get; set; }
        public ReconnectHandler? OnReconnect { get; set; }
        public SampleCompleteHandler? OnSampleComplete { get; set; }
        public SampleSetCompleteHandler? OnSampleSetComplete { get; set; }
        public PauseCompleteHandler? OnPauseComplete { get; set; }
        public StopCompleteHandler? OnStopComplete { get; set; }
        public ResumeCompleteHandler? OnResumeComplete { get; set; }
        public UpdateSystemStatusHandler? OnUpdateSystemStatus { get; set; }
        public UpdateLockStateHandler? OnUpdateLockState { get; set; }
        public DeleteSampleStatusHandler? OnDeleteSampleStatusUpdate { get; set; }
        public ExportStatusHandler? OnExportStatusUpdate { get; set; }
        public ExportCompleteHandler? OnExportComplete { get; set; } = null;
        public DeleteSampleResultsCompleteHandler? OnDeleteResultsComplete { get; set; }
        public SampleStatusChangedHandler? OnSampleStatusChanged { get; set; }
        public WorklistCompleteHandler? OnWorklistComplete { get; set; }
        public UpdateReagentUsesHandler? OnUpdateReagentRemaining { get; set; }
        public UpdateWasteTubesHandler? OnUpdateWasteTubeCapacity { get; set; }
        public UpdateViCellIdentifier? OnUpdateViCellIdentifier { get; set; }
		public UpdateCleanFluidicsStatusHandler? OnCleanFluidicsStatusUpdate { get; set; }
        public UpdatePrimeReagentsStatusHandler? OnPrimeReagentsStatusUpdate { get; set; }
        public UpdatePurgeReagentsStatusHandler? OnPurgeReagentsStatusUpdate { get; set; }
        public UpdateDecontaminateStatusHandler? OnDecontaminateStatusUpdate { get; set; }
        public UpdateSoftwareVersionHandler? OnSoftwareVersionUpdate { get; set; }
        public UpdateFirmwareVersionHandler? OnFirmwareVersionUpdate { get; set; }
        public UpdateErrorStatusHandler? OnErrorStatusUpdate { get; set; }

		#endregion

		#region Private_Member_Variables
		
	    private int _reconnectPeriod = 10;

        private Session _opcSession;
        private SessionReconnectHandler? _reconnectHandler = null;
        private NamespaceTable? _namespaceUris = null;

        private ReferenceDescriptionCollection? _methodCollection =null;
        private ReferenceDescription? _browsedMethods = null;
        private NodeId? _parentMethodNode = null;

        private ReferenceDescriptionCollection _playCtrlCollection;
        private ReferenceDescription? _browsedPlayCtrl = null;
        private NodeId? _parentPlayNode = null;

	#endregion


	#region Private_Callback_Functions

        // ******************************************************************
        private void SampleCompleteCB(SampleResult result)
        {
            CurrentSampleName = "";
            LastSampleResult = result;
        }

		// ******************************************************************
        private void UpdateViCellIdentifierCB(string id)
        {
            ViCellIdentifier = id;
        }

        // ******************************************************************
        private void UpdateLockStateCB(LockStateEnum state)
        {
            CurrentLockState = state;
        }

        // ******************************************************************
        private void UpdateSystemStatusCB(ViCellStatusEnum status)
        {
            CurrentStatus = status;
        }

        // ******************************************************************
        private void UpdateReagentCB(UInt32 remaining)
        {
            ReagentUsesRemaining = remaining;
        }

        // ******************************************************************
        private void UpdateWasteTubeCB(UInt32 remaining)
        {
            WasteTubeRemainingCapacityRemaining = remaining;
        }

        private void SampleStatusChangedCB(SampleStatusData status)
        {
            if (status.SampleStatus == SampleStatusEnum.Completed)
                if (!LastSampleResult.AnalysisBy.Equals(status.AnalysisBy))
                    LastSampleResult = new SampleResult();

            CurrentSampleId = status.SampleId;
            CurrentSampleStatus = status.SampleStatus;
            CurrentSamplePosition = status.SamplePosition;
            CurrentSampleOwner = status.AnalysisBy;
            CurrentSampleDataUuid = status.SampleDataUuid;
        }

        private void WorklistCompleteCB(Opc.Ua.Uuid[] worklist)
        {
            WorklistUuids = worklist;
        }

        // ******************************************************************
        private void UpdateDeleteStatusCB(DeleteSampleStatus status)
        {
            CurrentDeleteStatus = status.DeleteStatus;
            if (status.DeleteStatus == DeleteStatusEnum.Done)
            {
                if (OnDeleteResultsComplete != null)
                    OnDeleteResultsComplete.Invoke(true);
            }
            else if (status.DeleteStatus == DeleteStatusEnum.Failed)
            {
                if (OnDeleteResultsComplete != null)
                    OnDeleteResultsComplete.Invoke(false);
            }
        }

        private BinaryWriter? _exportWriter = null;
        private string _currBulkDataId = "";
        // ******************************************************************
        private void UpdateExportStatusCB(ExportStatusData status)
        {
            CurrentExportStatus = status.ExportStatus;
            switch (status.ExportStatus)
            {
	            case ExportStatusEnum.Ready:
	            {
		            if (_exportWriter != null)
		            {
			            _currBulkDataId = status.BulkDataId;
			            uint idx = 0;
			            VcbResultRetrieveExportBlock res = new VcbResultRetrieveExportBlock();
			            ExportStatusEnum finalStatus = ExportStatusEnum.Unknown;
			            try
			            {
				            do
				            {
					            res = RetrieveExportBlock(idx++, _currBulkDataId);
					            if (res.ErrorLevel == ErrorLevelEnum.NoError)
					            {
						            if ((res.BlockData.Status == GetBlockStateEnum.GetBlockDone) ||
						                (res.BlockData.Status == GetBlockStateEnum.GetBlockSuccess))
						            {
							            if ((res.BlockData.FileData == null) || (res.BlockData.FileData.Length == 0))
								            break;
							            _exportWriter.Write(res.BlockData.FileData);
							            if (res.BlockData.Status == GetBlockStateEnum.GetBlockDone)
							            {
								            finalStatus = ExportStatusEnum.Ready;
								            break;
							            }
						            }
						            else
						            {
							            finalStatus = ExportStatusEnum.Failed;
							            break;
						            }
					            }
					            else
					            {
						            finalStatus = ExportStatusEnum.Failed;
						            break;
					            }
					            Thread.Yield();
				            } while (res.BlockData.Status != GetBlockStateEnum.GetBlockDone);
			            }
			            catch (Exception e)
			            {
				            Debug.WriteLine(e.Message);
			            }

			            try
			            {
				            _exportWriter.Close();
				            _exportWriter = null;
                            if (OnExportComplete != null)
                            {
                                OnExportComplete(finalStatus, _exportFilename);
                            }
                        }
			            catch (Exception e)
			            {
				            Debug.WriteLine(e.Message);
			            }
		            }
		            break;
	            }

	            case ExportStatusEnum.Collecting:
	            {
		            CurrentExportPercent = status.ExportPercent;
		            break;
	            }
			}
        }

#endregion

#region Constructor

        // ******************************************************************
        public ViCellBLU()
        {
            CurrentStatus = ViCellStatusEnum.Faulted;
            CurrentLockState = LockStateEnum.Unlocked;
            CurrentSampleName = "";

			// Only add callbacks here if internal processing or local data caching is needed.
            OnSampleComplete += this.SampleCompleteCB;
            OnUpdateViCellIdentifier += this.UpdateViCellIdentifierCB;
            OnUpdateLockState += this.UpdateLockStateCB;
            OnUpdateSystemStatus += this.UpdateSystemStatusCB;
            OnUpdateReagentRemaining += this.UpdateReagentCB;
            OnUpdateWasteTubeCapacity += UpdateWasteTubeCB;
            OnSampleStatusChanged += SampleStatusChangedCB;
            OnWorklistComplete += WorklistCompleteCB;
            OnDeleteSampleStatusUpdate += UpdateDeleteStatusCB;
            OnExportStatusUpdate += UpdateExportStatusCB;

            OpcAppConfig = new ApplicationConfiguration()
            {
                ApplicationName = "ViCellBLU_dotNET",
                ApplicationUri = Utils.Format(@"urn:{0}:ViCellBLU:Server", System.Net.Dns.GetHostName()),
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration(),
                TransportConfigurations = new TransportConfigurationCollection(),
                TransportQuotas = new TransportQuotas(),
                ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 },
                DisableHiResClock = true,
                TraceConfiguration = new TraceConfiguration(),
                CertificateValidator = new CertificateValidator()
            };

            OpcAppConfig.TransportQuotas = new TransportQuotas()
            {
                OperationTimeout = 600000,
                MaxStringLength = 1048576,
                MaxByteStringLength = 1048576,
                MaxArrayLength = 65535,
                MaxMessageSize = 4194304,
                MaxBufferSize = 65535,
                ChannelLifetime = 300000,
                SecurityTokenLifetime = 3600000
            };

            OpcAppConfig.ClientConfiguration.MinSubscriptionLifetime = 10000;
        }

		#endregion

		#region Methods

        public List<CellType> GetCellTypes()
        {
	        // @todo - get celltypes for the current user from the instrument

	        List<CellType> celltypes = new List<CellType>();
	        RefreshCellTypes(out celltypes);

	        return celltypes;
        }

        // ******************************************************************
        public VcbResultGetCellTypes RefreshCellTypes(out List<CellType> results) 
        {
            VcbResultGetCellTypes callResult = new VcbResultGetCellTypes()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };

            results = new List<CellType>();
            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.GetCellTypes));
                NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentMethodNode;
                cmRequest.MethodId = methodNode;

                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRawCellTypesData(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }

                if (callResult.MethodResult == MethodResultEnum.Success)
                {
                    results = callResult.CellTypes;
                }
                else
                {
                    results = new List<CellType>();
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        // ******************************************************************
        public List<QualityControl> GetQualityControls() 
        {
			// @todo - get quality for the current user from the instrument

			List<QualityControl> qcs = new List<QualityControl>();
			RefreshQCs(out qcs);

			return qcs; 
        }

        private VcbResultGetQualityControls RefreshQCs(out List<QualityControl> results)
        {
            VcbResultGetQualityControls callResult = new VcbResultGetQualityControls()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };

            results = new List<QualityControl>();
            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.GetQualityControls));
                NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentMethodNode;
                cmRequest.MethodId = methodNode;

                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRawQcData(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }

                if (callResult.MethodResult == MethodResultEnum.Success)
                {
                    results = callResult.QualityControls;
                }
                else
                {
                    results = new List<QualityControl>();
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        /// <summary>
        /// Lock the instrument before sending other commands.
        /// </summary>
        /// <returns>
        /// Success - the request was sent
        /// NoConnection - if not connected
        /// Error - an error occured
        /// </returns>
        public ViCellBlu.VcbResult RequestLock()
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };
            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.RequestLock));
                NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest
                {
                    ObjectId = _parentMethodNode,
                    MethodId = methodNode
                };

                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection
                {
                    cmRequest
                };
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out CallMethodResultCollection resultCollection, out DiagnosticInfoCollection diagResults);

                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        // ******************************************************************
        public ViCellBlu.VcbResult ReleaseLock()
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };
            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.ReleaseLock));
                NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentMethodNode;
                cmRequest.MethodId = methodNode;
                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        // ******************************************************************
        public ViCellBlu.VcbResult CreateCellType(CellType cellTyp)
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };
            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                if (cellTyp == null)
                {
                    callResult.ResponseDescription = "NULL data";
                    return callResult;
                }

                ViCellBlu.CellType bcell = GetCellTypeDataType(cellTyp);

                ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.CreateCellType));
                NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);
                
                var reqHeader = new RequestHeader();                
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentMethodNode;
                cmRequest.MethodId = methodNode;
                cmRequest.InputArguments.Add(new Variant(bcell));
                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        // ******************************************************************
        public ViCellBlu.VcbResult DeleteCellType(String name)
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };
            try
            {
                if (!IsConnected)
                {
                    return new ViCellBlu.VcbResult
                    {
                        ResponseDescription = "Not Connected"
                    };
                }

                ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.DeleteCellType));
                NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();                
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentMethodNode;
                cmRequest.MethodId = methodNode;
                cmRequest.InputArguments.Add(new Variant(name));
                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                var respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        // ******************************************************************
        public ViCellBlu.VcbResult CreateQualityControl(ViCellBlu.QualityControl qc)
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };

            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                if (qc == null)
                {
                    callResult.ResponseDescription = "NULL data";
                    return callResult;
                }

                ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.CreateQualityControl));
                NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentMethodNode;
                cmRequest.MethodId = methodNode;
                cmRequest.InputArguments.Add(new Variant(qc));
                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                var respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        // ******************************************************************
        public ViCellBlu.VcbResultGetSampleResults GetSampleResults(
            String username, 
            DateTime startDate, 
            DateTime endDate, 
            FilterOnEnum filterType, 
            String cellOrQcName, 
            String searchNameString, 
            String searchTagString,
            out List<SampleResult> results)
        {
            ViCellBlu.VcbResultGetSampleResults callResult = new ViCellBlu.VcbResultGetSampleResults()
            {
	            ErrorLevel = ErrorLevelEnum.Error,
	            MethodResult = MethodResultEnum.Failure
            };

            results = new List<SampleResult>();
            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.GetSampleResults));
                NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentMethodNode;
                cmRequest.MethodId = methodNode;
                cmRequest.InputArguments = new VariantCollection();
                cmRequest.InputArguments.Add(new Variant(username)); // User name string

                cmRequest.InputArguments.Add(new Variant(startDate));
                cmRequest.InputArguments.Add(new Variant(endDate));
                cmRequest.InputArguments.Add(new Variant(filterType)); // filter ON: 0 = sample set, 1 = sample

                cmRequest.InputArguments.Add(new Variant(cellOrQcName)); // Cell type or QC name
                cmRequest.InputArguments.Add(new Variant(searchNameString)); // Search string (sample or sample set name)
                cmRequest.InputArguments.Add(new Variant(searchTagString)); 

                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRawSampleResults(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }

                if (callResult.MethodResult == MethodResultEnum.Success)
                {
                    results = callResult.SampleResults;
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        private string _exportFilename = "";
        // ******************************************************************
        public ViCellBlu.VcbResultStartExport RetrieveSampleExport(List<Guid> sampleIds, string fileName)
        {
            var callResult = new ViCellBlu.VcbResultStartExport
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure,
	            BulkDataId = ""
            };

            try
            {
                if (_exportWriter != null)
                {
                    callResult.ResponseDescription = "Export Busy";
                    return callResult;
                }

                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                var uuids = new List<Uuid>();
                foreach (var id in sampleIds)
                {
                    uuids.Add(new Uuid(id));
                }
                
                var method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.StartExport));
                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                var cmRequest = new CallMethodRequest { ObjectId = _parentMethodNode, MethodId = methodNode };
                cmRequest.InputArguments = new VariantCollection();
                cmRequest.InputArguments.Add(new Variant(uuids));
                cmRequest.InputArguments.Add(new Variant(ExportTypeEnum.Csv));
                cmRequest.InputArguments.Add(new Variant(ExportImagesEnum.All));
                cmRequest.InputArguments.Add(new Variant((uint)1));	// Not used since All is specified.
                cmRequest.InputArguments.Add(new Variant(false));
                var cmReqCollection = new CallMethodRequestCollection { cmRequest };
                var respHdr = _opcSession.Call(reqHeader, cmReqCollection, out var resultCollection, out var diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRawStartExport(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }

                if (callResult.ErrorLevel == ErrorLevelEnum.NoError)
                {
	                _currBulkDataId = callResult.BulkDataId;
                    _exportFilename = fileName;
                    _exportWriter = new BinaryWriter(File.Open(fileName, FileMode.Create));
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

		private string _encryptedExportFilename = "";
		// ******************************************************************
		public ViCellBlu.VcbResultStartExport RetrieveOfflineSampleExport(List<Guid> sampleIds, string fileName)
		{
			var callResult = new ViCellBlu.VcbResultStartExport
			{
				ErrorLevel = ErrorLevelEnum.Error,
				MethodResult = MethodResultEnum.Failure,
				BulkDataId = ""
			};

			try
			{
				if (_exportWriter != null)
				{
					callResult.ResponseDescription = "Export Busy";
					return callResult;
				}

				if (!IsConnected)
				{
					callResult.ResponseDescription = "Not Connected";
					return callResult;
				}

				var uuids = new List<Uuid>();
				foreach (var id in sampleIds)
				{
					uuids.Add(new Uuid(id));
				}

				var method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.StartExport));
				var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

				var reqHeader = new RequestHeader();
				var cmRequest = new CallMethodRequest { ObjectId = _parentMethodNode, MethodId = methodNode };
				cmRequest.InputArguments = new VariantCollection();
				cmRequest.InputArguments.Add(new Variant(uuids));
				cmRequest.InputArguments.Add(new Variant(ExportTypeEnum.Offline));
				cmRequest.InputArguments.Add(new Variant(ExportImagesEnum.All));
				cmRequest.InputArguments.Add(new Variant((uint)1));   // Not used since All is specified.
				cmRequest.InputArguments.Add(new Variant(true));
				var cmReqCollection = new CallMethodRequestCollection { cmRequest };
				var respHdr = _opcSession.Call(reqHeader, cmReqCollection, out var resultCollection, out var diagResults);
				if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
				{
					callResult = DecodeHelper.DecodeRawStartExport(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
				}

				if (callResult.ErrorLevel == ErrorLevelEnum.NoError)
				{
					_currBulkDataId = callResult.BulkDataId;
					_encryptedExportFilename = fileName;
					_exportWriter = new BinaryWriter(File.Open(fileName, FileMode.Create));
				}
			}
			catch (Exception e)
			{
				callResult.ResponseDescription = "Exception: " + e.Message;
				Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
			}

			return callResult;
		}

		// ******************************************************************
		public ViCellBlu.VcbResultStartExport StartLogDataExport(string filename, DateTime startDate, DateTime endDate)
		{
			var callResult = new ViCellBlu.VcbResultStartExport
			{
				ErrorLevel = ErrorLevelEnum.Error, 
				MethodResult = MethodResultEnum.Failure,
				BulkDataId = ""
			};

			try
			{
				if (_exportWriter != null)
				{
					callResult.ResponseDescription = "Export Busy";
					return callResult;
				}

				if (!IsConnected)
				{
					callResult.ResponseDescription = "Not Connected";
					return callResult;
				}

				var method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.StartLogDataExport));
				var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

				var reqHeader = new RequestHeader();
				var cmRequest = new CallMethodRequest { ObjectId = _parentMethodNode, MethodId = methodNode };
				cmRequest.InputArguments = new VariantCollection();
				cmRequest.InputArguments.Add(new Variant(filename));
				cmRequest.InputArguments.Add(new Variant(startDate));
				cmRequest.InputArguments.Add(new Variant(endDate));

				var cmReqCollection = new CallMethodRequestCollection { cmRequest };
				var respHdr = _opcSession.Call(reqHeader, cmReqCollection, out var resultCollection, out var diagResults);
				if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
				{
					callResult = DecodeHelper.DecodeRawStartExport(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
				}

				if (callResult.ErrorLevel == ErrorLevelEnum.NoError)
				{
					_currBulkDataId = callResult.BulkDataId;
					_exportFilename = filename;
					_exportWriter = new BinaryWriter(File.Open(filename, FileMode.Create));
				}
			}
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        // ******************************************************************
		private ViCellBlu.VcbResultRetrieveExportBlock RetrieveExportBlock(uint blockIndex, string bulkDataId)
        {
            var callResult = new ViCellBlu.VcbResultRetrieveExportBlock
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };

            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                var method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.RetrieveExportDataBlock));
                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                var cmRequest = new CallMethodRequest { ObjectId = _parentMethodNode, MethodId = methodNode };
                cmRequest.InputArguments = new VariantCollection();
                cmRequest.InputArguments.Add(new Variant(bulkDataId));
                cmRequest.InputArguments.Add(new Variant(blockIndex));
                var cmReqCollection = new CallMethodRequestCollection { cmRequest };
                var respHdr = _opcSession.Call(reqHeader, cmReqCollection, out var resultCollection, out var diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRawRetrieveExportBlock(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        // ******************************************************************
        public ViCellBlu.VcbResult DeleteSampleResults(List<Guid> sampleIds)
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };
            
            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.DeleteSampleResults));
                NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                // @todo - add check of return value of .Call and then adjust this function's return value

                List<Opc.Ua.Uuid> uuids = new List<Uuid>();
                foreach(var id in sampleIds)
                {
                    uuids.Add(new Uuid(id));
                }
                
                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentMethodNode;
                cmRequest.MethodId = methodNode;
                cmRequest.InputArguments = new VariantCollection();
                cmRequest.InputArguments.Add(new Variant(uuids));
                cmRequest.InputArguments.Add(new Variant(false));

                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        // ******************************************************************
        public ViCellBlu.VcbResult ImportConfig(String fileName)
        {
            var callResult = new ViCellBlu.VcbResult
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };
            
            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                if (!File.Exists(fileName))
                {
                    callResult.ResponseDescription = "Import configuration file does not exist.";
                    return callResult;
                }

                var fileContents = File.ReadAllBytes(fileName);

                var method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.ImportConfig));
                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                var cmRequest = new CallMethodRequest {ObjectId = _parentMethodNode, MethodId = methodNode};
                cmRequest.InputArguments.Add(new Variant(fileContents));
                var cmReqCollection = new CallMethodRequestCollection {cmRequest};
                var respHdr = _opcSession.Call(reqHeader, cmReqCollection, out var resultCollection, out var diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        // ******************************************************************
        public ViCellBlu.VcbResultExportConfig ExportConfig(String fullFilePathToSaveConfigIncludingExtension)
        {
            var callResult = new ViCellBlu.VcbResultExportConfig
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure, 
	            FileData = null
            };

            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                var method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.ExportConfig));
                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                var cmRequest = new CallMethodRequest { ObjectId = _parentMethodNode, MethodId = methodNode };
                var cmReqCollection = new CallMethodRequestCollection { cmRequest };
                var respHdr = _opcSession.Call(reqHeader, cmReqCollection, out var resultCollection, out var diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRawExportConfig(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }

                if (callResult.ErrorLevel == ErrorLevelEnum.NoError && callResult.FileData != null)
                {
                    File.WriteAllBytes(fullFilePathToSaveConfigIncludingExtension, callResult.FileData);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

	#endregion

	#region PlayControls
        // ******************************************************************
        public ViCellBlu.VcbResult StartSample(SampleConfig sampleCfg)
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult() 
            { 
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };

            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                if (sampleCfg == null)
                {
                    callResult.ResponseDescription = "NULL data";
                    return callResult;
                }

                ViCellBlu.SampleConfig sample = GetSampleDataType(sampleCfg);

                ReferenceDescription playMethod = _playCtrlCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.StartSample));
                NodeId playNode = ExpandedNodeId.ToNodeId(playMethod.NodeId, _opcSession.NamespaceUris);

                // @todo - add check of return value of .Call and then adjust this function's return value
                //var ret = _opcSession.Call(_parentPlayNode, playNode, sample);
                //return VcbResult.Success;

                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentPlayNode;
                cmRequest.MethodId = playNode;
                cmRequest.InputArguments = new VariantCollection();
                cmRequest.InputArguments.Add(new Variant(sample));


                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        // ******************************************************************
        public ViCellBlu.VcbResult StartSampleSet(SampleSetConfig sampleSet, ViCellBlu.PlatePrecessionEnum sortOrder)
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };
            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                if (sampleSet == null)
                {
                    callResult.ResponseDescription = "NULL data";
                    return callResult;
                }

                ViCellBlu.SampleSet bsset = GetSampleSetDataType(sampleSet, sortOrder);
                
                ReferenceDescription playMethod = _playCtrlCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.StartSampleSet));
                NodeId playNode = ExpandedNodeId.ToNodeId(playMethod.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentPlayNode;
                cmRequest.MethodId = playNode;
                cmRequest.InputArguments = new VariantCollection();
                cmRequest.InputArguments.Add(new Variant(bsset));

                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        // ******************************************************************
        public ViCellBlu.VcbResult Pause()
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };
            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                ReferenceDescription playMethod = _playCtrlCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.Pause));
                NodeId playNode = ExpandedNodeId.ToNodeId(playMethod.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentPlayNode;
                cmRequest.MethodId = playNode;
                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
            
            return callResult;
        }

        // ******************************************************************
        public ViCellBlu.VcbResult Resume()
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };
            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                ReferenceDescription playMethod = _playCtrlCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.Resume));
                NodeId playNode = ExpandedNodeId.ToNodeId(playMethod.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentPlayNode;
                cmRequest.MethodId = playNode;
                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
            
            return callResult;
        }

        // ******************************************************************
        public ViCellBlu.VcbResult Stop()
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };

            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                ReferenceDescription playMethod = _playCtrlCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.Stop));
                NodeId playNode = ExpandedNodeId.ToNodeId(playMethod.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentPlayNode;
                cmRequest.MethodId = playNode;
                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
            
            return callResult;
        }

		// ******************************************************************
		// Not supported in Shepherd Viability Science Module.
		// ******************************************************************
        public ViCellBlu.VcbResult EjectStage()
        { // Just return an error.
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };

			callResult.ResponseDescription = "Not Supported";

			return callResult;
        }

		// ******************************************************************
		public ViCellBlu.VcbResult CleanFluidics()
		{
			ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
			{
				ErrorLevel = ErrorLevelEnum.Error,
				MethodResult = MethodResultEnum.Failure
			};

			try
			{
				if (!IsConnected)
				{
					callResult.ResponseDescription = "Not Connected";
					return callResult;
				}

				ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.CleanFluidics));
				NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

				var reqHeader = new RequestHeader();
				CallMethodRequest cmRequest = new CallMethodRequest();
				cmRequest.ObjectId = _parentMethodNode;
				cmRequest.MethodId = methodNode;

				CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
				cmReqCollection.Add(cmRequest);
				CallMethodResultCollection resultCollection;
				DiagnosticInfoCollection diagResults;
				ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

				if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
				{
					callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
				}
			}
			catch (Exception e)
			{
				callResult.ResponseDescription = "Exception: " + e.Message;
				Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
			}

			return callResult;
		}

	#endregion
	#region Connection

        // ******************************************************************
        public ViCellBlu.VcbResult Disconnect()
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };

            try
            {
                LastSampleResult = new SampleResult{AnalysisBy = ""};
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                if (_opcSession != null)
                {
                    if (CurrentLockState == LockStateEnum.Locked)
                        ReleaseLock();
                    _opcSession.Close();
                    _reconnectHandler?.Dispose();
                    _reconnectHandler = null;
                    callResult.ErrorLevel = ErrorLevelEnum.NoError;
                    callResult.MethodResult = MethodResultEnum.Success;
                    callResult.ResponseDescription = "";

	                return callResult;
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }

        private string _username = "";
        // ******************************************************************
        public ViCellBlu.VcbResult Connect(
	        String username, 
	        String password, 
	        IPAddress ipAddr, 
	        UInt32 port = 62641, 
	        UInt32 discoverTimeout = 15000, 
	        UInt32 cnxTimeout = 60000)
        {
            ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };

            LastSampleResult = new SampleResult { AnalysisBy = "" };

			_username = username;

            string endpointUrl = "opc.tcp://" + ipAddr.ToString() + ":" + port.ToString() + "/ViCellBlu/Server";
            try
            {
	            try
                {
                    OpcAppConfig.Validate(ApplicationType.Client).Wait();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                var application = new ApplicationInstance()
                {
                    ApplicationType = ApplicationType.Client,
                    ApplicationName = "ViCellBLU dotNET",
                    ApplicationConfiguration = OpcAppConfig
                };

                var haveAppCertificateTask = application.CheckApplicationInstanceCertificate(false, 0);
                var haveAppCertificate = haveAppCertificateTask.Result;
                if (haveAppCertificate)
                    OpcAppConfig.ApplicationUri = X509Utils.GetApplicationUriFromCertificate(OpcAppConfig.SecurityConfiguration.ApplicationCertificate.Certificate);

                var selectedEndpoint = CoreClientUtils.SelectEndpoint(OpcAppConfig, endpointUrl, haveAppCertificate, (int)discoverTimeout);
                var endpointConfiguration = EndpointConfiguration.Create(OpcAppConfig);
                var endpoint = new ConfiguredEndpoint(null, selectedEndpoint, endpointConfiguration);

                var user = new UserIdentity(username, password);

                var sessionTask = Session.Create(OpcAppConfig, endpoint, true, false, "ViCellBLU_dotNET", cnxTimeout, user, null);
                _opcSession = sessionTask.Result;

                _namespaceUris = _opcSession.NamespaceUris;
                _opcSession.KeepAlive += Client_KeepAlive;

                var scoutXrefs = Browse(out _);
                var viCellBlu = scoutXrefs.First(n => n.BrowseName.Name.Equals(ViCellBlu.BrowseNames.ViCellBluState));
                var viCellBluRefs = Browse(out _, viCellBlu.NodeId);

                _browsedPlayCtrl = viCellBluRefs.First(n => n.BrowseName.Name.Equals(ViCellBlu.BrowseNames.PlayControl));
                _playCtrlCollection = Browse(out _, _browsedPlayCtrl.NodeId);
                _parentPlayNode = ExpandedNodeId.ToNodeId(_browsedPlayCtrl.NodeId, _opcSession.NamespaceUris);

                _browsedMethods = viCellBluRefs.First(n => n.BrowseName.Name.Equals(ViCellBlu.BrowseNames.Methods));
                _methodCollection = Browse(out _, _browsedMethods.NodeId);
                _parentMethodNode = ExpandedNodeId.ToNodeId(_browsedMethods.NodeId, _opcSession.NamespaceUris);

                SetupVariables(MyMonitoredItemVariableHandler);
                SetupEvents(MyMonitoredItemEventHandler);

                _exportWriter = null;

                return new ViCellBlu.VcbResult
                {
                    ErrorLevel = ErrorLevelEnum.NoError,
                    MethodResult = MethodResultEnum.Success,
                    ResponseDescription = ""
                };
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Connect-Exception: " + e.ToString();
                Debug.WriteLine("--- EXCEPTION: Connect ---", e.Message);
            }

            return callResult;
        }

        // ******************************************************************
        private VcbResult SetupVariables(MonitoredItemNotificationEventHandler handler)
        {
            try
            {
                var subscription = new Subscription(_opcSession.DefaultSubscription) { };
                subscription.PublishingInterval = 1000;
                subscription.DisplayName = "Vi-Cell Test Client Subscription";
                subscription.PublishingEnabled = true;
                var nsIndex = (ushort)(_opcSession.NamespaceUris.GetIndex(ViCellBlu.Namespaces.ViCellBlu));
                var list = new List<MonitoredItem> { };

                var subDictionary = new Dictionary<uint, string>
                {
                    { ViCellBlu.Variables.ViCellBluState_ViCellIdentifier, ViCellBlu.BrowseNames.ViCellIdentifier  },
                    { ViCellBlu.Variables.ViCellBluState_ViCellStatus, ViCellBlu.BrowseNames.ViCellStatus },
                    { ViCellBlu.Variables.ViCellBluState_LockState, ViCellBlu.BrowseNames.LockState },
                    { ViCellBlu.Variables.ViCellBluState_ReagentUsesRemaining, ViCellBlu.BrowseNames.ReagentUsesRemaining },
                    { ViCellBlu.Variables.ViCellBluState_WasteTubeRemainingCapacity, ViCellBlu.BrowseNames.WasteTubeRemainingCapacity },
                    { ViCellBlu.Variables.ViCellBluState_SoftwareVersion, ViCellBlu.BrowseNames.SoftwareVersion },
                    { ViCellBlu.Variables.ViCellBluState_FirmwareVersion, ViCellBlu.BrowseNames.FirmwareVersion },
                };

                foreach (KeyValuePair<uint, string> entry in subDictionary)
                {
                    list.Add(new MonitoredItem(subscription.DefaultItem)
                    {
                        StartNodeId = new NodeId(entry.Key, nsIndex),
                        DisplayName = entry.Value,
                    });
                }

                list.ForEach(i => i.Notification += handler);
                subscription.AddItems(list);
                _opcSession.AddSubscription(subscription);
                subscription.Create();

                return VcbResult.Success;
            }
            catch (Exception e)
            {
                Debug.WriteLine("--- EXCEPTION: SetupSubscriptions ---", e.Message);
            }
            return VcbResult.Error;

        }

        // ******************************************************************
        private VcbResult SetupEvents(MonitoredItemNotificationEventHandler handler)
        {
            try
            {
				var index = 0;
				fields.Clear();

				List<SimpleAttributeOperand> monitoredFields = new List<SimpleAttributeOperand>();

				fields.Add("EventId", index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName("EventId") },
					AttributeId = Attributes.Value
				});
				fields.Add("EventType", index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName("EventType") },
					AttributeId = Attributes.Value
				});
				fields.Add("SourceName", index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName("SourceName") },
					AttributeId = Attributes.Value
				});
				fields.Add("Time", index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName("Time") },
					AttributeId = Attributes.Value
				});
				fields.Add("Message", index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName("Message") },
					AttributeId = Attributes.Value
				});
				fields.Add("Severity", index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName("Severity") },
					AttributeId = Attributes.Value
				});
				fields.Add("Session/CreateSession", index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName("Session/CreateSession") },
					AttributeId = Attributes.Value
				});
				fields.Add("Session/ActivateSession", index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName("Session/ActivateSession") },
					AttributeId = Attributes.Value
				});
				fields.Add(ViCellBlu.BrowseNames.SampleStatusChangedEvent, index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName(ViCellBlu.BrowseNames.SampleStatusData) },
					AttributeId = Attributes.Value
				});
				fields.Add(ViCellBlu.BrowseNames.SampleCompleteEvent, index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName(ViCellBlu.BrowseNames.SampleResult) },
					AttributeId = Attributes.Value
				});
				fields.Add(ViCellBlu.BrowseNames.WorkListCompleteEvent, index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName(ViCellBlu.BrowseNames.SampleDataUuidList) },
					AttributeId = Attributes.Value
				});
				fields.Add(ViCellBlu.BrowseNames.ExportStatusEvent, index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName(ViCellBlu.BrowseNames.ExportStatusData) },
					AttributeId = Attributes.Value
				});
				fields.Add(ViCellBlu.BrowseNames.DeleteSampleResultsProgressEvent, index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName(ViCellBlu.BrowseNames.DeleteStatusInfo) },
					AttributeId = Attributes.Value
				});
				fields.Add(ViCellBlu.BrowseNames.CleanFluidicsStatusEvent, index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName(ViCellBlu.BrowseNames.CleanFluidicsStatus) },
					AttributeId = Attributes.Value
				});
				fields.Add(ViCellBlu.BrowseNames.PrimeReagentsStatusEvent, index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName(ViCellBlu.BrowseNames.PrimeReagentsStatus) },
					AttributeId = Attributes.Value
				});
				fields.Add(ViCellBlu.BrowseNames.PurgeReagentsStatusEvent, index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName(ViCellBlu.BrowseNames.PurgeReagentsStatus) },
					AttributeId = Attributes.Value
				});
				fields.Add(ViCellBlu.BrowseNames.DecontaminateStatusEvent, index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName(ViCellBlu.BrowseNames.DecontaminateStatus) },
					AttributeId = Attributes.Value
				});
				fields.Add(ViCellBlu.BrowseNames.ErrorStatusEvent, index);
				monitoredFields.Insert(index++, new SimpleAttributeOperand
				{
					TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
					BrowsePath = new[] { new QualifiedName(ViCellBlu.BrowseNames.Status) },
					AttributeId = Attributes.Value
				});

                var subscription = new Subscription(_opcSession.DefaultSubscription);
                subscription.PublishingInterval = 1000;
                subscription.DisplayName = "Vi-Cell Client Event Subscription";
                subscription.PublishingEnabled = true;

                var monItem = new MonitoredItem(subscription.DefaultItem)
                {
                    NodeClass = NodeClass.Object,
                    StartNodeId = Opc.Ua.ObjectIds.Server,
                    AttributeId = Attributes.EventNotifier,
                    SamplingInterval = -1,
                    QueueSize = 0,
                    CacheQueueSize = 0,
                    Filter = new EventFilter() { SelectClauses = monitoredFields.ToArray() }
                };

                monItem.Notification += handler;
                subscription.AddItem(monItem);
                _opcSession.AddSubscription(subscription);
                subscription.Create();

                return VcbResult.Success;
            }
            catch (Exception e)
            {
                Debug.WriteLine("--- EXCEPTION: SetupSubscriptions ---", e.Message);
            }

            return VcbResult.Error;
        }

        // ******************************************************************
        public VcbResultGetDiskSpace GetDiskSpacePercentages()
        {
            VcbResultGetDiskSpace callResult = new VcbResultGetDiskSpace()
            {
	            ErrorLevel = ErrorLevelEnum.Error, 
	            MethodResult = MethodResultEnum.Failure
            };
            try
            {
                if (!IsConnected)
                {
                    callResult.ResponseDescription = "Not Connected";
                    return callResult;
                }

                ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.GetAvailableDiskSpace));
                NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                var reqHeader = new RequestHeader();
                CallMethodRequest cmRequest = new CallMethodRequest();
                cmRequest.ObjectId = _parentMethodNode;
                cmRequest.MethodId = methodNode;
                CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
                cmReqCollection.Add(cmRequest);
                CallMethodResultCollection resultCollection;
                DiagnosticInfoCollection diagResults;
                ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);
                if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                {
                    callResult = DecodeHelper.DecodeRawDiskSpaceData(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
                Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }

            return callResult;
        }
        
		// ******************************************************************
		public ViCellBlu.VcbResultReagentVolume GetReagentVolume (CellHealthFluidTypeEnum type)
		{
			ViCellBlu.VcbResultReagentVolume callResult = new ViCellBlu.VcbResultReagentVolume()
			{
				ErrorLevel = ErrorLevelEnum.Error, 
				MethodResult = MethodResultEnum.Failure,
				Volume =  0
			};

			try
			{
				if (!IsConnected)
				{
					callResult.ResponseDescription = "Not Connected";
					return callResult;
				}

				if (type == CellHealthFluidTypeEnum.Unknown)
				{
					callResult.ResponseDescription = "Invalid Arg";
					return callResult;
				}

				ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.GetReagentVolume));
				NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

				var reqHeader = new RequestHeader();
				CallMethodRequest cmRequest = new CallMethodRequest();
				cmRequest.ObjectId = _parentMethodNode;
				cmRequest.MethodId = methodNode;
				cmRequest.InputArguments.Add(new Variant(type));
				CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
				cmReqCollection.Add(cmRequest);
				CallMethodResultCollection resultCollection;
				DiagnosticInfoCollection diagResults;

				ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);
				if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
				{
					callResult = DecodeHelper.DecodeGetReagentVolumeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
				}
			}
			catch (Exception e)
			{
				callResult.ResponseDescription = "Exception: " + e.Message;
				Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
			}

			return callResult;
		}

		// ******************************************************************
		public ViCellBlu.VcbResult SetReagentVolume(CellHealthFluidTypeEnum type, Int32 volume)
		{
			ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
			{
				ErrorLevel = ErrorLevelEnum.Error,
				MethodResult = MethodResultEnum.Failure,
			};

			if (volume < 0)
			{
				callResult.ResponseDescription = "Volume must be >= 0";
			}
			else
			{
				try
				{
					if (!IsConnected)
					{
						callResult.ResponseDescription = "Not Connected";
						return callResult;
					}

					if (type == CellHealthFluidTypeEnum.Unknown)
					{
						callResult.ResponseDescription = "Invalid Arg";
						return callResult;
					}

					ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.SetReagentVolume));
					NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

					var reqHeader = new RequestHeader();
					CallMethodRequest cmRequest = new CallMethodRequest();
					cmRequest.ObjectId = _parentMethodNode;
					cmRequest.MethodId = methodNode;
					cmRequest.InputArguments.Add(new Variant(type));
					cmRequest.InputArguments.Add(new Variant(volume));
					CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
					cmReqCollection.Add(cmRequest);
					CallMethodResultCollection resultCollection;
					DiagnosticInfoCollection diagResults;

					ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);
					if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
					{
						callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
					}
				}
				catch (Exception e)
				{
					callResult.ResponseDescription = "Exeption: " + e.Message;
					Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
				}
			}

			return callResult;
		}

		// ******************************************************************
		public ViCellBlu.VcbResult AddReagentVolume(CellHealthFluidTypeEnum type, Int32 volume)
		{
			ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
			{
				ErrorLevel = ErrorLevelEnum.Error,
				MethodResult = MethodResultEnum.Failure,
			};

			if (volume < 0)
			{
				callResult.ResponseDescription = "Volume must be >= 0";
			}
			else
			{
				try
				{
					if (!IsConnected)
					{
						callResult.ResponseDescription = "Not Connected";
						return callResult;
					}

					if (type == CellHealthFluidTypeEnum.Unknown)
					{
						callResult.ResponseDescription = "Invalid Arg";
						return callResult;
					}

					ReferenceDescription method = _methodCollection.First(n =>
						n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.AddReagentVolume));
					NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

					var reqHeader = new RequestHeader();
					CallMethodRequest cmRequest = new CallMethodRequest();
					cmRequest.ObjectId = _parentMethodNode;
					cmRequest.MethodId = methodNode;
					cmRequest.InputArguments.Add(new Variant(type));
					cmRequest.InputArguments.Add(new Variant(volume));
					CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
					cmReqCollection.Add(cmRequest);
					CallMethodResultCollection resultCollection;
					DiagnosticInfoCollection diagResults;

					ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection,
						out diagResults);
					if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
					{
						callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value,
							_opcSession.MessageContext);
					}
				}
				catch (Exception e)
				{
					callResult.ResponseDescription = "Exeption: " + e.Message;
					Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
				}
			}

			return callResult;
		}

		// ******************************************************************
		public ViCellBlu.VcbResult ShutdownOrReboot (ShutdownOrRebootEnum operation)
		{
			ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult() {
				ErrorLevel = ErrorLevelEnum.Error, 
				MethodResult = MethodResultEnum.Failure
			};

			try
			{
				if (!IsConnected)
				{
					callResult.ResponseDescription = "Not Connected";
					return callResult;
				}

				ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.ShutdownOrReboot));
				NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

				var reqHeader = new RequestHeader();
				CallMethodRequest cmRequest = new CallMethodRequest();
				cmRequest.ObjectId = _parentMethodNode;
				cmRequest.MethodId = methodNode;
				cmRequest.InputArguments.Add(new Variant(operation));
				CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
				cmReqCollection.Add(cmRequest);
				CallMethodResultCollection resultCollection;
				DiagnosticInfoCollection diagResults;
				ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

				if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
				{
					callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
				}

			}
			catch (Exception e)
			{
				callResult.ResponseDescription = "Exception: " + e.Message;
				Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
			}

			return callResult;
		}

		// ******************************************************************
		public ViCellBlu.VcbResult DeleteCampaignData()
		{
			ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
			{
				ErrorLevel = ErrorLevelEnum.Error,
				MethodResult = MethodResultEnum.Failure
			};

			try
			{
				if (!IsConnected)
				{
					callResult.ResponseDescription = "Not Connected";
					return callResult;
				}

				ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.DeleteCampaignData));
				NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

				var reqHeader = new RequestHeader();
				CallMethodRequest cmRequest = new CallMethodRequest();
				cmRequest.ObjectId = _parentMethodNode;
				cmRequest.MethodId = methodNode;
				CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
				cmReqCollection.Add(cmRequest);
				CallMethodResultCollection resultCollection;
				DiagnosticInfoCollection diagResults;
				ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

				if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
				{
					callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
				}

			}
			catch (Exception e)
			{
				callResult.ResponseDescription = "Exception: " + e.Message;
				Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
			}

			return callResult;
		}
		
		// ******************************************************************
		public ViCellBlu.VcbResult PrimeReagents()
		{
			ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
			{
				ErrorLevel = ErrorLevelEnum.Error,
				MethodResult = MethodResultEnum.Failure
			};

			try
			{
				if (!IsConnected)
				{
					callResult.ResponseDescription = "Not Connected";
					return callResult;
				}

				ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.PrimeReagents));
				NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

				var reqHeader = new RequestHeader();
				CallMethodRequest cmRequest = new CallMethodRequest();
				cmRequest.ObjectId = _parentMethodNode;
				cmRequest.MethodId = methodNode;

				CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
				cmReqCollection.Add(cmRequest);
				CallMethodResultCollection resultCollection;
				DiagnosticInfoCollection diagResults;
				ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

				if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
				{
					callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
				}
			}
			catch (Exception e)
			{
				callResult.ResponseDescription = "Exception: " + e.Message;
				Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
			}

			return callResult;
		}

		// ******************************************************************
		public ViCellBlu.VcbResult CancelPrimeReagents()
		{
			ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
			{
				ErrorLevel = ErrorLevelEnum.Error,
				MethodResult = MethodResultEnum.Failure
			};

			try
			{
				if (!IsConnected)
				{
					callResult.ResponseDescription = "Not Connected";
					return callResult;
				}

				ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.CancelPrimeReagents));
				NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

				var reqHeader = new RequestHeader();
				CallMethodRequest cmRequest = new CallMethodRequest();
				cmRequest.ObjectId = _parentMethodNode;
				cmRequest.MethodId = methodNode;

				CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
				cmReqCollection.Add(cmRequest);
				CallMethodResultCollection resultCollection;
				DiagnosticInfoCollection diagResults;
				ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

				if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
				{
					callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
				}
			}
			catch (Exception e)
			{
				callResult.ResponseDescription = "Exception: " + e.Message;
				Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
			}

			return callResult;
		}

		// ******************************************************************
		public ViCellBlu.VcbResult PurgeReagents()
		{
			ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
			{
				ErrorLevel = ErrorLevelEnum.Error,
				MethodResult = MethodResultEnum.Failure
			};

			try
			{
				if (!IsConnected)
				{
					callResult.ResponseDescription = "Not Connected";
					return callResult;
				}

				ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.PurgeReagents));
				NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

				var reqHeader = new RequestHeader();
				CallMethodRequest cmRequest = new CallMethodRequest();
				cmRequest.ObjectId = _parentMethodNode;
				cmRequest.MethodId = methodNode;

				CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
				cmReqCollection.Add(cmRequest);
				CallMethodResultCollection resultCollection;
				DiagnosticInfoCollection diagResults;
				ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

				if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
				{
					callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
				}
			}
			catch (Exception e)
			{
				callResult.ResponseDescription = "Exception: " + e.Message;
				Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
			}

			return callResult;
		}

		// ******************************************************************
		public ViCellBlu.VcbResult CancelPurgeReagents()
		{
			ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
			{
				ErrorLevel = ErrorLevelEnum.Error,
				MethodResult = MethodResultEnum.Failure
			};

			try
			{
				if (!IsConnected)
				{
					callResult.ResponseDescription = "Not Connected";
					return callResult;
				}

				ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.CancelPurgeReagents));
				NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

				var reqHeader = new RequestHeader();
				CallMethodRequest cmRequest = new CallMethodRequest();
				cmRequest.ObjectId = _parentMethodNode;
				cmRequest.MethodId = methodNode;

				CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
				cmReqCollection.Add(cmRequest);
				CallMethodResultCollection resultCollection;
				DiagnosticInfoCollection diagResults;
				ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

				if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
				{
					callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
				}
			}
			catch (Exception e)
			{
				callResult.ResponseDescription = "Exception: " + e.Message;
				Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
			}

			return callResult;
		}

		// ******************************************************************
		public ViCellBlu.VcbResult Decontaminate()
		{
			ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
			{
				ErrorLevel = ErrorLevelEnum.Error,
				MethodResult = MethodResultEnum.Failure
			};

			try
			{
				if (!IsConnected)
				{
					callResult.ResponseDescription = "Not Connected";
					return callResult;
				}

				ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.Decontaminate));
				NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

				var reqHeader = new RequestHeader();
				CallMethodRequest cmRequest = new CallMethodRequest();
				cmRequest.ObjectId = _parentMethodNode;
				cmRequest.MethodId = methodNode;

				CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
				cmReqCollection.Add(cmRequest);
				CallMethodResultCollection resultCollection;
				DiagnosticInfoCollection diagResults;
				ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

				if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
				{
					callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
				}
			}
			catch (Exception e)
			{
				callResult.ResponseDescription = "Exception: " + e.Message;
				Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
			}

			return callResult;
		}

		// ******************************************************************
		public ViCellBlu.VcbResult CancelDecontaminate()
		{
			ViCellBlu.VcbResult callResult = new ViCellBlu.VcbResult()
			{
				ErrorLevel = ErrorLevelEnum.Error,
				MethodResult = MethodResultEnum.Failure
			};

			try
			{
				if (!IsConnected)
				{
					callResult.ResponseDescription = "Not Connected";
					return callResult;
				}

				ReferenceDescription method = _methodCollection.First(n => n.DisplayName.ToString().Equals(ViCellBlu.BrowseNames.CancelDecontaminate));
				NodeId methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

				var reqHeader = new RequestHeader();
				CallMethodRequest cmRequest = new CallMethodRequest();
				cmRequest.ObjectId = _parentMethodNode;
				cmRequest.MethodId = methodNode;

				CallMethodRequestCollection cmReqCollection = new CallMethodRequestCollection();
				cmReqCollection.Add(cmRequest);
				CallMethodResultCollection resultCollection;
				DiagnosticInfoCollection diagResults;
				ResponseHeader respHdr = _opcSession.Call(reqHeader, cmReqCollection, out resultCollection, out diagResults);

				if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
				{
					callResult = DecodeHelper.DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
				}
			}
			catch (Exception e)
			{
				callResult.ResponseDescription = "Exception: " + e.Message;
				Debug.WriteLine($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
			}

			return callResult;
		}

	#endregion

	#region Private_Functions
        // ******************************************************************
        private void Client_KeepAlive(Session sender, KeepAliveEventArgs e)
        {
            if (e.Status != null && ServiceResult.IsBad(e.Status.StatusCode))
            {
                if (_opcSession != null && OnDisconnect != null && _opcSession.Connected)
                    OnDisconnect.Invoke(e.Status.StatusCode);
            }

            if ((e.Status == null) || !ServiceResult.IsNotGood(e.Status))
            {
                return;
            }

            if (sender.DefunctRequestCount > 0)
            {
                Debug.WriteLine($"{e.Status} {sender.OutstandingRequestCount}/{sender.DefunctRequestCount}");
            }

            if (_reconnectHandler != null)
            {
                return;
            }

            Debug.WriteLine("--- RECONNECTING ---");
            _reconnectHandler = new SessionReconnectHandler();
            _reconnectHandler.BeginReconnect(sender, _reconnectPeriod * 1000, Client_ReconnectComplete);
        }

        // ******************************************************************
        private void Client_ReconnectComplete(object sender, EventArgs e)
        {
            // ignore callbacks from discarded objects.
            if (!ReferenceEquals(sender, _reconnectHandler))
            {
                return;
            }

            _opcSession = _reconnectHandler.Session;
            SetupVariables(MyMonitoredItemVariableHandler);
            SetupEvents(MyMonitoredItemEventHandler);
            _reconnectHandler?.Dispose();
            _reconnectHandler = null;

            if (OnReconnect != null && _opcSession != null)
                OnReconnect.Invoke();
            Debug.WriteLine(_opcSession == null ? "--- FAILED TO RECONNECT ---" : "--- RECONNECTED ---");
        }

        // ******************************************************************
        public ReferenceDescriptionCollection Browse(out byte[] continuationPoint, ExpandedNodeId expandedNodeId)
        {
            var nodeId = ExpandedNodeId.ToNodeId(expandedNodeId, _namespaceUris);
            return Browse(out continuationPoint, nodeId);
        }

        // ******************************************************************
        public ReferenceDescriptionCollection Browse(out byte[] continuationPoint, NodeId? nodeId = null)
        {
            if (null == nodeId)
            {
                nodeId = Opc.Ua.ObjectIds.ObjectsFolder;
            }

            if (null == _opcSession)
            {
                continuationPoint = new byte[1];
                return new ReferenceDescriptionCollection(1);
            }

            _opcSession.Browse(null, null, nodeId, 0u, BrowseDirection.Forward, ReferenceTypeIds.HierarchicalReferences, true,
                (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method, out continuationPoint, out var references);

            return references;
        }

        // ******************************************************************
        private void MyMonitoredItemVariableHandler(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {
            try
            {
                foreach (var value in monitoredItem.DequeueValues())
                {
                    switch (monitoredItem.DisplayName)
                    {
                        case ViCellBlu.BrowseNames.ViCellIdentifier:
                        {
                            var id = value.Value.ToString();
                            if(!string.IsNullOrEmpty(id) && OnUpdateViCellIdentifier != null)
                            {
                                OnUpdateViCellIdentifier(id);
                            }
                            break;
                        }

                        case ViCellBlu.BrowseNames.ViCellStatus:
                        {
                            var status = (ViCellStatusEnum)value.Value;
                            OnUpdateSystemStatus?.Invoke(status);
                            break;
                        }

                        case ViCellBlu.BrowseNames.LockState:
                        {
                            var state = (LockStateEnum)value.Value;
                            OnUpdateLockState?.Invoke(state);
                            break;
                        }

                        case ViCellBlu.BrowseNames.ReagentUsesRemaining:
                        {
                            uint remain = 0;
                            UInt32.TryParse(value.Value.ToString(), out remain);
                            OnUpdateReagentRemaining?.Invoke(remain);
                            break;
                        }

                        case ViCellBlu.BrowseNames.WasteTubeRemainingCapacity:
                        {
                            uint remain = 0;
                            UInt32.TryParse(value.Value.ToString(), out remain);
                            OnUpdateWasteTubeCapacity?.Invoke(remain);
                            break;
                        }

                        case ViCellBlu.BrowseNames.SoftwareVersion:
                        {
	                        var version = (string)value.Value;
	                        OnSoftwareVersionUpdate?.Invoke(version);
	                        break;
                        }

                        case ViCellBlu.BrowseNames.FirmwareVersion:
                        {
	                        var version = (string)value.Value;
	                        OnFirmwareVersionUpdate?.Invoke(version);
	                        break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // ******************************************************************
        private void MyMonitoredItemEventHandler(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {
            try
            {
                foreach (var value in monitoredItem.DequeueEvents())
                {
					if (value.EventFields == null)
                    {
                        continue;
                    }

					int index = 0;
					try
					{
						index = fields[value.EventFields[2].ToString()];
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
					}

					var str = value.EventFields[2].ToString();
                    switch (str)
                    {
	                    case "Session/CreateSession":
		                    break;
						case "Session/ActivateSession":
							break;

	                    case ViCellBlu.BrowseNames.SampleStatusChangedEvent:
	                    {
		                    if (value.EventFields[index].Value != null && _opcSession != null)
							{
								var data = DecodeHelper.DecodeRawSampleStatusData(value.EventFields[index].Value, _opcSession.MessageContext);
								OnSampleStatusChanged?.Invoke(data);
							}
		                    break;
	                    }
	                    case ViCellBlu.BrowseNames.SampleCompleteEvent:
	                    {
		                    if (value.EventFields[index].Value != null && _opcSession != null)
		                    {
								var result = DecodeHelper.DecodeRawSampleComplete(value.EventFields[index].Value, _opcSession.MessageContext);
								OnSampleComplete?.Invoke(result);
		                    }
		                    break;
	                    }
	                    case ViCellBlu.BrowseNames.WorkListCompleteEvent:
	                    {
		                    if (value.EventFields[index].Value != null)
		                    {
			                    OnWorklistComplete?.Invoke(((Uuid[])value.EventFields[index].Value));
		                    }
		                    break;
	                    }
	                    case ViCellBlu.BrowseNames.ExportStatusEvent:
	                    {
		                    if (value.EventFields[index].Value != null && _opcSession != null)
		                    {
									var data = DecodeHelper.DecodeExportStatus(value.EventFields[index].Value, _opcSession.MessageContext);
									OnExportStatusUpdate?.Invoke(data);
		                    }
		                    break;
	                    }
	                    case ViCellBlu.BrowseNames.DeleteSampleResultsProgressEvent:
	                    {
		                    if (value.EventFields[index].Value != null && _opcSession != null)
		                    {
								var data = DecodeHelper.DecodeDeleteSampleStatus(value.EventFields[index].Value, _opcSession.MessageContext);
								OnDeleteSampleStatusUpdate?.Invoke(data);
		                    }
		                    break;
	                    }
						case ViCellBlu.BrowseNames.CleanFluidicsStatusEvent:
	                    {
		                    if (value.EventFields[index].Value != null && _opcSession != null)
		                    {
			                    var result = DecodeHelper.DecodeCleanFluidicsStatus(value.EventFields[index].Value, _opcSession.MessageContext);
			                    OnCleanFluidicsStatusUpdate?.Invoke(result);
		                    }
		                    break;
	                    }
	                    case ViCellBlu.BrowseNames.PrimeReagentsStatusEvent:
	                    {
		                    if (value.EventFields[index].Value != null && _opcSession != null)
		                    {
                                var result = DecodeHelper.DecodePrimeReagentsStatus(value.EventFields[index].Value, _opcSession.MessageContext);
			                    OnPrimeReagentsStatusUpdate?.Invoke(result);
		                    }
                            break;
	                    }
						case ViCellBlu.BrowseNames.PurgeReagentsStatusEvent:
	                    {
		                    if (value.EventFields[index].Value != null && _opcSession != null)
		                    {
			                    var result = DecodeHelper.DecodePurgeReagentsStatus(value.EventFields[index].Value, _opcSession.MessageContext);
			                    OnPurgeReagentsStatusUpdate?.Invoke(result);
		                    }
		                    break;
	                    }
	                    case ViCellBlu.BrowseNames.DecontaminateStatusEvent:
	                    {
		                    if (value.EventFields[index].Value != null && _opcSession != null)
		                    {
			                    var result = DecodeHelper.DecodeDecontaminateStatus(value.EventFields[index].Value, _opcSession.MessageContext);
			                    OnDecontaminateStatusUpdate?.Invoke(result);
		                    }
		                    break;
	                    }
	                    case ViCellBlu.BrowseNames.ErrorStatusEvent:
	                    {
		                    if (value.EventFields[index].Value != null && _opcSession != null)
		                    { 
			                    var result = DecodeHelper.DecodeErrorStatus(value.EventFields[index].Value, _opcSession.MessageContext);
			                    OnErrorStatusUpdate?.Invoke(result);
		                    }
                            break;
	                    }
                    }
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        // ******************************************************************
        private ViCellBlu.CellType GetCellTypeDataType(CellType ct)
        {
            ViCellBlu.CellType bcell = new CellType();
            bcell.CellTypeName = ct.CellTypeName;
            bcell.ConcentrationAdjustmentFactor = Convert.ToSingle(ct.ConcentrationAdjustmentFactor);
            bcell.CellSharpness = (float)ct.CellSharpness;
            bcell.DeclusterDegree = ct.DeclusterDegree;
            bcell.MaxDiameter = ct.MaxDiameter;
            bcell.MinCircularity = ct.MinCircularity;
            bcell.MinDiameter = ct.MinDiameter;
            bcell.NumAspirationCycles = (int)ct.NumAspirationCycles;
            bcell.NumImages = (int)ct.NumImages;
            bcell.NumMixingCycles = (int)ct.NumMixingCycles;
            bcell.ViableSpotArea = (float)ct.ViableSpotArea;
            bcell.ViableSpotBrightness = (float)ct.ViableSpotBrightness;
            return bcell;
        }

        // ******************************************************************
        private ViCellBlu.SampleSet GetSampleSetDataType(SampleSetConfig scfg, ViCellBlu.PlatePrecessionEnum sortOrder)
        {
            ViCellBlu.SampleSet bsset = new ViCellBlu.SampleSet();
            bsset.SampleSetName = scfg.Name;
            bsset.PlatePrecession = sortOrder;
            foreach (var sample in scfg.Samples)
            {
                var tmp = GetSampleDataType(sample);
                // @todo - move and rename PlatePrecession - move 'sortOrder' to the set and out of the sample
                tmp.PlatePrecession = sortOrder;
                bsset.Samples.Add(tmp);
            }
            return bsset;
        }

        // ******************************************************************
        private ViCellBlu.SampleConfig GetSampleDataType(SampleConfig scfg)
        {
            ViCellBlu.SampleConfig sdt = new ViCellBlu.SampleConfig();
            sdt.SampleName = scfg.Name;
            sdt.SamplePosition.Column = (uint)scfg.Position.Column;
            sdt.SamplePosition.Row = ((char)scfg.Position.Row).ToString();
            sdt.Tag = scfg.Tag;
            sdt.Dilution = scfg.Dilution;
            sdt.CellType.CellTypeName = scfg.CellTypeName;
            sdt.QualityControl.QualityControlName = scfg.QCName;
            sdt.SaveEveryNthImage = scfg.SaveEveryNthImage;
            sdt.WashType = (ViCellBlu.WashTypeEnum)scfg.WashType;
            return sdt;
        }

	#endregion

    }
}
