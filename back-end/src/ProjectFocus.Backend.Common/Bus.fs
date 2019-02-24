namespace ProjectFocus.Backend.Common

open System
open System.Text

open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection

open Newtonsoft.Json

open RawRabbit
open RawRabbit.Configuration
open RawRabbit.Instantiation
open RawRabbit.Serialization

module Bus =

    type RabbitMqOptions () = inherit RawRabbitConfiguration()

    type private NewtonsoftSerializer () =

        interface ISerializer with
            member __.ContentType = "application/json"
            member __.Serialize o =
                let s = JsonConvert.SerializeObject o
                Encoding.UTF8.GetBytes s
            member __.Deserialize<'T> a =
                let s = Encoding.UTF8.GetString a
                JsonConvert.DeserializeObject<'T> s

            member __.Deserialize (t,a) =
                let s = Encoding.UTF8.GetString a
                JsonConvert.DeserializeObject(s, t)

    let add (configuration: IConfiguration) (services: IServiceCollection) =
        let options = new RabbitMqOptions()
        let section = configuration.GetSection("rabbitmq")
        section.Bind options
        let rawRabbitOptions = new RawRabbitOptions()
        rawRabbitOptions.ClientConfiguration <- options
        rawRabbitOptions.DependencyInjection <- fun ioc -> ioc.AddSingleton<ISerializer, NewtonsoftSerializer>() |>ignore
        let client = RawRabbitFactory.CreateSingleton rawRabbitOptions
        services.AddSingleton<IBusClient> client |> ignore

    let get (provider: IServiceProvider) =
        let busClient = provider.GetService<IBusClient>();
        busClient

    let publishAsync<'TMessage> (bus: IBusClient) (message: 'TMessage)  =
        async {
            do! bus.PublishAsync(message) |> Async.AwaitTask
        }