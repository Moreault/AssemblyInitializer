namespace ToolBX.AssemblyInitializer.Console;

internal sealed class ConsoleInitializerContext(IServiceProvider services, IConfiguration configuration, IHostEnvironment environment) : IInitializerContext
{
    public IServiceProvider Services { get; } = services;
    public IConfiguration Configuration { get; } = configuration;
    public IHostEnvironment Environment { get; } = environment;
}
