namespace ToolBX.AssemblyInitializer;

public abstract class AutoStartup
{
    public const string DiscoveryRequiresUnreferencedCode =
        "AssemblyInitializer discovers IAssemblyInitializer implementations by scanning every loaded assembly at runtime. " +
        "The trimmer cannot statically determine which types are instantiated and may remove them.";

    public const string DiscoveryRequiresDynamicCode =
        "AssemblyInitializer instantiates discovered IAssemblyInitializer implementations via Activator.CreateInstance, which requires runtime code generation.";

    public IConfiguration Configuration { get; }

    private IReadOnlyList<IAssemblyInitializer> Initializers => _initializers.Value;
    private readonly Lazy<IReadOnlyList<IAssemblyInitializer>> _initializers;

    [RequiresUnreferencedCode(DiscoveryRequiresUnreferencedCode)]
    [RequiresDynamicCode(DiscoveryRequiresDynamicCode)]
    protected AutoStartup(IConfiguration configuration)
    {
        Configuration = configuration;
        _initializers = new Lazy<IReadOnlyList<IAssemblyInitializer>>(DiscoverInitializers);
    }

    [RequiresUnreferencedCode(DiscoveryRequiresUnreferencedCode)]
    [RequiresDynamicCode(DiscoveryRequiresDynamicCode)]
    private static IReadOnlyList<IAssemblyInitializer> DiscoverInitializers() => Types
        .Where(x => x.IsClass && !x.IsAbstract && x.Implements<IAssemblyInitializer>())
        .Select(x => (IAssemblyInitializer?)Activator.CreateInstance(x) ?? throw new InvalidOperationException($"Couldn't create instance of {x.FullName}"))
        .ToList();

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        AutoConfig.ServiceCollectionExtensions.AddAutoConfig(services, Configuration);
        services.AddAutoInjectServices();

        foreach (var initializer in Initializers)
            initializer.Configure(services, Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        foreach (var initializer in Initializers)
            initializer.Configure(app, env);
    }
}