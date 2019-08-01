using Modularity.Core.Abstractions;
using Modularity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Modularity.Core.Loaders
{
    public class FileModuleLoader : IModuleLoader
    {
        private IEnumerable<IModule> EnumerateAllAssembliesInFolder(string directory)
        {
            foreach (var file in Directory.GetFiles(directory, "*.dll"))
            {
                Module module;
                try
                {
                    var bytes = File.ReadAllBytes(file);
                    var assembly = Assembly.Load(bytes);
                    var entryObjectType = assembly.GetTypes().FirstOrDefault(x => typeof(IEntryObject).IsAssignableFrom(x));

                    module = new Module
                    {
                        Assembly = assembly,
                        AssemblyName = assembly.FullName,
                        EntryObject = (IEntryObject)Activator.CreateInstance(entryObjectType),
                        Exception = null,
                        Name = assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title
                    };
                }
                catch (Exception ex)
                {
                    module = new Module
                    {
                        Exception = ex,
                    };
                }
                yield return module;
            }
        }

        private IEnumerable<IModule> EnumerateAssembliesByConfig(string directory, string configFileName)
        {
            var parser = new ConfigurationParser();

            var configs = parser.Parse(Path.Combine(directory, configFileName));

            foreach (var config in configs.Where(x => x.IsActive))
            {
                Module module = new Module
                {
                    Assemblies = new List<Assembly>(),
                    Name = config.Name,
                    Exception = null,
                    EntryObjects = new List<IEntryObject>()
                };

                try
                {
                    if (config.Files != null && config.Files.Length > 0)
                        foreach (var file in config.Files)
                            try
                            {
                                using (var ms = new MemoryStream(File.ReadAllBytes(file)))
                                    AssemblyLoadContext.Default.LoadFromStream(ms);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.ToString());
                                module.Exceptions?.Add(ex);
                            }

                    var mainAssemblyName = Path.Combine(directory, config.Name, config.Name + ".dll");

                    foreach (var file in Directory.GetFiles(Path.Combine(directory, config.Name), "*.dll"))
                    {
                        var isMainAssembly = file == mainAssemblyName;
                        if (!config.LoadAllDependencies && !isMainAssembly)
                            continue;

                        var bytes = File.ReadAllBytes(file);

                        if (isMainAssembly)
                        {
                            var assembly = Assembly.Load(bytes);
                            module.Assembly = assembly;
                            module.AssemblyName = assembly.FullName;
                            module.EntryObjects = assembly.GetTypes().Where(x => typeof(IEntryObject).IsAssignableFrom(x)).Select(s => (IEntryObject)Activator.CreateInstance(s)).ToList();
                            module.EntryObject = module.EntryObjects?.FirstOrDefault();
                            module.Name = config.Name;
                        }
                        else
                        {
                            using (var ms = new MemoryStream(bytes))
                            {
                                var assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                                module.Assemblies.Add(assembly);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    module = new Module
                    {
                        Name = config.Name,
                        Exception = ex,
                    };
                }
                yield return module;
            }
        }

        public IList<IModule> Load(params object[] parameters)
        {
            string directory = parameters?.FirstOrDefault()?.ToString();
            if (string.IsNullOrEmpty(directory))
                throw new ArgumentNullException(nameof(directory));

            var configFileName = parameters.Length > 1 ? parameters.GetValue(1)?.ToString() : null;

            if (string.IsNullOrEmpty(configFileName))
                return EnumerateAllAssembliesInFolder(directory).ToList();
            else
                return EnumerateAssembliesByConfig(directory, configFileName).ToList();
        }
    }
}
