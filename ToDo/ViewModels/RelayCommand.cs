using System;
using System.Windows.Input;

namespace ToDo.ViewModels
{
    public class RelayCommand : ICommand
    {
        private Action<object> _execute { get; set; }
        private Func<object, bool> _canExecute { get; set; }

        public RelayCommand(Action<object> onExecute, Func<object, bool> onCanExecute = null)
        {
            _execute = onExecute;
            _canExecute = onCanExecute;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }

        #endregion
    }
}
