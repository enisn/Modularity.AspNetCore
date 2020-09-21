# Modularity.AspNetCore

This library allows to import your class libraries with controllers to your main project. So you can easily use plug-in & plug-out your features.

## Releases
[![Nuget](https://img.shields.io/nuget/v/Modularity.AspNetCore?label=Modularity.AspNetCore&logo=nuget)](https://www.nuget.org/packages/Modularity.AspNetCore/)

[![Nuget](https://img.shields.io/nuget/v/Modularity.Core?label=Modularity.Core&logo=nuget)](https://www.nuget.org/packages/Modularity.Core/)

## Badges
[![CodeFactor](https://www.codefactor.io/repository/github/enisn/modularity.aspnetcore/badge)](https://www.codefactor.io/repository/github/enisn/modularity.aspnetcore) 
[![Build status](https://ci.appveyor.com/api/projects/status/gvp97y2krx4ea3rm?svg=true)](https://ci.appveyor.com/project/enisn/modularity-aspnetcore)


***

![Modularity AspNetCore_v2](https://user-images.githubusercontent.com/23705418/62229283-062db280-b3c8-11e9-8be0-6cd3ba9c9c91.png)

***
# Getting Started
Firstly you'll need a main project and at least one module project. Let's start with creating them



## Creating Main Project

- Create an **AspNetCore** project as main web project.

- Add [Modularity.AspNetCore](https://www.nuget.org/packages/Modularity.AspNetCore/) package to your project.

- Go your **Startup** and add following codes to MvcBuilder in **ConfigureServices()**:

```csharp
   public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddModularity(); // <-- Add this line after AddMvc method


            services.AddModuleServices(Configuration); // <-- Add this to add module's services into DI Container.
        }
```

- Go **Configure()** method and add following code:

```csharp
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseModulartiy(env); // <-- Call UseModularity with HostingEnvirorment parameter

            //...

        }
```

- That's it! Your Main Application is ready to load all modules from **Plugins** folder.

- _(OPTIONAL)_ You can manage your modules with config file. Just create following **plugins.json** file under **Plugins** folder:

```json
[
  {
    "Name": "MyFirstPlugin",
    "IsActive": true,
    "LoadAllDependencies" :  true
  },
  {
    "Name": "MySecondPlugin",
    "IsActive": false,
    "LoadAllDependencies" :  true
  }
]
```

If you use configuration file, you need to place your modules their own folders like this:

![image](https://user-images.githubusercontent.com/23705418/62240890-b0193900-b3e0-11e9-8634-b7b9a4aa853c.png)

To automaticly copy after build your modules with folders, change build action like this:
```
xcopy "$(OutDir)*" "$(SolutionDir)MyMainWebApplication\Plugins\$(ProjectName)\" /Y
```
> **MyMainWebApplication**: This is your main host application.


***

## Creating a Module

- Create a .Net Standard class library.

- Add [Microsoft.AspNetCore.Mvc](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc/) package.

- Add [Modularity.AspNetCore.Abstractions](https://www.nuget.org/packages/Modularity.AspNetCore.Abstractions) package to project.

- Add [Modularity.Core.Abstractions](https://www.nuget.org/packages/Modularity.Core.Abstractions) package to project.

- Create a class and inherit from `IModuleStartup`. This class will be used initialization.

```csharp
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
```

- _(OPTIONAL)_ You can add following code to Build Events to copy your module DLLs to your main application's `Plugins` folder. If they're in same solution.

```
xcopy "$(OutDir)*" "$(SolutionDir)MyMainWebApplication\Plugins\" /Y
```
**MyMainWebApplication**: Your main web application name.
