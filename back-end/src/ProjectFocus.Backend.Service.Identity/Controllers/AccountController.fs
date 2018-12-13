namespace ProjectFocus.Backend.Service.Identity.Controllers

open System
open Microsoft.AspNetCore.Mvc
open RawRabbit
open ProjectFocus.Backend.Common.Auth
open ProjectFocus.Backend.Common.Command
open ProjectFocus.Backend.Service.Identity
open Microsoft.AspNetCore.Hosting

[<Route("[controller]")>]
[<ApiController>]
type AccountController private () =
    inherit ControllerBase()

    // new(host: IWebHost) as this =
    //     AccountController() then
    //     this.Host <- host

    // member val Host : IWebHost = null with get, set

    [<HttpPost("login")>]
    member this.Post([<FromBody>] command: AuthenticateUser) =
        async {
            //let! token = Integration.loginUser this.Host {Email = command.Email; Password = command.Password}
            //return JsonResult(token)
            return JsonResult("{}")
        }

    