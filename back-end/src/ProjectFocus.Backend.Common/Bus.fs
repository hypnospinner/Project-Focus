namespace ProjectFocus.Backend.Common

open RawRabbit

module Bus =

    let publishAsync<'TMessage> (bus: IBusClient) (message: 'TMessage)  =
        async {
            do! bus.PublishAsync(message) |> Async.AwaitTask
        }