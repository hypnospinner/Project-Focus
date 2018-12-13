namespace ProjectFocus.Backend.Service.Identity

open System
open MongoDB.Driver
open MongoDB.Driver.Linq
open Data

type MongoCollection = IMongoCollectionExtensions

module UserRepository =

    let private getCollection (db: IMongoDatabase) =
        db.GetCollection<User>("User")
    
    let getAsync (db: IMongoDatabase) (id: Guid) =
        async {
            return! db
                 |> getCollection
                 |> MongoCollection.AsQueryable
                 |> fun q -> q.FirstOrDefaultAsync (fun x -> x.Id = id)
                 |> Async.AwaitTask
        }

    let getByEmailAsync (db: IMongoDatabase) (email: string) =
        async {
            return! db
                 |> getCollection
                 |> MongoCollection.AsQueryable
                 |> fun q -> q.FirstOrDefaultAsync (fun x -> x.Email = email)
                 |> Async.AwaitTask
        }

    let existsWithEmailAsync (db: IMongoDatabase) (email: string) =
        async {
            return! db
                 |> getCollection
                 |> MongoCollection.AsQueryable
                 |> fun q -> q.AnyAsync (fun x -> x.Email = email)
                 |> Async.AwaitTask
        }

    let addAsync (db: IMongoDatabase) (user: User) =
        async {
            do! db
                 |> getCollection
                 |> fun x -> x.InsertOneAsync (user)
                 |> Async.AwaitTask
        }
