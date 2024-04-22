using Microsoft.Extensions.Configuration;

namespace ViCellBluOpcUaModelDesign
{
    /// <summary>
    /// Provides a way for values defined in the BecNamespaces class to become part
    /// of IConfiguration
    /// </summary>
    public class BeckmanNamespaceProvider : ConfigurationProvider
    {
        public override void Load()
        {
            Set("BecNamespaces.BecNamespace", BecNamespaces.BecNamespace);
            Set("BecNamespaces.PredefinedNodes", BecNamespaces.PredefinedNodes);
            Set("BecNamespaces.NamespaceClass", BecNamespaces.NamespaceClass);
            Set("BecNamespaces.ManufacturerName", BecNamespaces.ManufacturerName);
            Set("BecNamespaces.ProductName", BecNamespaces.ProductName);
            Set("BecNamespaces.ProductUri", BecNamespaces.ProductUri);
            Set("OpcUa.ConfigFile", ".\\ViCellBLU.Server.Config.xml");
        }
    }
}