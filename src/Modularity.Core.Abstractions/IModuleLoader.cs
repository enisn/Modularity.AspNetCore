using System.Collections.Generic;

namespace Modularity.Core.Abstractions
{
    public interface IModuleLoader
    {
        IList<IModule> Load(params object[] parameters);
    }
}
