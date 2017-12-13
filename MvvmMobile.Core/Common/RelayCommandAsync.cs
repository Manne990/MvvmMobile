using System;
using System.Threading.Tasks;

namespace MvvmMobile.Core.Common
{
    public sealed class RelayCommandAsync
    {
        // Private Members
        private readonly Func<object, Task> _asyncExecute;
        private readonly Func<Task> _executeNoParam;
        private readonly Predicate<object> _canExecute;
        private readonly bool _enableConcurrency;
        private volatile bool _isRunning;


        // -----------------------------------------------------------------------------

        // Constructors
        public RelayCommandAsync(Func<Task> execute, bool enableConcurrency = false)
        {
            _executeNoParam = execute;
            _enableConcurrency = enableConcurrency;
        }

        public RelayCommandAsync(Func<object, Task> asyncExecute, Predicate<object> canExecute = null, bool enableConcurrency = false)
        {
            _asyncExecute = asyncExecute;
            _canExecute = canExecute;
            _enableConcurrency = enableConcurrency;
        }


        // -----------------------------------------------------------------------------

        // Public Methods
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public async Task Execute(object parameter)
        {
            if (_isRunning)
            {
                return;
            }

            _isRunning = true;

            if (CanExecute(parameter))
            {
                await _asyncExecute?.Invoke(parameter);
            }

            _isRunning = false;
        }

        public async Task Execute()
        {
            if (_isRunning)
            {
                return;
            }

            _isRunning = true;

            if (CanExecute(null))
            {
                await _executeNoParam?.Invoke();
            }

            _isRunning = false;
        }
    }

    public sealed class RelayCommandAsync<T>
    {
        // Private Members
        private readonly Func<T, Task> _asyncExecute;
        private readonly Predicate<T> _canExecute;
        private readonly bool _enableConcurrency;
        private volatile bool _isRunning;


        // -----------------------------------------------------------------------------

        // Constructors
        public RelayCommandAsync(Func<T, Task> asyncExecute, Predicate<T> canExecute = null, bool enableConcurrency = false)
        {
            _asyncExecute = asyncExecute;
            _canExecute = canExecute;
            _enableConcurrency = enableConcurrency;
        }


        // -----------------------------------------------------------------------------

        // Public Methods
        public bool CanExecute(T parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public async Task Execute(T parameter)
        {
            if (_isRunning)
            {
                return;
            }

            _isRunning = true;

            if (CanExecute(parameter))
            {
                await _asyncExecute?.Invoke(parameter);
            }

            _isRunning = false;
        }
    }
}