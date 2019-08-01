using Modularity.AspNetCore.Abstractions;
using Modularity.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        .Where(x => x is IModuleStartup)
                        .Select(s => s.EntryObject as IModuleStartup)
                        .ToList();
                }
                return moduleStartups;
            }
        }
    }
}
