using System.Windows.Input;

using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel.Mock
{
    public class NewProblemFeature : IViewModelFeature, INewProblemFeature
    {
        public ICommand ProblemCommand { get; }

        public INotification ProceedToCreateProblem { get; }
    }
}
