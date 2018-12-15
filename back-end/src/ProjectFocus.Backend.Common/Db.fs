namespace ProjectFocus.Backend.Common

open System

open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Options

open MongoDB.Bson
open MongoDB.Bson.Serialization.Conventions
open MongoDB.Driver

module Db =

    type MongoOptions () =
            member val ConnectionString = String.Empty with get, set
            member val Database = String.Empty with get, set

    type private MongoConventions () =

        interface IConventionPack with
            member __.Conventions =
                Seq.ofList
                 [
                     IgnoreExtraElementsConvention(true) :> IConvention;
                     EnumRepresentationConvention(BsonType.String) :> IConvention;
                     CamelCaseElementNameConvention() :> IConvention
                 ]

    let add (configuration: IConfiguration) (services: IServiceCollection) =
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

    let get (provider: IServiceProvider) =
        use scope = provider.CreateScope()
        let dataBase = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
        dataBase
