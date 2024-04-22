using Opc.Ua;
using ViCellBlu;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    public interface ICellTypeManager
    {
        ServiceResult HandleCreateCellTypeRequest(NodeId sessionId, ref VcbResultCreateCellType methodResult, ref CellType cellTypeData);
        ServiceResult HandleDeleteCellTypeRequest(NodeId sessionId, ref VcbResultDeleteCellType methodResult, ref string cellTypeData);
        ServiceResult HandleCreateQualityControlRequest(NodeId sessionId, ref VcbResult methodResult, ref QualityControl qc);
        ServiceResult HandleRequestGetCellTypes(NodeId sessionId, ref VcbResultGetCellTypes methodResult);
        ServiceResult HandleRequestGetQualityControls(NodeId sessionId, ref VcbResultGetQualityControls methodResult);
    }
}
