namespace ToolBX.AssemblyInitializer.Console;

public abstract class ConsoleStartup : AutoStartup
{
    protected ConsoleStartup(IConfiguration configuration) : base(configuration)
    {
    }

    public abstract void Run(IServiceProvider serviceProvider);
}