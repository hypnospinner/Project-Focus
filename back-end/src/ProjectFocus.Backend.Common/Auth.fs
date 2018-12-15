namespace ProjectFocus.Backend.Common

open System
open System.IdentityModel.Tokens.Jwt
open System.Text

open Microsoft.IdentityModel.Tokens
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection

module Auth =

    type JsonWebToken =
        {
            Token: string;
            Expires: int64;
        }

    let private getSigningKey (secret: string) =
        new SymmetricSecurityKey(secret |> Encoding.UTF8.GetBytes) :> SecurityKey

    let private getTokenHeader signingKey =
        let signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        let jwtHeader = new JwtHeader(signingCredentials)
        jwtHeader

    let private getToken (tokenHandler: JwtSecurityTokenHandler)
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

    let private getValidationParams issuer signingKey =
        let parameters = new TokenValidationParameters()
        parameters.ValidateAudience <- false
        parameters.ValidIssuer <- issuer
        parameters.IssuerSigningKey <- signingKey
        parameters


    type JwtOptions () =
            member val SecretKey = String.Empty with get, set
            member val ExpiryMinutes = 5L with get, set
            member val Issuer = String.Empty with get, set

    let add (configuration: IConfiguration) (services: IServiceCollection) =
        let options = new JwtOptions()
        let section = configuration.GetSection("jwt")
        section.Bind options
        do services.Configure<JwtOptions> section |> ignore

        let tokenHandler = new JwtSecurityTokenHandler()
        let key = getSigningKey options.SecretKey
        let header = getTokenHeader key

        services.AddSingleton<Guid -> JsonWebToken> (getToken tokenHandler header options.ExpiryMinutes options.Issuer) |> ignore

        let tokenValidationParameters = getValidationParams options.Issuer
        services.AddAuthentication()
                .AddJwtBearer(fun cfg ->
                (
                    cfg.RequireHttpsMetadata <- false
                    cfg.SaveToken <- true
                    cfg.TokenValidationParameters <- getValidationParams options.Issuer key
                )) |> ignore

    let getJwt (provider: IServiceProvider) (userId: Guid) =
        let jwtFunc = provider.GetService<Guid -> JsonWebToken>();
        jwtFunc userId
