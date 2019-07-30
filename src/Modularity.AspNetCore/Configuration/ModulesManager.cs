using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Modularity.AspNetCore.Configuration
{
    public static class ModulesManager
    {
        public static IMvcBuilder AddModularity(this IMvcBuilder builder, Action<ModularityOptions> action = null)
        {
            action?.Invoke(ModularityOptions.Current);

            Modules.LoadedAssemblies = ModularityOptions.Current.ModuleLoader.Load(ModularityOptions.Current);

            foreach (var assembly in Modules.LoadedAssemblies)
            {
                builder.AddApplicationPart(assembly);
            }
            return builder;
        }

        public static IServiceCollection AddModuleServices(this IServiceCollection services, IConfiguration configuration)
        {
            foreach (var startup in Modules.ModuleStartups)
            {
                startup.Initialize(configuration);
                startup.ConfigureServices(services);
            }
            return services;
        }

        public static IApplicationBuilder UseModulartiy(this IApplicationBuilder app, IHostingEnvironment env)
        {
            foreach (var startup in Modules.ModuleStartups)
            {
                startup.Configure(app, env);
            }
            return app;
        }
    }
}
