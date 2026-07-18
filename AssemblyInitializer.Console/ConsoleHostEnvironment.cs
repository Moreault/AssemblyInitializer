namespace ToolBX.AssemblyInitializer.Console;

public record ConsoleHostEnvironment : IHostEnvironment
{
    public string EnvironmentName { get; set; } = string.Empty;
    public string ApplicationName { get; set; } = Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty;
    public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();
    public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
}
