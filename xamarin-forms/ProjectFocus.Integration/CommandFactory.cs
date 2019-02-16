using System;

using ProjectFocus.Interface;

namespace ProjectFocus.Integration
{
    public class CommandFactory : ICommandFactory
    {
        public IRelayCommand Create(Action execute, Func<bool> canExecute)
        {
            return new RelayCommand(execute, canExecute);
        }
    }
}
