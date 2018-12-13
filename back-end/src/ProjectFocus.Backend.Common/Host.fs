namespace ProjectFocus.Backend.Common

open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.AspNetCore
open RawRabbit
open RawRabbit.Common
open RawRabbit.Operations.Subscribe.Context
open System.Reflection
open Microsoft.Extensions.DependencyInjection
open MongoDB.Driver
open Auth
open System


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

    let db (host: IWebHost) =
        // This is a scoped service and so we create it.
        use scope = host.Services.CreateScope()
        let dataBase = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
        dataBase

    let bus (host: IWebHost) =
        // This function can be used to obtain a bus client but not
        // from any other function in this module. It's to be passed as parameter.
        let busClient = host.Services.GetService<IBusClient>();
        busClient

    let getJwt (host: IWebHost) =
        let jwtFunc = host.Services.GetService<Guid -> JsonWebToken>();
        jwtFunc

    let private asTask<'T> handle =
        let doHandleTask (cmd: 'T) = 
                async {
                          do! handle cmd
                          return new Ack() :> Acknowledgement
                      }|> Async.StartAsTask
        doHandleTask

    let subscribe<'TMessage> (getBus: IWebHost -> IBusClient) (handle: IWebHost -> 'TMessage -> Async<unit>) (host) =
        let bus = getBus host

        let useContext (ctx: ISubscribeContext) =
            ctx.UseSubscribeConfiguration(fun cfg ->
                cfg.FromDeclaredQueue(fun q ->
                    q.WithName(Assembly.GetEntryAssembly().GetName().Name)|>ignore)|>ignore)|>ignore

        bus.SubscribeAsync(handle host |> asTask, useContext) |> ignore
        host