namespace ProjectFocus.Backend.Service.Problem

open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive

open Giraffe


module WebHandler =

    let handlePing =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                return! json ("pong") (next) (ctx)
            }