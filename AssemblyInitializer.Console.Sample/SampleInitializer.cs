namespace AssemblyInitializer.Console.Sample;

/// <summary>
/// Demonstrates a decoupled initializer. It is discovered at compile time by the source generator —
/// no runtime assembly scanning — and instantiated via its public parameterless constructor.
/// </summary>
public class SampleInitializer : IAssemblyInitializer
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // This is where an initializer would contribute its own service registrations.
    }

    public void Configure(IInitializerContext context)
    {
        System.Console.WriteLine($"[SampleInitializer] Initialized for environment '{context.Environment.EnvironmentName}'.");
    }
}
