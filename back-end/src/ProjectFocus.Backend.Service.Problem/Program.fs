namespace ProjectFocus.Backend.Service.Problem

open Microsoft.AspNetCore.Hosting
open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.Command
open ProjectFocus.Backend.Common.Event

module Program =
    let exitCode = 0

    let private handleCommand (host: IWebHost) (command: AuthenticatedCommand) =
        async{
                printfn "A command %s has been received" (command.ToString());

                let hndProblemAdd =  ((host 
                                        |> Host.db
                                        |> ProblemRepository.addAsync
                                        |> ProblemService.addAsync),
                                      (host
                                        |> Host.bus
                                        |> Bus.publishAsync<AuthenticatedEvent>))
                                       ||> Handler.createProblem

                do! hndProblemAdd command
        }

    [<EntryPoint>]
    let main args =

        let runHost = Host.build<Startup>
                        >> Host.subscribe<AuthenticatedCommand> Host.bus handleCommand
                        >> Host.run
        
        runHost args

        exitCode
