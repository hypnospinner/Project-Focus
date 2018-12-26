namespace ProjectFocus.Backend.Api

open System
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive

open Giraffe

open ProjectFocus.Backend.Common.Command

module WebHandler =

    let handleCreateProblemAsync (userId: Guid) 
                                 (getBodyAsync: Async<CreateProblem>)
                                 (publishCreateProblemAsync: AuthenticatedCommand -> Async<unit>) =
        asyncResult {
            return! getBodyAsync
                    |> AsyncResult.ofAsync
                    |> AsyncResult.map (
                        function
                        | c -> { UserId = userId; Command = CreateProblem c })
                    |> AsyncResult.bind (
                        publishCreateProblemAsync >> AsyncResult.ofAsync)
                    |> AsyncResult.map (
                        function
                        | _ -> "Accepted!")
                    |> AsyncResult.mapError (
                        function
                        | _ -> "Something went wrong.")
        }

    let handleCreateUser =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! command = ctx.BindJsonAsync<CreateUser>()
                do! User.publishAddNewAsync (ctx.RequestServices) command
                return! Successful.OK ("Accepted!") (next) (ctx)
            }