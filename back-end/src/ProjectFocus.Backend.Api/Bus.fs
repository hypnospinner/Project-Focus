namespace ProjectFocus.Backend.Api

open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.Event

module Bus =

    let subscribe (subscriber: IBusSubscriber) =
        subscriber.Subscribe<AuthenticatedEvent> BusHandler.handleAuthenticatedEvent