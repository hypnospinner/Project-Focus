namespace ProjectFocus.Backend.Common

open Microsoft.AspNetCore.Builder

module AppBuilder =

    type IApplicationBuilder with 

        member x.UseMessageQueue (subscribe: IBusSubscriber -> unit)  =
            let provider = x.ApplicationServices
            let bus = provider |> Bus.get
            (new BusSubscriber (bus, provider)) |> subscribe