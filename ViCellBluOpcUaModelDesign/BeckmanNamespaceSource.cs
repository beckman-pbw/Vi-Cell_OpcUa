using Microsoft.Extensions.Configuration;

namespace ViCellBluOpcUaModelDesign
{
    public class BeckmanNamespaceSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new BeckmanNamespaceProvider();
        }
    }
}