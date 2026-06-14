// Uses the trimming/AOT-friendly factory overload and forwards the real process arguments.
ConsoleHost.UseStartup(configuration => new Startup(configuration), args);
