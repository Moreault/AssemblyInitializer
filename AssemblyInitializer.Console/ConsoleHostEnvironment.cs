namespace ToolBX.AssemblyInitializer.Console;

public record ConsoleHostEnvironment : IHostEnvironment
{
    public string EnvironmentName { get; set; } = string.Empty;
    public string ApplicationName { get; set; } = string.Empty;
    public string ContentRootPath { get; set; } = string.Empty;
    public IFileProvider ContentRootFileProvider { get; set; } = null!;
}