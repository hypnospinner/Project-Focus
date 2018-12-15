namespace ProjectFocus.Backend.Api

open Giraffe

module WebApp =

    let api () =
        choose [
            subRoute "/api"
                (choose [
                    POST >=> choose [
                        route "/problems" >=> WebHandler.handleCreateProblem
                        route "/users" >=> WebHandler.handleCreateUser
                    ]
                ])
            setStatusCode 404 >=> text "Not Found" ]
