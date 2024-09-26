namespace ToolBX.AssemblyInitializer.Console;

public static class ConsoleHost
{
    public static T UseStartup<T>(params string[] arguments) where T : ConsoleStartup
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var env = new ConsoleHostEnvironment
        {

        };

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        IServiceCollection services = new ServiceCollection();
        services.AddOptions();

        var startup = (T)Activator.CreateInstance(typeof(T), configuration)!;
        startup.Arguments = arguments;

        startup.ConfigureServices(services);

        var provider = services.BuildServiceProvider();
        var app = new ConsoleAppBuilder
        {
            ApplicationServices = provider
        };
        startup.Configure(app, env);

        startup.Run(provider);

        return startup;
    }
}