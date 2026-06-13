namespace ToolBX.AssemblyInitializer.Console;

public abstract class ConsoleStartup : AutoStartup
{
    public IReadOnlyList<string> Arguments { get; protected internal set; } = [];

    [RequiresUnreferencedCode(DiscoveryRequiresUnreferencedCode)]
    [RequiresDynamicCode(DiscoveryRequiresDynamicCode)]
    protected ConsoleStartup(IConfiguration configuration) : base(configuration)
    {
    }

    public abstract void Run(IServiceProvider serviceProvider);
}