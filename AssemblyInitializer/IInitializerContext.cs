namespace ToolBX.AssemblyInitializer;

/// <summary>
/// The post-build context handed to each <see cref="IAssemblyInitializer"/> once the service provider is available.
/// This is a slim, host-agnostic replacement for the old ASP.NET <c>IApplicationBuilder</c>/<c>IHostEnvironment</c> pair.
/// </summary>
public interface IInitializerContext
{
    /// <summary>The built service provider, for resolving services during initialization.</summary>
    IServiceProvider Services { get; }

    /// <summary>The configuration the application was built with.</summary>
    IConfiguration Configuration { get; }

    /// <summary>The host environment (name, content root, etc.).</summary>
    IHostEnvironment Environment { get; }
}
