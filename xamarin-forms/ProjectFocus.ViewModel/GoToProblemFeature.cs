using System;
using System.Windows.Input;
using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel
{
    [FeatureMetadata(nameof(GoToProblemFeature), FeatureScope.MainSelection)]
    public class GoToProblemFeature : FeatureViewModelBase, IGoToProblemFeature
    {
        public ICommandFactory CommandFactory { get; set; }
        public Func<IProblemViewModel> GetProblemViewModel { get; set; }
        public INotification ProceedToCreateProblem { get; set; }

        private ICommand _problemCommand;
        public ICommand ProblemCommand
        {
            get
            {
                if (_problemCommand == null)
                {
                    _problemCommand = CommandFactory.Create(
                        () => ProceedToCreateProblem.Publish(GetProblemViewModel()),
                        () => true);
                }
                return _problemCommand;
            }
        }
    }
}
