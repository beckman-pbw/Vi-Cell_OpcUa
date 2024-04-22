using System;
using Opc.Ua;
using ViCellBlu;
using CellHealthFluidTypeEnum = ViCellBlu.CellHealthFluidTypeEnum;
using VcbResult = ViCellBlu.VcbResult;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    public interface IReagentsManager
    {
        ServiceResult HandleRequestCleanFluidics(NodeId sessionId, ref VcbResult methodResult);
        ServiceResult HandleRequestGetReagentVolume(NodeId sessionId, CellHealthFluidTypeEnum type, ref VcbResultReagentVolume methodResult);
        ServiceResult HandleRequestSetReagentVolume(NodeId sessionId, CellHealthFluidTypeEnum type, Int32 volume, ref VcbResult methodResult);
        ServiceResult HandleRequestAddReagentVolume(NodeId sessionId, CellHealthFluidTypeEnum type, Int32 volume, ref VcbResult methodResult);
        ServiceResult HandleRequestPrimeReagents(NodeId sessionId, ref VcbResult methodResult);
		ServiceResult HandleRequestCancelPrimeReagents(NodeId sessionId, ref VcbResult methodResult);
		ServiceResult HandleRequestPurgeReagents(NodeId sessionId, ref VcbResult methodResult);
		ServiceResult HandleRequestCancelPurgeReagents(NodeId sessionId, ref VcbResult methodResult);
		ServiceResult HandleRequestDecontaminate(NodeId sessionId, ref VcbResult methodResult);
		ServiceResult HandleRequestCancelDecontaminate(NodeId sessionId, ref VcbResult methodResult);
    }
}
