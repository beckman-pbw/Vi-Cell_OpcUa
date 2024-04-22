using System;
using System.Collections.Concurrent;
using GrpcClient;
using GrpcClient.Interfaces;
using Opc.Ua;
using Opc.Ua.Server;
using ViCellBluOpcUaModelDesign.Events;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign.ViCellBluManagement
{
    /// <summary>
    /// Application wrapper for a user to link OPC/UA objects with the gRPC client and resources.
    /// </summary>
    public class BecOpcUaUser : IDisposable
    {
        private readonly NodeEventFactory _nodeEventFactory;
        private readonly ConcurrentDictionary<NodeId, IRegisteredEvent> _registeredEvents = new ConcurrentDictionary<NodeId, IRegisteredEvent>();
        private readonly Session _session;
        private string _username;
        private string _password;
        public OpcUaGrpcClient GrpcClient { get; }

        public BecOpcUaUser(IOpcUaFactory opcUaFactory, NodeEventFactory nodeEventFactory, Session session, UserNameIdentityToken userNameToken)
        {
            _nodeEventFactory = nodeEventFactory;
            _session = session;
            SetNewIdentity(userNameToken);
            GrpcClient = opcUaFactory.CreateGrpcClient();
            GrpcClient.Init(_username, _password);
        }

        public NodeId SessionId => _session.Id;
        public string Username => _username;
        public string Password => _password;

        /// <summary>
        /// Validate and set the username and password for a username token.
        /// </summary>
        public void SetNewIdentity(UserNameIdentityToken userNameToken)
        {
            var userName = userNameToken.UserName;
            var password = userNameToken.DecryptedPassword;

            if (string.IsNullOrEmpty(userName))
            {
                // an empty username is not accepted.
                throw ServiceResultException.Create(StatusCodes.BadIdentityTokenInvalid,
                    "Security token is not a valid username token. An empty username is not accepted.");
            }

            if (string.IsNullOrEmpty(password))
            {
                // an empty password is not accepted.
                throw ServiceResultException.Create(StatusCodes.BadIdentityTokenRejected,
                    "Security token is not a valid username token. An empty password is not accepted.");
            }

            _username = userName;
            _password = password;
            Console.WriteLine($"BecOpcUaUser.SetNewIdentity(): Username {_username}");
        }

        /// <summary>
        /// An OPC/UA client requested monitoring a new (event). This method is called by CreateMonitorItem()
        /// and utilizes the gRPC client to open a gRPC stream to the ScoutX gRPC server to receive events.
        /// The event gRPC message objects are read off the stream and then used to update the node in the
        /// node tree. The OPC framework will then send the notifications to the OPC/UA client based on the
        /// requested interval.
        /// </summary>
        /// <param name="nodeState"></param>
        public void RegisterForEvent(NodeState nodeState)
        {
            var registeredEvent = _nodeEventFactory.CreateRegisteredEvent(GrpcClient, nodeState);
            registeredEvent.Register();
            _registeredEvents[nodeState.NodeId] = registeredEvent;
        }

        public void Dispose()
        {
            foreach (var registeredEvent in _registeredEvents.Values)
            {
                registeredEvent.Dispose();
            }
            GrpcClient.Dispose();
        }
    }
}