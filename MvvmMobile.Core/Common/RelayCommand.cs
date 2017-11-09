using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace MvvmMobile.Core.Common
{
    public sealed class RelayCommand : ICommand
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

        // Events
        public event EventHandler CanExecuteChanged;


        // -----------------------------------------------------------------------------

        // Public Methods
        public bool CanExecute(object parameter)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());

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
            if(EqualityComparer<T>.Default.Equals(parameter, default(T)))
            {
                return;
            }

            if (CanExecute(parameter))
            {
                _execute?.Invoke(parameter);
            }
        }
    }
}