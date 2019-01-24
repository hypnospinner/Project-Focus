module ApiUnitTests

open System
open Xunit
open ProjectFocus.Backend.Common.Command
open ProjectFocus.Backend.Api

[<Fact>]
let ``Unit: Api problem creation works normally`` () =

    let mutable getBodyFunctionCalls = 0
    let getBodyAsync =
        async {
            getBodyFunctionCalls <- getBodyFunctionCalls + 1
            return { Name =""; Description=""; Content=""}
        }

    let mutable publishAddNewProblemAsyncCalls = 0
    let publishAddNewProblemAsync _ =
        async {
            publishAddNewProblemAsyncCalls <- publishAddNewProblemAsyncCalls + 1
        }

    let userId = Guid.NewGuid()
    async {
        let! result = WebHandler.handleCreateProblemAsync (userId) (getBodyAsync) (publishAddNewProblemAsync)
        match result with
        | Ok r -> Assert.Equal("Accepted!", r)
        | Error _ -> Assert.True false

        Assert.Equal(1, getBodyFunctionCalls)
        Assert.Equal(1, publishAddNewProblemAsyncCalls)
    }
