namespace ProjectFocus.Backend.Api

open Microsoft.AspNetCore.Hosting
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
