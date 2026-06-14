namespace ToolBX.AssemblyInitializer;

/// <summary>
/// Emitted by the AssemblyInitializer source generator onto any assembly that contains
/// <see cref="IAssemblyInitializer"/> implementations. <see cref="AutoStartup"/> uses it to find the
/// generated registrar without enumerating every type in every loaded assembly.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public sealed class HasAssemblyInitializersAttribute : Attribute;
