using ProjectFocus.Interface;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectFocus.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public INotification ProceedToCreateProblem { get; set; }

        // This is an automatic view-model-producing factory method
        // brought to us by Autofac.
        public Func<IProblemViewModel> GetProblemViewModel { get; set; }

        private ICommand _problemCommand;
        public ICommand ProblemCommand
        {
            get
            {
                if(_problemCommand == null)
                {
                    _problemCommand = new Command(() =>
                        ProceedToCreateProblem.Publish(GetProblemViewModel()));
                }
                return _problemCommand;
            }
        }
    }
}
