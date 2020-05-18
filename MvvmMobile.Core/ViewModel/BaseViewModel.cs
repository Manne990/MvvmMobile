using System;
using System.Collections.Generic;
using System.ComponentModel;
using MvvmMobile.Core.Navigation;

namespace MvvmMobile.Core.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged, IBaseViewModel
    {
        // Private Members
        private bool _isActive;
        private List<string> _delayedPropertyChanged;


        // -----------------------------------------------------------------------------

        // Constructors
        public BaseViewModel()
        {
            _delayedPropertyChanged = new List<string>();
        }


        // -----------------------------------------------------------------------------

        // Property Changed Notification
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (_isActive)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return;
            }

            if (_delayedPropertyChanged.Contains(propertyName))
            {
                return;
            }

            _delayedPropertyChanged?.Add(propertyName);
        }


        // -----------------------------------------------------------------------------

        // Lifecycle
        public virtual void OnLoaded() {}

        public virtual void OnActivated()
        {
            _isActive = true;

            foreach (var propertyName in _delayedPropertyChanged)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            _delayedPropertyChanged?.Clear();
        }

        public virtual void OnPaused()
        {
            _isActive = false;
            _delayedPropertyChanged = new List<string>();
        }


        // -----------------------------------------------------------------------------

        // Payload and Callback Handling
        public virtual void InitWithPayload(Guid payloadId) { }

        protected T LoadPayload<T>(Guid payloadId) where T : class
        {
            return Mvvm.Api.Resolver?.Resolve<IPayloads>()?.Get<T>(payloadId);
        }

        public Action<Guid> CallbackAction { get; set; }

        protected void NavigateBack(Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
        {
            Mvvm.Api.Resolver?.Resolve<INavigation>()?.NavigateBack(() => done?.Invoke(), behaviour);
        }

        protected void NavigateBack(IPayload payload, Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
        {
            if (CallbackAction == null)
            {
                NavigateBack(done, behaviour);
                return;
            }

            // Add payload
            var payloadId = Guid.NewGuid();

            Mvvm.Api.Resolver?.Resolve<IPayloads>()?.Add(payloadId, payload);
            Mvvm.Api.Resolver?.Resolve<INavigation>()?.NavigateBack(CallbackAction, payloadId, () => done?.Invoke(), behaviour);
        }
    }
}