using System.Windows.Input;
using ProjectFocus.Interface;

namespace ProjectFocus.ViewModel.Mock
{
    public class MainViewModel : IMainViewModel
    {
        public ICommand ProblemCommand { get; }
    }
}
