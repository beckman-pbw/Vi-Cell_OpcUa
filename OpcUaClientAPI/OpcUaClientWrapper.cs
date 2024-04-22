using ViCellBlu;
using log4net;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BrowseNames = ViCellBlu.BrowseNames;
using Methods = ViCellBlu.Methods;
using ObjectIds = Opc.Ua.ObjectIds;
using Session = Opc.Ua.Client.Session;

// TODO: We eventually will need to provide a certificate to install to ensure secure communication with our Server.
// The following ticket addresses this need: https://lsjira.beckman.com/browse/PC3549-2339

namespace OpcUaClientAPI
{
    public class OpcUaClientWrapper
    {
        #region Fields

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        private readonly string _endpointUrl = "opc.tcp://localhost:62641/ViCellBlu/Server";
        private bool _autoAccept = true;
        private readonly int _reconnectPeriod = 10;
        private readonly int _keepAliveInterval = 5;

        private Session _session;
        private SessionReconnectHandler _reconnectHandler;
        private NamespaceTable _namespaceUris;

        private readonly ReferenceDescriptionCollection _methodCollection;
        private readonly ReferenceDescription _browsedMethods;
        private readonly NodeId _parentNode;

        private int _sampleSize = 39; // total data inputs for one sample

        #endregion

        #region Constructors

        public OpcUaClientWrapper()
        {
            if (!IsConnectedToServer())
                ConnectToServer();

            var scoutXrefs = Browse(out _);
            var viCellBlu = scoutXrefs.First(n => n.BrowseName.Name.Equals(BrowseNames.ViCellBluState));
            var viCellBluRefs = Browse(out _, viCellBlu.NodeId);

            _browsedMethods = viCellBluRefs.First(n => n.BrowseName.Name.Equals(BrowseNames.Methods));
            _methodCollection = Browse(out _, _browsedMethods.NodeId);
            _parentNode = ExpandedNodeId.ToNodeId(_browsedMethods.NodeId, _session.NamespaceUris);
        }       

        #endregion

        // ******************************************************************************************************************
        // CONNECTION / UTILITIES
        // ******************************************************************************************************************

        private bool IsConnectedToServer()
        {
            return _session != null && _session.Connected;
        }

        private void ConnectToServer()
        {
            try
            {
                // Load the application configuration.
                var application = new ApplicationInstance
                {
                    ApplicationType = ApplicationType.Client,
                    ApplicationName = "Opc.Ua.ScoutX.TestClient"
                };

                var clientConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ViCellBLU.Client.Config.xml");
                Console.WriteLine($"Config Path = {clientConfigFile}");
                // Delaying to give the OPC/UA server a chance to fully complete its initialization.
                Thread.Sleep(2000);
                var configTask = application.LoadApplicationConfiguration(clientConfigFile, false);
                var config = configTask.Result;

                var haveAppCertificateTask = CheckApplicationCertificate(application, config);
                var haveAppCertificate = haveAppCertificateTask.Result;

                var selectedEndpoint = CoreClientUtils.SelectEndpoint(_endpointUrl, haveAppCertificate, 15000);

                var endpointConfiguration = EndpointConfiguration.Create(config);
                var endpoint = new ConfiguredEndpoint(null, selectedEndpoint, endpointConfiguration);

                var sessionTask = Session.Create(config, endpoint, false, "OPC UA Client Wrapper", 60000, new UserIdentity(new AnonymousIdentityToken()), null);
                _session = sessionTask.Result;
                _namespaceUris = _session.NamespaceUris;
                _session.KeepAlive += Client_KeepAlive;
            }
            catch (Exception e)
            {
                LogApiError("--- EXCEPTION: ConnectToServer ---", e);
                throw;
            }
        }

        private void Client_KeepAlive(Session sender, KeepAliveEventArgs e)
        {
            if (e.Status == null || !ServiceResult.IsNotGood(e.Status))
                return;

            LogApiInfo($"{e.Status} {sender.OutstandingRequestCount}/{sender.DefunctRequestCount}");

            if (_reconnectHandler != null)
                return;

            LogApiWarn("--- RECONNECTING ---");
            _reconnectHandler = new SessionReconnectHandler();
            _reconnectHandler.BeginReconnect(sender, _reconnectPeriod * 1000, Client_ReconnectComplete);
        }
        
        private async Task<bool> CheckApplicationCertificate(ApplicationInstance application, ApplicationConfiguration config)
        {
            // check the application certificate.
            var haveAppCertificate = await application.CheckApplicationInstanceCertificate(false, 0);
            if (!haveAppCertificate)
                throw new Exception("Application instance certificate invalid!");

            if (haveAppCertificate)
            {
                config.ApplicationUri = X509Utils.GetApplicationUriFromCertificate(config.SecurityConfiguration.ApplicationCertificate.Certificate);
                if (config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
                {
                    _autoAccept = true;
                }

                config.CertificateValidator.CertificateValidation += CertificateValidator_CertificateValidation;
            }
            else
                LogApiWarn("    WARN: missing application certificate, using unsecure connection.");
           
            return haveAppCertificate;
        }

        private void Client_ReconnectComplete(object sender, EventArgs e)
        {
            // ignore callbacks from discarded objects.
            if (!ReferenceEquals(sender, _reconnectHandler))
                return;

            _session = _reconnectHandler?.Session;
            _reconnectHandler?.Dispose();
            _reconnectHandler = null;

            LogApiWarn(_session == null ? "--- FAILED TO RECONNECT ---" : "--- RECONNECTED ---");
        }

        private void CertificateValidator_CertificateValidation(CertificateValidator validator, CertificateValidationEventArgs e)
        {
            if (e.Error.StatusCode != StatusCodes.BadCertificateUntrusted)
                return;

            e.Accept = _autoAccept;
            LogApiWarn(_autoAccept ? $"Accepted Certificate: {e.Certificate.Subject}" : $"Rejected Certificate: {e.Certificate.Subject}");
        }

        // ******************************************************************************************************************
        // API EXPOSURE
        // ******************************************************************************************************************


        #region RequestLock
        
        public void RequestLock(object[] arguments)
        {
            if (arguments.Length < 2)
            {
                LogApiWarn($"InvalidMethodParameters : Not enough arguments were passed to {MethodBase.GetCurrentMethod()?.Name}");
                return;
            }

            try
            {
                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(
                    Methods.ViCellBluState_Methods_RequestLock));

                var creds = new Credential
                {
                    Username = (string)arguments[0],
                    Password = (string)arguments[1]
                };

                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode, creds);
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }

        #endregion

        #region ReleaseLock

        public void ReleaseLock(object[] arguments)
        {
            if (arguments.Length < 2)
            {
                LogApiWarn($"InvalidMethodParameters : Not enough arguments were passed to {MethodBase.GetCurrentMethod()?.Name}");
                return;
            }

            try
            {
                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(
                    Methods.ViCellBluState_Methods_ReleaseLock));

                var creds = new Credential
                {
                    Username = (string)arguments[0],
                    Password = (string)arguments[1]
                };

                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode, creds);
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }

        #endregion

        #region StartSample

        public void StartSample(object[] arguments)
        {
            try
            {
                if (arguments.Length < 2 + _sampleSize)
                {
                    LogApiWarn($"InvalidMethodParameters : {MethodBase.GetCurrentMethod()?.Name} must have a sufficient parameter length.");
                    return;
                }

                var sample = CreateSampleDataType(arguments);

                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(
                    Methods.ViCellBluState_PlayControl_StartSample));

                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode, sample);
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }

        #endregion

        #region StartSampleSet

        public void StartSampleSet(object[] arguments)
        {
            if (arguments.Length == 0 || ((arguments.Length - 2) % _sampleSize) != 0) // Checking for sample size misalignment.
            {
                LogApiWarn($"InvalidMethodParameters : {MethodBase.GetCurrentMethod()?.Name} must have a sufficient parameter length.");
                return;
            }

            try
            {
                var samples = new List<SampleConfig>();
                if(arguments.Length > 2)
                    for (var a = 2; a < arguments.Length; a += _sampleSize)
                    {
                        var setRange = a + _sampleSize;
                        if (setRange > arguments.Length) 
                            continue;
                        
                        var range = SubArray(arguments, a, setRange);
                        samples.Add(CreateSampleDataType(range));
                    }

                var sampleSet = new SampleConfigCollection();
                sampleSet.AddRange(samples);

                var sampleSetMethod = new SampleSet
                {
                    SampleSetName = Convert.ToString(arguments[0]),
                    SampleSetUuid = new Uuid(Convert.ToString(arguments[1])),
                    Samples = sampleSet
                };

                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(
                    Methods.ViCellBluState_PlayControl_StartSampleSet));

                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode, sampleSetMethod);
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }

        #endregion

        #region Pause

        public void Pause()
        {
            CallParameterlessMethod(Methods.ViCellBluState_PlayControl_Pause);
        }

        #endregion

        #region Resume

        public void Resume()
        {
            CallParameterlessMethod(Methods.ViCellBluState_PlayControl_Resume);
        }

        #endregion

        #region Stop

        public void Stop()
        {
            CallParameterlessMethod(Methods.ViCellBluState_PlayControl_Stop);
        }

        #endregion

        #region EjectStage

        public void EjectStage()
        {
            CallParameterlessMethod(Methods.ViCellBluState_PlayControl_EjectStage);
        }

        #endregion

        #region GetSampleResults

        public void GetSampleResults(object[] arguments)
        {
            if (arguments.Length == 0)
            {
                LogApiWarn($"InvalidMethodParameters : {MethodBase.GetCurrentMethod()?.Name} must have a sufficient parameter length.");
                return;
            }

            try
            {
                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(
                    Methods.ViCellBluState_Methods_GetSampleResults));

                var filterOn = ConvertToFilterOn(Convert.ToUInt32(arguments[0]));
                var fromDate = ConvertToDateTime(Convert.ToInt64(arguments[1]));
                var toDate = ConvertToDateTime(Convert.ToInt64(arguments[2]));
                var userName = Convert.ToString(arguments[3]);
                var searchName = Convert.ToString(arguments[4]);
                var searchTag = Convert.ToString(arguments[5]);
                var cellTypeQualityControlName = Convert.ToString(arguments[6]);

                var arr = new List<object> {filterOn, fromDate, toDate, userName, searchName, searchTag, cellTypeQualityControlName};

                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode, arr.ToArray());
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }

        #endregion

        #region DeleteSampleResults

        public void DeleteSampleResults(object[] arguments)
        {
            if (arguments.Length == 0)
            {
                LogApiWarn($"InvalidMethodParameters : {MethodBase.GetCurrentMethod()?.Name} must have a sufficient parameter length.");
                return;
            }

            try
            {
                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(
                    Methods.ViCellBluState_Methods_DeleteSampleResults));

                var myUuidList = new List<Uuid>();
                foreach (var uuid in arguments)
                    myUuidList.Add(new Uuid(Convert.ToString(uuid)));

                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode, myUuidList.ToArray());
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }

        #endregion

        #region CreateCellType

        public void CreateCellType(object[] arguments)
        {
            if (arguments.Length == 0)
            {
                LogApiWarn($"InvalidMethodParameters : {MethodBase.GetCurrentMethod()?.Name} must have a sufficient parameter length.");
                return;
            }

            try
            {
                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(
                    Methods.ViCellBluState_Methods_CreateCellType));

                var myUuidList = new List<Uuid>();
                foreach (var uuid in arguments)
                    myUuidList.Add(new Uuid(Convert.ToString(uuid)));

                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode, myUuidList.ToArray());
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }

        #endregion

        #region DeleteCellType

        public void DeleteCellType(object[] arguments)
        {
            if (arguments.Length == 0)
            {
                LogApiWarn($"InvalidMethodParameters : {MethodBase.GetCurrentMethod()?.Name} must have a sufficient parameter length.");
                return;
            }

            try
            {
                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(
                    Methods.ViCellBluState_Methods_DeleteCellType));

                var uuid = new Uuid(Convert.ToString(arguments));

                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode, uuid);
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }

        #endregion

        #region ImportConfig

        public void ImportConfig(object[] arguments)
        {
            if (arguments.Length == 0)
            {
                LogApiWarn($"InvalidMethodParameters : {MethodBase.GetCurrentMethod()?.Name} must have a sufficient parameter length.");
                return;
            }

            try
            {
                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(
                    Methods.ViCellBluState_Methods_ImportConfig));

                var configFilePath = arguments;

                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode, configFilePath);
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }

        #endregion

        #region ExportConfig

        public void ExportConfig(object[] arguments)
        {
            if (arguments.Length == 0)
            {
                LogApiWarn($"InvalidMethodParameters : {MethodBase.GetCurrentMethod()?.Name} must have a sufficient parameter length.");
                return;
            }

            try
            {
                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(
                    Methods.ViCellBluState_Methods_ExportConfig));

                var configFileContents = arguments;

                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode, configFileContents);
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }

        #endregion

        #region RetrieveSampleExport

        public void RetrieveSampleExport(object[] arguments)
        {
            if (arguments.Length != 1)
            {
                LogApiWarn($"InvalidMethodParameters : {MethodBase.GetCurrentMethod()?.Name} must have a sufficient parameter length.");
                return;
            }

            try
            {
                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(
                    Methods.ViCellBluState_Methods_RetrieveSampleExport));

                var uuid = new Uuid(Convert.ToString(arguments[0]));

                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode, uuid);
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }

        #endregion

        #region CreateQualityControl

        public void CreateQualityControl(object[] arguments)
        {
            if (arguments.Length == 0)
            {
                LogApiWarn($"InvalidMethodParameters : {MethodBase.GetCurrentMethod()?.Name} must have a sufficient parameter length.");
                return;
            }

            try
            {
                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(
                    Methods.ViCellBluState_Methods_CreateQualityControl));

                var qualityControlData = new QualityControl
                {
                    AcceptanceLimits = Convert.ToInt32(arguments[13]),
                    AssayParameter = ConvertToAssayParameter(Convert.ToInt32(arguments[14])),
                    AssayValue = Convert.ToDouble(arguments[15]),
                    Comments = Convert.ToString(arguments[16]),
                    ExpirationDate = ConvertToDateTime(Convert.ToInt64(arguments[17])),
                    LotNumber = Convert.ToString(arguments[18]),
                    QualityControlName = Convert.ToString(arguments[19]),
                };

                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode, qualityControlData);
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }
        
        #endregion

        // ******************************************************************************************************************
        // API HELPER METHODS
        // ******************************************************************************************************************

        #region Logging

        private void LogApiError(string description, Exception ex)
        {
            Log.Error(description, ex);
            Console.WriteLine(description);
            Console.WriteLine(ex);
        }

        private void LogApiInfo(string description)
        {
            Log.Info(description);
            Console.WriteLine(description);
        }

        private void LogApiWarn(string description)
        {
            Log.Warn(description);
            Console.WriteLine(description);
        }

        #endregion

        private T[] SubArray<T>(T[] array, int startIndex, int endIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (endIndex > array.Length)
                throw new ArgumentOutOfRangeException(nameof(endIndex));
            if (endIndex < startIndex)
                throw new ArgumentOutOfRangeException($"{nameof(endIndex)} is less than {nameof(startIndex)}.");

            var length = endIndex - startIndex;
            var result = new T[length];
            Array.Copy(array, startIndex, result, 0, length);
            return result;
        }

        private void CallParameterlessMethod(uint methodType)
        {
            try
            {
                var method = _methodCollection.First(n => n.NodeId.Identifier.Equals(methodType));
                var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _session.NamespaceUris);
                var ret = _session.Call(_parentNode, methodNode);
            }
            catch (Exception e)
            {
                LogApiError($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
            }
        }

        private SampleConfig CreateSampleDataType(object[] args)
        {
            try
            {
                return new SampleConfig
                {
                    Dilution = Convert.ToUInt32(args[0]),
                    SampleName = Convert.ToString(args[1]),
                    SampleUuid = new Uuid(Convert.ToString(args[2])),
                    Tag = Convert.ToString(args[3]),
                    CellType = new CellType
                    {
                        ConcentrationAdjustmentFactor = Convert.ToSingle(args[4]),
                        CellSharpness = (float)Convert.ToDouble(args[5]),
                        CellTypeName = Convert.ToString(args[6]),
                        DeclusterDegree = ConvertToDegreeEnum(Convert.ToUInt32(args[8])),
                        MaxDiameter = Convert.ToDouble(args[9]),
                        MinCircularity = Convert.ToDouble(args[10]),
                        MinDiameter = Convert.ToDouble(args[11]),
                        NumAspirationCycles = Convert.ToInt32(args[12]),
                        NumImages = Convert.ToInt32(args[13]),
                        NumMixingCycles = Convert.ToInt32(args[14]),
                        ViableSpotArea = (float)Convert.ToDouble(args[15]),
                        ViableSpotBrightness = (float)Convert.ToDouble(args[16])
                    },
                    QualityControl = new QualityControl
                    {
                        AcceptanceLimits = Convert.ToInt32(args[30]),
                        AssayParameter = ConvertToAssayParameter(Convert.ToInt32(args[31])),
                        AssayValue = Convert.ToDouble(args[32]),
                        Comments = Convert.ToString(args[33]),
                        ExpirationDate = ConvertToDateTime(Convert.ToInt64(args[34])),
                        LotNumber = Convert.ToString(args[35]),
                        QualityControlName = Convert.ToString(args[36]),
                    },
                    WashType = ConvertToWashType(Convert.ToUInt32(args[38]))
                };

            }
            catch (Exception e)
            {
                Log.Error($"Error attempting {MethodBase.GetCurrentMethod()?.Name}", e);
                throw;
            }
        }

        private FilterOnEnum ConvertToFilterOn(uint filterType)
        {
            try
            {
                switch (filterType)
                {
                    case 0:
                        return FilterOnEnum.SampleSet;
                    case 1:
                        return FilterOnEnum.Sample;
                    default:
                        return FilterOnEnum.SampleSet;
                }
            }
            catch (Exception e)
            {
                LogApiError($"{MethodBase.GetCurrentMethod()?.Name} conversion failed. Defaulting to 'SampleSet'", e);
                return FilterOnEnum.SampleSet;
            }
        }

        private DeclusterDegreeEnum ConvertToDegreeEnum(uint declusterDegree)
        {
            try
            {
                switch (declusterDegree)
                {
                    case 1: return DeclusterDegreeEnum.Low;
                    case 2: return DeclusterDegreeEnum.Medium;
                    case 3: return DeclusterDegreeEnum.High;
                    default: return DeclusterDegreeEnum.None;
                }
            }
            catch (Exception e)
            {
                LogApiError($"{MethodBase.GetCurrentMethod()?.Name} conversion failed. Defaulting to 'None'", e);
                return DeclusterDegreeEnum.None;
            }
        }

        private WashTypeEnum ConvertToWashType(uint washType)
        {
            try
            {
                switch (washType)
                {
                    case 0:
                        return WashTypeEnum.NormalWash;
                    case 1:
                        return WashTypeEnum.FastWash;
                    default:
                        return WashTypeEnum.NormalWash;
                }
            }
            catch (Exception e)
            {
                LogApiError($"{MethodBase.GetCurrentMethod()?.Name} conversion failed. Defaulting to 'Normal Wash'", e);
                return WashTypeEnum.NormalWash;
            }
        }

        private DateTime ConvertToDateTime(long unixTimestampInMicroseconds)
        {
            try
            {
                var time = unixTimestampInMicroseconds;
                time /= 1000; // To Milliseconds

                return DateTimeOffset.FromUnixTimeMilliseconds(time).DateTime;
            }
            catch (Exception e)
            {
                LogApiError("ConvertToDateTime conversion failed. Defaulting to Datetime.Now", e);
                return DateTime.Now;
            }
        }

        private AssayParameterEnum ConvertToAssayParameter(int assayType)
        {
            try
            {
                switch (assayType)
                {
                    case 0:
                        return AssayParameterEnum.Concentration;
                    case 1:
                        return AssayParameterEnum.PopulationPercentage;
                    case 2:
                        return AssayParameterEnum.Size;
                    default:
                        return AssayParameterEnum.Concentration;
                }
            }
            catch (Exception e)
            {
                LogApiError("AssayParameterEnum conversion failed. Defaulting to type 'Concentration'", e);
                return AssayParameterEnum.Concentration;
            }
        }

        public ReferenceDescriptionCollection Browse(out byte[] continuationPoint, ExpandedNodeId expandedNodeId)
        {
            var nodeId = ExpandedNodeId.ToNodeId(expandedNodeId, _namespaceUris);
            return Browse(out continuationPoint, nodeId);
        }

        public ReferenceDescriptionCollection Browse(out byte[] continuationPoint, NodeId nodeId = null)
        {
            if (null == nodeId)
                nodeId = ObjectIds.ObjectsFolder;

            _session.Browse(null, null, nodeId, 0u, BrowseDirection.Forward, ReferenceTypeIds.HierarchicalReferences, true,
                (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method, out continuationPoint, out var references);

            return references;
        }

    }
}