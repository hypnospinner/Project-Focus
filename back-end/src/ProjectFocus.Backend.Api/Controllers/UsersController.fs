namespace ProjectFocus.Backend.Api.Controllers

open Microsoft.AspNetCore.Mvc
open RawRabbit
open ProjectFocus.Backend.Common.Command

[<Route("[controller]")>]
[<ApiController>]
type UsersController private () =
    inherit ControllerBase()
    new(busClient: IBusClient) as this =
        UsersController() then
        this.BusClient <- busClient
    member val BusClient : IBusClient = null with get, set

    // place an example url here
    [<HttpPost("register")>]
    member this.Post([<FromBody>] command: CreateUser) =
        this.BusClient.PublishAsync ( command ) |> ignore
        AcceptedResult()