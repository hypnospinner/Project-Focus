namespace ProjectFocus.Backend.Api

open System

open ProjectFocus.Backend.Common.Event

module BusHandler =

    let handleAuthenticatedEvent (provider: IServiceProvider) (event: AuthenticatedEvent) =
        async {
            printfn "An event %s has been caught" (event.ToString())
        }