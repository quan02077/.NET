using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Buoi06.BT3.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            else
            {
                return _canExecute(parameter);
            }
        }
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        private EventHandler _canExecuteChangedInternal;
        public event EventHandler CanExecuteChanged
        {
            add
            {
                _canExecuteChangedInternal += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                _canExecuteChangedInternal -= value;
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
