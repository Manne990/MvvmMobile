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
        public virtual void OnLoaded()
        {
        }

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
        protected T LoadPayload<T>(Guid payloadId) where T : class
        {
            // Get Payload
            var payloads = Mvvm.Api.Resolver.Resolve<IPayloads>();
            return payloads.GetAndRemove<T>(payloadId);
        }

        public Action<Guid> CallbackAction { get; set; }

        protected void NavigateBack(Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
        {
            Mvvm.Api.Resolver.Resolve<INavigation>().NavigateBack(() => 
                {
                    done?.Invoke();
                },
                behaviour);
        }

        protected void NavigateBack(IPayload payload, Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
        {
            if (CallbackAction == null)
            {
                return;
            }

            // Set payload id
            var payloadId = Guid.NewGuid();

            // Add payload
            var payloads = Mvvm.Api.Resolver.Resolve<IPayloads>();
            payloads.Add(payloadId, payload);

            Mvvm.Api.Resolver.Resolve<INavigation>().NavigateBack(CallbackAction, 
                                                                  payloadId,
                                                                  () => done?.Invoke(),
                                                                  behaviour);
        }

        public virtual void InitWithPayload(Guid payloadId)
        {
        }
    }
}