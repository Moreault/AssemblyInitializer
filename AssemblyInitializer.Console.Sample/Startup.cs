namespace AssemblyInitializer.Console.Sample;

public class Startup : ConsoleStartup
{
    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

    public override void Run(IServiceProvider serviceProvider)
    {
        //appsettings.json's properties need to be set to build action "Content" and to "Always copy"
        var options = serviceProvider.GetRequiredService<IOptions<SampleOptions>>().Value;

        System.Console.WriteLine(options.Greeting);
    }
}