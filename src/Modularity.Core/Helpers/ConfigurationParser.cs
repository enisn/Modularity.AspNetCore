using Modularity.Core.Configurations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Modularity.Core.Helpers
{
    public class ConfigurationParser
    {
        public virtual IList<ModuleConfiguration> Parse(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IList<ModuleConfiguration>>(json);
        }
    }
}
