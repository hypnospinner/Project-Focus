using ProjectFocus.DataModel;
using ProjectFocus.Interface;
using ProjectFocus.Interface.Data;
using ProjectFocus.Model;
using System;

namespace ProjectFocus.Service
{
    public class ProblemService : IProblemService
    {
        public IProblemStorage ProblemStorage { get; set; }
        public void CreateProblem(Problem problem, User user)
        {
            var createProblem = new CreateProblemDto
            {
                UserId = user.Id,
                Name = problem.Name,
                Description = problem.Description,
                Content = problem.Description,
                CreatedAt = DateTime.UtcNow
            };
            problem.Id = ProblemStorage.CreateProblem(createProblem);
        }
    }
}
