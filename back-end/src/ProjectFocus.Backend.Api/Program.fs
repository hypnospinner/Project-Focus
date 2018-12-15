namespace ProjectFocus.Backend.Api

open Microsoft.AspNetCore.Hosting
open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.Event

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let buildHost = Host.build<Startup> >> Host.run
        buildHost args
        
        exitCode
