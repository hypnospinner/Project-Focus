using Newtonsoft.Json;
using ProjectFocus.DataModel;
using ProjectFocus.Interface.Data;
using System;
using System.IO;

namespace ProjectFocus.Data
{
    public class ProblemStorage : IProblemStorage
    {
        public Guid CreateProblem(CreateProblemDto createProblem)
        {
            var problem = new
            {
                ReferenceId = Guid.NewGuid(),
                // [ToDo] Storing this is for testing purposes only
                // We'll have to make out a better, more efficient way
                // of storing data locally.
                CreateDto = createProblem
            };
            var jsonString = JsonConvert.SerializeObject(problem);
            // [ToDo] Add permissions to write to folders for each platform
            // see https://stackoverflow.com/questions/51152578/xamarin-forms-saving-file-in-filesystem
            File.WriteAllText("test.json", jsonString);
            return problem.ReferenceId;
        }
    }
}
