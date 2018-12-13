namespace ProjectFocus.Backend.Service.Problem

open System
open MongoDB.Driver
open MongoDB.Driver.Linq
open Domain

type MongoCursor = IAsyncCursorSourceExtensions
type MongoCollection = IMongoCollectionExtensions

// Repositories are each for dealing with the storage 
// of objects of one specific type.
// In this case, the type is Problem.
module ProblemRepository =

    let private getCollection (db: IMongoDatabase) =
        db.GetCollection<Problem>("Problem")
    
    let getAsync (db: IMongoDatabase) (id: Guid) =
        async {
            return! db
                 |> getCollection
                 |> MongoCollection.AsQueryable
                 |> fun q -> q.FirstOrDefaultAsync (fun x -> x.Id = id)
                 |> Async.AwaitTask
        }
    let addAsync (db: IMongoDatabase) (problem: Problem) =
        async {
            return! db
                 |> getCollection
                 |> fun x -> x.InsertOneAsync (problem)
                 |> Async.AwaitTask
        }

    let browseAsync (db: IMongoDatabase) =
        async {
            let! csList = db |> getCollection
                          |> MongoCollection.AsQueryable
                          |> MongoCursor.ToListAsync
                          |> Async.AwaitTask
            return csList |> List.ofSeq
        }
