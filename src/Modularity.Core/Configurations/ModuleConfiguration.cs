using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modularity.Core.Configurations
{
    public class ModuleConfiguration
    {
        public string Name { get; set; }
        public string[] Files { get; set; }
        public bool IsActive { get; set; }
        public bool LoadAllDependencies { get; set; }
        public string Version { get; set; }
    }
}
