using Modularity.Core.Abstractions;
using Modularity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                Module module;
                try
                {
                    module = new Module
                    {
                        Assemblies = new List<Assembly>(),
                        Name = config.Name,
                        Exception = null,
                        EntryObjects = new List<IEntryObject>()
                    };

                    var files = config.Files;
                    if (files == null || files.Length == 0)
                        files = Directory.GetFiles(Path.Combine(directory, config.Name), "*.dll");

                    foreach (var file in files)
                    {

                        var bytes = File.ReadAllBytes(file);
                        var assembly = Assembly.Load(bytes);

                        module.Assemblies.Add(assembly);

                        var entryObjectTypes = assembly.GetTypes().Where(x => x.IsClass && typeof(IEntryObject).IsAssignableFrom(x)).ToList();
                        if (entryObjectTypes != null && entryObjectTypes.Count > 0)
                        {
                            foreach (var entryobjectType in entryObjectTypes)
                                module.EntryObjects.Add((IEntryObject)Activator.CreateInstance(entryobjectType));

                            module.EntryObject = (IEntryObject)Activator.CreateInstance(entryObjectTypes.FirstOrDefault());
                            module.Assembly = assembly;
                            if (!config.LoadAllDependencies)
                                break;
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
