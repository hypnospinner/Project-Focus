namespace ProjectFocus.Backend.Common

open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration

module Host =

    // The only crucial case is getting a host.
    // All other functors can be derived from the host constructor.
    let run (host: IWebHost) =
        host.Run()

    let build<'TStartup when 'TStartup : not struct> (args: string[]) =
        // Construct configuration object
        let config = (new ConfigurationBuilder())
                      .AddEnvironmentVariables()
                      .AddCommandLine(args)
                      .Build()
        // Construct a web host with the given args
        let webHost = WebHost.CreateDefaultBuilder(args)
                          .UseConfiguration(config)
                          .UseStartup<'TStartup>()
                          .Build()
        webHost