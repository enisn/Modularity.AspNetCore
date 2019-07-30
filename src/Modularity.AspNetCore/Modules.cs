using Modularity.AspNetCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Modularity.AspNetCore
{
    public static class Modules
    {
        private static IList<IModuleStartup> moduleStartups;

        public static IList<Assembly> LoadedAssemblies { get; set; }

        public static IList<Type> AllModuleStartupTypes => LoadedAssemblies.SelectMany(s => s.GetTypes().Where(x => typeof(IModuleStartup).IsAssignableFrom(x))).ToList();

        public static IList<IModuleStartup> ModuleStartups
        {
            get
            {
                if (moduleStartups == null)
                    moduleStartups = AllModuleStartupTypes.Select(s => Activator.CreateInstance(s) as IModuleStartup).ToList();

                return moduleStartups;
            }
            set => moduleStartups = value;
        }
    }
}
