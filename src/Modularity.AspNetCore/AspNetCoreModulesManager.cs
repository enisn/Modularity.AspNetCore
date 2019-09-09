using Modularity.AspNetCore.Abstractions;
using Modularity.Core.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Modularity.AspNetCore
{
    public class AspNetCoreModulesManager : ModuleManager
    {
        public static AspNetCoreModulesManager Current { get; } = new AspNetCoreModulesManager();

        private IList<IModuleStartup> moduleStartups;

        public IList<IModuleStartup> ModuleStartups
        {
            get
            {
                if (moduleStartups == null)
                {
                    moduleStartups = Modules
                        .SelectMany(s => s.EntryObjects.Select(s2 => s2 as IModuleStartup).Where(x => x != null))
                        .ToList();
                }
                return moduleStartups;
            }
        }
    }
}
