namespace ProjectFocus.Backend.Service.Identity

open System

open ProjectFocus.Backend.Common.Command

module BusHandler =

     let handleCreateUser (provider: IServiceProvider) (command: CreateUser) =
        async{
                printfn "A command %s has been received" (command.ToString());

                do! User.addAsync (provider) {
                        Email = command.Email;
                        Name = command.Name;
                        Password = command.Password;
                    }

                do! User.publishAddedAsync (provider) {
                        Email = command.Email;
                        Name = command.Name;
                    }
        }