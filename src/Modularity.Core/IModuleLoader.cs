using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modularity.Core
{
    public interface IModuleLoader
    {
        IList<Assembly> Load(params object[] parameters);
    }
}
