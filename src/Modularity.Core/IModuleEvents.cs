using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modularity.Core
{
    public interface IModuleEvents
    {
        void Subscribe<T>(T obj, string key, Action<object[]> action);

        void Publish(string key, params object[] parameters);
    }
}
