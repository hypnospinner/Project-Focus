namespace ProjectFocus.Backend.Api.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc

[<Route("")>]
[<ApiController>]
type HomeController () =
    inherit ControllerBase()

    [<HttpGet("")>]
    member this.Get() =
        ActionResult<string>("Hello from home controller!")