using System;

using Xamarin.Forms;

using ProjectFocus.Interface;

namespace ProjectFocus.Integration
{
    public class RelayCommand : Command, IRelayCommand
    {
        public RelayCommand(Action execute, Func<bool> canExecute) : base(execute, canExecute)
        {
        }

        public void NotifyCanExecuteChanged()
        {
            ChangeCanExecute();
        }
    }
}
