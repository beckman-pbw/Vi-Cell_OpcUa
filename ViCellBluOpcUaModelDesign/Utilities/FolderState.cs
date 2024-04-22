using Opc.Ua;

namespace ViCellBluOpcUaModelDesign.Utilities
{
    public static partial class OpcCreator
    {
        public static FolderState CreateFolder(NodeState parent, string path, string name, string description,
            ushort namespaceIndex, string locale = "en-US")
        {
            var folder = new FolderState(parent)
            {
                SymbolicName = name,
                ReferenceTypeId = ReferenceTypes.Organizes,
                TypeDefinitionId = ObjectTypeIds.FolderType,
                NodeId = new NodeId(path, namespaceIndex),
                BrowseName = new QualifiedName(path, namespaceIndex),
                DisplayName = new LocalizedText(locale, name),
                WriteMask = AttributeWriteMask.None,
                UserWriteMask = AttributeWriteMask.None,
                EventNotifier = EventNotifiers.None
            };

            if (!string.IsNullOrEmpty(description))
            {
                folder.Description = new LocalizedText(locale, description);
            }

            parent?.AddChild(folder);

            return folder;
        }
    }
}
