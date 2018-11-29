namespace ProjectFocus.Backend.Service.Problem

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging
open ProjectFocus.Backend.Common
open RawRabbit.Pipe
open RawRabbit
open ProjectFocus.Backend.Common.Command
open ProjectFocus.Backend.Common.Event

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let ev = {
            UserId = Guid.NewGuid();
            Event = ProblemCreated {
                        Id = Guid.NewGuid();
                        CreatedAt = DateTime.UtcNow;
                        Name = "real problem";
                        Description = "everything is a problem";
                        Content = "making this work is a problem";
                    }}

        (Service.bus
                |> Service.command<AuthenticatedCommand> (fun client c -> 
                        (printfn "A command %s has been received" (c.ToString());
                         client.PublishAsync(ev) |>ignore ))
                |> Service.host<Startup>
                |> Service.run) args ()
        exitCode
