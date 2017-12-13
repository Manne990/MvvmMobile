using System;

namespace MvvmMobile.Core.Common
{
    public sealed class RelayCommand
    {
        // Private Members
        private readonly Action<object> _execute;
        private readonly Action _executeNoParam;
        private readonly Predicate<object> _canExecute;


        // -----------------------------------------------------------------------------

        // Constructors
        public RelayCommand(Action execute)
        {
            _executeNoParam = execute;
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


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
                _executeNoParam?.Invoke();
            }
        }
    }

    public sealed class RelayCommand<T>
    {
        // Private Members
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;


        // -----------------------------------------------------------------------------

        // Constructors
        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        // -----------------------------------------------------------------------------

        // Public Methods
        public bool CanExecute(T parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(T parameter)
        {
            if (CanExecute(parameter))
            {
                _execute?.Invoke(parameter);
            }
        }
    }
}