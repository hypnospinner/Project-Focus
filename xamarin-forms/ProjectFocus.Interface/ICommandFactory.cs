using System;

namespace ProjectFocus.Interface
{
    public interface ICommandFactory
    {
        IRelayCommand Create(Action execute, Func<bool> canExecute);
    }
}
