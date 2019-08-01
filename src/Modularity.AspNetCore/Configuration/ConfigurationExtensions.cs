using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modularity.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Modularity.AspNetCore.Configuration
{
    public static class ConfigurationExtensions
    {
        internal static ModularityOptions ModularityOptions { get; set; } = new ModularityOptions();
        public static IMvcBuilder AddModularity(this IMvcBuilder builder, Action<ModularityOptions> action = null)
        {
            action?.Invoke(ModularityOptions);

            AspNetCoreModulesManager.Current.LoadModules(ModularityOptions);

            foreach (var module in AspNetCoreModulesManager.Current.Modules)            
                if (module.Assembly != null)
                    builder.AddApplicationPart(module.Assembly);
            
            return builder;
        }

        public static IServiceCollection AddModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            foreach (var startup in AspNetCoreModulesManager.Current.ModuleStartups)
            {
                startup.Initialize(configuration);
                startup.ConfigureServices(services);
            }
            return services;
        }

        public static IApplicationBuilder UseModulartiy(this IApplicationBuilder app, IHostingEnvironment env)
        {
            foreach (var startup in AspNetCoreModulesManager.Current.ModuleStartups)
            {
                startup.Configure(app, env);
            }
            return app;
        }
    }
}
