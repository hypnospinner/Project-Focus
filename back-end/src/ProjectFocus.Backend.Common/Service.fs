namespace ProjectFocus.Backend.Common

open System
open Command
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.AspNetCore
open RawRabbit
open RawRabbit.Pipe
open RawRabbit.Common
open RawRabbit.Operations.Subscribe.Context
open System.Reflection
open System.Threading.Tasks
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open RawRabbit.Configuration
open RawRabbit.Instantiation
open RawRabbit.Serialization
open RawRabbit.Serialization
open System.Text
open Newtonsoft.Json

module Service =

    let run (getWebHost: string[] -> IWebHost) =
        fun (args: string[]) -> 
            (getWebHost args).Run

    let host<'TStartup when 'TStartup : not struct> (getBusClient: IWebHost -> IBusClient) =
        fun (args: string[]) -> 
            let config = (new ConfigurationBuilder())
                          .AddEnvironmentVariables()
                          .AddCommandLine(args)
                          .Build()
            let _host = WebHost.CreateDefaultBuilder(args)
                              .UseConfiguration(config)
                              .UseStartup<'TStartup>()
                              .Build()

            getBusClient _host |> ignore
            _host

    let bus =
        fun (host: IWebHost) ->
            let busClient = host
                             .Services
                             .GetService(typeof<IBusClient>)
                             :?> IBusClient;
            busClient

    let private asTask<'T> (handle: 'T -> unit) =
        let doHandleTask cmd = 
                async {
                          handle cmd
                          return new Ack() :> Acknowledgement
                      }|> Async.StartAsTask

        new Func<'T, Task<Acknowledgement>>(doHandleTask)

    let command<'TCommand> (handle: IBusClient -> 'TCommand -> unit) (getBusClient: IWebHost -> IBusClient) =
        fun (host: IWebHost) ->
            let busClient = getBusClient host

            let useContext (ctx: ISubscribeContext) =
                ctx.UseSubscribeConfiguration(fun cfg ->
                    cfg.FromDeclaredQueue(fun q ->
                        q.WithName(Assembly.GetEntryAssembly().GetName().Name)|>ignore)|>ignore)|>ignore

            busClient.SubscribeAsync(handle busClient |> asTask, useContext) |> ignore
            busClient

    let event<'TEvent> (handle: IBusClient -> 'TEvent -> unit) (getBusClient: IWebHost -> IBusClient) =
        fun (host: IWebHost) ->
            let busClient = getBusClient host

            let useContext (ctx: ISubscribeContext) =
                ctx.UseSubscribeConfiguration(fun cfg ->
                    cfg.FromDeclaredQueue(fun q ->
                        q.WithName(Assembly.GetEntryAssembly().GetName().Name)|>ignore)|>ignore)|>ignore

            busClient.SubscribeAsync(handle busClient |> asTask, useContext) |> ignore
            busClient

    type RabbitMqOptions () = inherit RawRabbitConfiguration()

    type CustomSerializer () = inherit RawRabbit.Serialization.JsonSerializer(new Newtonsoft.Json.JsonSerializer())

    type CoolSerializer () =

        interface ISerializer with
            member this.ContentType = "application/json"
            member this.Serialize o =
                let s = JsonConvert.SerializeObject o
                printfn "Serialized %s" s
                Encoding.UTF8.GetBytes s
            member this.Deserialize<'T> a =
                let s = Encoding.UTF8.GetString a
                printfn "Deserializing stuff %s" s
                JsonConvert.DeserializeObject<'T> s

            member this.Deserialize (t,a) =
                let s = Encoding.UTF8.GetString a
                printfn "Deserializing type %s %s" t.Name s
                JsonConvert.DeserializeObject(s, t)

    let addRabbitMq (configuration: IConfiguration) (services: IServiceCollection) =
        let options = new RabbitMqOptions()
        let section = configuration.GetSection("rabbitmq")
        section.Bind options
        let rawRabbitOptions = new RawRabbitOptions()
        rawRabbitOptions.ClientConfiguration <- options
        rawRabbitOptions.DependencyInjection <- fun ioc -> ioc.AddSingleton<ISerializer, CoolSerializer>() |>ignore
        let client = RawRabbitFactory.CreateSingleton rawRabbitOptions
        services.AddSingleton<IBusClient> client |> ignore