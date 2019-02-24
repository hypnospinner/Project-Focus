namespace ProjectFocus.Backend.Service.Problem

open Giraffe

module WebApp =

    let api () =
        choose [
            subRoute "/account"
                (choose [
                    POST >=> choose [
                        route "/login" >=> WebHandler.handlePing
                    ]
                ])
            setStatusCode 404 >=> text "Not Found" ]
