namespace ProjectFocus.Backend.Common

open System

type IBusSubscriber =
    abstract member Subscribe<'TMessage> : (IServiceProvider -> 'TMessage -> Async<unit>) -> unit;