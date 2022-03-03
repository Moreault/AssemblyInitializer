using Microsoft.Extensions.FileProviders;

namespace ToolBX.AssemblyInitializer.Console;

public record ConsoleHostEnvironment : IHostEnvironment
{
    public string EnvironmentName { get; set; }
    public string ApplicationName { get; set; }
    public string ContentRootPath { get; set; }
    public IFileProvider ContentRootFileProvider { get; set; }
}