using System;
using ViCellBlu;
using Opc.Ua;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    public interface ISampleProcessingManager
    {
        ServiceResult HandleStartRequest(NodeId sessionId, ref VcbResult methodResult, ref SampleConfig sampleToStart);
        ServiceResult HandleStartSetRequest(NodeId sessionId, ref VcbResult methodResult, ref SampleSet sampleSetToStart);
        ServiceResult HandleStopRequest(NodeId sessionId, ref VcbResult methodResult);
        ServiceResult HandlePauseRequest(NodeId sessionId, ref VcbResult methodResult);
        ServiceResult HandleResumeRequest(NodeId sessionId, ref VcbResult methodResult);
        ServiceResult HandleEjectStageRequest(NodeId sessionId, ref VcbResultEjectStage methodResult);
    }
}