namespace ProjectFocus.Backend.Common

open System
open Command

module Event =

    type UserCreated =
        {
            Name: string;
            Email: string;
        }

    type UserAuthenticated =
        {
            Email: string;
        }

    type ProblemCreated =
        {
            Id: Guid;
            CreatedAt: DateTime;
            Name: string;
            Description: string;
            Content: string;
        }

    type RejectedEvent =
        {
            Reason: string;
            Code: string;
            Command: Command;
        }

    type UnrestrictedEvent =
        | UserCreated of UserCreated
        | UserAuthenticated of UserAuthenticated

    type AuthRequiredEvent =
        | ProblemCreated of ProblemCreated

    type AuthenticatedEvent =
        {
            UserId: Guid;
            Event: AuthRequiredEvent;
        }

    type Event =
       | AuthenticatedEvent of AuthenticatedEvent
       | UnrestrictedEvent of UnrestrictedEvent
       | RejectedEvent of RejectedEvent