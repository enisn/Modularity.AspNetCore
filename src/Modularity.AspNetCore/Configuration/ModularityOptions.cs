using Modularity.AspNetCore.Implementations;
using Modularity.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modularity.AspNetCore.Configuration
{
    public class ModularityOptions
    {
        internal static ModularityOptions Current { get; set; } = new ModularityOptions();

        /// <summary>
        /// Folder to load DLL files. Default is "Plugins"
        /// </summary>
        public string Folder { get; set; } = "Plugins";

        public IModuleLoader ModuleLoader { get; set; } = new FileModuleLoader();
    }
}
