namespace ProjectFocus.Backend.Service.Identity

open Giraffe
open FSharp.Control.Tasks.V2.ContextInsensitive
open Microsoft.AspNetCore.Http
open MongoDB.Driver
open ProjectFocus.Backend.Common.Command
open Microsoft.Extensions.DependencyInjection
open System
open ProjectFocus.Backend.Common.Auth

module WebApp =

    let encryptionIterations = 10000

    let handleLogin =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let db = ctx.GetService<IMongoDatabase>()
                let getJwt = ctx.GetService<Guid -> JsonWebToken>()

                let! command = ctx.BindJsonAsync<AuthenticateUser>()

                let! token = UserService.loginAsync (db |> UserRepository.getByEmailAsync)
                               (encryptionIterations |> Encryption.getPasswordHash)
                               (getJwt)
                               {Email = command.Email; Password = command.Password}
                let response = token |> function | None -> "failure" | Some t -> t.Token
                return! json (response) (next) (ctx)
            }

    let api () =
        choose [
            subRoute "/account"
                (choose [
                    POST >=> choose [
                        route "/login" >=> handleLogin
                    ]
                ])
            setStatusCode 404 >=> text "Not Found" ]