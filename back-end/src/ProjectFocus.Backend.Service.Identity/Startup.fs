namespace ProjectFocus.Backend.Service.Identity

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Giraffe
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open ProjectFocus.Backend.Common

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddGiraffe() |> ignore
        services |> Service.addJwt this.Configuration
        services |> Service.addRabbitMq this.Configuration
        services |> Service.addMongoDb this.Configuration


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
        else
            app.UseHsts() |> ignore

        //app.UseHttpsRedirection() |> ignore
        app.UseGiraffe (WebApp.api())

    member val Configuration : IConfiguration = null with get, set