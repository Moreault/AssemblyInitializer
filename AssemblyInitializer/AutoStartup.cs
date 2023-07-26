using ToolBX.Reflection4Humans.Extensions;
using ToolBX.Reflection4Humans.TypeFetcher;

namespace ToolBX.AssemblyInitializer;

public abstract class AutoStartup
{
    public IConfiguration Configuration { get; }

    private IEnumerable<IAssemblyInitializer> Initializers => _initializers.Value;
    private readonly Lazy<IList<IAssemblyInitializer>> _initializers = new(() => Types.Where(x => x.IsClass && !x.IsAbstract && x.Implements<IAssemblyInitializer>()).Select(x => (IAssemblyInitializer?)Activator.CreateInstance(x) ?? throw new Exception($"Couldn't create instance of {x.FullName}")).ToList());

    protected AutoStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoConfig(Configuration);
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