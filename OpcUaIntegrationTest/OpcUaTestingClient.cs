using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;

namespace OpcUaIntegrationTest
{
    public class OpcUaTestingClient
    {
        const int ReconnectPeriod = 10;
        Session _session;
        SessionReconnectHandler _reconnectHandler;
        readonly string _endpointUrl;
        readonly int _clientRunTime;
        static bool _autoAccept = false;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private string _username;
        private string _password;

        private Opc.Ua.NamespaceTable _namespaceUris;

        public Subscription Subscription { get; set; }

        public OpcUaTestingClient(string endPointUrl, bool autoAccept, int stopTimeout)
        {
            _endpointUrl = endPointUrl;
            OpcUaTestingClient._autoAccept = autoAccept;
            _clientRunTime = stopTimeout * 1000;
        }

        public void ConnectToServer(string username, string password)
        {
            _username = username;
            _password = password;
            // We would normally want to catch exceptions, but this is a testing framework, so fail the test if the OPC/UA client is not initialized.
            Init();

            if (null == _session)
            {
                throw new Exception("OpcUaTestingClient.ConnectToServer(): _session is null after Init().");
            }

            // return error conditions
            if (_session.KeepAliveStopped)
            {
                ExitCode = ExitCode.ErrorNoKeepAlive;
                return;
            }

            ExitCode = ExitCode.Ok;
        }

        public static ExitCode ExitCode { get; private set; }

        private void Init()
        {
            ExitCode = ExitCode.ErrorCreateApplication;

            // Load the application configuration.
            ApplicationInstance application = new ApplicationInstance
            {
                ApplicationType = ApplicationType.Client,
                ApplicationName = "Opc.Ua.ScoutX.TestClient"
            };

            var testDirectory = TestContext.CurrentContext.TestDirectory;
            var clientConfigFile = Path.Combine(testDirectory, "ViCellBLU.Client.Config.xml");

            // Not sure why we need to delay here except to give the OPC/UA server a chance to fully complete its initialization.
            Thread.Sleep(4000);
            var configTask = application.LoadApplicationConfiguration(clientConfigFile, false);
            ApplicationConfiguration config = configTask.Result;

            var haveAppCertificateTask = CheckApplicationCertificate(application, config);
            var haveAppCertificate = haveAppCertificateTask.Result;

            ExitCode = ExitCode.ErrorDiscoverEndpoints;
            try
            {
                var selectedEndpoint = CoreClientUtils.SelectEndpoint(_endpointUrl, haveAppCertificate, 15000);
                ExitCode = ExitCode.ErrorCreateSession;
                var endpointConfiguration = EndpointConfiguration.Create(config);
                var endpoint = new ConfiguredEndpoint(null, selectedEndpoint, endpointConfiguration);

                SetupSession(config, endpoint, _username, _password);

                ExitCode = ExitCode.Ok;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SetupSubscriptions()
        {
            // 5 - Create a subscription with publishing interval of 1 second.
            ExitCode = ExitCode.ErrorCreateSubscription;
            Subscription = new Subscription(_session.DefaultSubscription) {PublishingInterval = 1000};

            // 6 - Add a list of items (server current time and status) to the subscription.
            ExitCode = ExitCode.ErrorMonitoredItem;
            var list = new List<MonitoredItem>
            {
                new MonitoredItem(Subscription.DefaultItem)
                {
                    DisplayName = "ServerStatusCurrentTime",
                    StartNodeId = "i=" + Opc.Ua.Variables.Server_ServerStatus_CurrentTime.ToString()
                }
            };
            list.ForEach(i => i.Notification += OnNotification);
            Subscription.AddItems(list);

            Console.WriteLine("7 - Add the subscription to the session.");
            ExitCode = ExitCode.ErrorAddSubscription;
            _session.AddSubscription(Subscription);
            Subscription.Create();
        }

        private void SetupSession(ApplicationConfiguration config, ConfiguredEndpoint endpoint, string username, string password)
        {
            var userToken = new UserNameIdentityToken
            {
                UserName = username,
                Password = Encoding.ASCII.GetBytes(password)
            };

            var sessionTask = Session.Create(config, endpoint, false, "OPC UA Console Client", 60000,
                new UserIdentity(userToken), null);

            _session = sessionTask.Result;
            Assert.NotNull(_session);
            _namespaceUris = _session.NamespaceUris;

            // register keep alive handler
            _session.KeepAlive += Client_KeepAlive;

            ExitCode = ExitCode.ErrorBrowseNamespace;

            var references = _session.FetchReferences(Opc.Ua.ObjectIds.ObjectsFolder);

            DumpNodes();
        }

        /// <summary>
        /// This is more a debugging aid to display the nodes retrieved from the server.
        /// </summary>
        public string DumpNodes()
        {
            var references = Browse(out var continuationPoint);
            var buf = new StringBuilder();
            buf.AppendLine(" DisplayName, BrowseName, NodeClass");
            foreach (var rd in references)
            {
                buf.AppendLine($" {rd.DisplayName}, {rd.BrowseName}, {rd.NodeClass}");
                var nextRefs = Browse(out var nextCp, ExpandedNodeId.ToNodeId(rd.NodeId, _session.NamespaceUris));

                foreach (var nextRd in nextRefs)
                {
                    buf.AppendLine("   + {nextRd.DisplayName}, {nextRd.BrowseName}, {nextRd.NodeClass}");
                }
            }

            return buf.ToString();
        }

        public ReferenceDescriptionCollection Browse(out byte[] continuationPoint, ExpandedNodeId expandedNodeId)
        {
            var nodeId = ExpandedNodeId.ToNodeId(expandedNodeId, _namespaceUris);
            return Browse(out continuationPoint, nodeId);
        }

        public ReferenceDescriptionCollection Browse(out byte[] continuationPoint, NodeId nodeId = null)
        {
            if (null == nodeId)
            {
                nodeId = Opc.Ua.ObjectIds.ObjectsFolder;
            }

            _session.Browse(
                null,
                null,
                nodeId,
                0u,
                BrowseDirection.Forward,
                ReferenceTypeIds.HierarchicalReferences,
                true,
                (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method,
                out continuationPoint,
                out var references);
            return references;
        }

        private static async Task<bool> CheckApplicationCertificate(ApplicationInstance application, ApplicationConfiguration config)
        {
            // check the application certificate.
            bool haveAppCertificate = await application.CheckApplicationInstanceCertificate(false, 0);
            if (!haveAppCertificate)
            {
                throw new Exception("Application instance certificate invalid!");
            }

            if (haveAppCertificate)
            {
                config.ApplicationUri =
                    X509Utils.GetApplicationUriFromCertificate(config.SecurityConfiguration.ApplicationCertificate.Certificate);
                if (config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
                {
                    _autoAccept = true;
                }

                config.CertificateValidator.CertificateValidation +=
                    new CertificateValidationEventHandler(CertificateValidator_CertificateValidation);
            }
            else
            {
                Console.WriteLine("    WARN: missing application certificate, using unsecure connection.");
            }

            return haveAppCertificate;
        }

        private void Client_KeepAlive(Session sender, KeepAliveEventArgs e)
        {
            if (e.Status != null && ServiceResult.IsNotGood(e.Status))
            {
                Console.WriteLine("{0} {1}/{2}", e.Status, sender.OutstandingRequestCount, sender.DefunctRequestCount);

                if (_reconnectHandler == null)
                {
                    Console.WriteLine("--- RECONNECTING ---");
                    _reconnectHandler = new SessionReconnectHandler();
                    _reconnectHandler.BeginReconnect(sender, ReconnectPeriod * 1000, Client_ReconnectComplete);
                }
            }
        }

        private void Client_ReconnectComplete(object sender, EventArgs e)
        {
            // ignore callbacks from discarded objects.
            if (!Object.ReferenceEquals(sender, _reconnectHandler))
            {
                return;
            }

            _session = _reconnectHandler.Session;
            _reconnectHandler.Dispose();
            _reconnectHandler = null;

            Console.WriteLine("--- RECONNECTED ---");
        }

        private static void OnNotification(MonitoredItem item, MonitoredItemNotificationEventArgs e)
        {
            foreach (var value in item.DequeueValues())
            {
                Console.WriteLine("{0}: {1}, {2}, {3}", item.DisplayName, value.Value, value.SourceTimestamp, value.StatusCode);
            }
        }

        private static void CertificateValidator_CertificateValidation(CertificateValidator validator, CertificateValidationEventArgs e)
        {
            if (e.Error.StatusCode == StatusCodes.BadCertificateUntrusted)
            {
                e.Accept = _autoAccept;
                if (_autoAccept)
                {
                    Console.WriteLine("Accepted Certificate: {0}", e.Certificate.Subject);
                }
                else
                {
                    Console.WriteLine("Rejected Certificate: {0}", e.Certificate.Subject);
                }
            }
        }

    }
}