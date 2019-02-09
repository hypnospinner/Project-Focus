namespace ProjectFocus.Backend.Service.Identity

open Giraffe
open ProjectFocus.Backend.Common

module WebApp =

    let api () =
        choose [
            subRoute "/account"
                (choose [
                    POST >=> choose [
                        route "/login" >=> WebHandler.handleLogin
                    ]
                    GET >=> route "/try" >=> Auth.authorize >=> text "Success!"
                ])
            setStatusCode 404 >=> text "Not Found" ]
