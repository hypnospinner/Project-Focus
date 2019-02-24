namespace ProjectFocus.Backend.Service.Problem

open System
open MongoDB.Driver
open MongoDB.Driver.Linq

type MongoCollection = IMongoCollectionExtensions
type MongoCursor = IAsyncCursorSourceExtensions

module Data =

    type Problem =
        {
            Id: Guid;
            UserId: Guid;
            CreatedAt: DateTime;
            Name: string;
            Description: string;
            Content: string;
        }
    let private getProblemsCollection (db: IMongoDatabase) =
        db.GetCollection<Problem>("Problem")
    
    let problemGetAsync (db: IMongoDatabase) (id: Guid) =
        async {
            return! db
                 |> getProblemsCollection
                 |> MongoCollection.AsQueryable
                 |> fun q -> q.FirstOrDefaultAsync (fun x -> x.Id = id)
                 |> Async.AwaitTask
        }
    let problemAddAsync (db: IMongoDatabase) (problem: Problem) =
        async {
            return! db
                 |> getProblemsCollection
                 |> fun x -> x.InsertOneAsync (problem)
                 |> Async.AwaitTask
        }

    let problemsBrowseAsync (db: IMongoDatabase) =
        async {
            let! csList = db |> getProblemsCollection
                          |> MongoCollection.AsQueryable
                          |> MongoCursor.ToListAsync
                          |> Async.AwaitTask
            return csList |> List.ofSeq
        }
