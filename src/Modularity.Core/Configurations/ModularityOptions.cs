using Modularity.Core.Abstractions;
using Modularity.Core.Loaders;
using System;

namespace Modularity.Core.Configurations
{
    public class ModularityOptions
    {
        /// <summary>
        /// Folder to load DLL files. Default is "Plugins"
        /// </summary>
        public string Folder { get; set; } = "Plugins";

        /// <summary>
        /// Default is "plugins.json". If you leave this empty, all DLL files will be loaded to application.
        /// </summary>
        public string ConfigurationFile { get; set; } = "plugins.json";

        [Obsolete]
        public bool IgnoreExceptions { get; set; } = true;

        public IModuleLoader ModuleLoader { get; set; } = new FileModuleLoader();
    }
}
