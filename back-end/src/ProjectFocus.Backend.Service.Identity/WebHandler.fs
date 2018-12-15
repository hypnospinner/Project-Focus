namespace ProjectFocus.Backend.Service.Identity

open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive

open Giraffe

open ProjectFocus.Backend.Common.Command

module WebHandler =

    let handleLogin =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! command = ctx.BindJsonAsync<AuthenticateUser>()
                let! token =  User.loginAsync (ctx.RequestServices) {Email = command.Email; Password = command.Password}
                
                // [ToDo] Change to response error code.
                let response = token |> function | None -> "failure" | Some t -> t.Token
                return! json (response) (next) (ctx)
            }