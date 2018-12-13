namespace ProjectFocus.Backend.Service.Identity

open System
open Domain
open ProjectFocus.Backend.Common.Command
open ProjectFocus.Backend.Common.Event

module Handler =

    let createUser (storeUser: NewUserParams -> Async<unit>)
                   (publishEvent: UnrestrictedEvent -> Async<unit>)
                   (command: UnrestrictedCommand) =
        command
        |> function
           | CreateUser c -> async {
                                do! storeUser {
                                        Email = c.Email;
                                        Name = c.Name;
                                        Password = c.Password;
                                    }

                                do! publishEvent  (UserCreated {
                                        Email = c.Email;
                                        Name = c.Name;
                                    })
                            }
           | _ -> async.Zero ()
            // In future we can use a rejected event for
            // faulty situations.
            //     | _ -> RejectedEvent {
            //                 Code ="1";
            //                 Command = AuthenticatedCommand command;
            //                 Reason = "Unknown command." }
