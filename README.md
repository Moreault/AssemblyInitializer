![AssemblyInitializer](https://github.com/Moreault/AssemblyInitializer/blob/master/assemblyinitializer.png)
# AssemblyInitializer
Helps decouple initialization logic by splitting it into AssemblyInitializer classes.

## Good to know
This package includes the AutoInject library. It also automatically initializes AutoInject so you don’t need to think about it at all. Just put those [AutoInject] attributes on the classes you want to inject and you’re good to go.

For console applications, it also grants you the possibility to optionally use an appsettings.json file right out of the box.

## Getting started

Put your initialization logic inside AssemblyInitializer classes. You’re free to call them whatever you want so long as they implement the IAssemblyInitializer interface.

```c#
public class SomeAssemblyInitializer : IAssemblyInitializer
{
    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        //This is where you would normally manually inject your services but you don't need to do that if you're using AutoInject

        //But if you really don't like AutoInject for some reason or can't use it because what you need to inject is from a third party
        services.AddSingleton<ISomeService, SomeService>();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        //I need to do this in Rough Trigger to initialize some loading logic
        var configurator = app.ApplicationServices.GetRequiredService<IConfigurator>();
        configurator.InitializeCurrentUserApplicationDataPath();
    }
}
```

### Setup

#### Web

All you need to do is for your Startup class to inherit from AutoStartup.

#### Console

There are a few more steps involved for Console applications. Be sure to grab the AssemblyInitializer.Console package instead of simply AssemblyInitializer.

First, you need to add a Startup class that inherits ConsoleStartup like this. 

Then, in your Program.cs file you should call the ConsoleHost like this.

And you’re good to go!

## Why

The goal of this library is to unify the way initialization is done across all application types. It also has the added advantage of decoupling initialization logic from the rest of your code or even from itself.

You can have as many AssemblyInitializers as you want so you could have one per concern if your initialization code is particularly crowded.