namespace ProjectFocus.Backend.Api

open Giraffe
open ProjectFocus.Backend.Common

module WebApp =

    let api () =
        choose [
            subRoute "/api"
                (choose [
                    POST >=> choose [
                        route "/problems" >=> Auth.authorize >=> WebApp.handle (
                            fun ctx -> WebHandler.handleCreateProblemAsync (WebApp.userId ctx) 
                                                                           (WebApp.body ctx)
                                                                           (Problem.publishAddNewAsync (WebApp.provider ctx)))
                        route "/users" >=> WebHandler.handleCreateUser
                    ]
                ])
            setStatusCode 404 >=> text "Not Found" ]
