namespace ProjectFocus.Backend.Service.Identity

open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.Command

module Bus =

    let subscribe (subscriber: IBusSubscriber) =
        subscriber.Subscribe<CreateUser> BusHandler.handleCreateUser