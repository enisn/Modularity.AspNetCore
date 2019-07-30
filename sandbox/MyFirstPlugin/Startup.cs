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
            this.Configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
        }

        public void ConfigureServices(IServiceCollection services)
        {

        }
    }
}
