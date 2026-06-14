namespace ToolBX.AssemblyInitializer;

public abstract class AutoStartup
{
    private const string RegistrarTypeName = "ToolBX.AssemblyInitializer.Generated.AssemblyInitializerRegistrar";

    public IConfiguration Configuration { get; }

    private IReadOnlyList<IAssemblyInitializer> Initializers => _initializers.Value;
    private readonly Lazy<IReadOnlyList<IAssemblyInitializer>> _initializers = new(DiscoverInitializers);

    protected AutoStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        AutoConfig.ServiceCollectionExtensions.AddAutoConfig(services, Configuration);
        services.AddAutoInjectServices();

        foreach (var initializer in Initializers)
            initializer.ConfigureServices(services, Configuration);
    }

    public void Configure(IInitializerContext context)
    {
        foreach (var initializer in Initializers)
            initializer.Configure(context);
    }

    // Each assembly containing initializers carries [HasAssemblyInitializers] (emitted by the source generator)
    // and a generated registrar whose CreateInitializers() instantiates them with plain 'new' expressions. We only
    // reflect across the thin, well-known registrar boundary — no type enumeration and no Activator.CreateInstance.
    private static IReadOnlyList<IAssemblyInitializer> DiscoverInitializers()
    {
        var initializers = new List<IAssemblyInitializer>();

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (assembly.GetCustomAttribute<HasAssemblyInitializersAttribute>() is null)
                continue;

            var method = assembly.GetType(RegistrarTypeName)
                ?.GetMethod("CreateInitializers", BindingFlags.Public | BindingFlags.Static);

            if (method?.Invoke(null, null) is IEnumerable<IAssemblyInitializer> created)
                initializers.AddRange(created);
        }

        return initializers;
    }
}
