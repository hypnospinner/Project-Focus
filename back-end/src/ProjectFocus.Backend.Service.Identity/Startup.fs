namespace ProjectFocus.Backend.Service.Identity

open Giraffe
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open ProjectFocus.Backend.Common
open ProjectFocus.Backend.Common.AppBuilder

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        services.AddGiraffe() |> ignore
        services |> Auth.add this.Configuration
        services |> Bus.add this.Configuration
        services |> Db.add this.Configuration


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
        else
            app.UseHsts() |> ignore

        
        //app.UseHttpsRedirection() |> ignore
        app.UseAuthentication() |> ignore
        app.UseGiraffe (WebApp.api())
        app.UseMessageQueue (Bus.subscribe)

    member val Configuration : IConfiguration = null with get, set