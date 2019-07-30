using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modularity.AspNetCore.Abstractions;
using System;

namespace MyFirstPlugin
{
    public class Startup : IModuleStartup
    {
        public IConfiguration Configuration { get; private set; }
        public void Initialize(IConfiguration configuration)
        {
            this.Configuration = configuration; // Get main application's configuration and keep it to use in ConfigureServices()
        }

        public void ConfigureServices(IServiceCollection services)
        {
            /* Do your services configurations */
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /* Do your application configurations  */
        }
    }
}
