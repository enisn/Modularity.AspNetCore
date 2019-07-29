using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modularity.AspNetCore.Abstractions
{
    public interface IModuleStartup
    {
        /// <summary>
        /// This method will be called first when plugin was loaded successfully.
        /// </summary>
        /// <param name="configuration">Main application's configuration will be sent as parameter.</param>
        void Initialize(IConfiguration configuration);

        /// <summary>
        /// Main application's service provider will be sent as parameter. Just add your own services. Main application already has AddMvc() or other stuffs. If you user external libraries, you can initialize from here.
        /// </summary>
        /// <param name="services"></param>
        void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Finally, this method will be called from main application's Configure() method.
        /// </summary>
        /// <param name="app">ApplicationBuilder from Main application</param>
        /// <param name="env">HostingEnvirorment from main application</param>
        void Configure(IApplicationBuilder app, IHostingEnvironment env);
    }
}
