namespace ProjectFocus.Backend.Service.Identity

open System
open System.Security.Cryptography

module Encryption =

    let defaultSaltLength = 40
    let defaultDerivedCount = 10000

    type EncryptionParams =
         {
             ClearText: string;
             Salt: byte[];
         }

    let getSalt saltLength () =
        let mutable bytes = Byte.MinValue |> Array.create<byte> saltLength
        RandomNumberGenerator.Create().GetBytes(bytes)
        bytes

    let getPasswordHash derivedCount (parameters: EncryptionParams) =
        let pbkdf2 = new Rfc2898DeriveBytes(parameters.ClearText, parameters.Salt, derivedCount)
        parameters.Salt.Length |> pbkdf2.GetBytes