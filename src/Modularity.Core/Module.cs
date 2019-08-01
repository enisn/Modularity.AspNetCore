using Modularity.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modularity.Core
{
    public class Module : IModule
    {
        public string Name { get; set; }
        public string AssemblyName { get; set; }
        public Assembly Assembly { get; set; }
        public IList<Assembly> Assemblies { get; set; }
        public Exception Exception { get; set; }
        public IList<Exception> Exceptions { get; set; } = new List<Exception>();
        public IList<IEntryObject> EntryObjects { get; set; }
    }
}
