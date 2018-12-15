namespace ProjectFocus.Backend.Service.Identity

open System

open ProjectFocus.Backend.Common.Auth

open Data
open Encryption

module Logic =

    type NewUserParams =
        {
            Name: string;
            Email: string;
            Password: string;
        }

    type LoginUserParams =
        {
            Email: string;
            Password: string;
        }
    let userGetAsync (getUserByEmail: string -> Async<User>) (email: string) =
        async {
            let! user = getUserByEmail (email.ToLowerInvariant())
            return if user.Id = Guid.Empty then None else Some user
        }

    let userAddAsync (userExistsWithEmail: string -> Async<bool>)
                 (userAdd: User -> Async<unit>)
                 (getSalt: unit -> byte[])
                 (getHash: EncryptionParams -> byte[])
                 (parameters: NewUserParams) =
        async {
            let email = parameters.Email.ToLowerInvariant()
            let! exists = userExistsWithEmail email

            //[ToDo] Add exceptions handling logic
            if exists then failwith "User already exists"

            let salt = getSalt()
            let user = {
                    Id = Guid.NewGuid();
                    Name = parameters.Name;
                    Email = email;
                    Password = {
                        ClearText = parameters.Password;
                        Salt = salt
                    } |> getHash |> Convert.ToBase64String;
                    Salt = salt |> Convert.ToBase64String;
                    CreatedAt = DateTime.UtcNow;
                }
            do! userAdd user
        }

    let userLoginAsync  (userGetByEmail: string -> Async<User>)
                    (getHash: EncryptionParams -> byte[])
                    (getJwt: Guid -> JsonWebToken)
                    (parameters: LoginUserParams) =
        async {
            let email = parameters.Email.ToLowerInvariant()
            let! user = userGetByEmail email
            return if user.Id = Guid.Empty
                   then None 
                   else if {
                               ClearText = parameters.Password;
                               Salt = user.Salt |> Convert.FromBase64String
                           } |> getHash
                             |> Convert.ToBase64String = user.Password
                       then Some (getJwt user.Id)
                       else None
        }