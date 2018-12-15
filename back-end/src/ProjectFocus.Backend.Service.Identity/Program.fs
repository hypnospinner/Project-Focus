namespace ProjectFocus.Backend.Service.Identity

open ProjectFocus.Backend.Common

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =
        let runHost = Host.build<Startup> >> Host.run
        runHost args
        exitCode
