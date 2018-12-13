namespace ProjectFocus.Backend.Service.Identity

open Microsoft.AspNetCore.Hosting
open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.Command
open ProjectFocus.Backend.Common.Event

module Program =
    let exitCode = 0

    //[ToDo] Move to config
    let saltLength = 40
    let encryptionIterations = 10000

    let private handleCommand (host: IWebHost) (command: CreateUser) =
        async{
                printfn "A command %s has been received" (command.ToString());

                let db = host |> Host.db

                let addUser = UserService.addAsync (db |> UserRepository.existsWithEmailAsync)
                                                   (db |> UserRepository.addAsync)
                                                   (saltLength |> Encryption.getSalt)
                                                   (encryptionIterations |> Encryption.getPasswordHash)

                let hndUserAdd =  ((addUser),
                                   (host
                                        |> Host.bus
                                        |> Bus.publishAsync<UnrestrictedEvent>))
                                  ||> Handler.createUser

                do! hndUserAdd (CreateUser command)
        }

    [<EntryPoint>]
    let main args =
        let runHost = Host.build<Startup>
                      >> Host.subscribe<CreateUser> Host.bus handleCommand
                      >> Host.run
        
        runHost args

        exitCode
