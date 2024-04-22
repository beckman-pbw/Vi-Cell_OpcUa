using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using GrpcService;
using Ninject.Extensions.Logging;
using Opc.Ua;
using Opc.Ua.Configuration;
using Opc.Ua.Server;
using ViCellBluOpcUaModelDesign.Enums;
using ViCellBluOpcUaModelDesign.Events;
using ViCellBluOpcUaModelDesign.Interfaces;
using ViCellBluOpcUaModelDesign.Services;
using ViCellBluOpcUaModelDesign.ViCellBluManagement;

namespace ViCellBluOpcUaModelDesign.OpcUa
{
    public class BecOpcServer : StandardServer
    {
        #region Constructor

        public BecOpcServer(ILogger logger, IOpcUaFactory opcUaFactory, BecOpcConfig config)
        {
            ServerStatus = ServerStatus.NotStarted;
            _logger = logger;
            _opcUaFactory = opcUaFactory;
            _becOpcConfig = config;
            Initialize();
        }

        private async void Initialize()
        {
            try
            {
                _appInstance = new ApplicationInstance();

                var appConfig = await _appInstance.LoadApplicationConfiguration(_becOpcConfig.ConfigFile, false);

                // check the application certificate.
                bool certOk = _appInstance.CheckApplicationInstanceCertificate(false, 0).Result;
                if (!certOk)
                {
                    throw new Exception("Application instance certificate invalid!");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to initialize _becServer object.", ex);
            }
        }

        #endregion

        #region Properties & Fields

        private ApplicationInstance _appInstance;
        private ICertificateValidator _certificateValidator;
        private readonly ILogger _logger;
        private readonly IOpcUaFactory _opcUaFactory;
        private readonly BecOpcConfig _becOpcConfig;
        private OpcEventManager _eventManager;
        private readonly ConcurrentDictionary<NodeId, BecOpcUaUser> _userLookup = new ConcurrentDictionary<NodeId, BecOpcUaUser>();
        public BecNodeManager BecNodeManager { get; private set; }
        public EventHandler<OpcServerStatusChangedEventArgs> ServerStatusChanged;

        private ServerStatus _serverStatus;
        public ServerStatus ServerStatus
        {
            get => _serverStatus;
            protected set
            {
                var changed = _serverStatus != value;
                _serverStatus = value;
                if (changed)
                {
                    ServerStatusChanged?.Invoke(this, new OpcServerStatusChangedEventArgs(value));
                }
            }
        }

        #endregion

        #region Public Methods

        public async Task StartServerAsync()
        {
            try
            {
                _logger.Info("Starting server ...");
                CheckForPredefinedNodes();
                await _appInstance.Start(this);
                ServerStatus = ServerStatus.Started;

                _eventManager = _opcUaFactory.CreateOpcEventManager(BecNodeManager);

                _logger.Info($"Server started! Status: {GetStatus()?.State}");
            }
            catch (Exception e)
            {
                var msg = "Error starting OPC UA server";
                Console.WriteLine(msg + Environment.NewLine + e.Message);
                _logger.Error(msg, e);
            }
        }

        public void StopServer()
        {
            try
            {
                _logger.Info("Stopping server...");
                _appInstance?.Stop();
                _eventManager.Dispose();
                ServerStatus = ServerStatus.NotStarted;
                _logger.Info("Server stopped!");
            }
            catch (Exception e)
            {
                var msg = "Error stopping OPC UA server";
                Console.WriteLine(msg + Environment.NewLine + e.Message);
                _logger.Error(msg, e);
            }
        }

        #endregion

        #region Overridden Methods

        /// <summary>
        /// Creates the node managers for the server.
        /// </summary>
        /// <remarks>
        /// This method allows the sub-class create any additional node managers which it uses. The SDK
        /// always creates a CoreNodeManager which handles the built-in nodes defined by the specification.
        /// Any additional NodeManagers are expected to handle application specific nodes.
        /// </remarks>
        protected override MasterNodeManager CreateMasterNodeManager(IServerInternal server, ApplicationConfiguration configuration)
        {
            // add the types defined in ModelDesign.xml to the factory (allows the method input args to be understood by the server)
            server.Factory.AddEncodeableTypes(_becOpcConfig.NodeAssembly);
            
            List<INodeManager> nodeManagers = new List<INodeManager>();

            // create the custom node managers.
            BecNodeManager = _opcUaFactory.CreateNodeManager(server, configuration);
            nodeManagers.Add(BecNodeManager);

            // create master node manager.
            return new MasterNodeManager(server, configuration, null, nodeManagers.ToArray());
        }

        /// <summary>
        /// Loads the non-configurable properties for the application.
        /// </summary>
        /// <remarks>
        /// These properties are exposed by the server but cannot be changed by administrators.
        /// </remarks>
        protected override ServerProperties LoadServerProperties()
        {
            ServerProperties properties = new ServerProperties();

            properties.ManufacturerName = _becOpcConfig.Manufacturer;
            properties.ProductName = _becOpcConfig.ProductName;
            properties.ProductUri = _becOpcConfig.ProductUrl;
            properties.SoftwareVersion = Utils.GetAssemblySoftwareVersion();
            properties.BuildNumber = Utils.GetAssemblyBuildNumber();
            properties.BuildDate = Utils.GetAssemblyTimestamp();

            // TBD - All applications have software certificates that need to added to the properties.

            return properties;
        }

        /// <summary>
        /// Creates the resource manager for the server.
        /// </summary>
        protected override ResourceManager CreateResourceManager(IServerInternal server, ApplicationConfiguration configuration)
        {
            ResourceManager resourceManager = new ResourceManager(server, configuration);

            FieldInfo[] fields = typeof(StatusCodes).GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo field in fields)
            {
                uint? id = field.GetValue(typeof(StatusCodes)) as uint?;

                if (id != null)
                {
                    resourceManager.Add(id.Value, "en-US", field.Name);
                }
            }

            return resourceManager;
        }

        /// <summary>
        /// Initializes the server before it starts up.
        /// </summary>
        /// <remarks>
        /// This method is called before any startup processing occurs. The sub-class may update the 
        /// configuration object or do any other application specific startup tasks.
        /// </remarks>
        protected override void OnServerStarting(ApplicationConfiguration configuration)
        {
            _logger.Info("Server is starting ..!");

            base.OnServerStarting(configuration);

            // it is up to the application to decide how to validate user identity tokens.
            // this function creates validator for X509 identity tokens.
            CreateUserIdentityValidators(configuration);
        }

        /// <summary>
        /// Called after the server has been started.
        /// </summary>
        protected override void OnServerStarted(IServerInternal server)
        {
            base.OnServerStarted(server);

            // request notifications when the user identity is changed. all valid users are accepted by default.
            server.SessionManager.ImpersonateUser += SessionManager_ImpersonateUser;
            server.SessionManager.SessionActivated += SessionManager_SessionActivated;
            server.SessionManager.SessionCreated += SessionManager_SessionCreated;
            server.SessionManager.SessionClosing += SessionManager_SessionClosing;
        }

        private void SessionManager_SessionClosing(Session session, SessionEventReason reason)
        {
            _logger.Info($"SessionManager_SessionClosing ...");

            var id = session.Identity;
            if (id == null)
            {
                _logger.Warn("SessionManager_SessionClosing:: session.Identity is NULL");
                return;
            }

            if (session.Id == null)
            {
                _logger.Error("SessionManager_SessionClosing:: session.Id is NULL");
                return;
            }

            if (_userLookup[session.Id] == null)
            {
                _logger.Error("SessionManager_SessionClosing:: _userLookup[session.Id] is NULL");
                return;
            }
            if (_userLookup[session.Id].GrpcClient == null)
            {
                _logger.Error("SessionManager_SessionClosing:: _userLookup[session.Id].GrpcClient is NULL");
                return;
            }


            _logger.Info($"SessionManager_SessionClosing - logout " + _userLookup[session.Id].Username);

            var requestLogoutUser = new RequestLogoutUser
            {
                Username = _userLookup[session.Id].Username,
            };

            _userLookup[session.Id].GrpcClient.LogoutRemoteUser(requestLogoutUser);

            _logger.Info($"SessionManager_SessionClosing - remove " + _userLookup[session.Id].Username);

            if (_userLookup.TryRemove(session.Id, out var user))
            {
                _logger.Debug($"SessionManager_SessionClosing:: user " + user.Username + " removed OK - dispose client");
                user.Dispose();
            }
            else
            {
                _logger.Warn($"SessionManager_SessionClosing:: failed to remove user");
            }

        }

        private void SessionManager_SessionCreated(Session session, SessionEventReason reason)
        {
            var id = session.Identity;
            _logger.Info($"SessionManager_SessionCreated ...");

            if (id == null)
            {
                _logger.Warn("SessionManager_SessionCreated:: session.Identity is NULL");
                return;
            }

            if (session.IdentityToken is UserNameIdentityToken userNameToken)
            {
                CreateNewUser(session, userNameToken);
            }
        }

        private void SessionManager_SessionActivated(Session session, SessionEventReason reason)
        {
            var id = session.Identity;
            _logger.Info($"SessionManager_SessionActivated ...");
            if (id == null)
            {
                _logger.Warn("SessionManager_SessionActivated:: session.Identity is NULL");
            }
        }

        #endregion

        #region Validation Methods

        private void CheckForPredefinedNodes()
        {
            // Test that we can find/access the ModelDesign.xml Predefined Nodes
            using (var stream = _becOpcConfig.NodeAssembly.GetManifestResourceStream(_becOpcConfig.PredefinedNodes))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"Unable to find binary node file '{_becOpcConfig.PredefinedNodes}'");
                }
            }
        }

        /// <summary>
        /// Creates the objects used to validate the user identity tokens supported by the server.
        /// </summary>
        private void CreateUserIdentityValidators(ApplicationConfiguration configuration)
        {
            for (int i = 0; i < configuration.ServerConfiguration.UserTokenPolicies.Count; i++)
            {
                var policy = configuration.ServerConfiguration.UserTokenPolicies[i];

                // create a validator for a certificate token policy.
                if (policy.TokenType == UserTokenType.Certificate)
                {
                    // check if user certificate trust lists are specified in configuration.
                    if (configuration.SecurityConfiguration.TrustedUserCertificates != null &&
                        configuration.SecurityConfiguration.UserIssuerCertificates != null)
                    {
                        var certificateValidator = new CertificateValidator();
                        certificateValidator.Update(configuration.SecurityConfiguration).Wait();
                        certificateValidator.Update(configuration.SecurityConfiguration.UserIssuerCertificates,
                            configuration.SecurityConfiguration.TrustedUserCertificates,
                            configuration.SecurityConfiguration.RejectedCertificateStore);

                        // set custom validator for user certificates.
                        _certificateValidator = certificateValidator.GetChannelValidator();
                    }
                }
            }
        }

        /// <summary>
        /// Called when a client tries to change its user identity.
        /// </summary>
        private void SessionManager_ImpersonateUser(Session session, ImpersonateEventArgs args)
        {
            // check for a user name token.
            _logger.Info($"SessionManager_ImpersonateUser");
            if (args.NewIdentity is UserNameIdentityToken userNameToken)
            {
                var currentUser = LookupUserBySession(session.Id);
                if (null == currentUser)
                {
                    CreateNewUser(session, userNameToken);
                }
                else
                {
                    _logger.Info($"SessionManager_ImpersonateUser - set user ID");
                    currentUser.SetNewIdentity(userNameToken);
                }

                return;
            }

            // check for x509 user token.
            if (args.NewIdentity is X509IdentityToken x509Token)
            {
                VerifyUserTokenCertificate(x509Token.Certificate);
                args.Identity = new UserIdentity(x509Token);
                _logger.Info($"SessionManager_ImpersonateUser:: X509 Token Accepted: {args.Identity.DisplayName}");
            }
        }

        private void CreateNewUser(Session session, UserNameIdentityToken userNameToken)
        {
            var newUser = _opcUaFactory.CreateOpcUaUser(session, userNameToken);
            var requestLoginUser = new RequestLoginUser 
            {
                Username = newUser.Username,
                Password = newUser.Password
            };

            if (MethodResultEnum.Success != newUser.GrpcClient.LoginRemoteUser(requestLoginUser).MethodResult)
            {
                // create an exception with a newUser.Username defined sub-code.
                throw new ServiceResultException(new ServiceResult(
                    StatusCodes.BadUserAccessDenied, "Invalid Username or Password"));
            }

            _eventManager.RegisterForScoutEvents(newUser.GrpcClient);
            _logger.Info("CreateNewUser: " + newUser.Username);
            _userLookup[session.Id] = newUser;
        }

        public BecOpcUaUser LookupUserBySession(NodeId sessionId)
        {
            return _userLookup.TryGetValue(sessionId, out var user) ? user : null;
        }

        /// <summary>
        /// Verifies that a certificate user token is trusted.
        /// </summary>
        private void VerifyUserTokenCertificate(X509Certificate2 certificate)
        {
	        _logger.Info("Validating server certificate");

			try
			{
                if (_certificateValidator != null)
                {
                    _certificateValidator.Validate(certificate);
                }
                else
                {
                    CertificateValidator.Validate(certificate);
                }
            }
            catch (Exception e)
            {
                TranslationInfo info;
                StatusCode result = StatusCodes.BadIdentityTokenRejected;
                ServiceResultException se = e as ServiceResultException;
                if (se != null && se.StatusCode == StatusCodes.BadCertificateUseNotAllowed)
                {
                    info = new TranslationInfo(
                        "InvalidCertificate",
                        "en-US",
                        "'{0}' is an invalid user certificate.",
                        certificate.Subject);

                    result = StatusCodes.BadIdentityTokenInvalid;
                }
                else
                {
                    // construct translation object with default text.
                    info = new TranslationInfo(
                        "UntrustedCertificate",
                        "en-US",
                        "'{0}' is not a trusted user certificate.",
                        certificate.Subject);
                }

                // create an exception with a vendor defined sub-code.
                throw new ServiceResultException(new ServiceResult(
                    result,
                    info.Key,
                    LoadServerProperties().ProductUri,
                    new LocalizedText(info)));
            }
        }

        #endregion
    }
}