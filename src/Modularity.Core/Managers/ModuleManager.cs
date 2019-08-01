using Modularity.Core.Abstractions;
using Modularity.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modularity.Core.Managers
{
    public class ModuleManager
    {
        public IList<IModule> Modules { get; private set; } = new List<IModule>();

        public IModuleLoader ModuleLoader { get; private set; }

        public virtual void LoadModules(ModularityOptions options = null)
        {
            if (options == null)
                options = new ModularityOptions();

            ModuleLoader = options.ModuleLoader;

            Modules = ModuleLoader.Load(options.Folder, options.ConfigurationFile);
        }

    }
}
