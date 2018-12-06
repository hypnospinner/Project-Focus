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

    let private equals (guid2: Guid) (guid1: Guid) =
        (guid1.ToByteArray(), guid2.ToByteArray())
            |> function
               | a1, a2 -> BitConverter.ToInt64 (a1, 0),
                           BitConverter.ToInt64 (a1, 8),
                           BitConverter.ToInt64 (a2, 0),
                           BitConverter.ToInt64 (a2, 8)
            |> function
               | l1p1, l1p2, l2p1, l2p2 -> (l1p1 = l2p1) && (l1p2 = l2p2)

    let private getCollection (db: IMongoDatabase) =
        db.GetCollection<Problem>("Problem")
    
    let getAsync (db: IMongoDatabase) (id: Guid) =
        async {
            return! db
                 |> getCollection
                 |> MongoCollection.AsQueryable
                 |> fun q -> q.FirstOrDefaultAsync (fun x -> x.Id |> equals id)
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
