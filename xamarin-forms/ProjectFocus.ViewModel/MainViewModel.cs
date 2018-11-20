using ProjectFocus.Interface;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectFocus.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        INavigationService _navigationService;
        Func<IProblemViewModel> _getProblemViewModel;

        // Change to property injection
        public MainViewModel(
            INavigationService navigationService,
            Func<IProblemViewModel> getProblemViewModel)
        {
            _navigationService = navigationService;
            _navigationService.Navigate(PageKey.Main, this);

            _getProblemViewModel = getProblemViewModel;
        }

        private ICommand _problemCommand;
        public ICommand ProblemCommand
        {
            get
            {
                if(_problemCommand == null)
                {
                    _problemCommand = new Command(() =>
                        _navigationService.Navigate(PageKey.Problem, _getProblemViewModel()));
                }
                return _problemCommand;
            }
        }
    }
}
