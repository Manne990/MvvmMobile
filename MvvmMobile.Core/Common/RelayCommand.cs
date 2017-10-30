using System;
using System.Windows.Input;

namespace MvvmMobile.Core.Common
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                _execute?.Invoke(parameter);
        }

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
    }
}