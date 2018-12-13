namespace ProjectFocus.Backend.Service.Identity

open Microsoft.AspNetCore.Hosting
open ProjectFocus.Backend.Common

module Integration =

    let saltLength = 40
    let encryptionIterations = 10000

    let loginUser host parameters =

        let db = host |> Host.db

        UserService.loginAsync (db |> UserRepository.getByEmailAsync)
                               (encryptionIterations |> Encryption.getPasswordHash)
                               (host |> Host.getJwt)
                               (parameters)