namespace ProjectFocus.Backend.Common

open System
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive

open Giraffe

module WebApp =

    let provider (ctx: HttpContext) =
        ctx.RequestServices

    let body (ctx: HttpContext) =
        async{
            let! a = ctx.BindJsonAsync<'T>() |> Async.AwaitTask
            return a
        }

    let userId (ctx: HttpContext) =
        Guid.Parse( ctx.User.Identity.Name)

    let handle (handler: HttpContext -> AsyncResult<'TSuccess, 'TError>) =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! result = ctx |> handler
                let httpResult = result
                                 |> function
                                    | Ok r -> Successful.OK r
                                    | Error e -> RequestErrors.BAD_REQUEST e
                return! httpResult next ctx
            }

