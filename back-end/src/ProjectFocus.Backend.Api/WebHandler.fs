namespace ProjectFocus.Backend.Api

open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive

open Giraffe

open ProjectFocus.Backend.Common.Command

module WebHandler =

    let handleCreateProblem =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! command = ctx.BindJsonAsync<AuthenticatedCommand>()
                do! Problem.publishAddNewAsync (ctx.RequestServices) command
                return! Successful.OK ("Accepted!") (next) (ctx)
            }

    let handleCreateUser =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! command = ctx.BindJsonAsync<CreateUser>()
                do! User.publishAddNewAsync (ctx.RequestServices) command
                return! Successful.OK ("Accepted!") (next) (ctx)
            }