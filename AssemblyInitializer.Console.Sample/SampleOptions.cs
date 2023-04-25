namespace AssemblyInitializer.Console.Sample;

[AutoConfig("Sample")]
public record SampleOptions
{
    public string Greeting { get; init; } = string.Empty;
}