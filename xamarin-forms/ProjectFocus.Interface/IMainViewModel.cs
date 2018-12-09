using System.Windows.Input;

namespace ProjectFocus.Interface
{
    public interface IMainViewModel
    {
        // [Design][ToDo] A simple command now at groundwork stage
        // -> an observable collection of features
        // in real implementation
        ICommand ProblemCommand { get; }

        void StartPresentation();
    }
}
