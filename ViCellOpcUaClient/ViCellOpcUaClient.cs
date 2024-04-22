using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading;

namespace ViCellOpcUaClient
{
    class ViCellOpcUaClient
    {
        /*  System Requirements
            Operating System : Windows 10 64-bit
            Framework : .NET Framework 4.8
        */

        /*  How to Build
            A) Rebuild the ViCellOpcUaClient Solution.
            B) Open Administrator Command Prompt Window and navigate to the bin/Debug or bin/Release folder
            C) Proceed to "Available API Calls" below
        */

        /*  Nuget/DLLs Utilized

            Nuget Package(s)
            - OpcFoundation.NetStandard.Opc.Ua (v1.4.365.48)
            
            DLL(s)
            - C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\Microsoft.CSharp.dll
            - C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.dll
            - C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Core.dll
            - C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Data.dll
            - C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Data.DataSetExtensions.dll
            - C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Net.Http.dll
            - C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Runtime.Serialization.dll
            - C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Xml.dll
            - C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Xml.Linq.dll

        */

        /*  Available API Calls      
           
            Syntax:  ViCellOpcUaClient.exe [Ip Address] [Port] [Username] [Password] [Command] [Command Parameter(s)]
            Example: ViCellOpcUaClient.exe "127.0.0.1", "62641", "factory_admin", "mYPassWord123", "GetCellTypes", "" 

                1) Command: GetCellTypes
                   Parameters: [None]
                   Description: Returns one or more user-defined/created cell types.
                   Example: GetCellTypes ""

                2) Command: StartSample
                   Parameters: [None]
                   Description: Starts a single predefined sample by passing the full path to the .bsc file
                   Example: StartSample ""

                3) Command: GetSampleResults
                   Parameters: [None]
                   Description: Returns one or more SampleResult definitions that occurred within the last 14 days. 
                                The daterange for this can be customized, but for this example client we simplify this to only the last 14 days. 
                                Parameter values should be deliminated with a single comma ','.
                   Example: GetSampleResults ""

                4) Command: RetrieveSampleExport
                   Parameters: [GUID (Required)]
                               [GUID (Optional)]
                               [GUID (Optional)]
                               ...
                   Description: Exports one or more samples contained in a single file (.zip format) to the folder location (C:\Instrument\Export). GUIDs should be deliminated with a single comma ','.
                                The GUID(s) that are passed to this command as parameters can be acquire after you have:
                                a) Ran a sample using StartSample
                                b) Get the sample results that contain the GUIDs using GetSampleResults
                                c) Note/Copy the GUID(s) returned from GetSampleResults
                                d) Run the RetrieveSampleExport, passing the GUID(s). 
                   Example: RetrieveSampleExport "61A6D85A-DEA9-41FD-A005-D57996FACC07,770A67A8-0C86-4B1A-BDFE-8E53CE619F67"

        */

        #region Fields

        private static Session _opcSession;
        private static ReferenceDescriptionCollection _methodCollection;
        private static NodeId _parentMethodNode;
        private static NamespaceTable _namespaceUris;

        public static ApplicationConfiguration OpcAppConfig { get; private set; }

        private static SessionReconnectHandler _reconnectHandler;

        private static ReferenceDescriptionCollection _playCtrlCollection;
        private static ReferenceDescription _browsedPlayCtrl;
        private static NodeId _parentPlayNode;

        private static ReferenceDescription _browsedMethods;

        private static int _discoverTimeout = 15000;
        private static uint _cnxTimeout = 60000;

        // StartExport fields
        private static BinaryWriter _exportWriter;
        public static ViCellBlu.ExportStatusEnum CurrentExportStatus { get; private set; } = ViCellBlu.ExportStatusEnum.Unknown;

        public delegate void ExportStatusHandler(ViCellBlu.ExportStatusData status);

        public static ExportStatusHandler OnExportStatusUpdate { get; set; } = null;

        private static DateTime timeoutPeriod;
        private static bool receivedReady = false;

        private const string beckmanNamespace = "https://www.beckman.com/BeckmanUa";

        #endregion

        #region Private Helper Methods

        private static bool IsCommandValid(string command)
        {
            switch (command)
            {
                case "RetrieveSampleExport":
                {
                    return true;
                }
                case "GetCellTypes":
                {
                    return true;
                }
                case "GetSampleResults":
                {
                    return true;
                }
                case "StartSample":
                {
                    return true;
                }
            }

            return false;
        }

        private static string GetCorrectCommandParameterSyntaxString(string command)
        {
            switch (command)
            {
                case "RetrieveSampleExport":
                {
                    return "[GUID(Required)],[GUID(Optional)],[GUID(Optional)]...";
                }
                case "GetCellTypes":
                {
                    return "[None]";
                }
                case "GetSampleResults":
                {
                    return "[None]";
                }
                case "StartSample":
                {
                    return "[None]";
                }
            }

            return "Could not determine command parameters, please refer to documentation in the ViCellOpcUaClient source code.";
        }

        private static void PrintProgress(int percentage)
        {
            Console.WriteLine($"=== PROGRESS: {percentage}% ===");
        }

        private static void PrintCorrectionMessage(string errorMessage)
        {
            if (errorMessage != string.Empty)
                Console.WriteLine($"{errorMessage}");
            Console.WriteLine("\nPlease correct and try again.");
            Console.WriteLine("The ViCellOpcUaClient will now close after you press any key.");
            Console.ReadKey();
        }

        private static void Client_KeepAlive(Session sender, KeepAliveEventArgs e)
        {
            if (e.Status != null && ServiceResult.IsBad(e.Status.StatusCode))
            {
                if (_opcSession != null && _opcSession.Connected)
                {
                    Console.WriteLine("You've been disconnected.");
                }
            }

            if ((e.Status == null) || !ServiceResult.IsNotGood(e.Status))
            {
                return;
            }

            if (sender.DefunctRequestCount > 0)
            {
                Console.WriteLine($"{e.Status} {sender.OutstandingRequestCount}/{sender.DefunctRequestCount}");
            }
        }

        private static ReferenceDescriptionCollection Browse(out byte[] continuationPoint, NodeId nodeId = null)
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

            _opcSession.Browse(null, null, nodeId, 0u, BrowseDirection.Forward, ReferenceTypeIds.HierarchicalReferences,
                true,
                (uint) NodeClass.Variable | (uint) NodeClass.Object | (uint) NodeClass.Method, out continuationPoint,
                out var references);

            return references;
        }

        private static ReferenceDescriptionCollection Browse(out byte[] continuationPoint, ExpandedNodeId expandedNodeId)
        {
            var nodeId = ExpandedNodeId.ToNodeId(expandedNodeId, _namespaceUris);
            return Browse(out continuationPoint, nodeId);
        }

        private static void SetOpcConfig()
        {
            OpcAppConfig.SecurityConfiguration.ApplicationCertificate = new CertificateIdentifier
            {
                StoreType = @"Directory",
                StorePath = @"%CommonApplicationData%\ViCellBlu_dotNET\pki\own",
                SubjectName = "CN=Vi-Cell BLU Client, C=US, S=Colorado, O=Beckman Coulter, DC=" + Dns.GetHostName()
            };
            OpcAppConfig.SecurityConfiguration.TrustedIssuerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\ViCellBlu_dotNET\pki\issuers" };
            OpcAppConfig.SecurityConfiguration.TrustedPeerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\ViCellBlu_dotNET\pki\trusted" };
            OpcAppConfig.SecurityConfiguration.RejectedCertificateStore = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\ViCellBlu_dotNET\pki\rejected" };
            OpcAppConfig.SecurityConfiguration.UserIssuerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\ViCellBlu_dotNET\pki\issuerUser" };
            OpcAppConfig.SecurityConfiguration.TrustedUserCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\ViCellBlu_dotNET\pki\trustedUser" };
            OpcAppConfig.SecurityConfiguration.AddAppCertToTrustedStore = true;
            OpcAppConfig.SecurityConfiguration.RejectSHA1SignedCertificates = false;
            OpcAppConfig.SecurityConfiguration.RejectUnknownRevocationStatus = true;
            OpcAppConfig.SecurityConfiguration.MinimumCertificateKeySize = 2048;
            OpcAppConfig.SecurityConfiguration.SendCertificateChain = true;
            OpcAppConfig.SecurityConfiguration.AutoAcceptUntrustedCertificates = true;
            OpcAppConfig.CertificateValidator.CertificateValidation += CertificateValidator_CertificateValidation;
        }

        private static void CertificateValidator_CertificateValidation(CertificateValidator validator, CertificateValidationEventArgs e)
        {
            if (e.Error.StatusCode != StatusCodes.BadCertificateUntrusted)
            {
                return;
            }
            // Always accepts the certificate 
            e.Accept = true;
        }

        #endregion

        static void Main(string[] args)
        {
            OnExportStatusUpdate += UpdateExportStatusCB;

            #region Gather Parameters

            if (args.Length != 6)
            {
                Console.WriteLine("\nYou have specified invalid parameters. There must be six parameters in total passed to the ViCellOpcUaClient.\n");
                Console.WriteLine("Syntax:  C:\\Path\\To\\My\\Folder\\ViCellOpcUaClient.exe [Ip Address] [Port] [Username] [Password] [Command] [Command Parameter(s)]");
                Console.WriteLine("Example: C:\\Path\\To\\My\\Folder\\ViCellOpcUaClient.exe \"127.0.0.1\" \"62641\" \"factory_admin\" \"MyPassword123\" \"GetCellTypes\" \"\"");
                PrintCorrectionMessage("");
                return;
            }

            #endregion

            #region Validate Parameters

            if (!IPAddress.TryParse(args[0], out _))
            {
                PrintCorrectionMessage($"The IP address you specified is not a valid IP format: {args[0]}");
                return;
            }

            if (!int.TryParse(args[1], out var port) || port < 1 || port > 65535)
            {
                PrintCorrectionMessage($"The port you specified is not a valid port format, or is out of valid port range: {args[0]}");
                return;
            }

            if (args[2] == string.Empty)
            {
                PrintCorrectionMessage("The username you specified is empty, please specify a non-empty string.");
                return;
            }

            if (args[3] == string.Empty)
            {
                PrintCorrectionMessage("The password you specified is empty, please specify a non-empty string.");
                return;
            }

            if (args[4] == string.Empty || !IsCommandValid(args[4])) 
            {
                PrintCorrectionMessage("The command you specified is incorrect, please specify a valid available command. Available Commands are:\nGetCellTypes\nStartSample\nGetSampleResults\nRetrieveSampleExport");
                return;
            }

            if (args[5] != string.Empty)
            {
                if (args[4] != "RetrieveSampleExport")
                {
                    var correctCommandSyntaxString = GetCorrectCommandParameterSyntaxString(args[4]);
                    PrintCorrectionMessage(
                        $"A command parameter you specified for the command \"{args[4]}\" is incorrect. The correct syntax for this command is:\n{correctCommandSyntaxString}");
                    return;
                }
            }

            #endregion

            #region Accepted Parameters

            Console.WriteLine($"Attempting to execute the following parameters: {string.Join(" ", args)}");

            var _ipAddress = args[0];
            var _port = args[1];
            var _username = args[2];
            var _password = args[3];
            var _command = args[4];
            var _commandParameters = args[5];

            PrintProgress(1);

            #endregion

            try
            {
                #region Connect to Remote Server

                try
                {
                    var endpointUrl = "opc.tcp://" + _ipAddress + ":" + _port + "/ViCellBlu/Server";

                    OpcAppConfig = new ApplicationConfiguration
                    {
                        ApplicationName = "ViCellOpcUaClient",
                        ApplicationUri = Utils.Format(@"urn:{0}:ViCellBLU:Server", Dns.GetHostName()),
                        ApplicationType = ApplicationType.Client,
                        SecurityConfiguration = new SecurityConfiguration(),
                        TransportConfigurations = new TransportConfigurationCollection(),
                        TransportQuotas = new TransportQuotas(),
                        ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 },
                        DisableHiResClock = true,
                        TraceConfiguration = new TraceConfiguration(),
                        CertificateValidator = new CertificateValidator()
                    };
                    OpcAppConfig.TransportQuotas = new TransportQuotas
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

                    SetOpcConfig();

                    OpcAppConfig.ClientConfiguration.MinSubscriptionLifetime = 10000;

                    OpcAppConfig.Validate(ApplicationType.Client).Wait();

                    var application = new ApplicationInstance
                    {
                        ApplicationType = ApplicationType.Client,
                        ApplicationName = "ViCellOpcUa Client",
                        ApplicationConfiguration = OpcAppConfig
                    };

                    var haveAppCertificateTask = application.CheckApplicationInstanceCertificate(false, 0);
                    var haveAppCertificate = haveAppCertificateTask.Result;
                    if (haveAppCertificate)
                        OpcAppConfig.ApplicationUri = X509Utils.GetApplicationUriFromCertificate(OpcAppConfig.SecurityConfiguration.ApplicationCertificate.Certificate);

                    var selectedEndpoint = CoreClientUtils.SelectEndpoint(OpcAppConfig, endpointUrl, haveAppCertificate, (int)_discoverTimeout);
                    var endpointConfiguration = EndpointConfiguration.Create(OpcAppConfig);
                    var endpoint = new ConfiguredEndpoint(null, selectedEndpoint, endpointConfiguration);

                    var user = new UserIdentity(_username, _password);

                    var sessionTask = Session.Create(OpcAppConfig, endpoint, true, false, "ViCellOpcUaClient", _cnxTimeout, user, null);
                    _opcSession = sessionTask.Result;

                    _namespaceUris = _opcSession.NamespaceUris;
                    _opcSession.KeepAlive += Client_KeepAlive;

                    var scoutXrefs = Browse(out _);
                    var viCellBlu = scoutXrefs.First(n => n.BrowseName.Name.Equals("ViCellBluStateObject"));
                    var viCellBluRefs = Browse(out _, viCellBlu.NodeId);

                    _browsedPlayCtrl = viCellBluRefs.First(n => n.BrowseName.Name.Equals("PlayControl"));
                    _playCtrlCollection = Browse(out _, _browsedPlayCtrl.NodeId);
                    _parentPlayNode = ExpandedNodeId.ToNodeId(_browsedPlayCtrl.NodeId, _opcSession.NamespaceUris);

                    _browsedMethods = viCellBluRefs.First(n => n.BrowseName.Name.Equals("Methods"));
                    _methodCollection = Browse(out _, _browsedMethods.NodeId);
                    _parentMethodNode = ExpandedNodeId.ToNodeId(_browsedMethods.NodeId, _opcSession.NamespaceUris);

                    PrintProgress(10);
                }
                catch (Exception ex)
                {
                    PrintCorrectionMessage($"Exception: Unable to connect to server : {ex.InnerException?.Message}");
                    return;
                }

                #endregion

                SetupEvents(MyMonitoredItemEventHandler);

                #region Acquire Lock

                try
                {
                    PrintProgress(15);

                    var methodRequestLock = _methodCollection.First(n => n.DisplayName.ToString().Equals("RequestLock"));
                    var methodNodeRequestLock = ExpandedNodeId.ToNodeId(methodRequestLock.NodeId, _opcSession.NamespaceUris);

                    var reqHeaderRequestLock = new RequestHeader();
                    var cmRequestRequestLock = new CallMethodRequest
                    {
                        ObjectId = _parentMethodNode, MethodId = methodNodeRequestLock
                    };
                    var cmReqCollectionRequestLock = new CallMethodRequestCollection {cmRequestRequestLock};
                    var respHdrRequestLock = _opcSession.Call(reqHeaderRequestLock, cmReqCollectionRequestLock, out var resultCollectionRequestLock, out var diagResultsRequestLock);

                    PrintProgress(20);
                }
                catch (Exception ex)
                {
                    PrintCorrectionMessage($"Exception attempting to Acquire Lock : {ex.InnerException?.Message}");
                    return;
                }

                #endregion

                #region Run Command

                switch (_command)
                {
                    case "GetCellTypes":
                    {
                        var callResult = new ViCellBlu.VcbResultGetCellTypes { ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning, MethodResult = ViCellBlu.MethodResultEnum.Failure };

                        var method = _methodCollection.First(n => n.DisplayName.ToString().Equals("GetCellTypes"));
                        var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);
                        var reqHeader = new RequestHeader();
                        var cmRequest = new CallMethodRequest {ObjectId = _parentMethodNode, MethodId = methodNode};
                        var cmReqCollection = new CallMethodRequestCollection {cmRequest};

                        var respHdr = _opcSession.Call(reqHeader, cmReqCollection, out var resultCollection, out var diagResults);

                        if (resultCollection.Count > 0 && resultCollection[0].OutputArguments.Count > 0)
                        {
                            callResult = DecodeRawCellTypesData(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                        }

                        if (callResult.MethodResult == ViCellBlu.MethodResultEnum.Success)
                        {
                            Console.WriteLine("\nGetCellTypes Success.\nResults:");
                            foreach (var cellType in callResult.CellTypes)
                            {
                                Console.WriteLine("CellType Name: " + cellType.CellTypeName);
                            }

                            Console.WriteLine('\n');
                        }
                        else
                        {
                            Console.WriteLine("GetCellTypes Failure.\n");
                        }

                        break;
                    }
                    case "StartSample":
                    {
                        // Available Default Cell Types:
                        // - BCI Default
                        // - Mammalian
                        // - Insect
                        // - Yeast
                        // - BCI Viab Beads
                        // - BCI L10 Beads

                        var sampleConfig = new ViCellBlu.SampleConfig
                        {
                            SampleName = "TestSampleName",
                            SamplePosition = {Column = 1, Row = "Y"}, // 96-Well-Plate (Columns 1-12, Rows A-H), A-CUP (Column 1, Row Y)
                            Tag = "SampleTag",
                            Dilution = 1,
                            CellType = new ViCellBlu.CellType {CellTypeName = "Insect"}, // You must choose between CellType OR QualityControl... if one is valid, the other must be string.Empty.
                            QualityControl = {QualityControlName = string.Empty}, // You must choose between CellType OR QualityControl... if one is valid, the other must be string.Empty.
                            SaveEveryNthImage = 1,
                            WashType = ViCellBlu.WashTypeEnum.NormalWash // Normal or Fast
                        };

                        var callResult = new ViCellBlu.VcbResult { ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning, MethodResult = ViCellBlu.MethodResultEnum.Failure };

                        var method = _playCtrlCollection.First(n => n.DisplayName.ToString().Equals("StartSample"));
                        var methodNodeStartSample = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                        var reqHeader = new RequestHeader();
                        var cmRequest = new CallMethodRequest
                        {
                            ObjectId = _parentPlayNode,
                            MethodId = methodNodeStartSample,
                            InputArguments = new VariantCollection {new Variant(sampleConfig)}
                        };
                        var cmReqCollection = new CallMethodRequestCollection { cmRequest };
                        var respHdr = _opcSession.Call(reqHeader, cmReqCollection, out var resultCollection, out var diagResults);

                        if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                        {
                            callResult = DecodeRaw(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                        }

                        Console.WriteLine(callResult.MethodResult == ViCellBlu.MethodResultEnum.Success
                            ? "StartSample Success.\n"
                            : "StartSample Failure.\n");

                        break;
                    }
                    case "GetSampleResults":
                    {
                        var callResult = new ViCellBlu.VcbResultGetSampleResults { ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning, MethodResult = ViCellBlu.MethodResultEnum.Failure };

                        var method = _methodCollection.First(n => n.DisplayName.ToString().Equals("GetSampleResults"));
                        var methodNodeGetSampleResults = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                        var results = new List<ViCellBlu.SampleResult>();

                        var reqHeader = new RequestHeader();
                        var cmRequest = new CallMethodRequest
                        {
                            ObjectId = _parentMethodNode,
                            MethodId = methodNodeGetSampleResults,
                            InputArguments = new VariantCollection
                            {
                                new Variant(string.Empty),                                  // User name string
                                new Variant(DateTime.Now.Subtract(TimeSpan.FromDays(14))),  // start date
                                new Variant(DateTime.Now),                                  // end date
                                new Variant(0),                                             // filter ON: 0 = sample set, 1 = sample
                                new Variant("Insect"),                                      // Cell type or QC name
                                new Variant(string.Empty),                                  // Search string (sample or sample set name)
                                new Variant(string.Empty)                                   // Search tag string
                            }
                        };

                        var cmReqCollection = new CallMethodRequestCollection { cmRequest };
                        var respHdr = _opcSession.Call(reqHeader, cmReqCollection, out var resultCollection, out var diagResults);

                        if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                        {
                            callResult = DecodeRawSampleResults(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                        }

                        if (callResult.MethodResult == ViCellBlu.MethodResultEnum.Success)
                        {
                            results = callResult.SampleResults;
                            Console.WriteLine("GetSampleResults Success.\nThe 'Sample Data Uuid' values below can be passed to the RetrieveSampleExport.\nSample Results:");
                            foreach (var result in results)
                            {
                                Console.WriteLine("\nAnalysis By: " + result.AnalysisBy + ", Date: " + result.AnalysisDateTime + ", Sample Data Uuid: " + result.SampleDataUuid);
                            }

                            Console.WriteLine('\n');
                        }
                        else
                        {
                            Console.WriteLine("GetSampleResults Failure.\n");
                        }

                        break;
                    }
                    case "RetrieveSampleExport":
                    {
                        var callResult = new ViCellBlu.VcbResultStartExport { ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning, MethodResult = ViCellBlu.MethodResultEnum.Failure, BulkDataId = string.Empty };

                        var method = _methodCollection.First(n => n.DisplayName.ToString().Equals("StartExport"));
                        var methodNodeRetrieveSampleExport = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                        var paramArray = _commandParameters.Split(',');
                        var folderPath = $"C:\\Instrument\\Export";
                        var fileName = $"C:\\Instrument\\Export\\{Guid.NewGuid()}.zip";
                        var uuids = paramArray.Select(t => new Uuid(t)).ToList();

                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        var reqHeader = new RequestHeader();
                        var cmRequest = new CallMethodRequest
                        {
                            ObjectId = _parentMethodNode,
                            MethodId = methodNodeRetrieveSampleExport,
                            InputArguments = new VariantCollection {new Variant(uuids)}
                        };
                        var cmReqCollection = new CallMethodRequestCollection { cmRequest };

                        Thread.Sleep(5000);

                        var respHdr = _opcSession.Call(reqHeader, cmReqCollection, out var resultCollection, out var diagResults);
                        if ((resultCollection.Count > 0) && (resultCollection[0].OutputArguments.Count > 0))
                        {
                            callResult = DecodeRawStartExport(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                        }

                        if (callResult.ErrorLevel == ViCellBlu.ErrorLevelEnum.NoError)
                        {
                            _exportWriter = new BinaryWriter(File.Open(fileName, FileMode.Create));

                            timeoutPeriod = DateTime.Now.AddMinutes(3);
                            while (DateTime.Now < timeoutPeriod && !receivedReady)
                            {
                                // Waiting for either 3 minutes to elapse, or for the export to complete
                            }
                        }

                        Console.WriteLine(callResult.MethodResult == ViCellBlu.MethodResultEnum.Success
                            ? "StartExport Success.\n"
                            : "StartExport Failure.\n");

                        break;
                    }
                }

                #endregion

                #region Release Lock

                PrintProgress(80);

                try
                {
                    var method = _methodCollection.First(n => n.DisplayName.ToString().Equals("ReleaseLock"));
                    var methodNode = ExpandedNodeId.ToNodeId(method.NodeId, _opcSession.NamespaceUris);

                    var reqHeaderReleaseLock = new RequestHeader();
                    var cmRequestReleaseLock = new CallMethodRequest { ObjectId = _parentMethodNode, MethodId = methodNode };
                    var cmReqCollectionReleaseLock = new CallMethodRequestCollection { cmRequestReleaseLock };

                    var respHdrReleaseLock = _opcSession.Call(reqHeaderReleaseLock, cmReqCollectionReleaseLock, out var resultCollectionReleaseLock, out var diagResultsReleaseLock);
                }
                catch (Exception ex)
                {
                    PrintCorrectionMessage($"Exception attempting to Release Lock : {ex.Message}");
                    return;
                }

                PrintProgress(85);

                #endregion

                #region Disconnect

                PrintProgress(90);

                try
                {
                    if (_opcSession != null)
                    {
                        _opcSession.Close();
                        _reconnectHandler?.Dispose();
                        _reconnectHandler = null;
                    }
                }
                catch (Exception e)
                {
                    PrintCorrectionMessage($"Exception attempting to Disconnect : {e.Message}");
                    return;
                }

                PrintProgress(95);

                #endregion
            }
            catch (Exception ex)
            {
                PrintCorrectionMessage($"General Exception: {ex.InnerException?.Message}");
            }

            PrintProgress(100);
            Console.WriteLine("Operation Completed. Press any key to continue.");
            Console.Read();
        }

        #region StartExport Helpers

        private static bool SetupEvents(MonitoredItemNotificationEventHandler handler)
        {
            try
            {
                var monitoredFields = new List<SimpleAttributeOperand>
                {
                    new SimpleAttributeOperand
                    {
                        TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType,
                        BrowsePath = new[] {new QualifiedName("ExportStatusInfo")}, // ExportStatusEvent
                        AttributeId = Attributes.Value
                    }
                };

                var subscription = new Subscription(_opcSession.DefaultSubscription)
                {
                    PublishingInterval = 1000,
                    DisplayName = "Vi-Cell Client Event Subscription",
                    PublishingEnabled = true
                };

                var monItem = new MonitoredItem(subscription.DefaultItem)
                {
                    NodeClass = NodeClass.Object,
                    StartNodeId = Opc.Ua.ObjectIds.Server,
                    AttributeId = Attributes.EventNotifier,
                    SamplingInterval = -1,
                    QueueSize = 0,
                    CacheQueueSize = 0,
                    Filter = new EventFilter { SelectClauses = monitoredFields.ToArray() }
                };

                monItem.Notification += handler;
                subscription.AddItem(monItem);
                _opcSession.AddSubscription(subscription);
                subscription.Create();

                return true;
            }
            catch  { }
            return false;

        }


        private static void MyMonitoredItemEventHandler(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {
            try
            {
                foreach (var value in monitoredItem.DequeueEvents())
                {
                    if (value?.EventFields?[0].Value == null) continue;

                    var data = DecodeExportStatus(value.EventFields[0].Value, _opcSession.MessageContext);
                    OnExportStatusUpdate.Invoke(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void UpdateExportStatusCB(ViCellBlu.ExportStatusData status)
        {
            CurrentExportStatus = status.ExportStatus;

            timeoutPeriod = DateTime.Now.AddMinutes(3);

            if (status.ExportStatus == ViCellBlu.ExportStatusEnum.Ready)
            {
                if (_exportWriter != null)
                {
                    uint idx = 0;
                    var res = new ViCellBlu.VcbResultRetrieveExportBlock();
                    try
                    {
                        do
                        {
                            res = RetrieveExportBlock(idx++, status.BulkDataId);
                            if (res.ErrorLevel == ViCellBlu.ErrorLevelEnum.NoError)
                            {
                                if ((res.BlockData.Status == ViCellBlu.GetBlockStateEnum.GetBlockDone) ||
                                    (res.BlockData.Status == ViCellBlu.GetBlockStateEnum.GetBlockSuccess))
                                {
                                    if ((res.BlockData.FileData == null) || (res.BlockData.FileData.Length == 0))
                                        break;
                                    _exportWriter.Write(res.BlockData.FileData);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                            Thread.Sleep(1);
                        } while (res.BlockData.Status != ViCellBlu.GetBlockStateEnum.GetBlockDone);
                    }
                    catch { }
                    _exportWriter.Close();
                    _exportWriter = null;
                }

                receivedReady = true;
            }
        }

        private static ViCellBlu.VcbResultRetrieveExportBlock RetrieveExportBlock(uint blockIndex, string bulkDataId)
        {
            var callResult = new ViCellBlu.VcbResultRetrieveExportBlock { ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning, MethodResult = ViCellBlu.MethodResultEnum.Failure };
            try
            {
                var method = _methodCollection.First(n => n.DisplayName.ToString().Equals("RetrieveExportDataBlock"));
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
                    callResult = DecodeRawRetrieveExportBlock(resultCollection[0].OutputArguments[0].Value, _opcSession.MessageContext);
                }
                return callResult;
            }
            catch (Exception e)
            {
                callResult.ResponseDescription = "Exception: " + e.Message;
            }
            callResult.ErrorLevel = ViCellBlu.ErrorLevelEnum.Error;
            callResult.MethodResult = ViCellBlu.MethodResultEnum.Failure;
            return callResult;
        }

        #endregion

        #region Decode Helpers

        public static ViCellBlu.VcbResult DecodeRaw(object rawResult, ServiceMessageContext messageContext)
        {
            var callResult = new ViCellBlu.VcbResult { ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning, MethodResult = ViCellBlu.MethodResultEnum.Failure };
            callResult.ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning;
            callResult.MethodResult = ViCellBlu.MethodResultEnum.Failure;
            callResult.ResponseDescription = "Decoding raw result ...";
            try
            {
                var val = (Opc.Ua.ExtensionObject)rawResult;
                var myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                callResult.ErrorLevel = ViCellBlu.ErrorLevelEnum.RequiresUserInteraction;
                callResult.MethodResult = ViCellBlu.MethodResultEnum.Failure;
                callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
            }
            return callResult;
        }

        public static ViCellBlu.VcbResultGetCellTypes DecodeRawCellTypesData(object rawResult, ServiceMessageContext messageContext)
        {
            var callResult = new ViCellBlu.VcbResultGetCellTypes { ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning, MethodResult = ViCellBlu.MethodResultEnum.Failure };
            callResult.ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning;
            callResult.MethodResult = ViCellBlu.MethodResultEnum.Failure;
            callResult.ResponseDescription = "Decoding raw result ...";
            try
            {
                var val = (Opc.Ua.ExtensionObject)rawResult;
                var myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                callResult.ErrorLevel = ViCellBlu.ErrorLevelEnum.RequiresUserInteraction;
                callResult.MethodResult = ViCellBlu.MethodResultEnum.Failure;
                callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
            }
            return callResult;
        }

        public static ViCellBlu.VcbResultGetSampleResults DecodeRawSampleResults(object rawResult, ServiceMessageContext messageContext)
        {
            var callResult = new ViCellBlu.VcbResultGetSampleResults { ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning, MethodResult = ViCellBlu.MethodResultEnum.Failure };
            callResult.ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning;
            callResult.MethodResult = ViCellBlu.MethodResultEnum.Failure;
            callResult.ResponseDescription = "Decoding raw result ...";
            try
            {
                var val = (Opc.Ua.ExtensionObject)rawResult;
                var myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                callResult.ErrorLevel = ViCellBlu.ErrorLevelEnum.RequiresUserInteraction;
                callResult.MethodResult = ViCellBlu.MethodResultEnum.Failure;
                callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
            }
            return callResult;
        }

        public static ViCellBlu.VcbResultStartExport DecodeRawStartExport(object rawResult, ServiceMessageContext messageContext)
        {
            var callResult = new ViCellBlu.VcbResultStartExport { ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning, MethodResult = ViCellBlu.MethodResultEnum.Failure };
            callResult.ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning;
            callResult.MethodResult = ViCellBlu.MethodResultEnum.Failure;
            callResult.ResponseDescription = "Decoding raw result ...";
            try
            {
                var val = (Opc.Ua.ExtensionObject)rawResult;
                var myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                callResult.ErrorLevel = ViCellBlu.ErrorLevelEnum.RequiresUserInteraction;
                callResult.MethodResult = ViCellBlu.MethodResultEnum.Failure;
                callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
            }
            return callResult;
        }

        public static ViCellBlu.VcbResultRetrieveExportBlock DecodeRawRetrieveExportBlock(object rawResult, ServiceMessageContext messageContext)
        {
            var callResult = new ViCellBlu.VcbResultRetrieveExportBlock { ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning, MethodResult = ViCellBlu.MethodResultEnum.Failure };
            callResult.ErrorLevel = ViCellBlu.ErrorLevelEnum.Warning;
            callResult.MethodResult = ViCellBlu.MethodResultEnum.Failure;
            callResult.ResponseDescription = "Decoding raw result ...";
            try
            {
                var val = (Opc.Ua.ExtensionObject)rawResult;
                var myData = (byte[])val.Body;
                callResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch (Exception ex)
            {
                callResult.ErrorLevel = ViCellBlu.ErrorLevelEnum.RequiresUserInteraction;
                callResult.MethodResult = ViCellBlu.MethodResultEnum.Failure;
                callResult.ResponseDescription = "DecodeRaw-Exception: " + ex.ToString();
            }
            return callResult;
        }

        public static ViCellBlu.ExportStatusData DecodeExportStatus(Object rawResult, ServiceMessageContext messageContext)
        {
            var outResult = new ViCellBlu.ExportStatusData();
            try
            {
                byte[] myData;
                var val = (Opc.Ua.ExtensionObject)rawResult;
                myData = (byte[])val.Body;
                outResult.Decode(new Opc.Ua.BinaryDecoder(myData, 0, myData.Count(), messageContext));
            }
            catch
            {
                outResult.BulkDataId = "";
            }
            return outResult;
        }
	#endregion

    }
}
