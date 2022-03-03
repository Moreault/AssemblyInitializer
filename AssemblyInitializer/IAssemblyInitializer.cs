namespace ToolBX.AssemblyInitializer;

public interface IAssemblyInitializer
{
    void Configure(IServiceCollection services, IConfiguration configuration);
    void Configure(IApplicationBuilder app, IHostEnvironment env);
}