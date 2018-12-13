namespace ProjectFocus.Backend.Service.Identity

open System
open System.Security.Cryptography
open Domain

module Encryption =

    let getSalt saltSize () =
        let mutable bytes = Byte.MinValue |> Array.create<byte> saltSize
        RandomNumberGenerator.Create().GetBytes(bytes)
        bytes

    let getPasswordHash derivedCount (parameters: EncryptionParams) =
        let pbkdf2 = new Rfc2898DeriveBytes(parameters.Password, parameters.Salt, derivedCount)
        parameters.Salt.Length |> pbkdf2.GetBytes