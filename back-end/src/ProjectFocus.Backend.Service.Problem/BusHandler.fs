namespace ProjectFocus.Backend.Service.Problem

open System

open ProjectFocus.Backend.Common.Command
open ProjectFocus.Backend.Common.Event

module BusHandler =

     let handleAuthenticatedCommand (provider: IServiceProvider) (command: AuthenticatedCommand) =
        printfn "A command %s has been received" (command.ToString());
        command.Command
        |> function
           | CreateProblem c -> async {
                                let! problem = Problem.addAsync (provider)  {
                                        UserId = command.UserId;
                                        Name = c.Name;
                                        Description = c.Description;
                                        Content = c.Content 
                                    }

                                do! Problem.publishAddedAsync (provider)  {
                                        UserId = problem.UserId;
                                        Event = ProblemCreated {
                                            Id = problem.Id;
                                            CreatedAt = problem.CreatedAt;
                                            Name = problem.Name;
                                            Description = problem.Description;
                                            Content = problem.Content 
                                        }
                                    }
                            }