namespace ProjectFocus.Backend.Service.Problem

open System

open Logic
open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.Event

module Problem =

    let addAsync (provider: IServiceProvider) (parameters: NewProblemParams) =
        Logic.problemAddAsync (provider |> Db.get |> Data.problemAddAsync)
                              (parameters)

    let publishAddedAsync (provider: IServiceProvider) (event: AuthenticatedEvent) =
        (provider |> Bus.get |> Bus.publishAsync<AuthenticatedEvent>) (event)