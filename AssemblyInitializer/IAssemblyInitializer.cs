namespace ToolBX.AssemblyInitializer;

/// <summary>
/// Implement this interface to contribute decoupled initialization logic. Implementations are discovered at
/// compile time by the AssemblyInitializer source generator and must have a public parameterless constructor.
/// </summary>
public interface IAssemblyInitializer
{
    /// <summary>Register services into the container before the service provider is built.</summary>
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);

    /// <summary>Run initialization logic after the service provider is built.</summary>
    void Configure(IInitializerContext context);
}
