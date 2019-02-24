namespace ProjectFocus.Backend.Api

open System

open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.Command

module Problem =

    let publishAddNewAsync (provider: IServiceProvider) (command: AuthenticatedCommand) =
        (provider |> Bus.get |> Bus.publishAsync<AuthenticatedCommand>) (command)

