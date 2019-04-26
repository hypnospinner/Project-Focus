using System.Windows.Input;

using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel.Mock
{
    public class NewProblemFeature : IFeatureViewModelBase, IGoToProblemFeature
    {
        public ICommand ProblemCommand { get; }

        public INotification ProceedToCreateProblem { get; }

        public void SetNotificationChanel(INotification notification)
        {
        }
    }
}
