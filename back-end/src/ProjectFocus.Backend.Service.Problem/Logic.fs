namespace ProjectFocus.Backend.Service.Problem

open System
open Data

module Logic =

    type NewProblemParams =
        {
            UserId: Guid;
            Name: string;
            Description: string;
            Content: string;
        }
    let problemGetAsync (getProblem: Guid -> Async<Problem>) (id) =
        async {
            let! problem = getProblem id
            return if problem.Id = Guid.Empty then None else Some problem
        }
    let problemAddAsync (addProblem: Problem -> Async<unit>) (parameters: NewProblemParams) =
        async {
            let problem =   {
                                Id = Guid.NewGuid();
                                UserId = parameters.UserId;
                                CreatedAt = DateTime.UtcNow;
                                Name = parameters.Name;
                                Description = parameters.Description;
                                Content = parameters.Content
                            }

            do! addProblem problem
            return problem
        }

    let problemsBrowseAsync (browseProblems: Async<Problem list>) =
        async {
            return! browseProblems
        }