using AutoMapper;
using GrpcClient;
using GrpcClient.Interfaces;
using Ninject.Extensions.Logging;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign.Events
{
    /// <summary>
    /// Builds on the ScoutX's gRPC RegisteredEvent (which is independent of OPC and thus the licensing encumbrances),
    /// introducing binding support to the OPC tree of nodes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OpcRegisteredEvent<T> : RegisteredEvent<T>
    {
        protected INodeService NodeService { get; }

        /// <summary>
        /// Provides access to the AutoMapper for conversion of gRPC types to OPC/UA types.
        /// </summary>
        protected IMapper Mapper { get; }

        protected NodeState NodeState { get; private set; }

        protected OpcRegisteredEvent(ILogger logger, IMapper mapper, IGrpcClient client, INodeService nodeService, NodeState nodeState) : base(logger, client)
        {
            Mapper = mapper;
            NodeService = nodeService;
            NodeState = nodeState;
        }
    }
}