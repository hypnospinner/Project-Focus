namespace ProjectFocus.Backend.Common

open System

module Command =

    type CreateUser =
        {
            Name: string;
            Email: string;
            Password: string;
        }

    type AuthenticateUser =
        {
            Email: string;
            Password: string;
        }

    type CreateProblem =
        {
            Name: string;
            Description: string;
            Content: string;
        }

    type UnrestrictedCommand =
        | CreateUser of CreateUser
        | AuthenticateUser of AuthenticateUser

    type AuthRequiredCommand =
        | CreateProblem of CreateProblem

    type AuthenticatedCommand =
        {
            UserId: Guid;
            Command: AuthRequiredCommand;
        }

    type Command =
       | AuthenticatedCommand of AuthenticatedCommand
       | UnrestrictedCommand of UnrestrictedCommand