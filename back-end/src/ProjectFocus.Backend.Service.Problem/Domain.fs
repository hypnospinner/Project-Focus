namespace ProjectFocus.Backend.Service.Problem

open System

// The domain contains business objects relevant
// to the specific micro-service's needs.
// Here they are used for business and storage needs.
module Domain =

    type Problem =
        {
            Id: Guid;
            UserId: Guid;
            CreatedAt: DateTime;
            Name: string;
            Description: string;
            Content: string;
        }