namespace ProjectFocus.Backend.Common

open System
open System.IdentityModel.Tokens.Jwt
open Microsoft.IdentityModel.Tokens
open System.Text

module Auth =

    type JsonWebToken =
        {
            Token: string;
            Expires: int64;
        }

    let signingKey (secret: string) =
        new SymmetricSecurityKey(secret |> Encoding.UTF8.GetBytes) :> SecurityKey

    let tokenHeader signingKey =
        let signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        let jwtHeader = new JwtHeader(signingCredentials)
        jwtHeader

    let token (tokenHandler: JwtSecurityTokenHandler)
              (header: JwtHeader)
              (expiryMinutes: int64)
              (issuer: string)
              (userId: Guid) =
        let nowUtc = DateTime.UtcNow
        let expires = nowUtc.AddMinutes(expiryMinutes |> float)
        let ageStart = (new DateTime(1970, 1, 1)).ToUniversalTime()
        let exp = (new TimeSpan(expires.Ticks - ageStart.Ticks)).TotalSeconds |> int64
        let now = (new TimeSpan(nowUtc.Ticks - ageStart.Ticks)).TotalSeconds |> int64
        let payload = new JwtPayload()
        payload.["sub"] <- userId
        payload.["iss"] <- issuer
        payload.["iat"] <- now
        payload.["exp"] <- exp
        payload.["unique_name"] <- userId
        let jwt = new JwtSecurityToken(header, payload)
        let tkn = tokenHandler.WriteToken jwt
        {
            Token = tkn;
            Expires = exp;
        }

    let validationParams issuer signingKey =
        let parameters = new TokenValidationParameters()
        parameters.ValidateAudience <- false
        parameters.ValidIssuer <- issuer
        parameters.IssuerSigningKey <- signingKey
        parameters

    
    //let createToken (userId: Guid) =
