using Modularity.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modularity.AspNetCore.Implementations
{
    public class FileModuleLoader : IModuleLoader
    {
        private IEnumerable<Assembly> EnumerateAssemblies(params object[] parameters)
        {
            string path = parameters?.FirstOrDefault()?.ToString();
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            foreach (var file in Directory.GetFiles(path, "*.dll"))
            {
                var bytes = File.ReadAllBytes(file);
                yield return Assembly.Load(bytes);
            }
        }

        public IList<Assembly> Load(params object[] parameters)
        {
            return EnumerateAssemblies(parameters).ToList();
        }
    }
}
