using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using log4net;
using Ninject.Extensions.Logging;
using Opc.Ua;
using Opc.Ua.Server;
using ViCellBluOpcUaModelDesign;
using ViCellBluOpcUaModelDesign.Utilities;
using ViCellBluOpcUaModelDesign.ViCellBluManagement;
using DataTypeIds = Opc.Ua.DataTypeIds;
using DataTypes = Opc.Ua.DataTypes;
using ObjectIds = Opc.Ua.ObjectIds;
using ObjectTypeIds = Opc.Ua.ObjectTypeIds;
using VariableTypeIds = Opc.Ua.VariableTypeIds;

namespace ViCellBluOpcUaModelDesign.OpcUa
{
    public class BecNodeManager : CustomNodeManager2
    {
        #region Constructor

        public BecNodeManager(IServerInternal server, ApplicationConfiguration configuration, BecOpcServer opcUaServer, BecOpcConfig becOpcConfig, ILogger logger)
            : base(server, configuration, becOpcConfig.OpcNamespace)
        {
            SystemContext.NodeIdFactory = this;
            _opcUaServer = opcUaServer;
            OperationContext = server.DefaultSystemContext.OperationContext;
            _becOpcConfig = becOpcConfig;
            _logger = logger;
            ushort namespaceIndex = 0;
            foreach (var ns in server.NamespaceUris.ToArray())
            {
                // Zero based index
                _namespaceLookup[ns] = namespaceIndex++;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion
        
        #region Properties

        private readonly BecOpcServer _opcUaServer;
        private readonly BecOpcConfig _becOpcConfig;
        private readonly ILogger _logger;
        private readonly Dictionary<string, ushort> _namespaceLookup = new Dictionary<string, ushort>();
        private FolderState _rootFolderState;

        public OperationContext OperationContext { get; private set; }

        public FolderState RootFolderState => _rootFolderState;
        #endregion

        #region Update Variables

        /// <summary>
        /// Find a node given a node identifier (uint) within the Beckman namespace.
        /// </summary>
        /// <param name="nodeIdentifier">The uint id within the BeckmanUa namespace.</param>
        /// <returns>The nodeState for the matching node with the given id.</returns>
        public NodeState FindNode(uint nodeIdentifier)
        {
            lock (Lock)
            {
                var nodeId = new NodeId(nodeIdentifier, LookupNamespaceIndex(ViCellBlu.Namespaces.ViCellBlu));
                return null != PredefinedNodes && PredefinedNodes.TryGetValue(nodeId, out var nodeState) ? nodeState : null;
            }
        }

        public NodeState FindVariable(string browseName)
        {
            lock (Lock)
            {
                if (string.IsNullOrEmpty(browseName) || PredefinedNodes == null) return null;

                foreach (var node in PredefinedNodes)
                {
                    if (node.Value?.BrowseName == null || node.Value?.NodeId == null) continue;

                    if (node.Value.BrowseName.Name.Equals(browseName) ||
                        node.Value.NodeId.Identifier.Equals(browseName))
                    {
                        return node.Value;
                    }
                }
            }

            return null;
        }

        public void UpdateVariable(string browseName, object newValue)
        {
            var nodeState = FindVariable(browseName);
            UpdateVariable(nodeState, newValue);
        }

        public void UpdateVariable(NodeState nodeState, object newValue)
        {
            if (nodeState is BaseObjectState obj)
            {
                obj.ClearChangeMasks(SystemContext, true);
            }

            if (nodeState is BaseDataVariableState variable)
            {
                variable.Value = newValue;
                variable.Timestamp = DateTime.Now;
                variable.ClearChangeMasks(SystemContext, false); // this is what updates the OPC UA clients
            }

            if (nodeState is PropertyState property)
            {
                property.Value = newValue;
                property.Timestamp = DateTime.Now;
                property.ClearChangeMasks(SystemContext, false); // this is what updates the OPC UA clients
            }
        }

        #endregion

        #region Helper Methods

        public ushort LookupNamespaceIndex(string namespaceUri)
        {
            return _namespaceLookup.TryGetValue(namespaceUri, out var namespaceIndex) ? namespaceIndex : (ushort)0;
        }

        #endregion

        #region Override Methods

        protected override ServiceResult CreateMonitoredItem(ServerSystemContext context, NodeHandle handle,
            uint subscriptionId, double publishingInterval, DiagnosticsMasks diagnosticsMasks,
            TimestampsToReturn timestampsToReturn, MonitoredItemCreateRequest itemToCreate,
            ref long globalIdCounter, out MonitoringFilterResult filterResult, out IMonitoredItem monitoredItem)
        {
            _logger.Debug($"subscriptionId = {subscriptionId} for Node {handle.Node.BrowseName.Name}");
            filterResult = null;
            monitoredItem = null;

            // Create Subscription to gRPC layer
            var opcUser = _opcUaServer.LookupUserBySession(context.SessionId);
            opcUser?.RegisterForEvent(handle.Node);

            // try to create the monitored item
            return base.CreateMonitoredItem(context, handle, subscriptionId, publishingInterval, diagnosticsMasks, timestampsToReturn,
                itemToCreate, ref globalIdCounter, out filterResult, out monitoredItem);
        }

        /// <summary>
        /// Does any initialization required before the address space can be used.
        /// </summary>
        /// <remarks>
        /// The externalReferences is an out parameter that allows the node manager to link to nodes
        /// in other node managers. For example, the 'Objects' node is managed by the CoreNodeManager and
        /// should have a reference to the root folder node(s) exposed by this node manager.  
        /// </remarks>
        public override void CreateAddressSpace(IDictionary<NodeId, IList<IReference>> externalReferences)
        {
            lock (Server.DiagnosticsLock)
            {
                HistoryServerCapabilitiesState capabilities = Server.DiagnosticsNodeManager.GetDefaultHistoryCapabilities();
                capabilities.AccessHistoryDataCapability.Value = true;
                capabilities.InsertDataCapability.Value = true;
                capabilities.ReplaceDataCapability.Value = true;
                capabilities.UpdateDataCapability.Value = true;
                capabilities.DeleteRawCapability.Value = true;
                capabilities.DeleteAtTimeCapability.Value = true;
                capabilities.InsertAnnotationCapability.Value = true;
            }

            lock (Lock)
            {
                base.CreateAddressSpace(externalReferences); // will call LoadPredefinedNodes()

                if (!externalReferences.TryGetValue(ObjectIds.ObjectsFolder, out var references))
                {
                    externalReferences[ObjectIds.ObjectsFolder] = references = new List<IReference>();
                }

                try
                {
                    // You can add any nodes programatically here
                    CreateRootFolder(references);

                }
                catch (Exception e)
                {
                    _logger.Error("Error creating the address space.", e);
                }
            }
        }

        /// <summary>
        /// Loads a node set from a file or resource and adds them to the set of predefined nodes.
        /// </summary>
        protected override NodeStateCollection LoadPredefinedNodes(ISystemContext context)
        {
            using (var stream = _becOpcConfig.NodeAssembly.GetManifestResourceStream(_becOpcConfig.PredefinedNodes))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"Unable to find binary node file '{_becOpcConfig.PredefinedNodes}'");
                }

                // load the custom event types from the generated ModelDesign.xml code
                NodeStateCollection predefinedNodes = new NodeStateCollection();
                predefinedNodes.LoadFromBinary(context, stream, true);
                return predefinedNodes;
            }
        }

        /// <summary>
        /// Creates the NodeId for the specified node.
        /// </summary>
        public override NodeId New(ISystemContext context, NodeState node)
        {
            var instance = node as BaseInstanceState;

            if (instance?.Parent != null)
            {
                if (instance.Parent.NodeId.Identifier is string id)
                {
                    return new NodeId(id + instance.SymbolicName, instance.Parent.NodeId.NamespaceIndex);
                }
            }

            return node.NodeId;
        }

        /// <summary>
        /// Returns a unique handle for the node.
        /// </summary>
        protected override NodeHandle GetManagerHandle(ServerSystemContext context, NodeId nodeId, IDictionary<NodeId, NodeState> cache)
        {
            lock (Lock)
            {
                // quickly exclude nodes that are not in the namespace. 
                if (!IsNodeIdInNamespace(nodeId))
                {
                    return null;
                }

                if (!PredefinedNodes.TryGetValue(nodeId, out var node))
                {
                    return null;
                }

                var handle = new NodeHandle { NodeId = nodeId, Node = node, Validated = true };


                return handle;
            }
        }

        /// <summary>
        /// Verifies that the specified node exists.
        /// </summary>
        protected override NodeState ValidateNode(ServerSystemContext context, NodeHandle handle, IDictionary<NodeId, NodeState> cache)
        {
            // not valid if no root.
            if (handle == null)
            {
                return null;
            }

            // check if previously validated.
            if (handle.Validated)
            {
                return handle.Node;
            }

            // TBD

            return null;
        }

        protected override NodeState AddBehaviourToPredefinedNode(ISystemContext context, 
            NodeState predefinedNode)
        {
            if (!(predefinedNode is BaseObjectState passiveNode))
            {
                return predefinedNode;
            }

            var typeId = passiveNode.TypeDefinitionId;

            if (!IsNodeIdInNamespace(typeId) || typeId.IdType != IdType.Numeric)
            {
                return predefinedNode;
            }

            // Use the injected method to lookup the correct node's behavior
            return _becOpcConfig.AddBehaviourToPredefinedNodeService.GetActiveNode(context, predefinedNode);
        }

        #endregion

        /// <summary>
        /// Creates the root folder that will hold all OPC UA server elements.
        /// </summary>
        /// <param name="references">Essentially, an out variable to link this folder to all other OCP UA References</param>
        private void CreateRootFolder(IList<IReference> references)
        {
            _rootFolderState = OpcCreator.CreateFolder(null, BecNamespaces.RootFolderPath, BecNamespaces.RootFolderDisplayName,
                BecNamespaces.RootFolderDescription, NamespaceIndex);
            _rootFolderState.AddReference(ReferenceTypes.Organizes, true, ObjectIds.ObjectsFolder);
            references.Add(new NodeStateReference(ReferenceTypes.Organizes, false, _rootFolderState.NodeId));
            _rootFolderState.EventNotifier = EventNotifiers.SubscribeToEvents;
            AddRootNotifier(_rootFolderState);
        }

        #region Create Type State Objects

        private BaseObjectTypeState CreateObjectType(NodeState parent, IDictionary<NodeId, IList<IReference>> externalReferences, string path, string name)
        {
            var type = new BaseObjectTypeState
            {
                SymbolicName = name,
                SuperTypeId = ObjectTypeIds.BaseObjectType,
                NodeId = new NodeId(path, NamespaceIndex),
                BrowseName = new QualifiedName(name, NamespaceIndex)
            };

            type.DisplayName = type.BrowseName.Name;
            type.WriteMask = AttributeWriteMask.None;
            type.UserWriteMask = AttributeWriteMask.None;
            type.IsAbstract = false;

            if (!externalReferences.TryGetValue(ObjectTypeIds.BaseObjectType, out var references))
            {
                externalReferences[ObjectTypeIds.BaseObjectType] = references = new List<IReference>();
            }

            references.Add(new NodeStateReference(ReferenceTypes.HasSubtype, false, type.NodeId));

            if (parent != null)
            {
                parent.AddReference(ReferenceTypes.Organizes, false, type.NodeId);
                type.AddReference(ReferenceTypes.Organizes, true, parent.NodeId);
            }

            AddPredefinedNode(SystemContext, type);
            return type;
        }

        private BaseVariableTypeState CreateVariableType(NodeState parent, IDictionary<NodeId, IList<IReference>> externalReferences,
            string path, string name, Type dataType, int valueRank)
        {
            
            var type = new BaseDataVariableTypeState
            {
                SymbolicName = name,
                SuperTypeId = VariableTypeIds.BaseDataVariableType,
                NodeId = new NodeId(path, NamespaceIndex),
                BrowseName = new QualifiedName(name, NamespaceIndex)
            };
            
            type.DisplayName = type.BrowseName.Name;
            type.WriteMask = AttributeWriteMask.None;
            type.UserWriteMask = AttributeWriteMask.None;
            type.IsAbstract = false;
            type.DataType = DataTypes.GetDataTypeId(dataType);
            type.ValueRank = valueRank;
            type.Value = null;

            IList<IReference> references = null;

            if (!externalReferences.TryGetValue(VariableTypeIds.BaseDataVariableType, out references))
            {
                externalReferences[VariableTypeIds.BaseDataVariableType] = references = new List<IReference>();
            }

            references.Add(new NodeStateReference(ReferenceTypes.HasSubtype, false, type.NodeId));

            if (parent != null)
            {
                parent.AddReference(ReferenceTypes.Organizes, false, type.NodeId);
                type.AddReference(ReferenceTypes.Organizes, true, parent.NodeId);
            }

            AddPredefinedNode(SystemContext, type);
            return type;
        }

        private DataTypeState CreateDataType(NodeState parent, IDictionary<NodeId, IList<IReference>> externalReferences, string path, string name)
        {
            var type = new DataTypeState
            {
                SymbolicName = name,
                SuperTypeId = DataTypeIds.Structure,
                NodeId = new NodeId(path, NamespaceIndex),
                BrowseName = new QualifiedName(name, NamespaceIndex)
            };

            type.DisplayName = type.BrowseName.Name;
            type.WriteMask = AttributeWriteMask.None;
            type.UserWriteMask = AttributeWriteMask.None;
            type.IsAbstract = false;

            IList<IReference> references = null;

            if (!externalReferences.TryGetValue(DataTypeIds.Structure, out references))
            {
                externalReferences[DataTypeIds.Structure] = references = new List<IReference>();
            }

            references.Add(new NodeStateReference(ReferenceTypeIds.HasSubtype, false, type.NodeId));

            if (parent != null)
            {
                parent.AddReference(ReferenceTypes.Organizes, false, type.NodeId);
                type.AddReference(ReferenceTypes.Organizes, true, parent.NodeId);
            }

            AddPredefinedNode(SystemContext, type);
            return type;
        }

        private ReferenceTypeState CreateReferenceType(NodeState parent, IDictionary<NodeId, IList<IReference>> externalReferences, string path, string name)
        {
            ReferenceTypeState type = new ReferenceTypeState();

            type.SymbolicName = name;
            type.SuperTypeId = ReferenceTypeIds.NonHierarchicalReferences;
            type.NodeId = new NodeId(path, NamespaceIndex);
            type.BrowseName = new QualifiedName(name, NamespaceIndex);
            type.DisplayName = type.BrowseName.Name;
            type.WriteMask = AttributeWriteMask.None;
            type.UserWriteMask = AttributeWriteMask.None;
            type.IsAbstract = false;
            type.Symmetric = true;
            type.InverseName = name;

            IList<IReference> references = null;

            if (!externalReferences.TryGetValue(ReferenceTypeIds.NonHierarchicalReferences, out references))
            {
                externalReferences[ReferenceTypeIds.NonHierarchicalReferences] = references = new List<IReference>();
            }

            references.Add(new NodeStateReference(ReferenceTypeIds.HasSubtype, false, type.NodeId));

            if (parent != null)
            {
                parent.AddReference(ReferenceTypes.Organizes, false, type.NodeId);
                type.AddReference(ReferenceTypes.Organizes, true, parent.NodeId);
            }

            AddPredefinedNode(SystemContext, type);
            return type;
        }

        private ViewState CreateView(NodeState parent, IDictionary<NodeId, IList<IReference>> externalReferences, string path, string name)
        {
            ViewState type = new ViewState();

            type.SymbolicName = name;
            type.NodeId = new NodeId(path, NamespaceIndex);
            type.BrowseName = new QualifiedName(name, NamespaceIndex);
            type.DisplayName = type.BrowseName.Name;
            type.WriteMask = AttributeWriteMask.None;
            type.UserWriteMask = AttributeWriteMask.None;
            type.ContainsNoLoops = true;

            IList<IReference> references = null;

            if (!externalReferences.TryGetValue(ObjectIds.ViewsFolder, out references))
            {
                externalReferences[ObjectIds.ViewsFolder] = references = new List<IReference>();
            }

            type.AddReference(ReferenceTypeIds.Organizes, true, ObjectIds.ViewsFolder);
            references.Add(new NodeStateReference(ReferenceTypeIds.Organizes, false, type.NodeId));

            if (parent != null)
            {
                parent.AddReference(ReferenceTypes.Organizes, false, type.NodeId);
                type.AddReference(ReferenceTypes.Organizes, true, parent.NodeId);
            }

            AddPredefinedNode(SystemContext, type);
            return type;
        }

        #endregion
    }
}