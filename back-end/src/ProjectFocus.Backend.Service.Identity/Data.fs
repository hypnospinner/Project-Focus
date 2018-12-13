namespace ProjectFocus.Backend.Service.Identity

open System

module Data =

    type User =
        {
            Id: Guid;
            Name: string;
            Email: string;
            Password: string;
            Salt: string;
            CreatedAt: DateTime;
        }