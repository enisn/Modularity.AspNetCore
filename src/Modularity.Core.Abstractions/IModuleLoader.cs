using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modularity.Core.Abstractions
{
    public interface IModuleLoader
    {
        IList<IModule> Load(params object[] parameters);
    }
}
