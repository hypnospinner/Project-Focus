using ProjectFocus.Model;

namespace ProjectFocus.Interface
{
    public interface IProblemService
    {
        void CreateProblem(Problem problem, User user);
    }
}
