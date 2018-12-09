namespace ProjectFocus.Backend.Api.Controllers

open Microsoft.AspNetCore.Mvc
open RawRabbit
open ProjectFocus.Backend.Common.Command

[<Route("[controller]")>]
[<ApiController>]
type ProblemsController private () =
    inherit ControllerBase()

    new(busClient: IBusClient) as this =
        ProblemsController() then
        this.BusClient <- busClient

    member val BusClient : IBusClient = null with get, set

    // Use the following for test with Postman
    //{"UserId":"b97fc037-c513-48b0-9216-7d79f62d21e5","Command":{"Case":"CreateProblem","Fields":[{"Id":"15869b92-4872-455d-be2c-f3599a9dd715","CreatedAt":"2018-11-29T10:24:06.6462297Z","Name":"test problem","Description":"this is a test problem","Content":"this is the content of a test problem"}]}}
    [<HttpPost("")>]
    member this.Post([<FromBody>] command: AuthenticatedCommand) =
        this.BusClient.PublishAsync (command) |> ignore
        AcceptedResult()