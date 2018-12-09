namespace ProjectFocus.Backend.Service.Problem

open System
open Domain
open ProjectFocus.Backend.Common.Command
open ProjectFocus.Backend.Common.Event

// Handlers deal with events and commands.
// They call service functions and bus publishing functions.
module Handler =

    let createProblem (storeProblem: Problem -> Async<unit>)
                      (publishEvent: AuthenticatedEvent -> Async<unit>)
                      (command: AuthenticatedCommand) =
        command.Command
        |> function
           | CreateProblem c -> async {
                                let problem = {
                                        Id = Guid.NewGuid();
                                        UserId = command.UserId;
                                        CreatedAt = DateTime.UtcNow;
                                        Name = c.Name;
                                        Description = c.Description;
                                        Content = c.Content 
                                    }
                                do! storeProblem problem

                                let event = {
                                        UserId = problem.UserId;
                                        Event = ProblemCreated {
                                            Id = problem.Id;
                                            CreatedAt = problem.CreatedAt;
                                            Name = problem.Name;
                                            Description = problem.Description;
                                            Content = problem.Content 
                                        }
                                    }
                                do! publishEvent event
                            }
            // In future we can use a rejected event for
                // faulty situations.
                //     | _ -> RejectedEvent {
                //                 Code ="1";
                //                 Command = AuthenticatedCommand command;
                //                 Reason = "Unknown command." }
