namespace ProjectFocus.Backend.Common

open System
open System.Reflection

open RawRabbit
open RawRabbit.Common
open RawRabbit.Operations.Subscribe.Context

type BusSubscriber private () =

    new(busClient: IBusClient, provider: IServiceProvider) as this =
        BusSubscriber() then
        this.BusClient <- busClient
        this.Provider <- provider

    member val private BusClient : IBusClient = null with get, set

    member val private Provider : IServiceProvider = null with get, set

    interface IBusSubscriber with
        member this.Subscribe<'TMessage> (handler: (IServiceProvider -> 'TMessage -> Async<unit>)) =
            let asTask handle =
                let doHandleTask msg = 
                        async {
                                  do! handle msg
                                  return new Ack() :> Acknowledgement
                              }|> Async.StartAsTask
                doHandleTask

            let useContext (ctx: ISubscribeContext) =
                ctx.UseSubscribeConfiguration(fun cfg ->
                    cfg.FromDeclaredQueue(fun q ->
                        q.WithName(Assembly.GetEntryAssembly().GetName().Name)|>ignore)|>ignore)|>ignore

            this.BusClient.SubscribeAsync(handler this.Provider |> asTask, useContext) |> ignore