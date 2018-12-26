namespace ProjectFocus.Backend.Api

open ProjectFocus.Backend.Common

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let buildHost = Host.build<Startup> >> Host.run
        buildHost args
        
        exitCode
