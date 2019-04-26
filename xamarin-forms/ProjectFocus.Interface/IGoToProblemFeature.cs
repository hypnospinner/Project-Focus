using System;
using System.Windows.Input;

namespace ProjectFocus.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGoToProblemFeature
    {
        INotification ProceedToCreateProblem { get; }

        ICommand ProblemCommand { get; }
    }
}
