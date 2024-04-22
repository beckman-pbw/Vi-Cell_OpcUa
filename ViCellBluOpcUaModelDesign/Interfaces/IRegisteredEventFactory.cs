using GrpcClient.Interfaces;
using Opc.Ua;
using ViCellBluOpcUaModelDesign.Events;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    public interface IRegisteredEventFactory
    {
        LockStateRegisteredVariable CreateLockStateRegisteredVariable(IGrpcClient client, NodeState nodeState);
        ViCellStatusRegisteredVariable CreateViCellStatusRegisteredVariable(IGrpcClient client, NodeState nodeState);
        ViCellIdentifierRegisteredVariable CreateViCellIdentifierRegisteredVariable(IGrpcClient client, NodeState nodeState);
        SampleStatusChangedRegisteredEvent CreateSampleStatusChangedRegisteredEvent(IGrpcClient client, NodeState nodeState);
        SampleCompleteRegisteredEvent CreateSampleCompleteRegisteredEvent(IGrpcClient client, NodeState nodeState);
        WorkListCompleteRegisteredEvent CreateWorkListCompletedRegisteredEvent(IGrpcClient client, NodeState nodeState);
        ReagentUseRemainingRegisteredVariable CreateReagentUseRemainingRegisteredVariable(IGrpcClient client, NodeState nodeState);
        WasteTubeCapacityRegisteredVariable CreateWasteTubeCapacityRegisteredVariable(IGrpcClient client, NodeState nodeState);
        DeleteSampleResultsRegisteredEvent CreateDeleteSampleResultsCompletedRegisteredEvent(IGrpcClient client, NodeState nodeState);
        ExportStatusRegisteredEvent CreateExportStatusRegisteredEvent(IGrpcClient client, NodeState nodeState);
        CleanFluidicsStatusRegisteredEvent CreateCleanFluidicsStatusRegisteredEvent(IGrpcClient client, NodeState nodeState);
        PrimeReagentsStatusRegisteredEvent CreatePrimeReagentsStatusRegisteredEvent(IGrpcClient client, NodeState nodeState);
        PurgeReagentsStatusRegisteredEvent CreatePurgeReagentsStatusRegisteredEvent(IGrpcClient client, NodeState nodeState);
        DecontaminateStatusRegisteredEvent CreateDecontaminateStatusRegisteredEvent(IGrpcClient client, NodeState nodeState);
        SoftwareVersionRegisteredVariable CreateSoftwareVersionRegisteredVariable(IGrpcClient client, NodeState nodeState);
        FirmwareVersionRegisteredVariable CreateFirmwareVersionRegisteredVariable(IGrpcClient client, NodeState nodeState);
        ErrorStatusRegisteredEvent CreateErrorStatusRegisteredEvent(IGrpcClient client, NodeState nodeState);
    }
}