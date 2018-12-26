namespace ProjectFocus.Backend.Service.Identity

open System

open Logic
open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.Event

module User =

    let addAsync (provider: IServiceProvider) (parameters: NewUserParams) =
        Logic.userAddAsync (provider |> Db.get |> Data.userExistsWithEmailAsync)
                           (provider |> Db.get |> Data.userAddAsync)
                           (Encryption.defaultSaltLength |> Encryption.getSalt)
                           (Encryption.defaultDerivedCount |> Encryption.getPasswordHash)
                           (parameters)

    let publishAddedAsync (provider: IServiceProvider) (event: UserCreated) =
        (provider |> Bus.get |> Bus.publishAsync<UnrestrictedEvent>) (UserCreated event)

    let loginAsync (provider: IServiceProvider) (parameters: LoginUserParams) =
        Logic.userLoginAsync (provider |> Db.get |> Data.userGetByEmailAsync)
                             (Encryption.defaultDerivedCount |> Encryption.getPasswordHash)
                             (provider |> Auth.getJwt)
                             (parameters)