namespace ProjectFocus.Backend.Service.Problem

open Microsoft.AspNetCore.Hosting
open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.Command
open ProjectFocus.Backend.Common.Event

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let runHost = Host.build<Startup> >> Host.run
        runHost args

        exitCode
