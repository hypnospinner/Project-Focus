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

    let private handleEvent (host: IWebHost) event =
        async{
            printfn "An event %s has been caught" (event.ToString())
        }

    [<EntryPoint>]
    let main args =

        let buildHost = Host.build<Startup>
                        >> Host.subscribe<AuthenticatedEvent> Host.bus handleEvent
                        >> Host.run
        buildHost args
        
        exitCode
