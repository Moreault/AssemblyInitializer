namespace AssemblyInitializer.Console.Sample;

[ToolBX.AutoConfig.AutoConfig("Sample")]
public record SampleOptions
{
    public string Greeting { get; init; } = string.Empty;
}