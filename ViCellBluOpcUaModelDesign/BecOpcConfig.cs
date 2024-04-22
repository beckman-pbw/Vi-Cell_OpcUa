using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Ninject.Extensions.Logging;
using ViCellBluOpcUaModelDesign.Interfaces;

namespace ViCellBluOpcUaModelDesign
{
    public class BecOpcConfig
    {
        private ILogger _logger;
        public string ConfigFile { get; set; }
        public string OpcNamespace { get; set; }
        public string PredefinedNodes { get; set; }
        public Assembly NodeAssembly { get; set; }
        public string Manufacturer { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public IAddBehaviorToPredefinedNodeService AddBehaviourToPredefinedNodeService { get; set; }

        public BecOpcConfig(ILogger logger, IConfiguration configuration,
            IAddBehaviorToPredefinedNodeService addBehaviourToPredefinedNodeService)
        {
            _logger = logger;
            // typeof(BeckmanUaNamespaceClass).Assembly
            // Validate config file exists
            _logger.Info("Loading OPC UA server config file...");
            ConfigFile = configuration.GetValue<string>("OpcUa.ConfigFile");
            if (!File.Exists(ConfigFile))
            {
                _logger.Error($"Unable to find OPC UA server config file ('{Path.GetFullPath(ConfigFile)}')");
                Environment.Exit(1);
            }

            var namespaceClassName = configuration.GetValue<string>("BecNamespaces.NamespaceClass");
            NodeAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.GetTypes().FirstOrDefault(t => namespaceClassName.Equals(t.FullName)) != null);
            if (null == NodeAssembly)
            {
                _logger.Error($"Unable find an assembly containing the namespace class {namespaceClassName}");
                Environment.Exit(1);
            }
            
            OpcNamespace = configuration.GetValue<string>("BecNamespaces.BecNamespace");
            PredefinedNodes = configuration.GetValue<string>("BecNamespaces.PredefinedNodes");
            Manufacturer = configuration.GetValue<string>("BecNamespaces.ManufacturerName");
            ProductName = configuration.GetValue<string>("BecNamespaces.ProductName");
            ProductUrl = configuration.GetValue<string>("BecNamespaces.ProductUri");

            AddBehaviourToPredefinedNodeService = addBehaviourToPredefinedNodeService;
        }
    }
}