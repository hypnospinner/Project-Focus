namespace ProjectFocus.Backend.Service.Identity

open System
open MongoDB.Driver
open MongoDB.Driver.Linq

type MongoCollection = IMongoCollectionExtensions

module Data =

    type User =
        {
            Id: Guid;
            Name: string;
            Email: string;
            Password: string;
            Salt: string;
            CreatedAt: DateTime;
        }

    let private getUsersCollection (db: IMongoDatabase) =
        db.GetCollection<User>("User")
    
    let userGetAsync (db: IMongoDatabase) (id: Guid) =
        async {
            return! db
                 |> getUsersCollection
                 |> MongoCollection.AsQueryable
                 |> fun q -> q.FirstOrDefaultAsync (fun x -> x.Id = id)
                 |> Async.AwaitTask
        }

    let userGetByEmailAsync (db: IMongoDatabase) (email: string) =
        async {
            return! db
                 |> getUsersCollection
                 |> MongoCollection.AsQueryable
                 |> fun q -> q.FirstOrDefaultAsync (fun x -> x.Email = email)
                 |> Async.AwaitTask
        }

    let userExistsWithEmailAsync (db: IMongoDatabase) (email: string) =
        async {
            return! db
                 |> getUsersCollection
                 |> MongoCollection.AsQueryable
                 |> fun q -> q.AnyAsync (fun x -> x.Email = email)
                 |> Async.AwaitTask
        }

    let userAddAsync (db: IMongoDatabase) (user: User) =
        async {
            do! db
                 |> getUsersCollection
                 |> fun x -> x.InsertOneAsync (user)
                 |> Async.AwaitTask
        }
