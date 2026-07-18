namespace ToolBX.AssemblyInitializer.Console;

public static class ConsoleHost
{
    private const string StartupActivationMessage =
        "This overload activates the startup type with Activator.CreateInstance. Prefer the overload that takes a " +
        "Func<IConfiguration, TStartup> factory for trimming/NativeAOT-friendly startup creation.";

    /// <summary>
    /// Builds configuration, runs the startup produced by <paramref name="startupFactory"/>, and returns it.
    /// This is the trimming/AOT-friendly entry point: the startup is created with an ordinary constructor call.
    /// </summary>
    public static T UseStartup<T>(Func<IConfiguration, T> startupFactory, params string[] arguments) where T : ConsoleStartup
    {
        ArgumentNullException.ThrowIfNull(startupFactory);

        var (configuration, environment) = Bootstrap();
        return Launch(startupFactory(configuration), configuration, environment, arguments);
    }

    /// <summary>
    /// Convenience entry point that activates <typeparamref name="T"/> via reflection. Not trimming/AOT-safe;
    /// prefer <see cref="UseStartup{T}(Func{IConfiguration, T}, string[])"/> when targeting NativeAOT.
    /// </summary>
    [RequiresUnreferencedCode(StartupActivationMessage)]
    [RequiresDynamicCode(StartupActivationMessage)]
    public static T UseStartup<T>(params string[] arguments) where T : ConsoleStartup
    {
        var (configuration, environment) = Bootstrap();
        var startup = (T)Activator.CreateInstance(typeof(T), configuration)!;
        return Launch(startup, configuration, environment, arguments);
    }

    private static (IConfiguration Configuration, ConsoleHostEnvironment Environment) Bootstrap()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        return (configuration, new ConsoleHostEnvironment { EnvironmentName = environmentName });
    }

    private static T Launch<T>(T startup, IConfiguration configuration, ConsoleHostEnvironment environment, string[] arguments)
        where T : ConsoleStartup
    {
        startup.Arguments = arguments;

        var services = new ServiceCollection();
        services.AddOptions();
        startup.ConfigureServices(services);

        var provider = services.BuildServiceProvider();
        startup.Configure(new ConsoleInitializerContext(provider, configuration, environment));
        startup.Run(provider);

        return startup;
    }
}
