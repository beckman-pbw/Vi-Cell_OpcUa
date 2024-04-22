using Opc.Ua;
using ViCellBlu;

namespace ViCellBluOpcUaModelDesign.Interfaces
{
    public interface IConfigurationManager
    {
        ServiceResult HandleRequestExportConfig(NodeId sessionId, ref VcbResultExportConfig methodResult);
        ServiceResult HandleRequestImportConfig(NodeId sessionId, ref VcbResult methodResult, byte[] fileData);
        ServiceResult HandleRequestGetAvailableDiskSpace(NodeId sessionId, ref VcbResultGetDiskSpace methodResult);
    }
}
