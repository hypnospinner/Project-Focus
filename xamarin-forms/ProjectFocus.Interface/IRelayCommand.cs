using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectFocus.Interface
{
    public interface IRelayCommand : ICommand
    {
        void NotifyCanExecuteChanged();
    }
}
