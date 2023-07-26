namespace ToolBX.AssemblyInitializer.Console;

public class ConsoleAppBuilder : IApplicationBuilder
{
    public IServiceProvider ApplicationServices { get; set; } = null!;
    public IFeatureCollection ServerFeatures => null!;
    public IDictionary<string, object?> Properties => null!;

    public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
    {
        throw new NotImplementedException();
    }

    public IApplicationBuilder New()
    {
        throw new NotImplementedException();
    }

    public RequestDelegate Build()
    {
        throw new NotImplementedException();
    }

}