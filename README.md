![AssemblyInitializer](https://github.com/Moreault/AssemblyInitializer/blob/master/assemblyinitializer.png)
# AssemblyInitializer
Helps decouple initialization logic by splitting it into AssemblyInitializer classes.

## Good to know
This package includes the AutoInject library. It also automatically initializes AutoInject so you don't need to think about it at all. Just put those [AutoInject] attributes on the classes you want to inject and you're good to go.

For console applications, it also grants you the possibility to optionally use an appsettings.json file right out of the box.

Initializers are discovered at **compile time** by a source generator (not by scanning assemblies at runtime), so the library is trimming/NativeAOT-friendly.

## Getting started

Put your initialization logic inside classes that implement `IAssemblyInitializer`. You're free to call them whatever you want. Each initializer must have a public parameterless constructor.

```c#
public class SomeAssemblyInitializer : IAssemblyInitializer
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //This is where you would normally manually inject your services but you don't need to do that if you're using AutoInject

        //But if you really don't like AutoInject for some reason or can't use it because what you need to inject is from a third party
        services.AddSingleton<ISomeService, SomeService>();
    }

    public void Configure(IInitializerContext context)
    {
        //Runs after the service provider is built. Resolve whatever you need from context.Services.
        var configurator = context.Services.GetRequiredService<IConfigurator>();
        configurator.InitializeCurrentUserApplicationDataPath();
    }
}
```

`IInitializerContext` exposes the built `IServiceProvider` (`Services`), the `IConfiguration` (`Configuration`) and the `IHostEnvironment` (`Environment`). It replaces the old ASP.NET `IApplicationBuilder`/`IHostEnvironment` pair, so the core package no longer depends on the ASP.NET shared framework.

### Setup

#### Console

Grab the `ToolBX.AssemblyInitializer.Console` package. Add a `Startup` class that inherits `ConsoleStartup`:

```c#
public class Startup : ConsoleStartup
{
    public Startup(IConfiguration configuration) : base(configuration) { }

    public override void Run(IServiceProvider serviceProvider)
    {
        //Your application's entry logic.
    }
}
```

Then start it from `Program.cs`. Prefer the factory overload — it creates the startup with a plain constructor call, which is trimming/NativeAOT-safe:

```c#
ConsoleHost.UseStartup(configuration => new Startup(configuration), args);
```

There is also a convenience `ConsoleHost.UseStartup<Startup>(args)` overload that activates the startup via reflection. It is marked `[RequiresUnreferencedCode]`/`[RequiresDynamicCode]` and is not recommended when targeting NativeAOT.

## Why

The goal of this library is to unify the way initialization is done across all application types. It also has the added advantage of decoupling initialization logic from the rest of your code or even from itself.

You can have as many AssemblyInitializers as you want so you could have one per concern if your initialization code is particularly crowded.
