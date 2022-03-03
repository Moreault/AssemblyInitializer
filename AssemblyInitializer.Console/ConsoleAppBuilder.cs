using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace ToolBX.AssemblyInitializer.Console;

public class ConsoleAppBuilder : IApplicationBuilder
{
    public IServiceProvider ApplicationServices { get; set; }
    public IFeatureCollection ServerFeatures { get; }
    public IDictionary<string, object> Properties { get; }

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