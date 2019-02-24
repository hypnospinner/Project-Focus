namespace ProjectFocus.Backend.Api

open System

open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.Command

module User =
    let publishAddNewAsync (provider: IServiceProvider) (command: CreateUser) =
        (provider |> Bus.get |> Bus.publishAsync<CreateUser>) (command)
