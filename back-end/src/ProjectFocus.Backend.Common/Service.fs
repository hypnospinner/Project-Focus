namespace ProjectFocus.Backend.Common

open System
open Microsoft.Extensions.Configuration
open RawRabbit
open Microsoft.Extensions.DependencyInjection
open RawRabbit.Configuration
open RawRabbit.Instantiation
open RawRabbit.Serialization
open System.Text
open Newtonsoft.Json
open MongoDB.Driver
open Microsoft.Extensions.Options
open MongoDB.Bson.Serialization.Conventions
open MongoDB.Bson
open System.IdentityModel.Tokens.Jwt
open Auth


module Service =

    type RabbitMqOptions () = inherit RawRabbitConfiguration()

    type private NewtonsoftSerializer () =

        interface ISerializer with
            member this.ContentType = "application/json"
            member this.Serialize o =
                let s = JsonConvert.SerializeObject o
                Encoding.UTF8.GetBytes s
            member this.Deserialize<'T> a =
                let s = Encoding.UTF8.GetString a
                JsonConvert.DeserializeObject<'T> s

            member this.Deserialize (t,a) =
                let s = Encoding.UTF8.GetString a
                JsonConvert.DeserializeObject(s, t)

    let addRabbitMq (configuration: IConfiguration) (services: IServiceCollection) =
        let options = new RabbitMqOptions()
        let section = configuration.GetSection("rabbitmq")
        section.Bind options
        let rawRabbitOptions = new RawRabbitOptions()
        rawRabbitOptions.ClientConfiguration <- options
        rawRabbitOptions.DependencyInjection <- fun ioc -> ioc.AddSingleton<ISerializer, NewtonsoftSerializer>() |>ignore
        let client = RawRabbitFactory.CreateSingleton rawRabbitOptions
        services.AddSingleton<IBusClient> client |> ignore

    type MongoOptions () =
            member val ConnectionString = String.Empty with get, set
            member val Database = String.Empty with get, set

    type MongoConventions () =

        interface IConventionPack with
            member this.Conventions =
                Seq.ofList
                 [ 
                     IgnoreExtraElementsConvention(true) :> IConvention;
                     EnumRepresentationConvention(BsonType.String) :> IConvention;
                     CamelCaseElementNameConvention() :> IConvention
                 ]

    let addMongoDb (configuration: IConfiguration) (services: IServiceCollection) =
        services
        |> fun s -> s.Configure<MongoOptions> (configuration.GetSection("mongo"))
        |> fun s -> s.AddSingleton<MongoClient> (fun serviceProvider ->
            (
                let options = serviceProvider.GetService<IOptions<MongoOptions>>()
                MongoClient(options.Value.ConnectionString)
            ))
        |> fun s -> s.AddScoped<IMongoDatabase> (fun serviceProvider ->
           (
               let options = serviceProvider.GetService<IOptions<MongoOptions>>()
               let client = serviceProvider.GetService<MongoClient>()
               client.GetDatabase(options.Value.Database)
           ))
        |> ignore
        ConventionRegistry.Register("ProjectFocusConventions", MongoConventions(), fun _ -> true)

    type JwtOptions () =
            member val SecretKey = String.Empty with get, set
            member val ExpiryMinutes = 5L with get, set
            member val Issuer = String.Empty with get, set

    let addJwt (configuration: IConfiguration) (services: IServiceCollection) =
        let options = new JwtOptions()
        let section = configuration.GetSection("jwt")
        section.Bind options
        do services.Configure<JwtOptions> section |> ignore

        let tokenHandler = new JwtSecurityTokenHandler()
        let key = Auth.signingKey options.SecretKey
        let header = Auth.tokenHeader key

        services.AddSingleton<Guid -> JsonWebToken> (Auth.token tokenHandler header options.ExpiryMinutes options.Issuer) |> ignore

        let tokenValidationParameters = Auth.validationParams options.Issuer
        services.AddAuthentication()
                .AddJwtBearer(fun cfg ->
                (
                    cfg.RequireHttpsMetadata <- false
                    cfg.SaveToken <- true
                    cfg.TokenValidationParameters <- Auth.validationParams options.Issuer key
                )) |> ignore
        ()