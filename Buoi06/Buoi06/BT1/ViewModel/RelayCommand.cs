using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Buoi06.BT1.ViewModel
{
    public class RelayCommand: ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if(execute == null)
            {
                throw new ArgumentNullException(nameof(execute), "Action execute");
            }    
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            else
                return _canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            if(_execute != null)
                _execute(parameter);
        }
        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if(handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
