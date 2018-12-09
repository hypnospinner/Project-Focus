namespace ProjectFocus.Backend.Service.Problem

open System
open Domain

// Services contain business logic. They use
// storage interaction functions for different
// entity types.
module ProblemService =

    let getAsync (getProblem: Guid -> Async<Problem>) (id) =
        async {
            let! problem = getProblem id
            return if problem.Id = Guid.Empty then None else Some problem
        }
    let addAsync (addProblem: Problem -> Async<unit>) (problem) =
        async {
            return! addProblem problem
        }

    let browseAsync (browseProblems: Async<Problem list>) =
        async {
            return! browseProblems
        }