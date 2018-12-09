namespace ProjectFocus.Backend.Service.Problem

open System
open MongoDB.Driver
open MongoDB.Driver.Linq
open Domain
open Microsoft.AspNetCore.Hosting

// This methods here are for dealing with a database
// as a whole. One possible operation at the database level
// would be creating schema or seeding the database with
// predefined values.
module Database =

    let seed (getDb: IWebHost -> IMongoDatabase) (host: IWebHost) =
        async{

              let! cursor = (host |> getDb).ListCollectionsAsync() |> Async.AwaitTask
              let! anyCollections = cursor.AnyAsync() |> Async.AwaitTask
              // seed code here
              return ()
        }