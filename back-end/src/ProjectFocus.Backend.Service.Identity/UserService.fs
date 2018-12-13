namespace ProjectFocus.Backend.Service.Identity

open System
open Domain
open Data

open ProjectFocus.Backend.Common.Auth

module UserService =

    let getAsync (getUserByEmail: string -> Async<User>) (email: string) =
        async {
            let! user = getUserByEmail (email.ToLowerInvariant())
            return if user.Id = Guid.Empty then None else Some user
        }

    let addAsync (existsUserWithEmail: string -> Async<bool>)
                 (addUser: User -> Async<unit>)
                 (getSalt: unit -> byte[])
                 (getHash: EncryptionParams -> byte[])
                 (parameters: NewUserParams) =
        async {
            let email = parameters.Email.ToLowerInvariant()
            let! exists = existsUserWithEmail email

            //[ToDo] Add exceptions handling logic
            if exists then failwith "User already exists"

            let salt = getSalt()
            let user = {
                    Id = Guid.NewGuid();
                    Name = parameters.Name;
                    Email = email;
                    Password = {
                        Password = parameters.Password;
                        Salt = salt
                    } |> getHash |> Convert.ToBase64String;
                    Salt = salt |> Convert.ToBase64String;
                    CreatedAt = DateTime.UtcNow;
                }
            do! addUser user
        }

    let loginAsync  (getUserByEmail: string -> Async<User>)
                    (getHash: EncryptionParams -> byte[])
                    (getJwt: Guid -> JsonWebToken)
                    (parameters: LoginUserParams) =
        async {
            let email = parameters.Email.ToLowerInvariant()
            let! user = getUserByEmail email
            return if user.Id = Guid.Empty
                   then None 
                   else if {
                               Password = parameters.Password;
                               Salt = user.Salt |> Convert.FromBase64String
                           } |> getHash
                             |> Convert.ToBase64String = user.Password
                       then Some (getJwt user.Id)
                       else None
        }