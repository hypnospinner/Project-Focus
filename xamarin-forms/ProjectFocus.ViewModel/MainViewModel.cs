using ProjectFocus.Interface;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectFocus.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public INavigationService NavigationService { get; set; }

        // This is an automatic view-model-producing factory method
        // brought to us by Autofac.
        public Func<IProblemViewModel> GetProblemViewModel { get; set; }
    
        public void StartPresentation()
        {
            NavigationService.Navigate(PageKey.Main, this);
        }

        private ICommand _problemCommand;
        public ICommand ProblemCommand
        {
            get
            {
                if(_problemCommand == null)
                {
                    _problemCommand = new Command(() =>
                        NavigationService.Navigate(PageKey.Problem, GetProblemViewModel()));
                }
                return _problemCommand;
            }
        }
    }
}
