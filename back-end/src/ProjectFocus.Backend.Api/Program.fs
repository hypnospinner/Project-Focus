namespace ProjectFocus.Backend.Api

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging
open RawRabbit.Pipe
open RawRabbit
open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.Event

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        (Service.bus
                |> Service.event (fun client e -> 
                    ( printfn "An event %s has been caught" (e.ToString());
                      ))
                |> Service.host<Startup>
                |> Service.run) args ()
        exitCode
