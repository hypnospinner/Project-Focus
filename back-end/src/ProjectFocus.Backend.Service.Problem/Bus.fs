namespace ProjectFocus.Backend.Service.Problem

open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.Command

module Bus =

    let subscribe (subscriber: IBusSubscriber) =
        subscriber.Subscribe<AuthenticatedCommand> BusHandler.handleAuthenticatedCommand