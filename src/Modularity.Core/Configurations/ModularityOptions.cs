using Modularity.Core.Abstractions;
using Modularity.Core.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IModuleLoader ModuleLoader { get; set; } = new FileModuleLoader();
    }
}
