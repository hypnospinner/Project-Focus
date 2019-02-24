using System;
using System.Windows.Input;

namespace ProjectFocus.Interface
{
    public interface INewProblemFeature
    {
        INotification ProceedToCreateProblem { get; }

        ICommand ProblemCommand { get; }
    }
}
