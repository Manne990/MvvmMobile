using System;
using System.Windows.Input;

namespace MvvmMobile.Core.Common
{
    public class RelayCommand : ICommand
    {
        // Private Members
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;


        // -----------------------------------------------------------------------------

        // Constructors
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        // -----------------------------------------------------------------------------

        // Events
        public event EventHandler CanExecuteChanged;


        // -----------------------------------------------------------------------------

        // Public Methods
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _execute?.Invoke(parameter);
            }
        }

        public void Execute()
        {
            if (CanExecute(null))
            {
                _execute?.Invoke(null);
            }
        }
    }
}