using System.Windows.Input;

namespace ProjectFocus.Interface
{
    public interface IMainViewModel
    {
        ICommand ProblemCommand { get; }
    }
}
