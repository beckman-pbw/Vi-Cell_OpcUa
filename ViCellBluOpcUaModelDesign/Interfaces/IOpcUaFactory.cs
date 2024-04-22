using ViCellBlu;
using GrpcClient;
using Opc.Ua;
using Opc.Ua.Server;
using ViCellBluOpcUaModelDesign.OpcUa;
using ViCellBluOpcUaModelDesign.Services;
using ViCellBluOpcUaModelDesign.ViCellBluManagement;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    public interface IOpcUaFactory
    {
        BecNodeManager CreateNodeManager(IServerInternal server, ApplicationConfiguration configuration);
        BecOpcUaUser CreateOpcUaUser(Session session, UserNameIdentityToken userNameToken);
        MethodFolderState CreateMethodFolderState(NodeState parent);
        PlayControlFolderState CreatePlayControlFolderState(NodeState parent);
        OpcUaGrpcClient CreateGrpcClient();
        OpcEventManager CreateOpcEventManager(BecNodeManager nodeManager);
    }
}